using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// The data row ViewModel. Bind a GridRow model.
    /// </summary>
    public interface IGridRowVM
    {
        /// <summary>
        /// The model.
        /// </summary>
        IGridRow GridRow { get; }

        // list of cellVM
    }
}
