using DevApp.Ctrl;
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

namespace DevApp.ViewModels
{
    public class DynDataGridVM : ViewModelBase
    {
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

        IGridRowVM _selectedItem;

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
        public DynDataGridVM(IDynDataGrid datagrid)
        {
            _collCell = new GridMappingCell(datagrid);

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
        public IGridRowVM SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
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
            return true;
        }

        private void AddRow()
        {

            // create data rows
            IGridRow row = new GridRow(_datagrid);

            // get columns
            foreach( IGridColumn col in  _datagrid.ListColumn)
            {
                // create a cell matching the type of the column
                IGridColumnString colString = col as IGridColumnString;
                if(colString!=null)
                {
                    IGridCell cell = new GridCellString(colString, "");
                    row.AddCell(cell);
                    continue;
                }

                // other type
                // TODO:
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
            if (_selectedItem == null)
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
            GridRowVM rowVM = _selectedItem as GridRowVM;
            _collDataRow.Remove(_selectedItem);
            _selectedItem = null;
            RaisePropertyChanged("CollDataRow");

            // remove the row from the datagrid
            rowVM.GridRow.Datagrid.RemoveRow(rowVM.GridRow);
        }

        //---------------------------------------------------------------------
        private bool CanAddCol()
        {
            return true;
        }

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

            string text;
            CommonDlgResult res = AppCtrlProvider.AppCtrl.CommonDlg.ShowDlgInputText("Input", "Column name:", "col", out text);
            if (res != CommonDlgResult.Ok)
                return;

            // create a column, depending on the type
            IGridColumnString column = new GridColumnString(text);
            //column.IsEditionReadOnly = true;
            _datagrid.AddColumn(column);

            // create a empty cell for each row
            foreach(IGridRow gridRow in _datagrid.ListRow)
            {
                // depending on the type of the new column
                //IGridCell 
            }
            //ici();  // and the VM?

            // update the UI, add the colVM
            AddColumnVM(column);
        }

        //---------------------------------------------------------------------
        private bool CanDelCol()
        {
            // get the column of the selected cell (having the focus)
            if (_selectedItem == null)
                return false;
            return true;
        }

        private void DelCol()
        {
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
            RaisePropertyChanged("CollColumnGrid");

            //----build the rows of the dataGrid
            foreach(IGridRow gridRow in _datagrid.ListRow)
            {
                IGridRowVM rowVM = new GridRowVM(gridRow);
                _collDataRow.Add(rowVM);
            }
            RaisePropertyChanged("CollDataRow");

        }

        private void AddColumnVM(IGridColumn column)
        {
            IGridColumnVM columnGridVM;

            // is it a string column?
            IGridColumnString columnString = column as IGridColumnString;
            if (columnString != null)
            {
                columnGridVM = new GridColumnStringVM(columnString);
                _collColumnGrid.Add(columnGridVM);
                return;
            }

            // other type
            // TODO:
        }
    #endregion
}
    }
