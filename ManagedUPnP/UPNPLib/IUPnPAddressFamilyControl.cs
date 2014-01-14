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
    /// Encapsulates the IUPnPAddressFamilyControl COM interface.
    /// </summary>
    [ComVisible(true), ComImport,
    Guid("E3BF6178-694E-459F-A5A6-191EA0FFA1C7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUPnPAddressFamilyControl
    {
        /// <summary>
        /// Gets the address family current set.
        /// </summary>
        /// <param name="addressFamiy">The address family.</param>
        [PreserveSig]
        void GetAddressFamily([Out] int addressFamiy);

        /// <summary>
        /// Sets the address family.
        /// </summary>
        /// <param name="addressFamiy">The address family.</param>
        [PreserveSig]
        void SetAddressFamily([In] int addressFamiy);
    }
}
#endif