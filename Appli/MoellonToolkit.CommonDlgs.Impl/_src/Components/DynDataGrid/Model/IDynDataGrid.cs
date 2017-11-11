using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A data grid
    /// </summary>
    public interface IDynDataGrid
    {
        // columns
        IEnumerable<IGridColumn> ListColumn { get; }

        IEnumerable<IGridRow> ListRow { get; }


        bool AddColumn(IGridColumn column);

        bool AddRow(IGridRow row);

        // remove the row from the datagrid
        bool RemoveRow(IGridRow row);
    }
}
