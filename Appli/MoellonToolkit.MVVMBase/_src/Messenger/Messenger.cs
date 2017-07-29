using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Collections;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// A messenger using string code as the message identifiant.
    /// String Code -> List(Who, Action)
    /// 
    /// Only one object (registered instance) can be attached to one msg code.
    /// Many Actions (from differents objects) can be attached to one message code.
    /// 
    /// MessengerByCode
    /// MessengerCode
    /// </summary>
    public class Messenger  : IMessenger
    {
        /// <summary>
        /// Register: Link betwwen Message Code and the action to execute.
        /// Notify: Action to execute when receiving the message: when the notified msg code match a code present in the dict.
        /// 
        /// Action: a function call.
        /// </summary>
        private ConcurrentDictionary<string, MsgCodeListAction> _dictMsgCodeListAction;

        /// <summary>
        /// The last error occurs.
        /// </summary>
        private MessengerErrCode _lastErrCode = MessengerErrCode.None;

        //---------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public Messenger()
        {
            _dictMsgCodeListAction = new ConcurrentDictionary<string, MsgCodeListAction>();
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// return the number of registered msgCode (containing recipient - Action).
        /// </summary>
        public int RegisteredMsgCodeCount
        {
            get { return _dictMsgCodeListAction.Count; }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// return the number of registered recipient (and actions).
        /// </summary>
        public int RegisteredRecipientCount
        {
            get 
            {
                int count = 0;

                // scan each msgCode entry
                foreach (KeyValuePair<string, MsgCodeListAction> pair in _dictMsgCodeListAction)
                    count += pair.Value.Count;
                return count; 
            }
        }

        //---------------------------------------------------------------------
        public MessengerErrCode GetLastErrCode()
        {
            return _lastErrCode;
        }

        //---------------------------------------------------------------------
        public bool IsMsgCodeSyntaxOk(string msgCode)
        {
            // the syntax code must be ok
            if (string.IsNullOrWhiteSpace(msgCode))
                return false;
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Remove all unreferenced recipient.
        /// Remove msgCode if hasn't recipient-action.
        /// </summary>
        /// <returns></returns>
        public int CleanUnRefRecipient()
        {
            int removedCount = 0;
            List<string> listMsgCodeToRemove= new List<string>();
 
            // scan the dict
            foreach (KeyValuePair<string, MsgCodeListAction> pair in _dictMsgCodeListAction)
            {
                // clean inside
                removedCount += pair.Value.CleanUnRefRecipient();

                // no more recipient-action attached to the msgCode? 
                if (pair.Value.Count == 0)
                    listMsgCodeToRemove.Add(pair.Key);
            }

            //now remove all unregistered msgCode
            RemoveListMsgCode(listMsgCodeToRemove);
            return removedCount;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Register a msg Code with a generic action.
        /// Link/Attach a msg code to an action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messengerName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Register<T>(string msgCode, object recipient, Action<T> actionT)
        {
            _lastErrCode = MessengerErrCode.None;

            // the syntax code must be ok
            if (!IsMsgCodeSyntaxOk(msgCode))
            {
                _lastErrCode = MessengerErrCode.MsgCodeSyntaxWrong;
                return false;
            }

            if (recipient == null)
            {
                _lastErrCode = MessengerErrCode.RecipientObjIsNull;
                return false;
            }

            if (actionT == null)
            {
                _lastErrCode = MessengerErrCode.ActionTIsNull;
                return false;
            }

            // if the code is not already used, create an entry in the dict
            MsgCodeListAction msgCodeListAction;
            if (_dictMsgCodeListAction.ContainsKey(msgCode))
            {
                // msg code already exists, get it
                msgCodeListAction = _dictMsgCodeListAction[msgCode];
            }else
            {
                // msg code not exists so create it
                msgCodeListAction = new MsgCodeListAction(msgCode);
                _dictMsgCodeListAction.TryAdd(msgCode, msgCodeListAction);
            }

            // check that the object is already registered with the code msg
            RecipientObjAction regInstAction = msgCodeListAction.FindByRecipientObj(recipient);
            if (regInstAction != null)
            {
                _lastErrCode = MessengerErrCode.RecipientAlreadyRegisteredOnCodeMsg;
                return false;
            }

            // add the registered inst-Action to the msgCode
            msgCodeListAction.AddRecipientObjAction(recipient, actionT);

            // check the registering
            msgCodeListAction.FindByRecipientObj(recipient);
            if (regInstAction != null)
            {
                _lastErrCode = MessengerErrCode.RegisterMsgCodeRecipientActionFailed;
                return false;
            }
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Register a msg Code with an action.
        /// Link/Attach a msg code to an action.
        /// </summary>
        /// <param name="msgCode"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Register(string msgCode, object recipient, Action action)
        {
            // TODO: décrire astuce! pour passer d'un type fixe a un type paramétré.
            return Register<int>(msgCode, recipient, Exception => action());
            //return Register<int>(msgCode, handler);
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Unregistered the msgcode - recipient object, (no matter of the registered and the type).
        /// </summary>
        /// <param name="msgCode"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        public bool UnRegister(string msgCode, object recipient)
        {
            _lastErrCode = MessengerErrCode.None;

            // the syntax code must be ok
            if (!IsMsgCodeSyntaxOk(msgCode))
            {
                _lastErrCode = MessengerErrCode.MsgCodeSyntaxWrong;
                return false;
            }

            if (recipient == null)
            {
                _lastErrCode = MessengerErrCode.RecipientObjIsNull;
                return false;
            }

            // the code not exists
            MsgCodeListAction msgCodeListAction;
            if (!_dictMsgCodeListAction.ContainsKey(msgCode))
            {
                _lastErrCode = MessengerErrCode.MsgCodeNotFound;
                return false;
            }

            // msg code already exists, get it
            msgCodeListAction = _dictMsgCodeListAction[msgCode];
            
            // the recipient is attached to the msg code?
            RecipientObjAction recipientObjAction= msgCodeListAction.FindByRecipientObj(recipient);
            if(recipientObjAction==null)
            {
                _lastErrCode = MessengerErrCode.RecipientNotRegisteredOnCodeMsg;
                return false;
            }

            // remove the recipient obj attached to the msgCode (only one instance)
            msgCodeListAction.RemoveRecipientObjAction(recipientObjAction);

            // remove the MsgCodeListAction is the object is empty (a MsgCode with no recipient)
            if (msgCodeListAction.Count == 0)
                RemoveMsgCodeListAction(msgCodeListAction);
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Notify, send a message.
        /// Execute all actions susbscribed under the msgCode.
        /// 
        /// Return the number of actions notifications executed, or -1 if an error occurs.
        /// If no subscriber is found return 0.
        /// </summary>
        /// <param name="messengerName"></param>
        /// <param name="obj"></param>
        public int Notify<T>(object whoNotify, string msgCode, T obj)
        {
            _lastErrCode = MessengerErrCode.None;

            // check params
            if (whoNotify == null)
            {
                _lastErrCode = MessengerErrCode.ObjNotifyIsNull;
                return -1;
            }

            // the syntax code must be ok
            if (!IsMsgCodeSyntaxOk(msgCode))
            {
                _lastErrCode = MessengerErrCode.MsgCodeSyntaxWrong;
                return -1;
            }

            // find the CodeListAction
            if (!_dictMsgCodeListAction.ContainsKey(msgCode))
            {
                _lastErrCode = MessengerErrCode.MsgCodeNotFound;
                // not an error
                return 0;
            }

            // msg code already exists, get it
            MsgCodeListAction msgCodeListAction= _dictMsgCodeListAction[msgCode];

            // execute all actions presents
            int notifCount = 0;
            List<RecipientObjAction> listRemoveRecipientAction = new List<RecipientObjAction>();
            foreach (RecipientObjAction recipientAction in msgCodeListAction.GetListRecipientObjAction())
            {
                // check that the object already exists (with the weak reference)
                if (recipientAction.Recipient != null)
                {
                    // the type of the notify parameter should match the type of the param of the registered recipient
                    if (recipientAction.ActionType == typeof(T))
                    {
                        // ok, execute the action
                        ((Action<T>)recipientAction.ActionT)(obj);
                        notifCount++;
                    }
                }
                else
                {
                    // the recipient is now null, unregister it
                    listRemoveRecipientAction.Add(recipientAction);
                }
            }

            //now remove all unregistered recipient
            while (listRemoveRecipientAction.Count > 0)
            {
                msgCodeListAction.RemoveRecipientObjAction(listRemoveRecipientAction[0]);
                listRemoveRecipientAction.RemoveAt(0);
            }

            // no recipient-action attached to the msgCode, remove the msgCode
            if (msgCodeListAction.Count == 0)
                RemoveMsgCodeListAction(msgCodeListAction);

            return notifCount;
        }

        //---------------------------------------------------------------------
        public int Notify(object whoNotify, string msgCode)
        {
            return Notify<int>(whoNotify, msgCode, 0);
        }

        //=====================================================================
        #region Private Methods.

        //---------------------------------------------------------------------
        private bool RemoveMsgCodeListAction(MsgCodeListAction msgCodeListAction)
        {
            MsgCodeListAction outObj;

            if(msgCodeListAction==null)
                return false;

            // check that the key is present
            if (!_dictMsgCodeListAction.ContainsKey(msgCodeListAction.MsgCode))
                return false;

            _dictMsgCodeListAction.TryRemove(msgCodeListAction.MsgCode, out outObj);
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Remove all empty msgCode.
        /// </summary>
        /// <param name="listMsgCodeToRemove"></param>
        /// <returns></returns>
        private int RemoveListMsgCode(List<string> listMsgCodeToRemove)
        {
            int removed = 0;
            MsgCodeListAction outObj;
            if (listMsgCodeToRemove == null)
                return 0;

            while (listMsgCodeToRemove.Count > 0)
            {
                if (!_dictMsgCodeListAction.ContainsKey(listMsgCodeToRemove[0]))
                    continue;
                _dictMsgCodeListAction.TryRemove(listMsgCodeToRemove[0], out outObj);
                removed++;

                listMsgCodeToRemove.RemoveAt(0);
            }
            return removed;
        }

        #endregion
    }
}
