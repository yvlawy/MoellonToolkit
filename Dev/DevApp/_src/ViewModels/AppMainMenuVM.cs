using DevApp.Ctrl;
using DevApp.Enums;
using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using NLog;
using System.Windows.Input;

namespace DevApp.ViewModels
{
    /// <summary>
    /// The main menu of the application.
    /// New/Open/Quit,...
    /// </summary>
    public class AppMainMenuVM : ViewModelBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ICommand _appOpenFileCommand;

        ICommand _appExitCommand;


        //=====================================================================
        #region Constructor.

        //-------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public AppMainMenuVM()
        {
            Init();
        }
        #endregion

        //=====================================================================
        #region Command Properties.

        //---------------------------------------------------------------------
        public ICommand AppOpenFileCommand
        {
            get
            {
                if (_appOpenFileCommand == null)
                {
                    _appOpenFileCommand = new RelayCommand(param => AppOpenFile(),
                                                             param => CanAppOpenFile());
                }
                return _appOpenFileCommand;
            }
        }

        //---------------------------------------------------------------------
        public ICommand AppExitCommand
        {
            get
            {
                if (_appExitCommand == null)
                {
                    _appExitCommand = new RelayCommand(param => AppExit(),
                                                             param => CanAppExit());
                }
                return _appExitCommand;
            }
        }
        #endregion

        //=====================================================================
        #region Properties.
        #endregion


        //=====================================================================
        #region Private methods.

        //---------------------------------------------------------------------
        private void Init()
        {
            // list of object model, in the treeview on the left side of the view
            //_textModel_MainList_VM= new TextModel_MainList_VM();
        }

        //---------------------------------------------------------------------
        private bool CanAppOpenFile()
        {
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Open a file dialog box to open and load the data content of a file.
        /// -> Open a folder!
        /// </summary>
        private void AppOpenFile()
        {
            //string defaultExt = ".xml|*";
            //string filter = "Xml Document (*.xml, *.xlsx)|*.xls;*.xlsx";
            string pathName;
            string dbName;

            if (AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgSelectFolder(@"C:\", out pathName, out dbName) != CommonDlgResult.Ok)
                // the user cancel the dlg box
                return;

            // refresh all the views!  close all tab items
            AppCtrlProvider.AppCtrl.Messenger.Notify(this, ViewDef.RefreshAllView.ToString());
        }

        //---------------------------------------------------------------------
        private bool CanAppExit()
        {
            return true;
        }

        //---------------------------------------------------------------------
        private void AppExit()
        {
            AppCtrlProvider.AppCtrl.RequestCloseApp();
        }

        #endregion
    }
}
