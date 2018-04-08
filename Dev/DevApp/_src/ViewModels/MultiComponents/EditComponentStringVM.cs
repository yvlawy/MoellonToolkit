using DevApp.DataModel;
using DevApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.ViewModels
{
    public class EditComponentStringVM : EditComponentBaseVM
    {
        public EditComponentStringVM(DataString dataString)
        {
            Name = dataString.Name;
            Value = dataString.Value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
