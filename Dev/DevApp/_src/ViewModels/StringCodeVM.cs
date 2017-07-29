using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.ViewModels
{
    /// <summary>
    /// A stringCode, displayed in a combobox.
    /// </summary>
    public class StringCodeVM
    {
        public StringCodeVM(StringCode stringCode)
        {
            StringCode = stringCode;
        }

        public StringCode StringCode
        { get; private set; }
    }
}
