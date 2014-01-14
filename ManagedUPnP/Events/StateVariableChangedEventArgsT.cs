//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates the event arguments for when an evented state variable.
    /// </summary>
    /// <typeparam name="T">The data type for the state variable.</typeparam>
    public class StateVariableChangedEventArgs<T> : EventArgs
    {
        #region Protected Locals

        /// <summary>
        /// The state variable name which changed.
        /// </summary>
        protected string msStateVarName;

        /// <summary>
        /// The new state variable value.
        /// </summary>
        protected T mtStateVarValue;

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new state variable changed event arguments.
        /// </summary>
        /// <param name="stateVarName">The state variable name.</param>
        /// <param name="stateVarValue">The state variable value.</param>
        public StateVariableChangedEventArgs(string stateVarName, T stateVarValue)
        {
            msStateVarName = stateVarName;
            mtStateVarValue = stateVarValue;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the state variable name which changed.
        /// </summary>
        public string StateVarName
        {
            get
            {
                return msStateVarName;
            }
        }

        /// <summary>
        /// Gets the new state variable value.
        /// </summary>
        public T StateVarValue
        {
            get
            {
                return mtStateVarValue;
            }
        }

        #endregion
    }
}
