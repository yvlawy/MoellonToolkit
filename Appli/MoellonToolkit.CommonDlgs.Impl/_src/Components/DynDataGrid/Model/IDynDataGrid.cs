using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A data grid
    /// </summary>
    public interface IDynDataGrid
    {
        /// <summary>
        /// provider of the action called when a cell is modified.
        /// </summary>
        GridCellChangedProvider GridCellChangedProvider { get; }

        /// <summary>
        /// List of columns of the dataGrid.
        /// </summary>
        IEnumerable<IGridColumn> ListColumn { get; }

        /// <summary>
        /// List of rows of the dataGrid.
        /// </summary>
        IEnumerable<IGridRow> ListRow { get; }

        /// <summary>
        /// Find a column by the name.
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        IGridColumn FindColumnByName(string colName);

        /// <summary>
        /// Add a new column in the dataGrid.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        bool AddColumn(IGridColumn column);

        /// <summary>
        /// Add a new row in the dataGrid.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        bool AddRow(IGridRow row);

        /// <summary>
        /// remove the row from the datagrid.
        /// remove cells of the row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        bool RemoveRow(IGridRow row);

        /// <summary>
        /// Remove all row the dataGrid.
        /// </summary>
        void RemoveAllRow();

        /// <summary>
        /// Remove a column from the dataGrid.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        bool RemoveColumn(IGridColumn column);

        /// <summary>
        /// Set a string value to a cell in a row.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        bool SetCellValue(IGridRow row , string colName, string cellValue);

        bool SetCellValue(IGridRow row, IGridColumn colum, string cellValue);

        bool SetCellValue(IGridRow row, string colName, bool cellValue);

        bool SetCellValue(IGridRow row, IGridColumn colum, bool cellValue);
    }
}
