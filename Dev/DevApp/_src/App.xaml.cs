using DevApp.Ctrl;
using DevApp.Enums;
using MoellonToolkit.CommonDlgs.Defs;
using MoellonToolkit.CommonDlgs.Impl;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DevApp
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The Controller of the application : To manage the Applicative logic.
        /// </summary>
        IAppCtrl _appCtrl;

        public App()
        {
            Application.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

            logger.Info("");
            logger.Info("---->OnStartup()");

            InitAppCtrl();

            MessengerRegisterActions();

            // TODO: a mettre dans un controller
            // passer en param le MainWindow
            //ICommonDlg commonDlg = new CommonDlg();
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// A log/trace is defined in the app.config file.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // create and show the main view
            //_appCtrl.ShowView(ViewDef.Main);
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// When an error occurs, comes here.
        /// 
        /// see (how to dispatch from another thread to the UI thread):
        /// http://mike.woelmer.com/2009/04/dealing-with-unhandled-exceptions-in-wpf/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //logger.Error(e.Exception.StackTrace);
            //Debug.Fail(e.Exception.Message);
            //Debug.Fail(e.Exception.StackTrace);

            MessageBox.Show("ERR, " + e.Exception.Message);

            // Return exit code
            Application.Current.Shutdown(-1);

            // Prevent default unhandled exception processing
            e.Handled = true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Init the application, create the AppControler.
        /// </summary>
        private void InitAppCtrl()
        {
            // Create and init the Application Controller
            _appCtrl = AppCtrlBuilder.CreateAppCtrl();

            // start the application (load data,...): show the splash screen
            _appCtrl.StartApp();
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Messenger, register actions, (subscribe to events).
        /// </summary>
        void MessengerRegisterActions()
        {
            // add messenger registering action
            //_appCtrl.Messenger.Register(
            //    AppGenDef.MsgCode.CloseApp.ToString(),
            //    () =>
            //    {
            //        DoAppClose();    
            //    });
        }

    }
}
