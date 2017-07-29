using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MoellonToolkit.MVVMBase
{
    /// <summary>
    /// Base class of all View Model.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region DisplayName
 
        ///
        /// Retourne le nom de l'objet
        ///
        public virtual string DisplayName { get; protected set; }
 
        #endregion

        #region Debugging Aides
 
        ///
        /// Utilise la réflexion pour vérifier que l'élément bindé existe bien
        /// Ne s'active qu'à la compilation par debug
        ///
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
 
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name : " + propertyName;
 
                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
 
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
 
        #endregion

        //=====================================================================        
        #region INotifyPropertyChanged Members
 
        //---------------------------------------------------------------------
        /// <summary>
        /// Event fired when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
 
        
        //---------------------------------------------------------------------
        /// <summary>
        /// A property content changed, fire an event.
        /// Provide the name of the property.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
 
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
        
        public virtual void Dispose()
        {
            // unregisters its own messages, so that we risk no leak
            //Messenger.Default.Unregister<...>(this);

            // sends a message telling that this ViewModel is being cleaned
            //Messenger.Default.Send(new ViewModelDisposingMessage(this));

            // remove each VM childs

        }
    }
}
