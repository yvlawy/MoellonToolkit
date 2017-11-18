using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Base of a row displayed in the grid.
    /// </summary>
    public abstract class GridRowBaseVM : ViewModelBase, IGridRowVM
    {

        public GridRowBaseVM()
        { }

        /// <summary>
        /// The model.
        /// </summary>
        public IGridRow GridRow { get; protected set; }
    }
}
