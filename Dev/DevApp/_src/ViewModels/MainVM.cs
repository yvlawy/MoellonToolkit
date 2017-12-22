using DevApp.Ctrl;
using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MoellonToolkit.CommonDlgs.Impl.Components;

namespace DevApp.ViewModels
{
    /// <summary>
    /// The VM of the main view.
    /// </summary>
    public class MainVM : ViewModelBase
    {
        string _name = "My App";

        /// <summary>
        /// The main menu of the application.
        /// </summary>
        AppMainMenuVM _appMainMenuVM;

        ICommand _requestCloseWindowCmd;

        ICommand _showDlgInformationCmd;
        ICommand _showDlgQuestionCmd;
        ICommand _showDlgErrorCmd;
        ICommand _showDlgWarningCmd;

        ICommand _showDlg_WXL_InformationCmd;
        ICommand _showDlg_WXL_HXL_QuestionCmd;
        ICommand _showDlg_WL_HXXL_ErrorCmd;

        ICommand _showDlgInputTextCmd;
        ICommand _showDlg_WL_InputTextCmd;

        ICommand _showDlgSelectFileCmd;

        /// <summary>
        /// button command: show a dialogBox to save a file.
        /// </summary>
        ICommand _showDlgSaveFileCmd;

        /// <summary>
        /// Button command: show a dialogBox wich display a combo with items, the user can select one item.
        /// </summary>
        ICommand _showDlgComboChoiceCmd;

        /// <summary>
        /// Button command: show a dialogBox wich display a combo with items, the user can select one or many items.
        /// </summary>
        ICommand _showDlgListChoiceCmd;

        /// <summary>
        /// List of culture code, 
        /// exp: gb, fr, UserDefined,...
        /// </summary>
        ObservableCollection<CultureCodeVM> _listCultureCode;
        CultureCodeVM _cultureCodeSelected;

        /// <summary>
        /// List of string code.
        /// exp: Ok, No, Cancel,...
        /// </summary>
        ObservableCollection<StringCodeVM> _listStringCode;
        StringCodeVM _stringCodeSelected;

        string _stringCodeToEdit;
        string _prevStringCodeToEdit;

        bool _stringCodeToEditModified = false;
        bool _stringCodeToEditIsReadOnly= true;
        //ICommand _stringCodeToEditKeyDownCmd;

        /// <summary>
        /// save the modified TextCode.
        /// </summary>
        ICommand _saveTextCodeCmd;
        bool _saveTextCodeIsEnabled;

        /// <summary>
        /// Cancel the modified TextCode.
        /// </summary>
        ICommand _cancelTextCodeCmd;
        bool _cancelTextCodeIsEnabled;

        /// <summary>
        /// The current translated text page (with a current CultureCode).
        /// </summary>
        TranslatedTextPage _translatedTextPage;

        //=====================================================================
        #region Constructor.

        //-------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainVM()
        {
            _listCultureCode = new ObservableCollection<CultureCodeVM>();
            _listStringCode = new ObservableCollection<StringCodeVM>();
            Init();
        }

        #endregion

        //=====================================================================
        #region Properties.

        //---------------------------------------------------------------------
        public string Name { get { return _name; } }

        //---------------------------------------------------------------------
        public AppMainMenuVM AppMainMenuVM
        {
            get { return _appMainMenuVM; }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Return true if the window is closing.
        /// Its the main Window, so if the window is closing, the app is closing, so call a method of the ctrl.
        /// </summary>
        public bool IsWindowClosing
        {
            get
            {
                if (AppCtrlProvider.AppCtrl.AppState == AppState.Closing)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// The dynamic dataGrid: its the base component: only a grid without any button.
        /// </summary>
        public DynDataGridVM DynDataGridVM
        { get; set; }

        /// <summary>
        /// The Edit dynamic dataGrid (having Add/Del row and col).
        /// </summary>
        public EditDynDataGridVM EditDynDataGridVM
        { get; set; }

        #endregion

        //=====================================================================		
        #region Command Properties.

        //-------------------------------------------------------------------
        /// <summary>
        /// Cancel Command, close the dlg.
        /// </summary>
        public ICommand RequestCloseWindowCmd
        {
            get
            {
                if (_requestCloseWindowCmd == null)
                {
                    _requestCloseWindowCmd = new RelayCommand(param => DoRequestCloseWindow(),
                                               param => CanDoRequestCloseWindow());
                }
                return _requestCloseWindowCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box Information.
        /// </summary>
        public ICommand ShowDlgInformationCmd
        {
            get
            {
                if (_showDlgInformationCmd== null)
                {
                    _showDlgInformationCmd = new RelayCommand(param => DoShowDlgInformation(),
                                               param => CanDoShowDlgInformation());
                }
                return _showDlgInformationCmd;
            }
        }


        /// <summary>
        /// Show a Dialog box Question.
        /// </summary>
        public ICommand ShowDlgQuestionCmd
        {
            get
            {
                if (_showDlgQuestionCmd == null)
                {
                    _showDlgQuestionCmd = new RelayCommand(param => DoShowDlgQuestion(),
                                               param => CanDoShowDlgQuestion());
                }
                return _showDlgQuestionCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box Error.
        /// </summary>
        public ICommand ShowDlgErrorCmd
        {
            get
            {
                if (_showDlgErrorCmd == null)
                {
                    _showDlgErrorCmd = new RelayCommand(param => DoShowDlgError(),
                                               param => CanDoShowDlgError());
                }
                return _showDlgErrorCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box Warning.
        /// </summary>
        public ICommand ShowDlgWarningCmd
        {
            get
            {
                if (_showDlgWarningCmd== null)
                {
                    _showDlgWarningCmd = new RelayCommand(param => DoShowDlgWarning(),
                                               param => CanDoShowDlgWarning());
                }
                return _showDlgWarningCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box Affirmation.
        /// </summary>
        public ICommand ShowDlg_WXL_InformationCmd
        {
            get
            {                
                if (_showDlg_WXL_InformationCmd== null)
                {
                    _showDlg_WXL_InformationCmd = new RelayCommand(param => DoShowDlg_WXL_Information(),
                                               param => CanDoShowDlg_WXL_Information());
                }
                return _showDlg_WXL_InformationCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box Question.
        /// </summary>
        public ICommand ShowDlg_WXL_HXL_QuestionCmd
        {
            get
            {
                if (_showDlg_WXL_HXL_QuestionCmd == null)
                {
                    _showDlg_WXL_HXL_QuestionCmd = new RelayCommand(param => DoShowDlg_WXL_HXL_Question(),
                                               param => CanDoShowDlg_WXL_HXL_Question());
                }
                return _showDlg_WXL_HXL_QuestionCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box error.
        /// </summary>
        public ICommand ShowDlg_WL_HXXL_ErrorCmd
        {
            get
            {
                if (_showDlg_WL_HXXL_ErrorCmd== null)
                {
                    _showDlg_WL_HXXL_ErrorCmd = new RelayCommand(param => DoShowDlg_WL_HXXL_Error(),
                                               param => CanDoShowDlg_WL_HXXL_Error());
                }
                return _showDlg_WL_HXXL_ErrorCmd;
            }
        }
        
        /// <summary>
        /// Show a Dialog box Text Input, to get a text.
        /// </summary>
        public ICommand ShowDlgInputTextCmd
        {
            get
            {
                if (_showDlgInputTextCmd == null)
                {
                    _showDlgInputTextCmd = new RelayCommand(param => DoShowDlgInputText(),
                                               param => CanDoShowDlgInputText());
                }
                return _showDlgInputTextCmd;
            }
        }

        public ICommand ShowDlg_WL_InputTextCmd
        {
            get
            {
                if (_showDlg_WL_InputTextCmd == null)
                {
                    _showDlg_WL_InputTextCmd = new RelayCommand(param => DoShowDlg_WL_InputText(),
                                               param => CanDoShowDlg_WL_InputText());
                }
                return _showDlg_WL_InputTextCmd;
            }
        }

        /// <summary>
        /// Show a Dialog box select a file.
        /// </summary>
        public ICommand ShowDlgSelectFileCmd
        {
            get
            {
                if (_showDlgSelectFileCmd == null)
                {
                    _showDlgSelectFileCmd = new RelayCommand(param => DoShowDlgSelectFile(),
                                               param => CanDoShowDlgSelectFile());
                }
                return _showDlgSelectFileCmd;
            }
        }

        /// <summary>
        /// button command: show a dialogBox to save a file.
        /// </summary>
        public ICommand ShowDlgSaveFileCmd
        {
            get
            {
                if (_showDlgSaveFileCmd == null)
                {
                    _showDlgSaveFileCmd = new RelayCommand(param => DoShowDlgSaveFile(),
                                               param => CanDoShowDlgSaveFile());
                }
                return _showDlgSaveFileCmd;
            }
        }

        /// <summary>
        /// Button command: show a dialogBox wich display a combo with items.
        /// </summary>
        public ICommand ShowDlgComboChoiceCmd
        {
            get
            {
                if (_showDlgComboChoiceCmd == null)
                {
                    _showDlgComboChoiceCmd = new RelayCommand(param => DoShowDlgComboChoice(),
                                               param => CanDoShowDlgComboChoice());
                }
                return _showDlgComboChoiceCmd;
            }
        }

        /// <summary>
        /// Button command: show a dialogBox wich display a combo with items, the user can select one or many items.
        /// </summary>
        public ICommand ShowDlgListChoiceCmd
        {
            get
            {
                if (_showDlgListChoiceCmd == null)
                {
                    _showDlgListChoiceCmd = new RelayCommand(param => DoShowDlgListChoice(),
                                               param => CanDoShowDlgListChoice());
                }
                return _showDlgListChoiceCmd;
            }
        }

        //--------------------------------------------------------
        /// <summary>
        /// list of cultureCode available: GB, FR, ES, UserDefined.
        /// </summary>
        public ObservableCollection<CultureCodeVM> ListCultureCode
        {
            get { return _listCultureCode; }
        }

        public CultureCodeVM CultureCodeSelected
        {
            get { return _cultureCodeSelected; }
            set {
                _cultureCodeSelected = value;
                CultureCodeIsSelected(_cultureCodeSelected.CultureCode);

                // just to refresh the field
                if(_stringCodeSelected!=null)
                    StringCodeSelected= _stringCodeSelected;

                // if the cultureCode is UserDefined, then the StringCodeToEdit is editable
                if (_cultureCodeSelected.CultureCode == CultureCode.UserDefined)
                {
                    // can edit the current text
                    if (StringCodeSelected!=null)
                        
                        StringCodeToEditIsReadOnly = false;
                    else
                        // no textCode selected, can't edit the text
                        StringCodeToEditIsReadOnly = true;

                    RaisePropertyChanged("CultureCodeSelected");
                    return;
                }

                // its not a Userdefined cultureCode, can't edit the text
                StringCodeToEditIsReadOnly = true;

                RaisePropertyChanged("CultureCodeSelected");

            }
        }

        //--------------------------------------------------------
        /// <summary>
        /// list of stringCode (with translated text) in the selected CultureCode.
        /// </summary>
        public ObservableCollection<StringCodeVM> ListStringCode
        {
            get {
                return _listStringCode;
            }
        }

        /// <summary>
        /// The selected stringCode.
        /// if one is selected and cultureCode is UserDefined -> so, the StringCodeToEdit can be edited.
        /// </summary>
        public StringCodeVM StringCodeSelected
        {
            get { return _stringCodeSelected; }
            set
            {
                _stringCodeSelected = value;

                // disabled save and cancel buttons
                SaveTextCodeIsEnabled = false;
                CancelTextCodeIsEnabled = false;
                _stringCodeToEditModified = false;

                // not null and the cultureCode is UserDefined
                if (_stringCodeSelected==null)
                {
                    // can't edit the text
                    StringCodeToEditIsReadOnly = true;
                    RaisePropertyChanged("StringCodeSelected");
                    return;
                }

                // the cultureCode is UserDefined
                if(CultureCodeSelected.CultureCode == CultureCode.UserDefined)
                    // can edit the text
                    StringCodeToEditIsReadOnly = false;
                else
                    StringCodeToEditIsReadOnly = true;

                // get the translated text for the selected cultreCode
                TranslatedText transText = _translatedTextPage.FindTranslatedTextByStringCode(_stringCodeSelected.StringCode);

                if (transText == null)
                {
                    StringCodeToEdit = "";
                }
                else
                {
                    StringCodeToEdit = transText.Text;
                }

                // save the previous text
                _prevStringCodeToEdit = _stringCodeToEdit;

                RaisePropertyChanged("StringCodeSelected");
            }
        }

        /// <summary>
        /// The string code to edit, if the CultureCode is UserDefined, otherwise the value is read-only.
        /// </summary>
        public string StringCodeToEdit
        {
            get
            {
                return _stringCodeToEdit;
            }

            set
            {
                if (_stringCodeToEdit == value)
                    return;

                _stringCodeToEdit = value;

                // the cultureCode is UserDefined so both btns Save and cancel are now enabled
                if (CultureCodeSelected.CultureCode != CultureCode.UserDefined)
                {
                    SaveTextCodeIsEnabled = false;
                    CancelTextCodeIsEnabled = false;
                    RaisePropertyChanged("StringCodeToEdit");
                    return;
                }

                if (_stringCodeToEditModified == true)
                {
                    SaveTextCodeIsEnabled = true;
                    CancelTextCodeIsEnabled = true;
                }

                RaisePropertyChanged("StringCodeToEdit");
            }
        }

        public bool StringCodeToEditIsReadOnly
        {
            get { return _stringCodeToEditIsReadOnly; }

            set
            {
                // save the previous value
                //if (_stringCodeToEditIsReadOnly == true & value == false)
                //    _prevStringCodeToEdit = _stringCodeToEdit;

                _stringCodeToEditIsReadOnly = value;

                RaisePropertyChanged("StringCodeToEditIsReadOnly");
            }
        }

        /// <summary>
        /// Button command: Save the modified textCode, in the textBox.
        /// Only if the cultureCode is UserDefined.
        /// </summary>
        public ICommand SaveTextCodeCmd
        {
            get
            {
                if (_saveTextCodeCmd == null)
                {
                    _saveTextCodeCmd = new RelayCommand(param => DoSaveTextCode(),
                                               param => CanDoSaveTextCode());
                }
                return _saveTextCodeCmd;
            }
        }

        public bool SaveTextCodeIsEnabled
        {
            get
            {
                return _saveTextCodeIsEnabled;
            }

            set
            {
                _saveTextCodeIsEnabled = value;
                RaisePropertyChanged("SaveTextCodeIsEnabled");

            }
        }

        /// <summary>
        /// Button command: Save the modified textCode, in the textBox.
        /// Only if the cultureCode is UserDefined.
        /// </summary>
        public ICommand CancelTextCodeCmd
        {
            get
            {
                if (_cancelTextCodeCmd == null)
                {
                    _cancelTextCodeCmd = new RelayCommand(param => DoCancelTextCode(),
                                               param => CanDoCancelTextCode());
                }
                return _cancelTextCodeCmd;
            }
        }

        public bool CancelTextCodeIsEnabled
        {
            get
            {
                return _cancelTextCodeIsEnabled;
            }

            set
            {
                _cancelTextCodeIsEnabled = value;
                RaisePropertyChanged("CancelTextCodeIsEnabled");

            }
        }

        #endregion

        //=====================================================================		
        #region Public methods.

        //-------------------------------------------------------------------
        /// <summary>
        /// Called for any char input by the user in the textbox.
        /// </summary>
        public void StringCodeToEditKeyDown(string textBefore)
        {
            if (StringCodeToEditIsReadOnly)
                return;

            // enabled both btn Save and cancel
            _stringCodeToEditModified = true;
            SaveTextCodeIsEnabled = true;
            CancelTextCodeIsEnabled = true;
        }
        #endregion

        //=====================================================================		
        #region Private Command methods.

        //-------------------------------------------------------------------
        private bool CanDoRequestCloseWindow()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoRequestCloseWindow()
        {
            // ask confirmation to close to the user
            AppCtrlProvider.AppCtrl.RequestCloseApp();
        }


        //-------------------------------------------------------------------
        private bool CanDoShowDlgInformation()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgInformation()
        {
            AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Yes, you like dogs.");
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgQuestion()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgQuestion()
        {
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlg("The question of the day", "Do you like dogs?", CommonDlgIcon.Question, CommonDlgButtons.YesNo);

            if (res == CommonDlgResult.Yes)
                AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Yes, you do like dogs.");
            else
                AppCtrlProvider.AppCtrl.CommonDlg.ShowError("No, you don't like dogs.");
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgError()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgError()
        {
            AppCtrlProvider.AppCtrl.CommonDlg.ShowError("You're wrong.");
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgWarning()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgWarning()
        {
            AppCtrlProvider.AppCtrl.CommonDlg.ShowWarning("Be carefull!");
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlg_WXL_Information()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlg_WXL_Information()
        {
            string msg = "Yes, you like dogs, cats, birds, horses, worms, lions, ants, girafs, rabbits, ...";
            AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation(WHSize.WXL, msg);
        }


        //-------------------------------------------------------------------
        private bool CanDoShowDlg_WXL_HXL_Question()
        {
            return true;
        }

        /// <summary>
        /// A long question text.
        /// </summary>
        private void DoShowDlg_WXL_HXL_Question()
        {
            string text = "Le temps est maussade aujourd'hui, il pleut, il ne fait pas beau, mon ordinateur ne marche pas bien, que faire?";
            //CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlg(WHSize.WXL_HXL, "La question du jour", text, CommonDlgIcon.Question, CommonDlgButtons.YesNo);
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowQuestion(WHSize.WXL_HXL, text);

            // decode res: yes or no
        }
        
        //-------------------------------------------------------------------
        private bool CanDoShowDlg_WL_HXXL_Error()
        {
            return true;
        }

        /// <summary>
        /// A long question text.
        /// </summary>
        private void DoShowDlg_WL_HXXL_Error()
        {
            string text = "The application has failed down, please contact your system administrator as soon as possible.";
            AppCtrlProvider.AppCtrl.CommonDlg.ShowError(WHSize.WL_HXXL, text);
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgInputText()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgInputText()
        {
            string text;
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgInputText("Input", "Give a name:", "name", out text);
            if (res != CommonDlgResult.Ok)
            {
                AppCtrlProvider.AppCtrl.CommonDlg.ShowWarning("The user cancel the operation!");
                return;
            }
            AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Name is: " + text);

        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlg_WL_InputText()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlg_WL_InputText()
        {
            string text;
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgInputText(WHSize.WL, "Input", "Give a name:", "name", out text);
            if (res != CommonDlgResult.Ok)
            {
                AppCtrlProvider.AppCtrl.CommonDlg.ShowWarning(WHSize.WL,"The user cancelled the operation!");
                return;
            }
            AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Name is: " + text);

        }
        
        //-------------------------------------------------------------------
        private bool CanDoShowDlgSelectFile()
        {
            return true;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgSelectFile()
        {
            CommonDlgResult res;
            string pathName;
            string fileName;

            res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgSelectFile("C\\", "*.*", "All | *.*", out pathName, out fileName);

            if (res == CommonDlgResult.Ok)
                AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("The file " + fileName + " is selected.");
            else
                AppCtrlProvider.AppCtrl.CommonDlg.ShowWarning("No file selected.");
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgSaveFile()
        {
            return true;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgSaveFile()
        {
            CommonDlgResult res;

            string pathName;
            string fileName;

            res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgSaveFile("C\\", "", "All | *.*", out pathName, out fileName);

            if (res == CommonDlgResult.Ok)
                AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("The file " + fileName + " is saved.");
            else
                AppCtrlProvider.AppCtrl.CommonDlg.ShowWarning("No file selected.");
        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgComboChoice()
        {
            return true;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgComboChoice()
        {
            List<DlgComboChoiceItem> listItem = new List<DlgComboChoiceItem>();
            DlgComboChoiceItem selectedBeforeItem = null;

            // create some object
            List<string> listString = new List<string>();
            listString.Add("dog");
            listString.Add("cat");
            listString.Add("horse");
            listString.Add("duck");

            foreach (string s in listString)
            {
                var item = new DlgComboChoiceItem(s, s);
                listItem.Add(item);

                // select the first item
                //if (selectedBeforeItem == null)
                //    selectedBeforeItem = item;
            }

            DlgComboChoiceItem selected;
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgComboChoice("Choose it", listItem, selectedBeforeItem, out selected);

            if (res == CommonDlgResult.Ok)
                AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Selected item:\n " + selected.Object.ToString() + ".");
            else
                AppCtrlProvider.AppCtrl.CommonDlg.ShowWarning("Aborted!");

        }

        //-------------------------------------------------------------------
        private bool CanDoShowDlgListChoice()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoShowDlgListChoice()
        {
            List<DlgListChoiceItem> listItem = new List<DlgListChoiceItem>();
            List<DlgListChoiceItem> listSelectedBeforeItem = null;

            // create some object
            List<string> listString = new List<string>();
            listString.Add("Au revoir");
            listString.Add("salut");
            listString.Add("bye");
            listString.Add("tchao");

            foreach (string s in listString)
            {
                var item = new DlgListChoiceItem(s, s);
                listItem.Add(item);

                // select the first item
                //if (selectedBeforeItem == null)
                //    selectedBeforeItem = item;
            }

            List<DlgListChoiceItem> listSelected;
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgListChoice("Choose it", listItem, listSelectedBeforeItem, out listSelected);

            if (res == CommonDlgResult.Ok)
            {
                string sel = "";
                foreach (var s in listSelected) sel += s + ", ";
                AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Les items suivants ont été selectionnés: " + sel + ".");
            }
            else
                AppCtrlProvider.AppCtrl.CommonDlg.ShowInformation("Abandon.");

        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Button command: Save the modified textCode, in the textBox.
        /// Only if the cultureCode is UserDefined, and if the text is modified.
        /// </summary>
        /// <returns></returns>
        private bool CanDoSaveTextCode()
        {
            if(_stringCodeToEditModified)
                return true;
            return false;
        }

        /// <summary>
        /// Button command: Save the modified textCode, in the textBox.
        /// Only if the cultureCode is UserDefined.
        /// </summary>
        private void DoSaveTextCode()
        {
            // save the new text for the code
            AppCtrlProvider.AppCtrl.CommonDlg.SetUserDefinedTranslatedText(_stringCodeSelected.StringCode, _stringCodeToEdit);

            // disabled both btn Save and cancel
            _stringCodeToEditModified = false;
            SaveTextCodeIsEnabled = false;
            CancelTextCodeIsEnabled = false;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Button command: Cancel the modified textCode, in the textBox.
        /// Only if the cultureCode is UserDefined, and if the text is modified.
        /// </summary>
        /// <returns></returns>
        private bool CanDoCancelTextCode()
        {
            if (_stringCodeToEditModified)
                return true;
            return false;
        }

        /// <summary>
        /// Button command: Save the modified textCode, in the textBox.
        /// Only if the cultureCode is UserDefined.
        /// </summary>
        private void DoCancelTextCode()
        {
            // cancel the edition of the text
            StringCodeToEdit = _prevStringCodeToEdit;

            // refresh, raise... disable btns

            // disabled both btn Save and cancel
            _stringCodeToEditModified = false;
            SaveTextCodeIsEnabled = false;
            CancelTextCodeIsEnabled = false;
        }

        //-------------------------------------------------------------------
        private bool CanDoStringCodeToEditKeyDown()
        {
            return true;
        }

        #endregion

        //=====================================================================
        #region Private methods.

        //---------------------------------------------------------------------
        private void Init()
        {
            _appMainMenuVM = new AppMainMenuVM();

            CultureCodeSelected = new CultureCodeVM(CultureCode.en_GB);
            _listCultureCode.Add(CultureCodeSelected);

            _listCultureCode.Add(new CultureCodeVM(CultureCode.fr_FR));
            _listCultureCode.Add(new CultureCodeVM(CultureCode.es_ES));
            _listCultureCode.Add(new CultureCodeVM(CultureCode.UserDefined));

            // get list of all existing CultureCode
            IEnumerable<StringCode> listStringCode = AppCtrlProvider.AppCtrl.CommonDlg.GetListStringCode();

            // get the list of StringCode (with translated text) for the cultureCode
            foreach (StringCode stringCode in listStringCode)
            {
                StringCodeVM stringCodeVM = new StringCodeVM(stringCode);
                _listStringCode.Add(stringCodeVM);
            }

            // get the grid factory to build typed cell and cellVM /$TASK-001 
            
            // create the view: dynamic dataGrid wihth butons Add/Del cols and rows
            EditDynDataGridVM = new EditDynDataGridVM(AppCtrlProvider.AppCtrl.CommonDlg, AppCtrlProvider.AppCtrl.DynDataGridFactory, AppCtrlProvider.AppCtrl.DataGrid);

            DynDataGridVM= new DynDataGridVM(AppCtrlProvider.AppCtrl.DynDataGridFactory, AppCtrlProvider.AppCtrl.DataGrid);
        }

        /// <summary>
        /// A cultureCode is selected in the combo, load the list of the StringCode.
        /// </summary>
        /// <param name="cultureCode"></param>
        private void CultureCodeIsSelected(CultureCode cultureCode)
        {
            AppCtrlProvider.AppCtrl.CommonDlg.SetCurrentCultureInfo(cultureCode);

            // get the cultureCode page
            _translatedTextPage  = AppCtrlProvider.AppCtrl.CommonDlg.FindTranslatedTextByCultureCode(cultureCode);
        }
        #endregion
    }
}
