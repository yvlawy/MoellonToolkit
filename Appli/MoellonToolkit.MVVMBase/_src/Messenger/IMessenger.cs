using System;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// Interface of the messenger.
    /// </summary>
    public interface IMessenger
    {
        // return the number of registered msgCode (containing recipient - Action).
        int RegisteredMsgCodeCount { get;}

        // return the number of registered recipient (and actions).
        int RegisteredRecipientCount { get; }

        MessengerErrCode GetLastErrCode();

        bool IsMsgCodeSyntaxOk(string msgCode);

        int CleanUnRefRecipient();

        bool Register<T>(string msgCode, object recipient, Action<T> actionT);
        bool Register(string msgCode, object recipient, Action action);

        bool UnRegister(string msgCode, object recipient);

        int Notify<T>(object whoNotify, string msgCode, T obj);
        int Notify(object whoNotify, string msgCode);
    }
}
