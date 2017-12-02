using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Baseof all columns of the datagrid.
    /// </summary>
    public interface IGridColumn
    {
        /// <summary>
        /// The name of the column, its unique in the data grid.
        /// </summary>
        string Name { get; }

        bool IsEditionReadOnly { get; set; }
    }
}
