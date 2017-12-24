using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// The dataGrid model, to use with the dynamic data Grid UI component.
    /// </summary>
    public class DynDataGrid : IDynDataGrid
    {
        /// <summary>
        /// Action, callback, called when a fdataGrid cell is modified.
        /// </summary>
        //Action<IGridCell> _actionGridValueModifiedInUI;

        List<IGridColumn> _listColumn;

        List<IGridRow> _listRow;

        public DynDataGrid()
        {
            GridCellValueModifiedProvider = new ActionGridCellValueModifiedProvider();
            _listColumn = new List<IGridColumn>();
            _listRow = new List<IGridRow>();
        }

        /// <summary>
        /// provider of the action called when a cell is modified.
        /// </summary>
        public ActionGridCellValueModifiedProvider GridCellValueModifiedProvider { get;private set;}

        public IEnumerable<IGridColumn> ListColumn
        { get { return _listColumn; } }

        public IEnumerable<IGridRow> ListRow
        { get { return _listRow; } }


        /// <summary>
        /// Find a column by the name.
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public IGridColumn FindColumnByName(string colName)
        {
            return _listColumn.Where(c => c.Name.Equals(colName)).FirstOrDefault();
        }

        /// <summary>
        /// Add a new column.
        /// Check that the name is not already used by another column.
        /// 
        /// TODO: if row are already presents?  (and so without a cell for the new column)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool AddColumn(IGridColumn column)
        {
            // check if the name of column is ok and not already used
            if (string.IsNullOrWhiteSpace(column.Name))
                return false;

            if (_listColumn.Where(c => c.Name.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
                return false;

            _listColumn.Add(column);
            return true;
        }

        public bool AddRow(IGridRow row)
        {
            // check if the row columns match the datagrid columns
            // TODO:

            _listRow.Add(row);
            return true;
        }


        // remove the row from the datagrid
        public bool RemoveRow(IGridRow row)
        {
            _listRow.Remove(row);
            return true;
        }

        public void RemoveAllRow()
        {
            _listRow.Clear();
        }

        public bool RemoveColumn(IGridColumn column)
        {
            if (column == null)
                return false;

            _listColumn.Remove(column);
            return true;
        }


        /// <summary>
        /// Set a string value to a cell in a row.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public bool SetCellValue(IGridRow row, string colName, string cellValue)
        {
            // find the column
            IGridColumn colum = _listColumn.Where(c => c.Name.Equals(colName)).FirstOrDefault();
            if (colum == null)
                return false;

            return SetCellValue(row, colum, cellValue);
        }

        /// <summary>
        /// Set a string value to a cell in a row.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public bool SetCellValue(IGridRow row, IGridColumn column, string cellValue)
        {
            if (row == null || column == null || cellValue == null)
                return false;

            // find the cell in the row, matching the column
            IGridCell cell = row.ListCell.Where(ce => ce.Column == column).FirstOrDefault();
            if (cell == null)
                return false;

            // the cell must be string
            GridCellString cellValueString = cell as GridCellString;
            if (cellValueString == null)
                return false;

            cellValueString.Cell = cellValue;
            return true;
        }

        /// <summary>
        /// Set a bool value to a cell in a row.
        /// The cell can be a bool, a checkButton or a radioButton.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public bool SetCellValue(IGridRow row, string colName, bool cellValue)
        {
            // find the column
            IGridColumn colum = _listColumn.Where(c => c.Name.Equals(colName)).FirstOrDefault();
            if (colum == null)
                return false;

            return SetCellValue(row, colum, cellValue);
        }

        /// <summary>
        /// Set a bool value to a cell in a row.
        /// The cell can be a bool, a checkButton or a radioButton.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public bool SetCellValue(IGridRow row, IGridColumn column, bool cellValue)
        {
            if (row == null || column == null)
                return false;

            // find the cell in the row, matching the column
            IGridCell cell = row.ListCell.Where(ce => ce.Column == column).FirstOrDefault();
            if (cell == null)
                return false;

            // the cell must be a bool: can be checkbox, a radioButton
            GridCellCheckBox cellValueBool = cell as GridCellCheckBox;
            if (cellValueBool != null)
            {
                cellValueBool.Cell = cellValue;
                return true;
            }

            // TODO: others types: radioButton
            return false;
        
        }

    }
}
