using MoellonToolkit.CommonDlgs.Impl;
using MoellonToolkit.CommonDlgs.Impl.Components;
using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A data grid cell value to edit.
    /// Its a map between a column and a row of the grid.
    /// </summary>
    public class GridCellValueVM: ViewModelBase, IGridCellValueVM
    {
        /// <summary>
        /// The concerned data model cell displayed in the UI cell data grid.
        /// </summary>
        private IGridCellValue _gridCell;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gridCell"></param>
        public GridCellValueVM(IGridCellValue gridCell)
        {
            _gridCell = gridCell;
            IsReadOnly = _gridCell.IsReadOnly;
            //InitCell();
        }

        public object ColumnBinding { get; set; }
        public object RowBinding { get; set; }

        /// <summary>
        /// get/set the value of the cell.
        /// TextBox UpdateSourceTrigger=Default in xaml. 
        /// </summary>
        public object Cell
        {
            get
            {
                return _gridCell.Content;
            }
            // update the cell value, from the UI
            set
            {
                SetCell(value);
            }
        }

        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Refresh the view.
        /// </summary>
        public void Refresh()
        {
            //InitCell();
            RaisePropertyChanged("Cell");
        }

        /// <summary>
        /// Set a new value in the cell.
        /// </summary>
        /// <param name="value"></param>
        private void SetCell(object value)
        {
            if (_gridCell.Content == value)
                // the new value is the same than the old one
                return;

            if (_gridCell.Column.IsEditionReadOnly)
                return;

            // set the new value to the cell
            _gridCell.Content= value;

            // raise action: cell content has changed
            _gridCell.RaiseGridCellChanged();

            RaisePropertyChanged("Cell");
        }
    }
}
