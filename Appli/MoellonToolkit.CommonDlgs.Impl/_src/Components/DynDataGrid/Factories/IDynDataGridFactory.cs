using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// To build typed cell and CellVM for the dynamic datagrid.
    /// </summary>
    public interface IDynDataGridFactory
    {
        /// <summary>
        /// Create a cell (for a row), depending on the column type.
        /// TODO: comme ca ou directement da le row? (a fournir alors)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        IGridCell CreateCell(IGridColumn column);

        // # créer une cellVM selon une cell
        IGridCellVM CreateCellVM(IGridCell cell);

    }
}
