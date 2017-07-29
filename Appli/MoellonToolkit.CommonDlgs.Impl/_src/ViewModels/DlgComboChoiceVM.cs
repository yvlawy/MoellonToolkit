using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MoellonToolkit.CommonDlgs.Impl
{
    /// <summary>
    /// The ViewModel of the View/UI.
    /// </summary>
    public class DlgComboChoiceVM : ViewModelBase
    {
        CoreSystem _coreSystem;

        // titre boite de dialogue
        private string _title;

        // btn on the left: Ok
        private string _textBtnOk;

        // the central btn: Cancel
        private string _textBtnCancel;

        private CommonDlgResult _dlgResult = CommonDlgResult.Cancel;

        private ICommand _btnOkCmd;

        private ICommand _btnCancelCmd;

        /// <summary>
        /// List of items displayed in the combobox.
        /// </summary>
        private List<DlgComboChoiceItem> _listItem;

        /// <summary>
        /// The selected item in the combobox.
        /// </summary>
        private DlgComboChoiceItem _selectedItem;

        //=====================================================================
        #region Constructor.

        //-------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public DlgComboChoiceVM(CoreSystem coreSystem)
        {
            _coreSystem = coreSystem;
            _listItem = new List<DlgComboChoiceItem>();

            // set default config
            Init();
        }
        #endregion

        #region Actions/Events.

        public Action DoCloseView;

        #endregion

        #region Properties.

        //---------------------------------------------------------------------
        /// <summary>
        /// The result of the DlgBox.
        /// </summary>
        public CommonDlgResult DlgResult
        {
            get
            {
                return _dlgResult;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Title of the dlgBox.
        /// </summary>
        public string Title
        {
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }

            get { return _title; }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// The text/content of the btn Ok.
        /// </summary>
        public string TextBtnOk
        {
            set
            {
                _textBtnOk = value;
                RaisePropertyChanged("TextBtnOk");
            }

            get
            {
                return _textBtnOk;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// The text/content of the btn2.
        /// </summary>
        public string TextBtnCancel
        {
            set
            {
                _textBtnCancel = value;
                RaisePropertyChanged("TextBtnCancel");
            }

            get
            {
                return _textBtnCancel;
            }
        }


        /// <summary>
        /// The list of items displayed in the combobox.
        /// </summary>
        public List<DlgComboChoiceItem> ListItem
        {
            set
            {
                _listItem.Clear();
                _listItem.AddRange(value);
            }
            get { return _listItem; }
        }

        /// <summary>
        /// The selected item in the combobox.
        /// </summary>
        public DlgComboChoiceItem SelectedItem
        {
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");

            }
            get { return _selectedItem; }
        }

        #endregion

        #region Command Properties.

        public ICommand BtnOkCmd
        {
            get
            {
                if (_btnOkCmd == null)
                {
                    _btnOkCmd = new RelayCommand(param => DoBtnOkCmd(),
                                                             param => CanDoBtnOkCmd());
                }
                return _btnOkCmd;
            }
        }

        public ICommand BtnCancelCmd
        {
            get
            {
                if (_btnCancelCmd == null)
                {
                    _btnCancelCmd = new RelayCommand(param => DoBtnCancelCmd(),
                                                             param => CanDoBtnCancelCmd());
                }
                return _btnCancelCmd;
            }
        }
        #endregion

        #region Private Command methods.

        /// <summary>
        /// The button OK is enabled only is an item is selected.
        /// </summary>
        /// <returns></returns>
        private bool CanDoBtnOkCmd()
        {
            if (_selectedItem == null)
                return false;

            return true;
        }

        private void DoBtnOkCmd()
        {
            // the default case
            _dlgResult = CommonDlgResult.Ok;


            // close the dlgBox
            if (DoCloseView != null)
                // prevent the manager of the dlgBox
                DoCloseView();
        }

        private bool CanDoBtnCancelCmd()
        {
            return true;
        }

        private void DoBtnCancelCmd()
        {
            // the default case
            _dlgResult = CommonDlgResult.Cancel;

            // close the dlgBox
            if (DoCloseView != null)
                // prevent the manager of the dlgBox
                DoCloseView();
        }

        #endregion

        //=====================================================================
        #region Private methods.

        //---------------------------------------------------------------------
        private void Init()
        {
            _title = "Title";
            _textBtnOk = _coreSystem.TranslationMgr.GetTranslation(StringCode.Ok);
            _textBtnCancel = _coreSystem.TranslationMgr.GetTranslation(StringCode.Cancel);
        }

        #endregion
    }
}
