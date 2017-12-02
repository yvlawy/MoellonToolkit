using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// The value of a cell: its a value (not a component).
    /// Map between a col and a row of a dataGrid.
    /// </summary>
    public interface IGridCellValueVM : IGridCellVM
    {
        /// <summary>
        /// the cell data: a value or a component.
        /// </summary>
        //object Value { get; set; }
        object Cell { get; set; }

        //object ColumnBinding { get; set; }

        //object RowBinding { get; set; }

        ///// <summary>
        ///// the data grid cell data value.
        ///// </summary>
        //object Value { get; set; }

        //bool IsReadOnly { get;  }
        //void Refresh();
    }
}
