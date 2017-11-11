using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridCellString : GridCellBase
    {
        public GridCellString(IGridColumnString column, string value):base(column)
        {
            Cell = value;
        }

        //public string Value
        //{ get; set; }
    }
}
