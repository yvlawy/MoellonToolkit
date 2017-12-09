using MoellonToolkit.CommonDlgs.Defs;
using MoellonToolkit.CommonDlgs.Impl.Components;
using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevApp.ViewModels
{
    /// <summary>
    /// An edit dynamic datagrid.
    /// with add/del row and col.
    /// </summary>
    public class EditDynDataGridVM :  ViewModelBase
    {
        /// <summary>
        /// To ask to the user row and col name.
        /// </summary>
        ICommonDlg _commonDlg;

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
        public EditDynDataGridVM(ICommonDlg commonDlg, IDynDataGridFactory gridFactory, IDynDataGrid datagrid)
        {
            _commonDlg = commonDlg;
            //_gridFactory = gridFactory;

            //_collCell = new GridMappingCell(gridFactory, datagrid);

            //_datagrid = datagrid;

            DynDataGridVM = new DynDataGridVM(gridFactory, datagrid);

            Init();
        }

        #region Properties.

        /// <summary>
        /// The dynamic dataGrid component.
        /// </summary>
        public DynDataGridVM DynDataGridVM { get; private set; }

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
            if (DynDataGridVM.CollColumnGrid.Count == 0)
                return false;
            return true;
        }

        private void AddRow()
        {
            // create a row with empty cells, at the end.
            IGridRow row = DynDataGridVM.CreateRowWithCells();
        }

        //---------------------------------------------------------------------
        private bool CanDelRow()
        {
            if (DynDataGridVM.SelectedRow == null)
                return false;
            return true;
        }

        private void DelRow()
        {
            // ask user to confirm the deletion
            // TODO:

            // get the next item in the datagrid, if exists
            //_collDataRow.GetEnumerator().

            // delete the last row
            DynDataGridVM.DelRow(DynDataGridVM.SelectedRow);
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
            if (DynDataGridVM.DynDataGrid.ListColumn.Where(c => c.Name.Equals(newColName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
            {
                _commonDlg.ShowError("The name is already used by a column.");
                return;
            }

            // create a column, depending on the type
            DynDataGridVM.CreateColumnWithCells(ModelDef.GridColumnType.String, newColName);
        }

        //---------------------------------------------------------------------
        private bool CanDelCol()
        {
            // get the column of the selected cell (having the focus)
            if (DynDataGridVM.SelectedRow == null)
                return false;
            return true;
        }

        /// <summary>
        /// Delete the column, containing the focused cell.
        /// </summary>
        private void DelCol()
        {
            // select the col to delete
            IGridColumn columnToRemove = DynDataGridVM.DynDataGrid.ListColumn.Last();
            if (columnToRemove == null)
                return;

            DynDataGridVM.DelColumn(columnToRemove);
        }

        #endregion

        #region Privates methods.

        private void Init()
        {
        }
        #endregion
    }
}
