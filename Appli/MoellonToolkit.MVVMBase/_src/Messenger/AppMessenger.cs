using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// The application messenger, add logs.
    /// 
    /// Use the Adapter pattern.
    /// </summary>
    public class AppMessenger : IMessenger
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The adapted Messenger.
        /// </summary>
        IMessenger _messenger;


        //---------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="_messenger"></param>
        public AppMessenger()
        {
            _messenger = new Messenger();
        }

        public delegate void ActivityLogHandler(ActivityLogLevel level, string msg);

        public event ActivityLogHandler ActivityLog;

        //---------------------------------------------------------------------
        // return the number of registered msgCode (containing recipient - Action).
        public int RegisteredMsgCodeCount
        {
            get { return _messenger.RegisteredMsgCodeCount; }
        }

        //---------------------------------------------------------------------
        // return the number of registered recipient (and actions).
        public int RegisteredRecipientCount
        {
            get { return _messenger.RegisteredRecipientCount; }
        }

        //---------------------------------------------------------------------
        public MessengerErrCode GetLastErrCode()
        {
            return _messenger.GetLastErrCode();
        }

        //---------------------------------------------------------------------
        public bool IsMsgCodeSyntaxOk(string msgCode)
        {
            return _messenger.IsMsgCodeSyntaxOk(msgCode);
        }

        //---------------------------------------------------------------------
        public int CleanUnRefRecipient()
        {
            return CleanUnRefRecipient();
        }

        //---------------------------------------------------------------------
        public bool Register<T>(string msgCode, object recipient, Action<T> actionT)
        {
            try
            {
                bool res;
                Log(ActivityLogLevel.Debug, "Register(): Code=" + msgCode);

                res = _messenger.Register(msgCode, recipient, actionT);
                if (!res)
                    Log(ActivityLogLevel.Error, "Register(): Code=" + msgCode + ", msg= Code is null or Unable to add the item, the name/key already exists.");
                return res;
            }
            catch (Exception e)
            {
                Log(ActivityLogLevel.Error, "Register(): Name=" + msgCode + ", msg=" + e.Message);
                throw;
            }
        }

        //---------------------------------------------------------------------
        public bool Register(string msgCode, object recipient, Action action)
        {
            return Register<int>(msgCode, recipient, Exception => action());
        }

        //---------------------------------------------------------------------
        public bool UnRegister(string msgCode, object recipient)
        {
            try
            {
                bool res;
                Log(ActivityLogLevel.Debug, "UnRegister(): Code=" + msgCode);

                res = _messenger.UnRegister(msgCode, recipient);
                if (!res)
                    Log(ActivityLogLevel.Error, "UnRegister(): Code=" + msgCode + ", msg= Code is null or Unable to remove the item, not exists.");
                return res;
            }
            catch (Exception e)
            {
                Log(ActivityLogLevel.Error, "UnRegister(): Code=" + msgCode + ", msg=" + e.Message);

                // TODO: créer une exception "chapeau"/englobante +élégante  (comme "Erreur Interne...")
                throw;
            }
        }

        //---------------------------------------------------------------------
        public int Notify<T>(object whoNotify, string msgCode, T obj)
        {
            try
            {
                Log(ActivityLogLevel.Debug, "Notify(): Code=" + msgCode);
                return _messenger.Notify<T>(whoNotify, msgCode, obj);
            }
            catch (Exception e)
            {
                Log(ActivityLogLevel.Error, "Notify(): Name=" + msgCode + ", msg=" + e.Message);
                // TODO: créer une exception "chapeau"/englobante +élégante  (comme "Erreur Interne...")
                throw;
            }
        }
        //---------------------------------------------------------------------
        public int Notify(object whoNotify, string msgCode)
        {
            return Notify<int>(whoNotify, msgCode, 0);
        }


        private void Log(ActivityLogLevel level, string msg)
        {
            if (ActivityLog != null)
                ActivityLog(level, msg);
        }
    }
}
