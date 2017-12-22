using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// The dynamic dataGrid.
    /// no UI interation.
    /// Manage the data model.
    /// </summary>
    public class DynDataGridVM : ViewModelBase
    {
        /// <summary>
        /// To build cell and cellVM, depending on type (of col or cell).
        /// </summary>
        IDynDataGridFactory _gridFactory;

        /// <summary>
        /// The datagrid model.
        /// Contains the data to display.
        /// </summary>
        IDynDataGrid _dynDataGrid;

        /// <summary>
        /// Displayed Columns definition.
        /// </summary>
        ObservableCollection<IGridColumnVM> _collColumnGrid = new ObservableCollection<IGridColumnVM>();

        /// <summary>
        /// Rows data.
        /// </summary>
        ObservableCollection<IGridRowVM> _collDataRow = new ObservableCollection<IGridRowVM>();

        /// <summary>
        /// The cells of the grid: mapping betweeen cols and rows.
        /// </summary>
        GridMappingCell _collCell;

        /// <summary>
        /// Selected row in the grid.
        /// </summary>
        IGridRowVM _selectedRow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dynDatagrid"></param>
        public DynDataGridVM(IDynDataGridFactory gridFactory, IDynDataGrid dynDatagrid)
        {
            _gridFactory = gridFactory;

            _collCell = new GridMappingCell(gridFactory, dynDatagrid);

            _dynDataGrid = dynDatagrid;
            Init();
        }

        #region Properties.

        /// <summary>
        /// To build cell and cellVM, depending on type (of col or cell).
        /// </summary>
        public IDynDataGridFactory Factory
        { get { return _gridFactory; } }

        /// <summary>
        /// The datagrid model.
        /// Contains the data to display.
        /// </summary>
        public IDynDataGrid DynDataGrid
        { get { return _dynDataGrid; } }

        /// <summary>
        /// Displayed Columns definition.
        /// </summary>
        public ObservableCollection<IGridColumnVM> CollColumnGrid
        {
            get { return _collColumnGrid; }
            set
            {
                if (_collColumnGrid != value)
                {
                    _collColumnGrid = value;
                    RaisePropertyChanged("CollColumnGrid");
                }
            }
        }

        /// <summary>
        /// Rows data.
        /// </summary>
        public ObservableCollection<IGridRowVM> CollDataRow
        {
            get { return _collDataRow; }
            set
            {
                if (_collDataRow != value)
                {
                    _collDataRow = value;
                    RaisePropertyChanged("CollDataRow");
                }
            }
        }


        /// <summary>
        /// list of data cell, mapping betweeen cols and rows (cells).
        /// </summary>
        public GridMappingCell CollCell
        {
            get { return _collCell; }
            set
            {
                if (_collCell != value)
                {
                    _collCell = value;
                    RaisePropertyChanged("CollCell");
                }
            }
        }

        /// <summary>
        /// datagrid row item selection
        /// </summary>
        public IGridRowVM SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
            }
        }

        #endregion

        #region Public methods: add/del row and col

        /// <summary>
        /// Create a row with (empty) cells, at the end.
        /// create row and cells models and ViewModel.
        /// Use this method for a basic usage: you use the component dataGrid as your application dataGrid model.
        /// </summary>
        /// <returns></returns>
        public IGridRow CreateRowWithCells()
        {
            IGridRow row = new GridRow(_dynDataGrid);

            // get columns
            foreach (IGridColumn col in _dynDataGrid.ListColumn)
            {
                // create the cell, matching the type defined in the column
                IGridCell cell = _gridFactory.CreateCell(_dynDataGrid, col);
                row.AddCell(cell);
            }

            // add the row to the dataGrid
            _dynDataGrid.AddRow(row);

            // add it to the view: create VM
            IGridRowVM rowVM = new GridRowVM(row);
            _collDataRow.Add(rowVM);
            RaisePropertyChanged("CollDataRow");

            return row;
        }

        /// <summary>
        /// Add a row data model to the grid UI, create a corresponding VM.
        /// The grid row must have cells.
        /// Use this method when you map your application dataGrid model to this component dataGrid model.
        /// </summary>
        /// <param name="gridRow"></param>
        /// <returns></returns>
        public IGridRowVM AddRow(IGridRow gridRow)
        {
            if (gridRow == null)
                return null;

            // check the cells (basic check: just the number of cells)
            if (gridRow.ListCell.Count() != _collColumnGrid.Count)
                return null;

            // the row should be present inthe dataGrid model
            if (!_dynDataGrid.ListRow.Contains(gridRow))
                return null;

            // add it to the view: create VM
            IGridRowVM rowVM = new GridRowVM(gridRow);
            _collDataRow.Add(rowVM);
            RaisePropertyChanged("CollDataRow");

            return rowVM;
        }

        /// <summary>
        /// Delete the row data model, delete also the corresponding VM.
        /// Update the UI.
        /// </summary>
        /// <param name="row"></param>
        public void DelRow(IGridRowVM row)
        {
            // get the next item in the datagrid, if exists
            //IEnumerator<IGridRowVM> enumRow = _collDataRow.GetEnumerator();

            //enumRow.Current

            // remove the VM
            GridRowVM rowVM = _selectedRow as GridRowVM;
            _collDataRow.Remove(_selectedRow);
            _selectedRow = null;

            // remove the row from the datagrid
            _dynDataGrid.RemoveRow(rowVM.GridRow);

            RaisePropertyChanged("CollDataRow");
        }

        /// <summary>
        /// create a column in the dataGird model and also in the corresponding VM, depending on the type.
        /// (the UI wil be updated automatically).
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <param name="colObj"></param>
        /// <param name="gridColumnVM"></param>
        /// <returns></returns>
        public DynDataGridErrCode CreateColumnWithCells(GridColumnType typeCol, string newColName, out IGridColumnVM gridColumnVM)
        {
            return CreateColumnWithCells(typeCol, newColName, null, out gridColumnVM);
        }

        /// <summary>
        /// create a column in the dataGird model and also in the corresponding VM, depending on the type.
        /// (the UI wil be updated automatically).
        /// Create all empty cells (model and view model).
        /// </summary>
        /// <param name="typeCol"></param>
        /// <param name="newColName"></param>
        /// <param name="colObj"></param>
        /// <param name="gridColumnVM"></param>
        /// <returns></returns>
        public DynDataGridErrCode CreateColumnWithCells(GridColumnType typeCol, string newColName, object colObj, out IGridColumnVM gridColumnVM)
        {
            gridColumnVM = null;

            // create the col in the data model, depending on the type
            IGridColumn column;
            DynDataGridErrCode errCode = _gridFactory.CreateColumn(_dynDataGrid, typeCol, newColName, colObj, out column);
            if (errCode != DynDataGridErrCode.Ok)
                return errCode;

            //column.IsEditionReadOnly = true;

            // add the column in the dataGrid model
            //_dynDataGrid.AddColumn(column);

            // create a empty cell for each row in the dataGrid model
            foreach (IGridRow gridRow in _dynDataGrid.ListRow)
            {
                // depending on the type of the new column
                IGridCell cell = _gridFactory.CreateCell(_dynDataGrid, column);

                gridRow.AddCell(cell);
            }

            // update the UI, add the colVM
            gridColumnVM= AddColumnVM(column);
            return DynDataGridErrCode.Ok;
        }

        /// <summary>
        /// Delte a column: in the dataGrid model and then in the VM.
        /// </summary>
        /// <param name="columnToRemove"></param>
        /// <returns></returns>
        public bool DelColumn(IGridColumn columnToRemove)
        {
            // get the VM
            IGridColumnVM colVM = _collColumnGrid.Where(c => c.GridColumn == columnToRemove).FirstOrDefault();

            // remove the VM
            _collColumnGrid.Remove(colVM);

            // remove the coll
            _dynDataGrid.RemoveColumn(columnToRemove);

            // no more col: remove all rows
            if (_dynDataGrid.ListColumn.Count() == 0)
            {
                // remove all rows
                _dynDataGrid.RemoveAllRow();

                // remove all rows VM
                _collDataRow.Clear();
                RaisePropertyChanged("CollDataRow");
                return true;
            }

            // remove the cells! in each row
            foreach (IGridRow gridRow in _dynDataGrid.ListRow)
            {
                // find the cellVM matching the col
                IGridCell cell = gridRow.FindCellByColumn(columnToRemove);
                gridRow.RemoveCell(cell);
            }

            RaisePropertyChanged("CollDataRow");
            return true;
        }

        #endregion

        #region Privates methods.

        /// <summary>
        /// Initialization, create the all VM: cols and rows (and cells), corresponding to the dataGrid model.
        /// </summary>
        private void Init()
        {
            //----build the columns of the dataGrid
            _collColumnGrid.Clear();

            // add list of column definition
            foreach (IGridColumn column in _dynDataGrid.ListColumn)
            {
                AddColumnVM(column);
            }
            //RaisePropertyChanged("CollColumnGrid");

            //----build the rows of the dataGrid
            // TODO: mettre dans une méthode
            foreach (IGridRow gridRow in _dynDataGrid.ListRow)
            {
                IGridRowVM rowVM = new GridRowVM(gridRow);
                _collDataRow.Add(rowVM);
            }
            RaisePropertyChanged("CollDataRow");
        }

        /// <summary>
        /// Check the name of the column.
        /// </summary>
        /// <param name="newColName"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        //private DynDataGridErrCode CheckColumnName(string newColName)
        //{
        //    newColName = newColName.Trim();
        //    if (string.IsNullOrWhiteSpace(newColName))
        //        return DynDataGridErrCode.ColumnNameWrong;

        //    if (_collColumnGrid.Where(c=>c.GridColumn.Name.Equals(newColName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()!=null)
        //        return DynDataGridErrCode.ColumnNameAlreadyUsed;

        //    return DynDataGridErrCode.Ok;
        //}

        /// <summary>
        /// Create Add a new columnVM based on the column model.
        /// Depends on the type.
        /// TODO: passer par la grid factory??
        /// </summary>
        /// <param name="column"></param>
        private IGridColumnVM AddColumnVM(IGridColumn column)
        {
            IGridColumnVM columnVM;

            // is it a string column?
            IGridColumnString columnString = column as IGridColumnString;
            if (columnString != null)
            {
                columnVM = new GridColumnStringVM(columnString);
                _collColumnGrid.Add(columnVM);
                return columnVM;
            }

            // is it a checkbox column?
            IGridColumnCheckBox columnCheckBox = column as IGridColumnCheckBox;
            if (columnCheckBox != null)
            {
                columnVM = new GridColumnCheckBoxVM(columnCheckBox);
                _collColumnGrid.Add(columnVM);
                return columnVM;
            }

            // other type
            // TODO:
            throw new Exception("Column type not implemented!");

        }
        #endregion

    }
}
