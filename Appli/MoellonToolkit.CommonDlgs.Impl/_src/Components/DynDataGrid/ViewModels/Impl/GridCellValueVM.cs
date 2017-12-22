﻿using MoellonToolkit.CommonDlgs.Impl;
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
        /// TODO: the basic type value? int, string,...
        /// 
        /// or the IValue (displayed with the TosString())?
        /// </summary>
        //private object _cell;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gridCell"></param>
        public GridCellValueVM(IGridCellValue gridCell)
        {
            _gridCell = gridCell;
            IsReadOnly = _gridCell.IsReadOnly;
            InitCell();
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
                //return _cell;
                return _gridCell.Cell;
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
            RaisePropertyChanged("Cell");
        }


        /// <summary>
        /// Init the cell value.
        /// </summary>
        private void InitCell()
        {
            //_cell = _gridCell.Cell;


            // save the value
            //_value = _dataGridCell.DataCell.Value.ToString();
            ////_dispDataCell.SetValue(val);

            RaisePropertyChanged("Cell");

        }

        /// <summary>
        /// Set a new value in the cell.
        /// </summary>
        /// <param name="value"></param>
        private void SetCell(object value)
        {
            if (_gridCell.Cell == value)
                // the new value is the same than the old one
                return;

            if (_gridCell.Column.IsEditionReadOnly)
                return;

            // set the new value to the cell
            _gridCell.Cell= value;

            // raise action: value modified in the UI
            _gridCell.RaiseValueModifiedInUI();

            RaisePropertyChanged("Cell");
        }
    }
}
