using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    /// Manage translations for string used in buttons and others UI components.
    /// </summary>
    public class TranslationMgr
    {
        /// <summary>
        /// list of pages of translated text for cultureCode
        /// </summary>
        Dictionary<CultureCode, TranslatedTextPage> _dictTextTranslatedPage = new Dictionary<CultureCode, TranslatedTextPage>();

        // List of translations
        //Dictionary<StringCode, CultureTranslations> _dictTrans = new Dictionary<StringCode, CultureTranslations>();

        /// <summary>
        /// The default culture info.
        /// </summary>
        CultureCode _cultureCode = CultureCode.en_GB;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TranslationMgr()
        {
            // create all translation
            CreateTranslations();
        }

        /// <summary>
        /// Define the default culture code.
        /// Used to translate text: Ok, Yes, No, Cancel,...
        /// </summary>
        /// <param name="cultureCode"></param>
        public void SetCurrentCultureInfo(CultureCode cultureCode)
        {
            _cultureCode = cultureCode;
        }

        public TranslatedTextPage FindTranslatedTextByCultureCode(CultureCode cultureCode)
        {
            // get the page of the default cultureCode
            if (!_dictTextTranslatedPage.ContainsKey(cultureCode))
                // the culture code does not exists!
                return null;

            return _dictTextTranslatedPage[cultureCode];
        }

        /// <summary>
        /// get the text translation of the string code in the current culture.
        /// If the code is not translated, return an empty string.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetTranslation(StringCode code)
        {
            TranslatedTextPage page = null;

            // get the page of the default cultureCode
            if (!_dictTextTranslatedPage.ContainsKey(_cultureCode))
                // the culture code does not exists!
                return "(null)";

            page= _dictTextTranslatedPage[_cultureCode];

            // now get the translated text matching the code
            TranslatedText translatedText = page.FindTranslatedTextByStringCode(code);
            if (translatedText == null)
                return "(null)";

            return translatedText.Text;
        }

        /// <summary>
        /// Set for the UserDefined cultureCode a new text matching a stringCode.
        /// </summary>
        /// <param name="stringCode"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool SetUserDefinedTranslatedText(StringCode stringCode, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            TranslatedTextPage page = null;

            // get the page of the default cultureCode
            if (!_dictTextTranslatedPage.ContainsKey(CultureCode.UserDefined))
                // the culture code does not exists!
                return false;

            page = _dictTextTranslatedPage[CultureCode.UserDefined];

            // now get the translated text matching the code
            TranslatedText translatedText = page.FindTranslatedTextByStringCode(stringCode);

            // the translated not yet exists, create it
            if (translatedText == null)
            {
                page.Add(stringCode, text);
                return true;
            }

            // exists, set the new text
            translatedText.ReplaceText(text);
            return true;
        }

        /// <summary>
        /// create all translations.
        /// </summary>
        private void CreateTranslations()
        {
            CreateTranslation_UserDefined();

            CreateTranslation_en_GB();
            CreateTranslation_fr_FR();
            CreateTranslation_es_ES();
        }

        private void CreateTranslation_UserDefined()
        {
            // create the page for the cultureInfo
            TranslatedTextPage textTranslatedPage = new TranslatedTextPage(CultureCode.UserDefined);
            _dictTextTranslatedPage.Add(CultureCode.UserDefined, textTranslatedPage);
        }

        /// <summary>
        /// create all translations.
        /// </summary>
        private void CreateTranslation_en_GB()
        {
            // create the page for the cultureInfo
            TranslatedTextPage textTranslatedPage = new TranslatedTextPage(CultureCode.en_GB);
            _dictTextTranslatedPage.Add(CultureCode.en_GB, textTranslatedPage);

            textTranslatedPage.Add(StringCode.Ok, "Ok");
            textTranslatedPage.Add(StringCode.Cancel, "Cancel");

            textTranslatedPage.Add(StringCode.No, "No");
            textTranslatedPage.Add(StringCode.Yes, "Yes");
            textTranslatedPage.Add(StringCode.Close, "Close");

            textTranslatedPage.Add(StringCode.DlgInformationTitle, "Information");
            textTranslatedPage.Add(StringCode.DlgWarningTitle, "Warning");
            textTranslatedPage.Add(StringCode.DlgErrorTitle, "Error");
            textTranslatedPage.Add(StringCode.DlgQuestionTitle, "Question");
        }

        /// <summary>
        /// create all translations.
        /// </summary>
        private void CreateTranslation_fr_FR()
        {
            // create the page for the cultureInfo
            TranslatedTextPage textTranslatedPage = new TranslatedTextPage(CultureCode.fr_FR);
            _dictTextTranslatedPage.Add(CultureCode.fr_FR, textTranslatedPage);

            textTranslatedPage.Add(StringCode.Ok, "Ok");
            textTranslatedPage.Add(StringCode.Cancel, "Annuler");

            textTranslatedPage.Add(StringCode.No, "Non");
            textTranslatedPage.Add(StringCode.Yes, "Oui");
            textTranslatedPage.Add(StringCode.Close, "Fermer");

            textTranslatedPage.Add(StringCode.DlgInformationTitle, "Information");
            textTranslatedPage.Add(StringCode.DlgWarningTitle, "Alerte");
            textTranslatedPage.Add(StringCode.DlgErrorTitle, "Erreur");
            textTranslatedPage.Add(StringCode.DlgQuestionTitle, "Question");
        }

        /// <summary>
        /// create all translations.
        /// </summary>
        private void CreateTranslation_es_ES()
        {
            // create the page for the cultureInfo
            TranslatedTextPage textTranslatedPage = new TranslatedTextPage(CultureCode.es_ES);
            _dictTextTranslatedPage.Add(CultureCode.es_ES, textTranslatedPage);

            textTranslatedPage.Add(StringCode.Ok, "Ok");
            textTranslatedPage.Add(StringCode.Cancel, "Cancelar");

            textTranslatedPage.Add(StringCode.No, "No");
            textTranslatedPage.Add(StringCode.Yes, "Si");
            textTranslatedPage.Add(StringCode.Close, "Cerrar");

            textTranslatedPage.Add(StringCode.DlgInformationTitle, "Informacion");
            textTranslatedPage.Add(StringCode.DlgWarningTitle, "Atencion");
            textTranslatedPage.Add(StringCode.DlgErrorTitle, "Error");
            textTranslatedPage.Add(StringCode.DlgQuestionTitle, "cuestion");
        }

    }
}