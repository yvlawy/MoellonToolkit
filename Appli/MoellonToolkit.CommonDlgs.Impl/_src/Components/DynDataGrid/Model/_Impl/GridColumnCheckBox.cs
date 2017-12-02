using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridColumnCheckBox : IGridColumnCheckBox
    {
        public GridColumnCheckBox(string name)
        {
            IsEditionReadOnly = false;
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsEditionReadOnly { get; set; }

    }
}
