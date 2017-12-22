using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridColumnString : GridColumnBase, IGridColumnString
    {
        public GridColumnString(string name, object colObj): base(name, colObj)
        {
        }

    }
}
