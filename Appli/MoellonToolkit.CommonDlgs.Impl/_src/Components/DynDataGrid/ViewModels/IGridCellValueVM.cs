using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// the cell value of a cell: map between a col and a row of a dataGrid.
    /// TODO: hériter de IGridCellVM.
    /// </summary>
    public interface IGridCellValueVM : IGridCellVM
    {
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
