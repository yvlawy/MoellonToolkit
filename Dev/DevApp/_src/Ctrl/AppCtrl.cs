using DevApp.Enums;
using DevApp.Mgr;
using DevApp.ViewModels;
using DevApp.Views;
using MoellonToolkit.CommonDlgs.Defs;
using MoellonToolkit.CommonDlgs.Impl.Components;
using MoellonToolkit.MVVMBase;
using NLog;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace DevApp.Ctrl
{
    /// <summary>
    /// The controller of the application, provides low layered resources to viewModels and views.
    /// </summary>
    public class AppCtrl : AppCtrlBase, IAppCtrl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Manager of Specifics dialogs of the application.
        /// </summary>
        private IAppDlg _appDlg;

        /// <summary>
        /// To save the data of the user.
        /// </summary>
        string _userAppData;

        // start application simulator
        System.Timers.Timer _timer;


        private SplashScreenVM _splashScreenVM;
        private Views.SplashScreen _splashScreen;

        // the main View-VM
        private MainVM _mainVM;
        private MainWindow _mainWindow;

        //---------------------------------------------------------------------
        /// <summary>
        /// Constructor 2.
        /// Don't call Init().
        /// for test.
        /// </summary>
        /// <param name="messenger"></param>
        public AppCtrl(IMessenger messenger, ICommonDlg commonDlg, IAppDlg appDlg):base(messenger,commonDlg)
        {
            _appDlg = appDlg;

            base.ShutDown += AppCtrl_ShutDown;
            MessengerRegisterActions();
            InitData();
        }

        //=====================================================================
        #region Properties.

        //---------------------------------------------------------------------
        /// <summary>
        /// Specifics dialog box of the application (not common).
        /// </summary>
        public IAppDlg AppDlg { get { return _appDlg; } }

        /// <summary>
        /// The factory to build items for dynamic dataGrid.
        /// One factory for all dataGrid.
        /// </summary>
        public IDynDataGridFactory DynDataGridFactory { get; private set; }

        /// <summary>
        /// A datagrid to test the dynamic data grid UI.
        /// </summary>
        public IDynDataGrid DataGrid { get; private set; }

        /// <summary>
        /// Action, callback, called when a dataGrid cell is modified.
        /// </summary>
        //public Action<IGridCell> ActionGridValueModifiedInUI { get; set; }

        #endregion

        //=====================================================================
        #region Public methods.

        //---------------------------------------------------------------------
        /// <summary>
        /// Provide the main window instance,
        /// must be created outside and provided to the AppDlg.
        /// </summary>
        /// <param name="mainWindow"></param>
        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// start the application (load data,...): show the splash screen.
        /// 
        /// </summary>
        /// <returns></returns>
        public bool StartApp()
        {
            _userAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // define the dialog translated text
            SetAllUserDefinedTranslatedText();

            ShowMainView();
            ShowSplashScreen();

            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Create a view with its viewModel.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public ViewModelBase ShowView(ViewDef view)
        {
            //switch (view)
            //{
            //    //case ViewDef.SplashScreen:
            //    //    return ShowSplashScreen();

            //    //case ViewDef.Main:
            //    //    return ShowMainView();
            //}

            return null;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Close a View: a window/dlg.
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public bool CloseView(ViewDef viewName)
        {
            // check for modified data, ask the user to save it or not
            // TODO

            //switch (viewName)
            //{
            //    // close the dlg
            //    //case AppGenDef.View.DlgEditEntityPropertiesTags:
            //    //    return _appDlg.CloseViewDlgEditEntityPropertiesTags();
            //}

            return false;

        }

        //---------------------------------------------------------------------
        public ViewModelBase CreateVM(ViewDef viewName)
        {
            //switch (viewName)
            //{
            //    //case AppGenDef.View.UCTabCtrlEditMainObject:
            //    //    _ucTabCtrlEditMainObjectVM = new UCTabCtrlEditMainObjectVM();
            //    //    return _ucTabCtrlEditMainObjectVM;

            //    //case AppGenDef.View.UCMainTreeView:
            //    //    _ucMainTreeViewVM = new UCMainTreeViewVM();
            //    //    return _ucMainTreeViewVM;
            //}

            return null;
        }

        //---------------------------------------------------------------------
        public ViewModelBase GetVM(ViewDef viewName)
        {
            switch (viewName)
            {
                case ViewDef.Main:
                    return _mainVM;
            }

            return null;

        }


        #endregion

        //=====================================================================
        #region Private methods.

        //---------------------------------------------------------------------
        private void AppCtrl_ShutDown(object sender, EventArgs e)
        {
            if (Application.Current != null)
                Application.Current.Shutdown();
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Messenger, register actions, (subscribe to events).
        /// </summary>
        void MessengerRegisterActions()
        {
            // add messenger registering action: coming from the menu option "Exit"
            //_messenger.Register(
            //    AppGenDef.MsgCode.RequestCloseApp.ToString(),
            //    () =>
            //    {
            //        RequestCloseApp(true);
            //    });
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// On the first (and unique) ellapsed top of the timer, 
        /// close the splash screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() => _splashScreen.Close()));

            _timer.Enabled = false;
        }

        //---------------------------------------------------------------------
        SplashScreenVM ShowSplashScreen()
        {
            _splashScreen = new Views.SplashScreen();
            _splashScreen.Owner = _mainWindow;
            _splashScreenVM = new SplashScreenVM();
            _splashScreen.DataContext = _splashScreenVM;
            _splashScreen.Show();

            // start the timer to close the splash
            _timer = new System.Timers.Timer(300);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;

            return _splashScreenVM;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// The main view is already created (very soon, at the start of the ctrl).
        /// </summary>
        /// <returns></returns>
        MainVM ShowMainView()
        {
            // refresh the UI  (content of the main view)
            // TODO:

            if (_mainVM == null)
            {
                _mainVM = new MainVM();

                // attach the VM to the view  (mainWindow created soon)
                _mainWindow.DataContext = _mainVM;
            }

            // show the main view of the app
            _mainWindow.Show();

            return _mainVM;
        }

        /// <summary>
        /// Define all the dialog translated text.
        /// </summary>
        private void SetAllUserDefinedTranslatedText()
        {
            CommonDlg.SetUserDefinedTranslatedText(StringCode.Cancel, "User-Cancel");
            CommonDlg.SetUserDefinedTranslatedText(StringCode.Close, "User-Close");
            CommonDlg.SetUserDefinedTranslatedText(StringCode.DlgErrorTitle, "User-Error");
            CommonDlg.SetUserDefinedTranslatedText(StringCode.DlgInformationTitle, "User-Information");
            CommonDlg.SetUserDefinedTranslatedText(StringCode.DlgQuestionTitle, "User-Question");

            CommonDlg.SetUserDefinedTranslatedText(StringCode.DlgWarningTitle, "User-Warning");
            CommonDlg.SetUserDefinedTranslatedText(StringCode.No, "User-No");
            CommonDlg.SetUserDefinedTranslatedText(StringCode.Ok, "User-Ok");

            CommonDlg.SetUserDefinedTranslatedText(StringCode.Yes, "User-Yes");
        }

        private void InitData()
        {
            DynDataGridFactory= new DynDataGridFactory();
            DataGrid = new DynDataGrid();


            //----create columns
            IGridColumn columnKey;
            DynDataGridFactory.CreateColumn(DataGrid, GridColumnType.String, "Key", out columnKey);
            columnKey.IsEditionReadOnly = true;

            IGridColumn columnValue;
            DynDataGridFactory.CreateColumn(DataGrid, GridColumnType.String, "Value", out columnValue);

            // $TASK-003: col checkbox
            IGridColumn columnCheck;
            DynDataGridFactory.CreateColumn(DataGrid, GridColumnType.CheckBox, "Checked", out columnCheck);

            //----create a data row
            IGridRow row = DynDataGridFactory.CreateRowWithCells(DataGrid);

            //----create data cells
            DataGrid.SetCellValue(row, columnKey, "keyYes");
            DataGrid.SetCellValue(row, "Value", "Oui");
            // checked by default
            DataGrid.SetCellValue(row, "Checked", true);

            // test: search a column by the name
            //DataGrid.ListRow.Where(r=>r.)

            // TODO:
            //DataGrid.AddRow(row);
        }

        #endregion

    }
}
