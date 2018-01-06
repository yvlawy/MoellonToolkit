using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Provider of the action: GridCellChanged.
    /// The action can be set of the last time.
    /// </summary>
    public class GridCellChangedProvider
    {
        /// <summary>
        /// Action, callback, called when a dataGrid cell is modified.
        /// </summary>
        public Action<IGridCell> GridCellChanged { get; set; }
    }
}
