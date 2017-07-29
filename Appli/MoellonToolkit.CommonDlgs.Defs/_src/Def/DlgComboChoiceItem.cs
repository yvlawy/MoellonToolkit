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
    public class DlgComboChoiceItem
    {
        /// <summary>
        /// Constructor.
        /// The nale is displayed in the combobox.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public DlgComboChoiceItem(string name, object obj)
        {
            Name = name;
            Object = obj;
        }

        public string Name { get; set; }

        /// <summary>
        /// Provide your object.
        /// </summary>
        public Object Object { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
