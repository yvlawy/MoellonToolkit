using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridCellString : GridCellBase, IGridCellValue
    {
        public GridCellString(IGridColumnString column, IGridRow row, string value, GridCellChangedProvider actionProvider) :base(column, row, actionProvider)
        {
            Content = value;
        }


        //public string Value
        //{ get; set; }

        public override string ToString()
        {
            string s = Content as string;
            if (s == null) s = "(null)";
            return "string: " + s;
        }
    }
}
