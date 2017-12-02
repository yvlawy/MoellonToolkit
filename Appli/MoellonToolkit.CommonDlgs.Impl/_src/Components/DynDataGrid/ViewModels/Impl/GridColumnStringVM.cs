using MoellonToolkit.CommonDlgs.Impl;
using MoellonToolkit.CommonDlgs.Impl.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A column displaying string values in the dynamic dataGrid.
    /// </summary>
    public class GridColumnStringVM: GridColumnVM
    {
        // TODO: besoin? deja présent dans classe de base
        //IGridColumnString _columnString;

        public GridColumnStringVM(IGridColumnString columnString):base(columnString)
        {
            //_columnString = columnString;
        }

    }


}
