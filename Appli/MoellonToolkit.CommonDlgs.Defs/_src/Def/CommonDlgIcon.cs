using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    /// Available Icon options.
    /// </summary>
    public enum CommonDlgIcon
    {
        None,

        //--Error, Stop, Failure,...       
        // Ok
        Error,

        //--Warning, Exclamation
        // Ok
        Warning,

        //--Information
        // Ok, Close
        Information,

        //--Question/interrogation
        // YesNo, YesNoCancel
        Question,
    }
}
