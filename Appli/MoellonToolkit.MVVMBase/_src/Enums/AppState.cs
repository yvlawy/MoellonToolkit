using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// State of the Application, managed by the controller.
    /// 
    /// is running, RequestAppClose -> AppClose -> is closing.
    /// </summary>
    public enum AppState
    {
        Running,
        Closing,
    }
}
