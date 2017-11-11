using MoellonToolkit.CommonDlgs.Impl;
using MoellonToolkit.CommonDlgs.Impl.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.ViewModels
{
    /// <summary>
    /// row grid VM.
    /// </summary>
    public class GridRowVM : GridRowBaseVM
    {
        public GridRowVM(IGridRow gridRow)
        {
            GridRow = gridRow;
        }

        //public IGridRow DataGridRow { get; private set; }

    }
}
