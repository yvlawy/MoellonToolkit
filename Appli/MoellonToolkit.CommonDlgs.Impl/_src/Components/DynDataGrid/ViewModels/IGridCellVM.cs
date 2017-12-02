using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Base of data grid cell VM: values and components (button, combobox).
    /// </summary>
    public interface IGridCellVM
    {
        object ColumnBinding { get; set; }

        object RowBinding { get; set; }

        /// <summary>
        /// the cell data: a value or a component.
        /// </summary>
        //object Value { get; set; }
        //object Cell { get; set; }

        bool IsReadOnly { get; }
        void Refresh();
    }
}
