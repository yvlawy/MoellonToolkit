using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Windows.Input;

namespace MoellonToolkit.CommonDlgs.Impl
{
    /// <summary>
    /// The ViewModel of the View/UI.
    /// </summary>
    public class DlgInputTextMultiVM : ViewModelBase
    {
        CoreSystem _coreSystem;

        // the title of the dlg box
        private string _title;


        private string _textTitle;

        private string _textInput;

        // btn on the left: Ok
        private string _textBtnOk;

        // the central btn: Cancel
        private string _textBtnCancel;

        private CommonDlgResult _dlgResult = CommonDlgResult.Cancel;

        // btn1: placed on the left
        private ICommand _btnOkCmd;

        // btn2: placed in the middle, betwwen the btn1 and the btn3
        private ICommand _btnCancelCmd;


        //=====================================================================
        #region Constructor.

        //-------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public DlgInputTextMultiVM(CoreSystem coreSystem)
        {
            _coreSystem = coreSystem;

            // set default config
            Init();
        }
        #endregion

        //=====================================================================
        #region Actions/Events.

        /// <summary>
        /// To close the dlgBox, needed because in MVVM.
        /// </summary>
        //public event CloseDlgEvent();
        //public EventHandler CloseDlgEvent(object sender);

        //-------------------------------------------------------------------
        /// <summary>
        /// A call-back, when a text changed (or created if null),
        /// run the action (methods, provide params: lang and modified text. 
        /// 
        /// </summary>
        //public Action<LangCulture, TextTranslModel> RaiseOneTextTranslChanged;
        public Action DoCloseView;

        #endregion


        //=====================================================================
        #region Properties.

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
        /// Title of the text input.
        /// </summary>
        public string TextTitle
        {
            set
            {
                _textTitle = value;
                RaisePropertyChanged("TextTitle");
            }

            get { return _textTitle; }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Text of the input.
        /// </summary>
        public string TextInput
        {
            set
            {
                _textInput = value;
                RaisePropertyChanged("TextInput");
            }

            get { return _textInput; }
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

        #endregion

        //=====================================================================
        #region Command Properties.

        //---------------------------------------------------------------------
        /// <summary>
        /// Click on the button 1.
        /// </summary>
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

        //---------------------------------------------------------------------
        /// <summary>
        /// Click on the button 2.
        /// </summary>
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

        //=====================================================================
        #region Public methods.
        #endregion

        //=====================================================================		
        #region Private Command methods.

        //---------------------------------------------------------------------
        private bool CanDoBtnOkCmd()
        {
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Set the result, close the dlgBox.
        /// </summary>
        private void DoBtnOkCmd()
        {
            // the default case
            _dlgResult = CommonDlgResult.Ok;

            // close the dlgBox
            if (DoCloseView != null)
                // prevent the manager of the dlgBox
                DoCloseView();
        }

        //---------------------------------------------------------------------
        private bool CanDoBtnCancelCmd()
        {
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Set the result, close the dlgBox.
        /// </summary>
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

        //---------------------------------------------------------------------
        /// <summary>
        /// Define the buttons of the dlg.
        /// Yes/No, Ok/Cancel,...
        /// </summary>
        private void UpdateUIButtons()
        {
            //if (_buttons == CommonDlgButtons.OkCancel)
            //{
                // set text to btn1 and btn2
                //TextBtnOk = _textOk;
                //TextBtnCancel = _textCancel;

            //}

        }

        #endregion
    }
}
