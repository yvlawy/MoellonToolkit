using MoellonToolkit.MVVMBase;
using MoellonToolkit.CommonDlgs.Defs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.ViewModels
{
    public class CultureCodeVM : ViewModelBase
    {
        public CultureCodeVM(CultureCode cultureCode)
        {
            CultureCode = cultureCode;
        }

        public CultureCode CultureCode { get;}

        public string Name
        {
            get { return CultureCode.ToString(); }
        }
    }
}
