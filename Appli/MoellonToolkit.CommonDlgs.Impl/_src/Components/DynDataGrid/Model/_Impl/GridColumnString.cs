using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    public class GridColumnString : IGridColumnString
    {
        public GridColumnString(string name)
        {
            IsEditionReadOnly = false;
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsEditionReadOnly { get; set; }

    }
}
