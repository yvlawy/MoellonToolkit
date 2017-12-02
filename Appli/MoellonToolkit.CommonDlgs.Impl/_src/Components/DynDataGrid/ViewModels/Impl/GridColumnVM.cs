using MoellonToolkit.CommonDlgs.Impl;
using MoellonToolkit.CommonDlgs.Impl.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridColumnVM : GridColumnBaseVM
    {
        public GridColumnVM(IGridColumn gridColumn)
        {
            GridColumn = gridColumn;
        }

        public string Name
        {
            get { return GridColumn.Name; }
        }

    }
}
