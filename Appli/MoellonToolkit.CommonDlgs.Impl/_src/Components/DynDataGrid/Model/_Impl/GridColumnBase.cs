using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public abstract class GridColumnBase: IGridColumn
    {
        public GridColumnBase(string name, object colObj)
        {
            IsEditionReadOnly = false;
            Name = name;
            Object = colObj;
        }

        public string Name { get; private set; }

        public bool IsEditionReadOnly { get; set; }

        public object Object { get; set; }
    }
}
