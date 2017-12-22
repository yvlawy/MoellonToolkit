using MoellonToolkit.CommonDlgs.Impl;
using MoellonToolkit.CommonDlgs.Impl.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// All the cells (values or components) of the grid, link between col and row.
    /// </summary>
    public class GridMappingCell : GridMappingCellsBase
    {
        /// <summary>
        /// The factory to build cell ViewModel depending on the type.
        /// 
        /// </summary>
        IDynDataGridFactory _gridFactory;

        /// <summary>
        /// The dynamic data grid model.
        /// </summary>
        IDynDataGrid _datagrid;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GridMappingCell(IDynDataGridFactory gridFactory, IDynDataGrid datagrid)
        {
            _gridFactory = gridFactory;
            _datagrid = datagrid;
        }
        public override IGridCellVM FindOrCreate(object columnBinding, object rowBinding)
        {
            // try to find it
            IGridCellVM value = this.Where(x => x.RowBinding == rowBinding &&
                                       x.ColumnBinding == columnBinding).FirstOrDefault();
            if (value != null)
                return value;

            // the value does not exists, create it
            return Create(columnBinding as IGridColumnVM, rowBinding as IGridRowVM);
        }

        /// <summary>
        /// refresh a cell, by the col and row.
        /// (called because of a row relation: update the linked cell.
        /// </summary>
        /// <param name="columnVM"></param>
        /// <param name="rowVM"></param>
        public void RefreshCell(IGridColumnVM columnVM, IGridRowVM rowVM)
        {
            IGridCellVM value = this.Where(x => x.RowBinding == rowVM && x.ColumnBinding == columnVM).FirstOrDefault();
            if (value == null)
                return;

            value.Refresh();
        }

        //=====================================================================
        #region Privates methods.

        /// <summary>
        /// Create a mapped value of a cell, between a col and a row.
        /// </summary>
        /// <param name="columnBinding"></param>
        /// <param name="rowBinding"></param>
        /// <returns></returns>
        private IGridCellVM Create(IGridColumnVM columnBinding, IGridRowVM rowBinding)
        {
            // find the cell in the row, by the columnDef
            IGridCell cell = rowBinding.GridRow.FindCellByColumn(columnBinding.GridColumn);

            // $TASK-001: the value does not exists, create the cellVM, can be a value or a component (button, combobox,...)
            IGridCellVM mapValue =_gridFactory.CreateCellVM(cell);

            // if problem here: cell or row data model is missing in the VM
            mapValue.ColumnBinding = columnBinding;
            mapValue.RowBinding = rowBinding;

            Add(mapValue);

            return mapValue;
        }

        #endregion
    }
}
