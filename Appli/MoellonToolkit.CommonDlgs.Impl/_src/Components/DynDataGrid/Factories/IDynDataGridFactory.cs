using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// To build typed cell and CellVM for the dynamic datagrid.
    /// </summary>
    public interface IDynDataGridFactory
    {
        /// <summary>
        /// create the col in the data model, depending on the type.
        /// Provide an object to attach to the column.
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <returns></returns>
        DynDataGridErrCode CreateColumn(IDynDataGrid dataGrid, GridColumnType typeCol, string newColName, out IGridColumn column);

        /// <summary>
        /// create the col in the data model, depending on the type.
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <returns></returns>
        DynDataGridErrCode CreateColumn(IDynDataGrid dataGrid, GridColumnType typeCol, string newColName, object colObj, out IGridColumn column);

        /// <summary>
        /// Create a new row in the dataGrid, with empty cells, one for each column.
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        IGridRow CreateRowWithCells(IDynDataGrid dataGrid);

        /// <summary>
        /// Create a row in the data grid, create all cells of the row, attach an object to the row.
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        IGridRow CreateRowWithCells(IDynDataGrid dataGrid, object objRowAttached);

        /// <summary>
        /// Create a cell (for a row), depending on the column type.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        IGridCell CreateCell(IDynDataGrid dataGrid, IGridColumn column, IGridRow row);

        /// <summary>
        /// Create a cell View model for the datagrid cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        IGridCellVM CreateCellVM(IGridCell cell);

        /// <summary>
        /// Check the name of the column, should be: not null, not empty, not already used by another column.
        /// </summary>
        /// <param name="newColName"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        DynDataGridErrCode CheckColumnName(IDynDataGrid dataGrid, string newColName);

    }
}
