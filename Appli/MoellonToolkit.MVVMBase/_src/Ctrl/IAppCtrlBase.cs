using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// The controller of the application.
    /// Link between UI/Views and ViewModels and Models.
    /// 
    /// Provide the messenger to communicate between VM and some commons dialogbox.
    /// </summary>
    public interface IAppCtrlBase
    {
        event EventHandler ShutDown;

        AppState AppState { get; }

        IMessenger Messenger { get; }

        // the new full WPF commonDlg, to replace the win32/WinForms CommonDlg
        ICommonDlg CommonDlg { get; }

        string MsgRequestCloseApp { get; set; }

        /// <summary>
        /// Request the close of the application: show a dlgbox to the user
        /// to confirm or cancel the closing action.
        /// </summary>
        /// <returns></returns>
        bool RequestCloseApp();

    }
}
