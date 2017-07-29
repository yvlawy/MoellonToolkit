using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    /// Define Common dlgbox: Show Error/Warning/Question/..., OpenFile, SaveFile,...
    /// </summary>
    public interface ICommonDlg
    {
        void SetCurrentCultureInfo(CultureCode cultureCode);

        // Set default title of dlg.
        // SetTitle(CommonDlgIcons dlgType, string title)

        // SetTitle(Cultureinfo cultInf, CommonDlgIcon dlgType, string title)

        // define the text of the button code
        // SetBtnText(CommonDlgButtons btn, string text)
        // SetBtnText(Cultureinfo cultInf, CommonDlgButtons btn, string text)

        // multilingue!, comment?
        // define the default cultureInfo/translation code, associated to default/defined titles and texts
        // SetDefaultCultureInfo(CultureInfo cultInf)

        // definir/ajouter un cultureInfo
        // TODO:  

        // Activer un cultureInfo
        // TODO:

        // change default icons
        // TODO:

        /// <summary>
        /// Find a translatedText by the culture code.
        /// exp: return the code page for en_GB (all the translated text for each string code).
        /// </summary>
        /// <param name="cultureCode"></param>
        /// <returns></returns>
        TranslatedTextPage FindTranslatedTextByCultureCode(CultureCode cultureCode);

        /// <summary>
        /// return the list of all existing StringCode (without any translation).
        /// </summary>
        /// <returns></returns>
        IEnumerable<StringCode> GetListStringCode();

        /// <summary>
        /// Set for the UserDefined cultureCode a new text matching a stringCode.
        /// </summary>
        /// <param name="stringCode"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        bool SetUserDefinedTranslatedText(StringCode stringCode, string text);

        /// <summary>
        /// Show a dlg Box containing a message, an icon and some buttons.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        CommonDlgResult ShowDlg(WHSize whSize, string dlgTitle, string message, CommonDlgIcon icon, CommonDlgButtons buttons);
        CommonDlgResult ShowDlg(string dlgTitle, string message, CommonDlgIcon icon, CommonDlgButtons buttons);

        /// <summary>	
        /// Shows an error message with an Ok button.
        /// </summary>
        /// <param name="message">The error message</param>
        CommonDlgResult ShowError(WHSize whSize, string title, string message);
        CommonDlgResult ShowError(WHSize whSize, string message);
        void ShowError(string title, string message);
        void ShowError(string message);

        /// <summary>
        /// Shows a warning message with an Ok button.
        /// </summary>
        /// <param name="message">The warning message</param>
        void ShowWarning(WHSize whSize, string title, string message);
        void ShowWarning(WHSize whSize, string message);
        void ShowWarning(string title, string message);
        void ShowWarning(string message);

        /// <summary>
        /// Shows an information message with an Ok button.
        /// </summary>
        /// <param name="message">The information message</param>
        void ShowInformation(WHSize whSize, string title, string message);
        void ShowInformation(WHSize whSize, string message);
        void ShowInformation(string title, string message);
        void ShowInformation(string message);


        /// <summary>
        /// Shows a question message with Yes/No buttons.
        /// </summary>
        /// <param name="message">The information message</param>
        CommonDlgResult ShowQuestion(WHSize whSize, string title, string message);
        CommonDlgResult ShowQuestion(WHSize whSize, string message);
        CommonDlgResult ShowQuestion(string title, string message);
        CommonDlgResult ShowQuestion(string message);

        // ShowQuestion with Yes/No/Cancel buttons
        // TODO:

        /// <summary>
        /// Shows a dialog box to input a text, with Ok/Cancel buttons.
        /// 
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="message"></param>
        /// <param name="initialtext"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        CommonDlgResult ShowDlgInputText(WHSize whSize, string dlgTitle, string message, string initialtext, out string text);
        CommonDlgResult ShowDlgInputText(string dlgTitle, string message, string initialtext, out string text);


        CommonDlgResult ShowDlgSelectFile(string defaultPath, string defaultExt, string filter, out string pathName, out string fileName);
        CommonDlgResult ShowDlgSelectFolder(string defaultPath, out string pathName, out string fileName);

        CommonDlgResult ShowDlgSaveFile(string defaultPath, string defaultExt, string filter, out string pathName, out string fileName);

        /// <summary>
        /// Show a dialog box with a combobox to selection/choice an item.
        /// only one item can be selected or any.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="listObject"></param>
        /// <param name="selectIt"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        CommonDlgResult ShowDlgComboChoice(string dlgTitle, List<DlgComboChoiceItem> listObject, DlgComboChoiceItem selectIt, out DlgComboChoiceItem selected);

        /// <summary>
        /// Show a dialog box with a listbox to selection/choice one or many items.
        /// </summary>
        /// <param name="dlgTitle"></param>
        /// <param name="listObject"></param>
        /// <param name="listSelectIt"></param>
        /// <param name="listSelected"></param>
        /// <returns></returns>
        CommonDlgResult ShowDlgListChoice(string dlgTitle, List<DlgListChoiceItem> listObject, List<DlgListChoiceItem> listSelectIt, out List<DlgListChoiceItem> listSelected);

    }
}
