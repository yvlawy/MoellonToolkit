using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    /// A page of translated text, for cultureCode
    /// </summary>
    public class TranslatedTextPage
    {
        // dict of StringCode-text
        Dictionary<StringCode, TranslatedText> _dictTranslatedText = new Dictionary<StringCode, TranslatedText>();

        public TranslatedTextPage(CultureCode cultureCode)
        {
            CultureCode = cultureCode;
        }

        public CultureCode CultureCode
        { get; private set; }

        public IEnumerable<TranslatedText> ListTranslatedText
        {
            get
            {
                List<TranslatedText> list = new List<TranslatedText>();
                foreach (var item in _dictTranslatedText.Values)
                {
                    list.Add(item);
                }

                return list;
            }
        }

        // now get the translated text matching the code
        public TranslatedText FindTranslatedTextByStringCode(StringCode code)
        {
            // if exists, bye
            if (!_dictTranslatedText.ContainsKey(code))
                return null;

            return _dictTranslatedText[code];
        }


        public bool Add(StringCode stringCode, string text)
        {
            // if exists, bye
            if (_dictTranslatedText.ContainsKey(stringCode))
                return false;

            TranslatedText translatedText = new TranslatedText(stringCode, text);
            _dictTranslatedText.Add(stringCode, translatedText);
            return true;
        }

        public bool AddOrReplace(StringCode stringCode, string text)
        {
            TranslatedText translatedText = null;

            // if not exists, create it
            if (_dictTranslatedText.ContainsKey(stringCode))
                translatedText = _dictTranslatedText[stringCode];
            else
            {
                translatedText = new TranslatedText(stringCode, text);
                return true;
            }

            // replace just the text
            translatedText.ReplaceText(text);
            return true;

        }
    }
}
