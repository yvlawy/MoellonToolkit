using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoellonToolkit.MVVMBase
{
    //=========================================================================
    /// <summary>
    /// Registered recipient object (who register) and the action (a function) to execute.
    /// </summary>
    public class RecipientObjAction
    {
        // The recipient object,  who registered the action. 
        WeakReference _recipientRef;

        /// <summary>
        /// The type of action.
        /// </summary>
        public Type _actionType;

        /// <summary>
        /// The action<T> to execute when notified.
        /// Action<T> _action;
        /// </summary>
        object _actionT;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="actionT"></param>
        public RecipientObjAction(object recipient, Type actionType, object actionT)
        {
            _recipientRef = new WeakReference(recipient);
            _actionType = actionType;
            _actionT = actionT;
        }

        /// <summary>
        /// The recipient object, if notified, execute the registered actions.
        /// can be null!! 
        /// if the recipient object is no more referenced in the application.
        /// </summary>
        public object Recipient
        {
            get
            {
                // return _recipient; 
                return _recipientRef.Target as object;
            }
        }

        public Type ActionType { get { return _actionType; } }

        public object ActionT
        {
            get { return _actionT; }
        }
    }
}
