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
    public class StateVariableChangedEventArgs : StateVariableChangedEventArgs<Object>
    {
        #region Public Initialisation

        /// <summary>
        /// Creates a new state variable changed event arguments.
        /// </summary>
        /// <param name="stateVarName">The state variable name.</param>
        /// <param name="stateVarValue">The state variable value.</param>
        public StateVariableChangedEventArgs(string stateVarName, Object stateVarValue)
            : base(stateVarName, stateVarValue)
        {
        }

        #endregion
    }
}
