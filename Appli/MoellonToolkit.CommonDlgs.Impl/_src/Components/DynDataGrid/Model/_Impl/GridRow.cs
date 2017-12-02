using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A row in a grid.
    /// Displayed in a rwoVM.
    /// </summary>
    public class GridRow : IGridRow
    {
        // list of cells
        List<IGridCell> _listCell = new List<IGridCell>();

        public GridRow(IDynDataGrid datagrid)
        {
            Datagrid = datagrid;
        }

        /// <summary>
        /// The owner data grid.
        /// </summary>
        public IDynDataGrid Datagrid
        { get; private set; }

        // list of cells
        public IEnumerable<IGridCell> ListCell
        { get { return _listCell; } }

        /// <summary>
        /// find the cell in the row, by the column.
        /// </summary>
        /// <param name="dataGridColumn"></param>
        /// <returns></returns>
        public IGridCell FindCellByColumn(IGridColumn column)
        {
            return _listCell.Where(c => c.Column.Name.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        /// <summary>
        /// Add a Cell in the row.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool AddCell(IGridCell cell)
        {
            // check that the column is known
            if (Datagrid.ListColumn.Where(c => c.Name.Equals(cell.Column.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() == null)
                return false;

            // check that no other cell are set for the column
            if (_listCell.Where(ce => ce.Column.Name.Equals(cell.Column.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
                return false;

            _listCell.Add(cell);
            return true;
        }

        public bool RemoveCell(IGridCell cell)
        {
            if (cell == null)
                return false;

            _listCell.Remove(cell);
            return true;
        }

    }
}
