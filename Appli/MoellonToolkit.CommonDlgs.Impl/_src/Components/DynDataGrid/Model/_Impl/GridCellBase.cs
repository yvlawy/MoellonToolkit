using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// Base of all grid cell: value and component.
    /// </summary>
    public abstract class GridCellBase: IGridCell
    {
        GridCellChangedProvider _gridCellChangedProvider;

        Action<IGridCell> _gridCellChanged;

        public GridCellBase(IGridColumn column, IGridRow row, GridCellChangedProvider actionProvider)
        {
            Column = column;
            Row = row;

            _gridCellChangedProvider = actionProvider;

            IsReadOnly = column.IsEditionReadOnly;
        }

        public bool IsReadOnly
        { get; private set; }

        public IGridColumn Column
        { get; private set; }

        public IGridRow Row
        { get; private set; }

        /// <summary>
        /// The value displayed in the grid cell UI.
        /// </summary>
        public object Content
        { get; set; }

        /// <summary>
        /// Called when the cell content has changed.
        /// Call the attached action, if present.
        /// </summary>
        public void RaiseGridCellChanged()
        {
            _gridCellChangedProvider.GridCellChanged?.Invoke(this);
        }

    }
}
