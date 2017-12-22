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
        ActionGridCellValueModifiedProvider _actionProvider;

        Action<IGridCell> _actionValueModifiedInUI;

        public GridCellBase(IGridColumn column, ActionGridCellValueModifiedProvider actionProvider)
        {
            Column = column;

            //_actionValueModifiedInUI = valueModifiedInUI;
            _actionProvider = actionProvider;

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

        public void RaiseValueModifiedInUI()
        {
            _actionProvider.ActionGridValueModifiedInUI?.Invoke(this);
            //_actionValueModifiedInUI?.Invoke(this);
        }

    }
}
