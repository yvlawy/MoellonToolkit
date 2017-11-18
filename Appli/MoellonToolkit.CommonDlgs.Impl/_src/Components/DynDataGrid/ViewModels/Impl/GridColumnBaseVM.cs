using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Base of a column displayed in the grid.
    /// </summary>
    public class GridColumnBaseVM : ViewModelBase, IGridColumnVM
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GridColumnBaseVM()
        {
            DisplayIndex = -1;
        }

        public IGridColumn GridColumn { get; protected set; }

        public int DisplayIndex { get; set; }
    }
}
