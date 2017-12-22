using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class ActionGridCellValueModifiedProvider
    {
        /// <summary>
        /// Action, callback, called when a fdataGrid cell is modified.
        /// </summary>
        public Action<IGridCell> ActionGridValueModifiedInUI { get; set; }
    }
}
