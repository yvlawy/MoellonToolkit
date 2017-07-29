using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    /// List of translation for a code, in managed culture languages.
   
    /// OBSOLETE
    /// </summary>
    public class CultureTranslations
    {
        // List of Map: culture - translation
        Dictionary<CultureCode, string> _dictCultureTrans = new Dictionary<CultureCode, string>();

        //public TransCode Code { get; set; }

        // find the culture, or create it
        public bool FindCultureTrans(CultureCode cultureCode, out string text)
        {
            text = "";
            //KeyValuePair<CultureCode, string> pair;

            // if not exists, create it
            if (!_dictCultureTrans.ContainsKey(cultureCode))
                return false;

            text = _dictCultureTrans.Where(p => p.Key == cultureCode).FirstOrDefault().Value;
            return true;
        }

        // find the culture, or create it
        public bool CreateCultureTrans(CultureCode cultureCode, string translation)
        {
            if (string.IsNullOrWhiteSpace(translation))
                return false;

            //KeyValuePair<CultureCode, string> pair;

            // if not exists, create it
            if (_dictCultureTrans.ContainsKey(cultureCode))
            {
                _dictCultureTrans.Remove(cultureCode);
            }

            // create it
            _dictCultureTrans.Add(cultureCode, translation);

            return true;
        }

        // add, find,..
        // TODO:
    }
}
