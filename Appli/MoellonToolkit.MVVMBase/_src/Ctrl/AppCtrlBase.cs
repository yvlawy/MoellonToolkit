using MoellonToolkit.MVVMBase;
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
    public abstract class AppCtrlBase : IAppCtrlBase
    {
        AppState _appState = AppState.Running;

        IMessenger _messenger;

        // the new full WPF commonDlg, to replace the win32/WinForms CommonDlg
        ICommonDlg _commonDlg;

        public AppCtrlBase(IMessenger messenger, ICommonDlg commonDlg)
        {
            _messenger = messenger;
            _commonDlg = commonDlg;
            MsgRequestCloseApp = "Are you sure you want to exit?";
        }

        public event EventHandler ShutDown;

        //=====================================================================
        #region Properties.

        //---------------------------------------------------------------------
        public AppState AppState { get { return _appState; } }

        //---------------------------------------------------------------------
        /// <summary>
        /// The messenger, to communicate between VM.
        /// </summary>
        public IMessenger Messenger { get { return _messenger; } }

        //---------------------------------------------------------------------
        /// <summary>
        /// Some commons dialog box.
        /// the new (future) full WPF commonDlg, to replace the win CommonDlg
        /// </summary>
        public ICommonDlg CommonDlg { get { return _commonDlg; } }

        public string MsgRequestCloseApp { get; set; }

        #endregion


        //---------------------------------------------------------------------
        /// <summary>
        /// Request the close of the application: show a dlgbox to the user
        /// to confirm or cancel the closing action.
        /// </summary>
        /// <returns></returns>
        public bool RequestCloseApp()
        {
            // has the user confirm or cancel the close?
            if (_appState == AppState.Closing)
                // the user has confirm to close 
                return true;

            if (_commonDlg.ShowQuestion(MsgRequestCloseApp) != CommonDlgResult.Yes)
                return false;

            // the user reply: OK, close the app, so the state of application is now: is closing 
            _appState = AppState.Closing;

            // an application is present? (not a test)
            if (ShutDown != null)
                ShutDown(this, new EventArgs());

            return true;
        }

    }
}
