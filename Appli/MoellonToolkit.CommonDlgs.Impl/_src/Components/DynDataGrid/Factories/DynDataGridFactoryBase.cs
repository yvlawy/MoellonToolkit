using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Factory to build item for dynamic dataGrid, model and ViewModel.
    /// A single factory can be used for many dataGrid components.
    /// </summary>
    public abstract class DynDataGridFactoryBase : IDynDataGridFactory
    {
        /// <summary>
        /// create the col in the data model, depending on the type.
        /// Provide an object to attach to the column.
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <returns></returns>
        public DynDataGridErrCode CreateColumn(IDynDataGrid dataGrid, GridColumnType typeCol, string newColName, out IGridColumn column)
        {
            column = null;

            // check the name
            DynDataGridErrCode errCode = CheckColumnName(dataGrid, newColName);
            if (errCode != DynDataGridErrCode.Ok)
                return errCode;

             CreateColumn(dataGrid, typeCol, newColName, null, out column);
            dataGrid.AddColumn(column);

            return DynDataGridErrCode.Ok;
        }

        /// <summary>
        /// create the col in the data model, depending on the type.
        /// Provide an object to attach to the column.
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <returns></returns>
        public DynDataGridErrCode CreateColumn(IDynDataGrid dataGrid, GridColumnType typeCol, string newColName, object colObj, out IGridColumn column)
        {
            column = null;

            // check the name
            DynDataGridErrCode errCode = CheckColumnName(dataGrid, newColName);
            if (errCode != DynDataGridErrCode.Ok)
                return errCode;

            column = CreateColumn(typeCol, newColName, colObj);
            dataGrid.AddColumn(column);

            return DynDataGridErrCode.Ok;
        }

        /// <summary>
        /// create the col in the data model, depending on the type.
        /// Provide an object to attach to the column.
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <returns></returns>
        protected IGridColumn CreateColumn(GridColumnType typeCol, string newColName, object colObj)
        {
            IGridColumn column;

            if (typeCol== GridColumnType.String)
            {
                column = new GridColumnString(newColName, colObj);
                return column;
            }

            if (typeCol == GridColumnType.CheckBox)
            {
                column = new GridColumnCheckBox(newColName, colObj);
                return column;
            }

            return null;
        }

        public IGridRow CreateRowWithCells(IDynDataGrid dataGrid)
        {
            return CreateRowWithCells(dataGrid, null);
        }

        /// <summary>
        /// Create a row in the data grid, create empty cells matching the columns.
        /// </summary>
        /// <param name="DataGrid"></param>
        /// <returns></returns>
        public IGridRow CreateRowWithCells(IDynDataGrid dataGrid, object obj)
        {
            IGridRow row = new GridRow(dataGrid, obj);

            // create a cell for each column
            foreach (IGridColumn column in dataGrid.ListColumn)
            {
                IGridCell cell = CreateCell(dataGrid, column, row);
                row.AddCell(cell);
            }

            dataGrid.AddRow(row);
            return row;
        }

        /// <summary>
        /// Create a cell (for a row), depending on the column type.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public IGridCell CreateCell(IDynDataGrid dataGrid, IGridColumn column, IGridRow row)
        {
            // create a cell matching the type of the column
            IGridColumnString colString = column as IGridColumnString;
            if (colString != null)
            {
                // set a default value
                return new GridCellString(colString, row, "", dataGrid.GridCellChangedProvider);
            }

            //IGridColumnInt colInt = column as GridColumnInt;
            //if (colInt != null)
            //{
            //    // set a default value
            //    return new GridCellInt(colInt, 0);
            //}

            IGridColumnCheckBox colCheckBox = column as IGridColumnCheckBox;
            if(colCheckBox!=null)
            {
                return new GridCellCheckBox(colCheckBox, row, false, dataGrid.GridCellChangedProvider);
            }


            throw new Exception("Factory.CreateCell(): type not yet implemented.");
        }

        /// <summary>
        /// Create a cellVM, can be a cell value or a cell component (button, comobox,...).
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public IGridCellVM CreateCellVM(IGridCell cell)
        {
            // is the cell a value? 
            IGridCellValue gridCellValue = cell as IGridCellValue;
            if(gridCellValue!=null)
            {
                IGridCellVM cellVM = new GridCellValueVM(gridCellValue);
                return cellVM;
            }

            // is the cell a checkbox?
            // $TASK-003
            IGridCellCheckBox gridCellCheckBox = cell as IGridCellCheckBox;
            if(gridCellCheckBox != null)
            {

                IGridCellVM cellVM = new GridCellCheckBoxVM(gridCellCheckBox);
                return cellVM;
            }

            //IGridCellComponent gridCellComponent = cell as IGridCellComponent;
            //if (gridCellComponent != null)
            //{
            //    IGridCellVM cellVM = new GridCellValueVM(cell);
            //    return cellVM;
            //}

            // error or not managed
            return null;
        }

        /// <summary>
        /// Check the name of the column, should be: not null, not empty, not already used by another column.
        /// </summary>
        /// <param name="newColName"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public DynDataGridErrCode CheckColumnName(IDynDataGrid dataGrid, string newColName)
        {
            newColName = newColName.Trim();
            if (string.IsNullOrWhiteSpace(newColName))
                return DynDataGridErrCode.ColumnNameWrong;

            if (dataGrid.ListColumn.Where(c => c.Name.Equals(newColName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
                return DynDataGridErrCode.ColumnNameAlreadyUsed;

            return DynDataGridErrCode.Ok;
        }

    }
}
