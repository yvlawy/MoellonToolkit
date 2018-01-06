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
        /// To link an object to the row, optionnal.
        /// (your own row data model for example or a rowId)
        /// </summary>
        object Object { get; set; }

        /// <summary>
        /// List of cells of the row.
        /// </summary>
        IEnumerable<IGridCell> ListCell { get; }

        /// <summary>
        /// find the cell in the row, by the column.
        /// </summary>
        /// <param name="dataGridColumn"></param>
        /// <returns></returns>
        IGridCell FindCellByColumn(IGridColumn dataGridColumn);

        /// <summary>
        /// Add a new cell in the row, use the factory to do that!
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        bool AddCell(IGridCell cell);


        /// <summary>
        /// remove a cell from the row, use the factory to do that!
        /// </summary>
        /// <param name="cell"></param>
        bool RemoveCell(IGridCell cell);

    }
}
