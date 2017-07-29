using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// List of Actions (and registered inst) for one msg code.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsgCodeListAction
    {
        //private object _lockObject = new object();

        /// <summary>
        /// The message code.
        /// </summary>
        string _code;

        /// <summary>
        /// list of recipient - Action attached to the msgCode.
        /// </summary>
        List<RecipientObjAction> _listRecipientObjAction = new List<RecipientObjAction>();
        //BlockingCollection<RecipientObjAction> _listRecipientObjAction = new BlockingCollection<RecipientObjAction>();
        //ConcurrentBag<RecipientObjAction> _listRecipientObjAction = new ConcurrentBag<RecipientObjAction>();

        //---------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"></param>
        public MsgCodeListAction(string code)
        {
            _code = code;
        }

        //---------------------------------------------------------------------
        public string MsgCode
        {
            get { return _code; }
        }

        //---------------------------------------------------------------------
        public int Count
        {
            get { return _listRecipientObjAction.Count; }
        }

        //---------------------------------------------------------------------
        public IEnumerable<RecipientObjAction> GetListRecipientObjAction()
        {
            return _listRecipientObjAction;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Check that the object is already registered with the code msg.
        /// </summary>
        /// <param name="recipient"></param>
        /// <returns></returns>
        public RecipientObjAction FindByRecipientObj(object recipient)
        {
            if (recipient == null)
                return null;

            return _listRecipientObjAction.Find(o => o.Recipient == recipient);

            //foreach (var ra in _listRecipientObjAction.GetConsumingEnumerable())
            //{
            //    if (ra.Recipient == recipient)
            //        return ra;
            //}
            //return null;

            //if (_listRecipientObjAction.Count == 0)
            //    return null;
            //// boom si objet pas trouvé!
            //RecipientObjAction[] recipAction = _listRecipientObjAction.ToArray();
            //foreach(RecipientObjAction r in recipAction)
            //    if(r.Recipient==recipient)
            //        return r;
            //return null;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Add the recipientObj - Action to the msgCode.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="who"></param>
        /// <param name="actionT"></param>
        /// <returns></returns>
        public bool AddRecipientObjAction<T>(object who, Action<T> actionT)
        {
            RecipientObjAction registInstAction = new RecipientObjAction(who, typeof(T), actionT);
            _listRecipientObjAction.Add(registInstAction);
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// remove the recipient obj attached to the msgCode (only one instance).
        /// </summary>
        /// <param name="recipientObjAction"></param>
        /// <returns></returns>
        public bool RemoveRecipientObjAction(RecipientObjAction recipientObjAction)
        {
            if (recipientObjAction == null)
                return false;

            if (!_listRecipientObjAction.Contains(recipientObjAction))
                return false;

            return _listRecipientObjAction.Remove(recipientObjAction);
            //return _listRecipientObjAction.TryTake(out recipientObjAction);
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Remove all unreferenced recipient.
        /// </summary>
        /// <returns></returns>
        public int CleanUnRefRecipient()
        {
            int removedCount = 0;
            List<RecipientObjAction> listRemoveRecipientAction = new List<RecipientObjAction>();

            foreach (RecipientObjAction r in _listRecipientObjAction)
            {
                if (r.Recipient == null)
                    listRemoveRecipientAction.Add(r);
            }

            //now remove all unregistered recipient
            removedCount = listRemoveRecipientAction.Count;
            foreach (RecipientObjAction r in listRemoveRecipientAction)
                _listRecipientObjAction.Remove(r);
            //_listRecipientObjAction.TryTake(out removed);

            return removedCount;
        }
    }

}
