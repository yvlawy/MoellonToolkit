using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridCellString : GridCellBase, IGridCellValue
    {
        public GridCellString(IGridColumnString column, string value, ActionGridCellValueModifiedProvider actionProvider) :base(column, actionProvider)
        {
            Cell = value;
        }


        //public string Value
        //{ get; set; }
    }
}
