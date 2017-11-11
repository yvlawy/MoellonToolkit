using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public abstract class GridCellBase: IGridCell
    {
        public GridCellBase(IGridColumn column)
        {
            Column = column;
            IsReadOnly = column.IsEditionReadOnly;
        }

        public bool IsReadOnly
        { get; private set; }

        public IGridColumn Column
        { get; private set; }

        /// <summary>
        /// The value displayed in the grid cell UI.
        /// </summary>
        public object Cell
        { get; set; }
    }
}
