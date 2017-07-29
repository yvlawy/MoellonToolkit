using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.MVVMBase
{
    public enum MessengerErrCode
    {
        None,
        MsgCodeSyntaxWrong,
        RecipientObjIsNull,
        ActionTIsNull,
        RecipientAlreadyRegisteredOnCodeMsg,
        RecipientNotRegisteredOnCodeMsg,

        ObjNotifyIsNull,
        MsgCodeNotFound,

        RegisterMsgCodeRecipientActionFailed,
    }
}
