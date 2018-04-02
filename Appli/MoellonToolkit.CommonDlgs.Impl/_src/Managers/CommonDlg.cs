using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace MoellonToolkit.CommonDlgs.Impl
{
    //*************************************************************************
    /// <summary>
    /// Provide Common dlgbox: Show Error/Warning/Question/..., OpenFile, SaveFile,...
    /// 
    /// Is in WPF techno, implemented in MVVM.
    /// 
    /// But Need Win32/WinForms for ShowSelectFolder() and ShowSelectFile().
    /// 
    /// ------------
    /// Add the libs: 
    /// System.Windows.Forms (WinForms) is For OpenFileDialog, DialogResult, FolderBrowserDialog.
    /// </summary>
    public class CommonDlg : ICommonDlg
    {
        //=====================================================================
        #region Private members.

        //private string _dlgTitleError = "Error";
        //private string _dlgTitleWarning = "Warning";
        //private string _dlgTitleInformation = "Information";
        //private string _dlgTitleQuestion = "Question";

        private Window _parent;

        /// <summary>
        /// The dlgMsg opened, referenced to close it (unable to close it from the ViewModel).
        /// </summary>
        private DlgMsg _dlgMsg;

        private DlgInputText _dlgInputText;

        private DlgInputTextMulti _dlgInputTextMulti;

        private DlgComboChoice _dlgComboChoice;

        private DlgListChoice _dlgListChoice;

        private CoreSystem _coreSystem;
        #endregion

        //=====================================================================
        #region Constructor.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent"></param>
        public CommonDlg()
        {
            _coreSystem = new CoreSystem();
        }
        #endregion

        //=====================================================================
        #region general Public methods.

        public void SetMainWindow(Window mainWindow)
        {
            _parent = mainWindow;
        }
        
        #endregion

        //=====================================================================
        #region Public methods: Parameters.


        /// <summary>
        /// Define the current culture info for text translation.
        /// </summary>
        /// <param name="cultureCode"></param>
        public void SetCurrentCultureInfo(CultureCode cultureCode)
        {
            _coreSystem.TranslationMgr.SetCurrentCultureInfo(cultureCode);
        }


        public TranslatedTextPage FindTranslatedTextByCultureCode(CultureCode cultureCode)
        {
            return _coreSystem.TranslationMgr.FindTranslatedTextByCultureCode(cultureCode);
        }

        /// <summary>
        /// return the list of all existing CultureCode (without any translation).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StringCode> GetListStringCode()
        {
            List<StringCode> listStringCode = new List<StringCode>();

            Array array= Enum.GetValues(typeof(StringCode));

            foreach(StringCode stringCode in array)
                listStringCode.Add(stringCode);

            return listStringCode;
        }

        /// <summary>
        /// Set for the UserDefined cultureCode a new text matching a stringCode.
        /// </summary>
        /// <param name="stringCode"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool SetUserDefinedTranslatedText(StringCode stringCode, string text)
        {
            return _coreSystem.TranslationMgr.SetUserDefinedTranslatedText(stringCode, text);
        }

        // Set the default title of the dlg.
        // SetTitle(CommonDlgIcons dlgType, string title)

        // SetTitle(Cultureinfo cultInf, CommonDlgIcons dlgType, string title)

        // define the text of the button code
        // SetBtnText(CommonDlgButtons btn, string text)
        // SetBtnText(CultureInfo cultInf, CommonDlgButtons btn, string text)

        // multilingue!, comment?
        // define the default cultureInfo/translation code, associated to default/defined titles and texts
        // SetDefaultCultureInfo(CultureInfo cultInf)

        // definir/ajouter un cultureInfo
        // TODO:  

        // Activer un cultureInfo
        // TODO:

        // change default icons
        // TODO:

        #endregion

        //=====================================================================
        #region Public methods: Show Dlg Message.

        //-------------------------------------------------------------------
        /// <summary>
        /// Show a dlg Box containing a message, an icon and some buttons.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlg(WHSize whSize, string dlgTitle, string message, CommonDlgIcon icon, CommonDlgButtons buttons)
        {

            if (dlgTitle == null)
                dlgTitle = "";
            if (string.IsNullOrWhiteSpace(message))
                message = "(null)";

            // create and define the dlgBox
            DlgMsgVM dlgVM = new DlgMsgVM(_coreSystem);
            dlgVM.Title = dlgTitle;
            dlgVM.Message = message;

            // set the icon and the buttons 
            dlgVM.Icon = icon;
            dlgVM.Buttons = buttons;

            // to close the dlgBox, like this, because its a WPF/MVVM implementation
            dlgVM.DoCloseView = () => DoCloseDlgMsg();

            _dlgMsg = new DlgMsg();
            _dlgMsg.DataContext = dlgVM;
            _dlgMsg.Owner = _parent;

            SetWindowWHSize(_dlgMsg, whSize);
            //ici();
            //// default values
            //_dlgMsg.Width = 300;
            //_dlgMsg.Height = 160;

            //if (whSize == WHSize.HL)
            //    _dlgMsg.Height = 250;

            //if (whSize == WHSize.HXL)
            //    _dlgMsg.Height = 300;

            //if (whSize == WHSize.HXXL)
            //    _dlgMsg.Height = 500;

            //if (whSize == WHSize.WL)
            //    _dlgMsg.Width= 400;

            //if (whSize == WHSize.WL_HL)
            //{
            //    _dlgMsg.Width = 400;
            //    _dlgMsg.Height = 250;
            //}

            //if (whSize == WHSize.WL_HXL)
            //{
            //    _dlgMsg.Width = 400;
            //    _dlgMsg.Height = 300;
            //}

            //if (whSize == WHSize.WL_HXXL)
            //{
            //    _dlgMsg.Width = 400;
            //    _dlgMsg.Height = 500;
            //}

            //if (whSize == WHSize.WXL)
            //{
            //    _dlgMsg.Width = 500;
            //}

            //if (whSize == WHSize.WXL_HL)
            //{
            //    _dlgMsg.Width = 500;
            //    _dlgMsg.Height = 250;
            //}

            //if (whSize == WHSize.WXL_HXL)
            //{
            //    _dlgMsg.Width = 500;
            //    _dlgMsg.Height = 300;
            //}

            //if (whSize == WHSize.WXL_HXXL)
            //{
            //    _dlgMsg.Width = 500;
            //    _dlgMsg.Height = 500;
            //}

            //if (whSize == WHSize.WXXL)
            //{
            //    _dlgMsg.Width = 700;
            //}

            //if (whSize == WHSize.WXXL_HL)
            //{
            //    _dlgMsg.Width = 700;
            //    _dlgMsg.Height = 250;
            //}

            //if (whSize == WHSize.WXXL_HXL)
            //{
            //    _dlgMsg.Width = 700;
            //    _dlgMsg.Height = 300;
            //}

            //if (whSize == WHSize.WXXL_HXXL)
            //{
            //    _dlgMsg.Width = 700;
            //    _dlgMsg.Height = 500;
            //}

            // open the dlgBox, is modal/synchronized, wait the close of the dlg
            _dlgMsg.ShowDialog();
            _dlgMsg = null;

            // get the result
            return dlgVM.DlgResult;

        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Show a dlg Box containing a message, an icon and some buttons.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlg(string dlgTitle, string message, CommonDlgIcon icon, CommonDlgButtons buttons)
        {
            return ShowDlg(WHSize.Default, dlgTitle, message, icon, buttons);
        }

        //-------------------------------------------------------------------
        /// <summary>	
        /// Shows an error message with an Ok button.
        /// </summary>
        /// <param name="message">The error message</param>
        public CommonDlgResult ShowError(WHSize whSize, string title, string message)
        {
            return ShowDlg(whSize, title, message, CommonDlgIcon.Error, CommonDlgButtons.Ok);
        }

        /// <summary>	
        /// Shows an error message with an Ok button.
        /// </summary>
        /// <param name="message">The error message</param>
        public CommonDlgResult ShowError(WHSize whSize, string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgErrorTitle);
            return ShowDlg(whSize, title, message, CommonDlgIcon.Error, CommonDlgButtons.Ok);
        }

        /// <summary>	
        /// Shows an error message with an Ok button.
        /// </summary>
        /// <param name="message">The error message</param>
        public void ShowError(string title, string message)
        {
            ShowDlg(title, message, CommonDlgIcon.Error, CommonDlgButtons.Ok);
        }

        /// <summary>	
        /// Shows an error message with an Ok button.
        /// </summary>
        /// <param name="message">The error message</param>
        public void ShowError(string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgErrorTitle);
            ShowDlg(title, message, CommonDlgIcon.Error, CommonDlgButtons.Ok);
        }


        //-------------------------------------------------------------------
        /// <summary>
        /// Shows a warning message with an Ok button.
        /// </summary>
        /// <param name="message">The warning message</param>
        public void ShowWarning(WHSize whSize, string title, string message)
        {
            ShowDlg(whSize, title, message, CommonDlgIcon.Warning, CommonDlgButtons.Ok);
        }

        /// <summary>
        /// Shows a warning message with an Ok button.
        /// </summary>
        /// <param name="message">The warning message</param>
        public void ShowWarning(WHSize whSize, string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgWarningTitle);
            ShowDlg(whSize, title, message, CommonDlgIcon.Warning, CommonDlgButtons.Ok);
        }

        /// <summary>
        /// Shows a warning message with an Ok button.
        /// </summary>
        /// <param name="message">The warning message</param>
        public void ShowWarning(string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgWarningTitle);
            ShowDlg(title, message, CommonDlgIcon.Warning, CommonDlgButtons.Ok);
        }

        /// <summary>
        /// Shows a warning message with an Ok button.
        /// </summary>
        /// <param name="message">The warning message</param>
        public void ShowWarning(string title, string message)
        {
            ShowDlg(title, message, CommonDlgIcon.Warning, CommonDlgButtons.Ok);
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Shows an information message with an Ok button.
        /// </summary>
        /// <param name="message">The information message</param>
        public void ShowInformation(WHSize whSize, string title, string message)
        {
            ShowDlg(title, message, CommonDlgIcon.Information, CommonDlgButtons.Ok);
        }

        /// <summary>
        /// Shows an information message with an Ok button.
        /// </summary>
        /// <param name="message">The information message</param>
        public void ShowInformation(WHSize whSize, string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgInformationTitle);
            ShowDlg(whSize, title, message, CommonDlgIcon.Information, CommonDlgButtons.Ok);
        }
        /// <summary>
        /// Shows an information message with an Ok button.
        /// </summary>
        /// <param name="message">The information message</param>
        public void ShowInformation(string title, string message)
        {
            ShowDlg(title, message, CommonDlgIcon.Information, CommonDlgButtons.Ok);
        }

        /// <summary>
        /// Shows an information message with an Ok button.
        /// </summary>
        /// <param name="message">The information message</param>
        public void ShowInformation(string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgInformationTitle);
            ShowDlg(title, message, CommonDlgIcon.Information, CommonDlgButtons.Ok);
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Shows a question message with a Yes/No buttons.
        /// The result can be: Yes, No or Cancel (when closed by the X).
        /// </summary>
        /// <param name="message">The information message</param>
        public CommonDlgResult ShowQuestion(WHSize whSize, string title, string message)
        {
            return ShowDlg(whSize, title, message, CommonDlgIcon.Question, CommonDlgButtons.YesNo);
        }

        /// <summary>
        /// Shows a question message with a Yes/No buttons.
        /// The result can be: Yes, No or Cancel (when closed by the X).
        /// </summary>
        /// <param name="message">The information message</param>
        public CommonDlgResult ShowQuestion(WHSize whSize, string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgQuestionTitle);
            return ShowDlg(whSize, title, message, CommonDlgIcon.Question, CommonDlgButtons.YesNo);
        }

        /// <summary>
        /// Shows a question message with a Yes/No buttons.
        /// The result can be: Yes, No or Cancel (when closed by the X).
        /// </summary>
        /// <param name="message">The information message</param>
        public CommonDlgResult ShowQuestion(string title, string message)
        {
            return ShowDlg(title, message, CommonDlgIcon.Question, CommonDlgButtons.YesNo);
        }

        /// <summary>
        /// Shows a question message with a Yes/No buttons.
        /// 
        /// The result can be: Yes, No or Cancel (when closed by the X).
        /// </summary>
        /// <param name="message">The information message</param>
        public CommonDlgResult ShowQuestion(string message)
        {
            string title = _coreSystem.TranslationMgr.GetTranslation(StringCode.DlgQuestionTitle);
            return ShowDlg(title, message, CommonDlgIcon.Question, CommonDlgButtons.YesNo);
        }

        // ShowQuestion with YesNoCancel buttons
        // TODO:


        #endregion

        //=====================================================================
        #region Public Show dlg input text.
        
        //---------------------------------------------------------------------
        /// <summary>
        /// Show a dlg box to get a text with buttons: Ok/Cancel.
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="initialtext"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlgInputText(WHSize whSize, string dlgTitle, string textTitle, string initialtext, out string text)
        {
            if (dlgTitle == null)
                dlgTitle = "";
            if (string.IsNullOrWhiteSpace(textTitle))
                textTitle = "";

            // create and define the dlgBox
            DlgInputTextVM dlgVM = new DlgInputTextVM(_coreSystem);
            dlgVM.Title = dlgTitle;
            dlgVM.TextTitle = textTitle;
            dlgVM.TextInput = initialtext;


            // to close the dlgBox, like this, because its a WPF/MVVM implementation
            dlgVM.DoCloseView = () => DoCloseDlgInputText();

            _dlgInputText = new DlgInputText();
            _dlgInputText.Owner = _parent;
            _dlgInputText.DataContext = dlgVM;

            SetWindowWHSize(_dlgInputText, whSize);

            // open the dlgBox, is modal/synchronized, wait the close of the dlg
            _dlgInputText.ShowDialog();
            _dlgInputText = null;

            // get the text
            text = dlgVM.TextInput;

            // get the result
            return dlgVM.DlgResult;
        }

        /// <summary>
        /// Show a dialog box to input a multi-lines text, with ok/cancel buttons.
        /// 
        /// The WHsize is not used!
        /// </summary>
        /// <param name="whSize"></param>
        /// <param name="dlgTitle"></param>
        /// <param name="textTitle"></param>
        /// <param name="initialtext"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlgInputTextMulti(WHSize whSize, string dlgTitle, string textTitle, string initialtext, out string text)
        {
            if (dlgTitle == null)
                dlgTitle = "";
            if (string.IsNullOrWhiteSpace(textTitle))
                textTitle = "";

            // create and define the dlgBox
            DlgInputTextMultiVM dlgVM = new DlgInputTextMultiVM(_coreSystem);
            dlgVM.Title = dlgTitle;
            dlgVM.TextTitle = textTitle;
            dlgVM.TextInput = initialtext;


            // to close the dlgBox, like this, because its a WPF/MVVM implementation
            dlgVM.DoCloseView = () => DoCloseDlgInputTextMulti();

            _dlgInputTextMulti = new DlgInputTextMulti();
            _dlgInputTextMulti.Owner = _parent;
            _dlgInputTextMulti.DataContext = dlgVM;

            // TODO: need to use special size for this dialogbox type!
            //SetWindowWHSize(_dlgInputTextMulti, whSize);

            // open the dlgBox, is modal/synchronized, wait the close of the dlg
            _dlgInputTextMulti.ShowDialog();
            _dlgInputTextMulti = null;

            // get the text
            text = dlgVM.TextInput;

            // get the result
            return dlgVM.DlgResult;
        }

        /// <summary>
        /// Show a dlg box to get a text with buttons: Ok/Cancel.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="textTitle"></param>
        /// <param name="initialtext"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlgInputText(string dlgTitle, string textTitle, string initialtext, out string text)
        {
            return ShowDlgInputText(WHSize.Default, dlgTitle, textTitle, initialtext, out text);
        }

        /// <summary>
        /// Show a dlg box to get a text multi-lines with buttons: Ok/Cancel.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="textTitle"></param>
        /// <param name="initialtext"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlgInputTextMulti(string dlgTitle, string textTitle, string initialtext, out string text)
        {
            return ShowDlgInputTextMulti(WHSize.Default, dlgTitle, textTitle, initialtext, out text);
        }

        #endregion

        //=====================================================================
        #region Public Show dlg Select Folder/file, save file.

        //---------------------------------------------------------------------
        /// <summary>
        /// Show a dialogBox to select a File.
        /// Select a file (not open the file just select it!).
        /// If the filter is bad formatted, return error.
        /// 
        /// Use a Win32 function, not in WPF.
        /// 
        /// exp:
        ///  defaultExt = ".xls|.xlsx";
        ///	 filter = "Excel documents (*.xls, *.xlsx)|*.xls;*.xlsx";
        ///	 
        /// </summary>
        /// <param name="defaultPath"></param>
        /// <param name="defaultExt"></param>
        /// <param name="filter"></param>
        public CommonDlgResult ShowDlgSelectFile(string defaultPath, string defaultExt, string filter, out string pathName, out string fileName)
            {
                pathName = "";
                fileName = "";

                if (string.IsNullOrWhiteSpace(defaultPath))
                    return CommonDlgResult.Error;
                if (string.IsNullOrWhiteSpace(defaultExt))
                    return CommonDlgResult.Error;
                if (string.IsNullOrWhiteSpace(filter))
                    return CommonDlgResult.Error;

                try
                {
                    OpenFileDialog dlg = new OpenFileDialog
                    {
                        InitialDirectory = defaultPath
                    };

                    dlg.DefaultExt = defaultExt;
                    dlg.Filter = filter;

                    DialogResult res = dlg.ShowDialog();
                    if (res != DialogResult.OK)
                        return CommonDlgResult.Cancel;

                    pathName = Path.GetDirectoryName(dlg.FileName);
                    fileName = Path.GetFileName(dlg.FileName);

                    dlg.Dispose();
                    dlg = null;

                    return CommonDlgResult.Ok;
                }
                catch
                {
                    return CommonDlgResult.Error;
                }
            }

            //---------------------------------------------------------------------        
            public CommonDlgResult ShowDlgSelectFolder(string defaultPath, out string pathName, out string fileName)
            {
                pathName = "";
                fileName = "";

                if (string.IsNullOrWhiteSpace(defaultPath))
                    return CommonDlgResult.Error;

                var dlg = new FolderBrowserDialog();
                dlg.SelectedPath = defaultPath;
                //dlg.RootFolder= defaultPath;


                if (dlg.ShowDialog() != DialogResult.OK)
                    return CommonDlgResult.Cancel;

                pathName = Path.GetDirectoryName(dlg.SelectedPath);
                if (pathName == null)
                    pathName = dlg.SelectedPath;
                fileName = dlg.SelectedPath.Split(Path.DirectorySeparatorChar).Last();

                dlg.Dispose();
                dlg = null;

                return CommonDlgResult.Ok;
            }

            /// <summary>
            /// Open a dialogBox to save a file.
            /// </summary>
            /// <param name="defaultPath"></param>
            /// <param name="defaultExt"></param>
            /// <param name="filter"></param>
            /// <param name="pathName"></param>
            /// <param name="fileName"></param>
            /// <returns></returns>
            public CommonDlgResult ShowDlgSaveFile(string defaultPath, string defaultExt, string filter, out string pathName, out string fileName)
            {
                pathName = "";
                fileName = "";

                try
                {
                    var dlg = new SaveFileDialog
                    {
                        InitialDirectory = defaultPath
                    };

                    dlg.DefaultExt = defaultExt;
                    dlg.Filter = filter;

                    DialogResult res = dlg.ShowDialog();
                    if (res != DialogResult.OK)
                        return CommonDlgResult.Cancel;

                    pathName = Path.GetDirectoryName(dlg.FileName);
                    fileName = Path.GetFileName(dlg.FileName);

                    return CommonDlgResult.Ok;
                }
                catch
                {
                    return CommonDlgResult.Error;
                }
            }
            #endregion

        //=====================================================================
        #region Public Show dlg ComboChoice, List Multi-Choice.

        /// <summary>
        /// Show a dialog box with a combobox to selection/choice an item.
        /// only one item can be selected or any.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="listObject"></param>
        /// <param name="selectIt"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlgComboChoice(string dlgTitle, List<DlgComboChoiceItem> listObject, DlgComboChoiceItem selectIt, out DlgComboChoiceItem selected)
        {
            // create and define the dlgBox
            DlgComboChoiceVM dlgVM = new DlgComboChoiceVM(_coreSystem);
            dlgVM.Title = dlgTitle;

            // fourni la liste de la combox
            dlgVM.ListItem = listObject;
            dlgVM.SelectedItem = selectIt;

            // to close the dlgBox, like this, because its a WPF/MVVM implementation
            dlgVM.DoCloseView = () => DoCloseDlgComboChoice();

            _dlgComboChoice = new DlgComboChoice();
            _dlgComboChoice.Owner = _parent;
            _dlgComboChoice.DataContext = dlgVM;

            // open the dlgBox, is modal/synchronized, wait the close of the dlg
            _dlgComboChoice.ShowDialog();
            selected = dlgVM.SelectedItem;

            // get the result
            return dlgVM.DlgResult;
        }

        /// <summary>
        /// Show a dialog box with a listbox to selection/choice one or many items.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="listObject"></param>
        /// <param name="listSelectIt"></param>
        /// <param name="listSelected"></param>
        /// <returns></returns>
        public CommonDlgResult ShowDlgListChoice(string dlgTitle, List<DlgListChoiceItem> listObject, List<DlgListChoiceItem> listSelectIt, out List<DlgListChoiceItem> listSelected)
        {
            listSelected = null;

            // create and define the dlgBox
            DlgListChoiceVM dlgVM = new DlgListChoiceVM(_coreSystem);
            dlgVM.Title = dlgTitle;

            // fourni la liste de la combox
            dlgVM.ListItem = listObject;

            //dlgVM.ListSelectedItem = selectIt;

            // to close the dlgBox, like this, because its a WPF/MVVM implementation
            dlgVM.DoCloseView = () => DoCloseDlgListChoice();

            _dlgListChoice = new DlgListChoice();
            _dlgListChoice.Owner = _parent;
            _dlgListChoice.DataContext = dlgVM;

            // open the dlgBox, is modal/synchronized, wait the close of the dlg
            _dlgListChoice.ShowDialog();
            listSelected = dlgVM.ListSelectedItem;

            // get the result
            return dlgVM.DlgResult;

        }

        #endregion

        //=====================================================================
        #region Private methods.

        private void SetWindowWHSize(Window window, WHSize whSize)
        {
            // default values
            window.Width = 300;
            window.Height = 160;

            if (whSize == WHSize.HL)
                window.Height = 250;

            if (whSize == WHSize.HXL)
                window.Height = 300;

            if (whSize == WHSize.HXXL)
                window.Height = 500;

            if (whSize == WHSize.WL)
                window.Width= 400;

            if (whSize == WHSize.WL_HL)
            {
                window.Width = 400;
                window.Height = 250;
            }

            if (whSize == WHSize.WL_HXL)
            {
                window.Width = 400;
                window.Height = 300;
            }

            if (whSize == WHSize.WL_HXXL)
            {
                window.Width = 400;
                window.Height = 500;
            }

            if (whSize == WHSize.WXL)
            {
                window.Width = 500;
            }

            if (whSize == WHSize.WXL_HL)
            {
                window.Width = 500;
                window.Height = 250;
            }

            if (whSize == WHSize.WXL_HXL)
            {
                window.Width = 500;
                window.Height = 300;
            }

            if (whSize == WHSize.WXL_HXXL)
            {
                window.Width = 500;
                window.Height = 500;
            }

            if (whSize == WHSize.WXXL)
            {
                window.Width = 700;
            }

            if (whSize == WHSize.WXXL_HL)
            {
                window.Width = 700;
                window.Height = 250;
            }

            if (whSize == WHSize.WXXL_HXL)
            {
                window.Width = 700;
                window.Height = 300;
            }

            if (whSize == WHSize.WXXL_HXXL)
            {
                window.Width = 700;
                window.Height = 500;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Call-back, called to close the dlgBox.
        /// </summary>
        private void DoCloseDlgMsg()
        {
            if (_dlgMsg == null)
                return;

            _dlgMsg.Close();
        }

        private void DoCloseDlgInputText()
        {
            if (_dlgInputText == null)
                return;

            _dlgInputText.Close();
        }

        private void DoCloseDlgInputTextMulti()
        {
            if (_dlgInputTextMulti == null)
                return;

            _dlgInputTextMulti.Close();
        }


        private void DoCloseDlgComboChoice()
        {
            if (_dlgComboChoice == null)
                return;

            _dlgComboChoice.Close();
        }


        private void DoCloseDlgListChoice()
        {
            if (_dlgListChoice == null)
                return;

            _dlgListChoice.Close();
        }


        #endregion
    }
}
