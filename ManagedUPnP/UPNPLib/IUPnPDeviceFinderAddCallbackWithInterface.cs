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
    /// Encapsulates the IUPnPDeviceFinderAddCallbackWithInterface COM interface.
    /// </summary>
    [ComVisible(true), ComImport,
    Guid("983DFC0B-1796-44DF-8975-CA545B620EE5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUPnPDeviceFinderAddCallbackWithInterface
    {
        /// <summary>
        /// Called when a device is added with network interface information.
        /// </summary>
        /// <param name="findData">The find data handler for which the device was found.</param>
        /// <param name="device">The device that was found.</param>
        /// <param name="guidInterface">The network itnerface for the device.</param>
        [PreserveSig]
        void DeviceAddedWithInterface(int findData, UPnPDevice device, ref Guid guidInterface);
    }
}
#endif