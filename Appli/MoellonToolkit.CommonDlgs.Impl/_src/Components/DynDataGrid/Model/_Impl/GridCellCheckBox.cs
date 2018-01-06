using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridCellCheckBox : GridCellBase, IGridCellCheckBox
    {
        /// <summary>
        /// Constructor.
        /// Provide the default (its a bool ).
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public GridCellCheckBox(IGridColumnCheckBox  column, IGridRow row, bool value, GridCellChangedProvider actionProvider) :base(column, row, actionProvider)
        {
            Content = value;
        }

        public override string ToString()
        {
            string s = ((bool)Content).ToString();
            if (s == null) s = "(null)";
            return "chkbox: " + s;
        }

    }
}
