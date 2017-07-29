using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MoellonToolkit.CommonDlgs.Impl
{
    /// <summary>
    /// The ViewModel of the View/UI.
    /// </summary>
    public class DlgListChoiceVM : ViewModelBase
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
        /// List of items displayed in the listbox.
        /// </summary>
        private List<DlgListChoiceItem> _listItem;

        //=====================================================================
        #region Constructor.

        //-------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public DlgListChoiceVM(CoreSystem coreSystem)
        {
            _coreSystem = coreSystem;
            _listItem = new List<DlgListChoiceItem>();

            // set default config
            Init();
        }
        #endregion

        #region Actions/Events.

        public Action DoCloseView;

        #endregion

        #region Properties.

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


        public List<DlgListChoiceItem> ListItem
        {
            set
            {
                _listItem.Clear();
                _listItem.AddRange(value);
            }
            get { return _listItem; }
        }

        /// <summary>
        /// gestion des items selectionnés.
        /// </summary>
        public List<DlgListChoiceItem> ListSelectedItem
        {
            set
            {
                // TODO: pas testé!
                foreach (var item in value)
                {
                    if (_listItem.Contains(item))
                        item.IsSelected = true;
                }
            }
            get
            {
                List<DlgListChoiceItem> list = new List<DlgListChoiceItem>();
                foreach (var item in _listItem)
                {
                    if (item.IsSelected)
                        list.Add(item);
                };
                return list;
            }
        }

        // TODO: plusieurs!
        //public DlgChoiceItem SelectedItem
        //{
        //    set
        //    {
        //        _selectedItem = value;
        //        RaisePropertyChanged("SelectedItem");

        //    }
        //    get { return _selectedItem; }
        //}

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

        private bool CanDoBtnOkCmd()
        {
            if (_listItem.Where(i => i.IsSelected).Count() == 0)
                return false;

            return true;
        }

        private void DoBtnOkCmd()
        {
            // the default case
            _dlgResult = CommonDlgResult.Ok;


            // mémorise la valeur selectionnée
            // TODO:

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
