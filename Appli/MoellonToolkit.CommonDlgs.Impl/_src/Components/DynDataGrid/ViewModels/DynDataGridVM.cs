using MoellonToolkit.CommonDlgs.Defs;
using MoellonToolkit.CommonDlgs.Impl;
using MoellonToolkit.CommonDlgs.Impl.Components;
using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// The dynamic dataGrid, with command to add row and col.
    /// </summary>
    public class DynDataGridVM : ViewModelBase
    {
        /// <summary>
        /// To ask to the user row and col name.
        /// </summary>
        ICommonDlg _commonDlg;

        /// <summary>
        /// To build cell and cellVM, depending on type (of col or cell).
        /// </summary>
        IDynDataGridFactory _gridFactory;

        /// <summary>
        /// datagrid model.
        /// data to display.
        /// </summary>
        IDynDataGrid _datagrid;

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

        ICommand _addRowCmd;
        ICommand _delRowCmd;

        /// <summary>
        /// Add a new data column.
        /// </summary>
        ICommand _addColCmd;
        ICommand _delColCmd;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="datagrid"></param>
        public DynDataGridVM(ICommonDlg commonDlg, IDynDataGridFactory gridFactory, IDynDataGrid datagrid)
        {
            _commonDlg = commonDlg;
            _gridFactory = gridFactory;

            _collCell = new GridMappingCell(gridFactory, datagrid);

            _datagrid = datagrid;
            Init();
        }

        #region Properties.

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

        #region Command properties

        /// <summary>
        /// Add a new empty row.
        /// </summary>
        public ICommand AddRowCmd
        {
            get
            {
                if (_addRowCmd == null)
                {
                    _addRowCmd = new RelayCommand(param => AddRow(),
                                                             param => CanAddRow());
                }
                return _addRowCmd;
            }
        }

        /// <summary>
        /// delete the selected row.
        /// </summary>
        public ICommand DelRowCmd
        {
            get
            {
                if (_delRowCmd == null)
                {
                    _delRowCmd = new RelayCommand(param => DelRow(),
                                                             param => CanDelRow());
                }
                return _delRowCmd;
            }
        }

        /// <summary>
        /// Add a new empty col.
        /// </summary>
        public ICommand AddColCmd
        {
            get
            {
                if (_addColCmd == null)
                {
                    _addColCmd = new RelayCommand(param => AddCol(),
                                                             param => CanAddCol());
                }
                return _addColCmd;
            }
        }

        /// <summary>
        /// delete the selected column.
        /// </summary>
        public ICommand DelColCmd
        {
            get
            {
                if (_delColCmd == null)
                {
                    _delColCmd = new RelayCommand(param => DelCol(),
                                                             param => CanDelCol());
                }
                return _delColCmd;
            }
        }

        #endregion

        #region Private Command properties

        //---------------------------------------------------------------------
        private bool CanAddRow()
        {
            // need at least a col to add rows
            if (_collColumnGrid.Count == 0)
                return false;
            return true;
        }

        private void AddRow()
        {

            // create data rows
            IGridRow row = new GridRow(_datagrid);

            // get columns
            foreach( IGridColumn col in  _datagrid.ListColumn)
            {
                // $TASK-001: create the cell, matching the type defined in the column
                IGridCell cell = _gridFactory.CreateCell(col);
                row.AddCell(cell);
            }

            _datagrid.AddRow(row);

            // add it to the view: create VM
            IGridRowVM rowVM = new GridRowVM(row);
            _collDataRow.Add(rowVM);
            RaisePropertyChanged("CollDataRow");
        }

        //---------------------------------------------------------------------
        private bool CanDelRow()
        {
            if (_selectedRow == null)
                return false;
            return true;
        }

        private void DelRow()
        {
            // ask user to confirm the deletion
            // TODO:

            // get the next item in the datagrid, if exists
            //_collDataRow.GetEnumerator().

            // remove the VM
            GridRowVM rowVM = _selectedRow as GridRowVM;
            _collDataRow.Remove(_selectedRow);
            _selectedRow = null;

            // remove the row from the datagrid
            rowVM.GridRow.Datagrid.RemoveRow(rowVM.GridRow);

            RaisePropertyChanged("CollDataRow");
        }

        //---------------------------------------------------------------------
        private bool CanAddCol()
        {
            return true;
        }

        /// <summary>
        /// Add a new column at the end of the existing col, create all missing cell on each row.
        /// No need to create cellVM, will be created automatically by the gridMappingcell.
        /// </summary>
        private void AddCol()
        {
            // ask to the user the name and the type
            //List<DlgComboChoiceItem> listItem = new List<DlgComboChoiceItem>();
            //DlgComboChoiceItem selectedBeforeItem = null;

            //var item = new DlgComboChoiceItem("string", "string");
            //listItem.Add(item);
            //selectedBeforeItem = item;
            //item = new DlgComboChoiceItem("int", "int");
            //listItem.Add(item);

            //DlgComboChoiceItem selected;

            string newColName;
            CommonDlgResult res = _commonDlg.ShowDlgInputText("Input", "Column name:", "col", out newColName);
            if (res != CommonDlgResult.Ok)
                return;

            // check the column name
            if (string.IsNullOrEmpty(newColName))
                return;
            newColName = newColName.Trim();
            if (_datagrid.ListColumn.Where(c => c.Name.Equals(newColName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
            {
                _commonDlg.ShowError("The name is already used by a column.");
                return;
            }

            // create a column, depending on the type
            IGridColumnString column = new GridColumnString(newColName);
            //column.IsEditionReadOnly = true;
            _datagrid.AddColumn(column);

            // create a empty cell for each row
            foreach(IGridRow gridRow in _datagrid.ListRow)
            {
                // depending on the type of the new column
                IGridCell cell = _gridFactory.CreateCell(column);

                gridRow.AddCell(cell);
            }
 
            // update the UI, add the colVM
            AddColumnVM(column);
        }

        //---------------------------------------------------------------------
        private bool CanDelCol()
        {
            // get the column of the selected cell (having the focus)
            if (_selectedRow == null)
                return false;
            return true;
        }

        /// <summary>
        /// Delete the column, containing the focused cell.
        /// $TASK-002
        /// </summary>
        private void DelCol()
        {
            // select the col to delete
            IGridColumn columnToRemove = _datagrid.ListColumn.Last();
            if (columnToRemove == null)
                return;

            // get the VM
            IGridColumnVM colVM = _collColumnGrid.Where(c => c.GridColumn == columnToRemove).FirstOrDefault();

            // remove the VM
            _collColumnGrid.Remove(colVM);

            // remove the coll
            _datagrid.RemoveColumn(columnToRemove);

            // no more col: remove all rows
            if(_datagrid.ListColumn.Count()==0)
            {
                // remove all rows
                _datagrid.RemoveAllRow();

                // remove all rows VM
                _collDataRow.Clear();
                RaisePropertyChanged("CollDataRow");
                return;
            }

            // remove the cells! in each row
            foreach(IGridRow gridRow in _datagrid.ListRow)
            {
                // find the cellVM matching the col
                IGridCell cell = gridRow.FindCellByColumn(columnToRemove);
                gridRow.RemoveCell(cell);
            }

            RaisePropertyChanged("CollDataRow");

        }
        #endregion

        #region Privates methods.
        private void Init()
        {
            //----build the columns of the dataGrid
            _collColumnGrid.Clear();

            // add list of column definition
            foreach (IGridColumn column in _datagrid.ListColumn)
            {
                AddColumnVM(column);
            }
            //RaisePropertyChanged("CollColumnGrid");

            //----build the rows of the dataGrid
            foreach(IGridRow gridRow in _datagrid.ListRow)
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
        private void AddColumnVM(IGridColumn column)
        {
            IGridColumnVM columnVM;

            // is it a string column?
            IGridColumnString columnString = column as IGridColumnString;
            if (columnString != null)
            {
                columnVM = new GridColumnStringVM(columnString);
                _collColumnGrid.Add(columnVM);
                return;
            }

            // is it a checkbox column?
            IGridColumnCheckBox columnCheckBox = column as IGridColumnCheckBox;
            if(columnCheckBox != null)
            {
                columnVM = new GridColumnCheckBoxVM(columnCheckBox);
                _collColumnGrid.Add(columnVM);
                return;
            }

            // other type
            // TODO:
        }
    #endregion
}
    }
