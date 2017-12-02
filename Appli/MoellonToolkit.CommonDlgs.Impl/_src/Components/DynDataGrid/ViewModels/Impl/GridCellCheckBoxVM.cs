using MoellonToolkit.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A data grid cell checkbox component.
    /// Its a map between a column and a row of the grid.
    /// </summary>
    public class GridCellCheckBoxVM : ViewModelBase, IGridCellComponentVM
    {
        /// <summary>
        /// The concerned data model cell displayed in the UI cell data grid.
        /// </summary>
        private IGridCellCheckBox _gridCell;

        /// <summary>
        /// TODO: ??
        /// </summary>
        private object _isChecked;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gridCell"></param>
        public GridCellCheckBoxVM(IGridCellCheckBox gridCell)
        {
            _gridCell = gridCell;
            IsReadOnly = _gridCell.IsReadOnly;
            InitCell();
        }


        public object ColumnBinding { get; set; }
        public object RowBinding { get; set; }

        /// <summary>
        /// get/set the value of the cell.
        /// </summary>
        public object IsChecked
        {
            get
            {
                return _isChecked;
            }
            // update the cell value, from the UI
            set
            {
                SetCell(value);
            }
        }

        public bool IsReadOnly { get; private set; }


        /// <summary>
        /// Call it after set the row relation.
        /// </summary>
        public void Refresh()
        {
            InitCell();
            RaisePropertyChanged("IsChecked");
        }
        /// <summary>
        /// Init the cell value.
        /// </summary>
        private void InitCell()
        {
            _isChecked = _gridCell.Cell;

            RaisePropertyChanged("IsChecked");

        }

        /// <summary>
        /// Set a new value in the cell.
        /// </summary>
        /// <param name="value"></param>
        private void SetCell(object value)
        {
            if (_isChecked == value)
                // the new value is the same than the old one
                return;

            if (_gridCell.Column.IsEditionReadOnly)
                return;

            // check the type
            _isChecked = value;

            RaisePropertyChanged("IsChecked");
        }
    }
}
