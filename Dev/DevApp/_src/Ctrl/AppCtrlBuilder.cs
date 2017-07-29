using DevApp.Mgr;
using DevApp.Views;
using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using MoellonToolkit.CommonDlgs.Impl;
using NLog;

namespace DevApp.Ctrl
{
    /// <summary>
    /// To build the app controller
    /// (inject concrete objets into it).
    /// </summary>
    public class AppCtrlBuilder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //=====================================================================
        #region Publics Static methods.

        /// <summary>
        /// Create a concrete and full AppController, set it into the Provider.
        /// This object Builder permit ot have an AppController with interfaces.
        /// </summary>
        /// <returns></returns>
        public static IAppCtrl CreateAppCtrl()
        {
            // the application messenger, has log to help debug
            AppMessenger messenger = new AppMessenger();
            messenger.ActivityLog += Messenger_ActivityLog;

            // create the common dialogBox manager, need the main window as a parent
            CommonDlg commonDlg = new CommonDlg();
            commonDlg.SetCurrentCultureInfo(CultureCode.en_GB);

            // dlgbox needs the window parent
            AppDlg appDlg = new AppDlg();

            AppCtrl appCtrl = new AppCtrl(messenger, commonDlg, appDlg);

            // save it into the AppCtrl provider (used by VM)
            AppCtrlProvider.Instance.SetAppCtrl(appCtrl);

            // create the main window, AFTER the controller
            MainWindow mainWindow = new MainWindow();

            commonDlg.SetMainWindow(mainWindow);
            appCtrl.SetMainWindow(mainWindow);
            appDlg.SetMainWindow(mainWindow);

            return appCtrl;
        }
        #endregion

        //=====================================================================
        #region Privates Static methods.

        /// <summary>
        /// To trace the activity of the messenger, useful to debug.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        private static void Messenger_ActivityLog(ActivityLogLevel level, string msg)
        {
            if (level == ActivityLogLevel.Debug)
                logger.Debug(msg);
            else if(level == ActivityLogLevel.Error)
                logger.Error(msg);

        }
        #endregion

    }
}
