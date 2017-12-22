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
        ActionGridCellValueModifiedProvider GridCellValueModifiedProvider { get; }
        //Action<IGridCell> GridValueModified { get; }

        // columns
        IEnumerable<IGridColumn> ListColumn { get; }

        IEnumerable<IGridRow> ListRow { get; }


        bool AddColumn(IGridColumn column);

        bool AddRow(IGridRow row);

        // remove the row from the datagrid
        bool RemoveRow(IGridRow row);

        void RemoveAllRow();

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
