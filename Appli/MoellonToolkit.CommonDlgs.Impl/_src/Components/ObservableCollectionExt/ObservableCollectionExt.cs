using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MoellonToolkit.CommonDlgs.Impl.Components
{
    /// <summary>
    /// A extended collection, to be refreshed when an a field changed.
    /// (because is refreshed only when the collection itself changes: add or remove).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionExt<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Clla it to refresh the UI components binding.
        /// </summary>
        /// <param name="action"></param>
        public void RaiseCollectionChanges()
        {
            // accept only Reset!
            var act = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset);
            //act.Action = action;
            OnCollectionChanged(act);
        }
    }
}
