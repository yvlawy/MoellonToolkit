using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>	
    /// Result of a dialog box execution.
    /// Available DialogResults options.
    /// </summary>
    public enum CommonDlgResult
    {
        // no result (void)
        None,
        Ok,
        Cancel,
        Yes,
        No,
        Close,

        // a error occurs, can be: ErrorParamWrong
        Error,
    }
}
