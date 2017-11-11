using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public interface IGridRow
    {
        /// <summary>
        /// The owner data grid.
        /// </summary>
        IDynDataGrid Datagrid { get; }

        /// <summary>
        /// find the cell in the row, by the column.
        /// </summary>
        /// <param name="dataGridColumn"></param>
        /// <returns></returns>
        IGridCell FindCellByColumn(IGridColumn dataGridColumn);

        bool AddCell(IGridCell cell);
    }
}
