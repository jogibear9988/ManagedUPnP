//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

// This interface is only required if compiling under Windows XP SP3,
// the UPnP.DLL exports this interface in Windows Vista and higher
#if Development_WindowsXP
using System;
using System.Runtime.InteropServices;

namespace UPNPLib
{
    /// <summary>
    /// Encapsulates the IUPnPServiceCallback COM interface.
    /// </summary>
    [ComVisible(true), ComImport,
    Guid("31FADCA9-AB73-464B-B67D-5C1D0F83C8B8"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUPnPServiceCallback
    {
        /// <summary>
        /// Occurs when a state variable has changed.
        /// </summary>
        /// <param name="service">The service for which the state variable belongs.</param>
        /// <param name="stateVarName">The state variable name that changed.</param>
        /// <param name="stateVarValue">The new value for the state variable.</param>
        [PreserveSig]
        void StateVariableChanged(UPnPService service, string stateVarName, object stateVarValue);

        /// <summary>
        /// Occurs when a service instance dies.
        /// </summary>
        /// <param name="service">The service that died.</param>
        [PreserveSig]
        void ServiceInstanceDied(UPnPService service);
    }
}
#endif