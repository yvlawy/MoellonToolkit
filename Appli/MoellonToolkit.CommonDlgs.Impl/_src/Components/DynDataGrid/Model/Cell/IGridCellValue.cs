﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A grid cell value: string, int,...
    /// the other type is component.
    /// </summary>
    public interface IGridCellValue : IGridCell
    {
        void RaiseValueModifiedInUI();
    }
}
