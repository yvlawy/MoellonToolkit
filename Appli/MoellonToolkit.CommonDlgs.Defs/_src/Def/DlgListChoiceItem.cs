using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Defs
{
    /// <summary>
    /// Represent an item displayed in the dialogBox,
    /// the name representing the object is diplayed in the combobox.
    /// </summary>
    public class DlgListChoiceItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public DlgListChoiceItem(string name, object obj)
        {
            Name = name;
            Object = obj;
        }

        public string Name { get; set; }

        /// <summary>
        /// Provide your object.
        /// </summary>
        public Object Object { get; set; }

        /// <summary>
        /// The item is selected.
        /// </summary>
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return Name;
        }


    }
}
