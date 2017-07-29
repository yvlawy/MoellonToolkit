using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;

namespace MoellonToolkit.CommonDlgs.Impl
{
    /// <summary>
    /// The ViewModel of the View/UI.
    /// </summary>
    public class DlgMsgVM : ViewModelBase
    {
        CoreSystem _coreSystem;

        // defaults text of btn, in gb.
        //private string _textOk = "Ok";
        //private string _textYes = "Yes";
        //private string _textNo = "No";
        //private string _textCancel = "Cancel";
        //private string _textClose = "Close";

        // default cultureInfo: gb-en

        // defaults icon images
        // TODO: from resx?

        // the title of the dlg box
        private string _title;

        // icon, positionned on the top left corner
        private CommonDlgIcon _icon;
        private ImageSource _iconImg;

        private CommonDlgButtons _buttons;

        // the text message, in the main area of the dlgBox
        private string _message;

        // btn on the left: Ok/Yes
        private string _textBtn1;

        // the central btn: Cancel/No/Close
        private string _textBtn2;

        // the third btn, Cancel
        private string _textBtn3;

        // visibility of btn2: visible or Collapse
        private Visibility _btn2Visibility;

        // visibility of btn3: visible or Collapse
        private Visibility _btn3Visibility;

        private CommonDlgResult _dlgResult = CommonDlgResult.Cancel;

        // btn1: placed on the left
        private ICommand _btn1Cmd;

        // btn2: placed in the middle, betwwen the btn1 and the btn3
        private ICommand _btn2Cmd;

        // btn3: placed on the right
        private ICommand _btn3Cmd;


        //=====================================================================
        #region Constructor.

        //-------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public DlgMsgVM(CoreSystem coreSystem)
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
        /// The icon model, refresh the icon image.
        /// </summary>
        public CommonDlgIcon Icon
        {
            set
            {
                _icon = value;
                UpdateUIIcon();
                RaisePropertyChanged("IconImg");
            }

            get
            {
                return _icon;
            }
        }

        //---------------------------------------------------------------------        
        /// <summary>
        /// The icon image.
        /// </summary>
        public ImageSource IconImg
        {
            get
            {
                return _iconImg;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Define the buttons of the dlg.
        /// Yes/No, Ok/Cancel,...
        /// </summary>
        public CommonDlgButtons Buttons
        {
            set
            {
                _buttons = value;
                UpdateUIButtons();
            }

            get
            {
                return _buttons;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Message of the dlgBox.
        /// </summary>
        public string Message
        {
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }

            get { return _message; }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// The text/content of the btn1.
        /// </summary>
        public string TextBtn1
        {
            set
            {
                _textBtn1 = value;
                RaisePropertyChanged("TextBtn1");
            }

            get
            {
                return _textBtn1;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// The text/content of the btn2.
        /// </summary>
        public string TextBtn2
        {
            set
            {
                _textBtn2 = value;
                RaisePropertyChanged("TextBtn2");
            }

            get
            {
                return _textBtn2;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// The text/content of the btn3.
        /// </summary>
        public string TextBtn3
        {
            set
            {
                _textBtn3 = value;
                RaisePropertyChanged("TextBtn3");
            }

            get
            {
                return _textBtn3;
            }
        }

        //---------------------------------------------------------------------
        // visibility of btn2: visible or Collapse
        public Visibility Btn2Visibility
        {
            set 
            { 
                _btn2Visibility= value;
                RaisePropertyChanged("Btn2Visibility");
            }
            get
            {
                return _btn2Visibility;
            }
        }

        //---------------------------------------------------------------------
        // visibility of btn3: visible or Collapse
        public Visibility Btn3Visibility
        {
            set
            {
                _btn3Visibility = value;
                RaisePropertyChanged("Btn3Visibility");
            }
            get
            {
                return _btn3Visibility;
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
        public ICommand Btn1Cmd
        {
            get
            {
                if (_btn1Cmd == null)
                {
                    _btn1Cmd = new RelayCommand(param => DoBtn1Cmd(),
                                                             param => CanDoBtn1Cmd());
                }
                return _btn1Cmd;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Click on the button 2.
        /// </summary>
        public ICommand Btn2Cmd
        {
            get
            {
                if (_btn2Cmd == null)
                {
                    _btn2Cmd = new RelayCommand(param => DoBtn2Cmd(),
                                                             param => CanDoBtn2Cmd());
                }
                return _btn2Cmd;
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Click on the button 3.
        /// </summary>
        public ICommand Btn3Cmd
        {
            get
            {
                if (_btn3Cmd == null)
                {
                    _btn3Cmd = new RelayCommand(param => DoBtn3Cmd(),
                                                             param => CanDoBtn3Cmd());
                }
                return _btn3Cmd;
            }
        }

        #endregion

        //=====================================================================
        #region Public methods.
        #endregion

        //=====================================================================		
        #region Private Command methods.

        //---------------------------------------------------------------------
        private bool CanDoBtn1Cmd()
        {
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Set the result, close the dlgBox.
        /// </summary>
        private void DoBtn1Cmd()
        {
            // the default case
            _dlgResult = CommonDlgResult.Ok;

            if (_buttons == CommonDlgButtons.Close)
                _dlgResult = CommonDlgResult.Close;

            if (_buttons == CommonDlgButtons.YesNo || _buttons == CommonDlgButtons.YesNoCancel)
                _dlgResult = CommonDlgResult.Yes;

            // close the dlgBox
            if (DoCloseView != null)
                // prevent the manager of the dlgBox
                DoCloseView();
        }

        //---------------------------------------------------------------------
        private bool CanDoBtn2Cmd()
        {
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Set the result, close the dlgBox.
        /// </summary>
        private void DoBtn2Cmd()
        {
            // the default case
            _dlgResult = CommonDlgResult.Cancel;

            if (_buttons == CommonDlgButtons.YesNo || _buttons == CommonDlgButtons.YesNoCancel)
                _dlgResult = CommonDlgResult.No;

            // close the dlgBox
            if (DoCloseView != null)
                // prevent the manager of the dlgBox
                DoCloseView();
        }

        //---------------------------------------------------------------------
        private bool CanDoBtn3Cmd()
        {
            return true;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Set the result, close the dlgBox.
        /// </summary>
        private void DoBtn3Cmd()
        {
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
        }

        //---------------------------------------------------------------------
        private void UpdateUIIcon()
        {
            if (_icon == CommonDlgIcon.Warning)
            {
                _iconImg = _coreSystem.ImageProvider.GetImg(ImageCode.DlgWarning);
            }

            if (_icon == CommonDlgIcon.Question)
            {
                _iconImg = _coreSystem.ImageProvider.GetImg(ImageCode.DlgQuestion);
            }
            if (_icon == CommonDlgIcon.Error)
            {
                _iconImg = _coreSystem.ImageProvider.GetImg(ImageCode.DlgError);
            }
            if (_icon == CommonDlgIcon.Information)
            {
                _iconImg = _coreSystem.ImageProvider.GetImg(ImageCode.DlgInformation);
            }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Define the buttons of the dlg.
        /// Yes/No, Ok/Cancel,...
        /// </summary>
        private void UpdateUIButtons()
        {
            if (_buttons == CommonDlgButtons.Ok)
            {
                // set text to btn1 
                TextBtn1 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Ok);

                // hide (collapse) the btn3
                Btn2Visibility = Visibility.Collapsed;
                Btn3Visibility = Visibility.Collapsed;
            }

            if (_buttons == CommonDlgButtons.Close)
            {
                // set text to btn1 
                TextBtn1 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Close);

                // hide (collapse) the btn3
                Btn2Visibility = Visibility.Collapsed;
                Btn3Visibility = Visibility.Collapsed;
            }

            if (_buttons == CommonDlgButtons.OkCancel)
            {
                // set text to btn1 and btn2
                //TextBtn1 = _textOk;
                TextBtn1 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Ok);
                TextBtn2 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Cancel);

                // hide (collapse) the btn3
                Btn3Visibility = Visibility.Collapsed;
            }

            if (_buttons == CommonDlgButtons.YesNo)
            {
                // set text to btn1 and btn2
                //TextBtn1 = _textYes;
                TextBtn1 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Yes);
                TextBtn2 = _coreSystem.TranslationMgr.GetTranslation(StringCode.No);

                // hide (collapse) the btn3
                Btn3Visibility = Visibility.Collapsed;
            }

            if (_buttons == CommonDlgButtons.YesNoCancel)
            {
                // set text to btn1 and btn2
                TextBtn1 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Yes);
                TextBtn2 = _coreSystem.TranslationMgr.GetTranslation(StringCode.No);
                TextBtn3 = _coreSystem.TranslationMgr.GetTranslation(StringCode.Cancel);
            }
        }

        #endregion
    }
}
