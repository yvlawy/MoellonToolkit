using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Base of column for the dynamic grid.
    /// columns are dynamics, known olny at the last moment during the execution.
    /// </summary>
    public interface IGridColumnVM
    {
        IGridColumn GridColumn { get; }

        // the position in the UI grid.
        int DisplayIndex { get; set; }
    }
}
