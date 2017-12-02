using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridCellCheckBox : GridCellBase, IGridCellCheckBox
    {
        /// <summary>
        /// Constrcutor.
        /// Provide the default (its a bool ).
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public GridCellCheckBox(IGridColumnCheckBox  column, bool value):base(column)
        {
            // TODO: c'est ca? 
            Cell = value;
        }

    }
}
