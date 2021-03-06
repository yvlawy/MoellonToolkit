﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A grid cell.
    /// A cell is in a row in a grid.
    /// </summary>
    public interface IGridCell
    {
        IGridColumn Column { get; }

        IGridRow Row { get; }
        
        bool IsReadOnly { get; }

        /// <summary>
        /// The cell content: a value (string, int,...) or a component: combobox, button,...
        /// </summary>
        object Content { get; set; }

        /// <summary>
        /// Raise that the cell content has changed.
        /// </summary>
        void RaiseGridCellChanged();

    }
}
