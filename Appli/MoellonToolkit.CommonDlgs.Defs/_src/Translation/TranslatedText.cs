using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    ///  a text translated for a stringCode.
    /// </summary>
    public class TranslatedText
    {
        public TranslatedText(StringCode stringCode, string text)
        {
            StringCode = stringCode;
            Text = text;
        }

        public StringCode StringCode
        { get; private set; }

        public string Text
        { get; private set; }

        public void ReplaceText(string text)
        {
            Text = text;
        }
    }
}
