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
        /// </summary>
        /// <returns></returns>
        public IGridRow CreateRowWithCells()
        {
            IGridRow row = new GridRow(_dynDataGrid);

            // get columns
            foreach (IGridColumn col in _dynDataGrid.ListColumn)
            {
                // create the cell, matching the type defined in the column
                IGridCell cell = _gridFactory.CreateCell(col);
                row.AddCell(cell);
            }

            _dynDataGrid.AddRow(row);

            // add it to the view: create VM
            IGridRowVM rowVM = new GridRowVM(row);
            _collDataRow.Add(rowVM);
            RaisePropertyChanged("CollDataRow");

            return row;
        }

        /// <summary>
        /// Delete the row, update the UI.
        /// </summary>
        /// <param name="row"></param>
        public void DelRow(IGridRowVM row)
        {
            // get the next item in the datagrid, if exists
            //_collDataRow.GetEnumerator().

            // remove the VM
            GridRowVM rowVM = _selectedRow as GridRowVM;
            _collDataRow.Remove(_selectedRow);
            _selectedRow = null;

            // remove the row from the datagrid
            _dynDataGrid.RemoveRow(rowVM.GridRow);

            RaisePropertyChanged("CollDataRow");
        }

        // create a column, depending on the type
        public IGridColumnVM CreateColumnWithCells(ModelDef.GridColumnType typeCol, string newColName)
        {
            IGridColumnString column = new GridColumnString(newColName);

            //column.IsEditionReadOnly = true;
            _dynDataGrid.AddColumn(column);

            // create a empty cell for each row
            foreach (IGridRow gridRow in _dynDataGrid.ListRow)
            {
                // depending on the type of the new column
                IGridCell cell = _gridFactory.CreateCell(column);

                gridRow.AddCell(cell);
            }

            // update the UI, add the colVM
            return AddColumnVM(column);
        }

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
            foreach (IGridRow gridRow in _dynDataGrid.ListRow)
            {
                IGridRowVM rowVM = new GridRowVM(gridRow);
                _collDataRow.Add(rowVM);
            }
            RaisePropertyChanged("CollDataRow");
        }

        /// <summary>
        /// Add a new columnVM.
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
