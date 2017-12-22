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
        public GridCellCheckBox(IGridColumnCheckBox  column, bool value, ActionGridCellValueModifiedProvider actionProvider) :base(column, actionProvider)
        {
            // TODO: c'est ca? 
            Cell = value;
        }

    }
}
