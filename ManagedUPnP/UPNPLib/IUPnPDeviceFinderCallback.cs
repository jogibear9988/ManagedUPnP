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
    /// Encapsulates the IUPnPDeviceFinderCallback COM Interface.
    /// </summary>
    [ComVisible(true), ComImport,
    Guid("415A984A-88B3-49F3-92AF-0508BEDF0D6C"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IUPnPDeviceFinderCallback
    {
        /// <summary>
        /// Occurs when a new device is added.
        /// </summary>
        /// <param name="findData">The find data handler for which the device was found.</param>
        /// <param name="device">The device that was found.</param>
        [PreserveSig]
        void DeviceAdded(int findData, UPnPDevice device);

        /// <summary>
        /// Occurs when a device is removed.
        /// </summary>
        /// <param name="findData">The find data handler for which the device was found.</param>
        /// <param name="udn">The UDN for the device that was removed.</param>
        [PreserveSig]
        void DeviceRemoved(int findData, string udn);

        /// <summary>
        /// Occurs when the search for discovery has completed its initial scan.
        /// </summary>
        /// <param name="findData">The find data handler for which the device was found.</param>
        [PreserveSig]
        void SearchComplete(int findData);
    }
}
#endif