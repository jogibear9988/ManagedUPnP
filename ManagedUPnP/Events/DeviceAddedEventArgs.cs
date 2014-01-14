//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using UPNPLib;

namespace ManagedUPnP
{
    /// <summary>
    /// The arguments for the device added event.
    /// </summary>
    public class DeviceAddedEventArgs : EventArgs
    {
        #region Private Locals

        /// <summary>
        /// The native com device for the device added.
        /// </summary>
        private IUPnPDevice mdCOMDevice;

        /// <summary>
        /// The device added.
        /// </summary>
        private Device mdDevice;

        /// <summary>
        /// The network interface Guid for the device.
        /// </summary>
        private Guid mgInterfaceGuid;

        /// <summary>
        /// True if the network interface Guid is available.
        /// </summary>
        private bool mbInterfaceGuidAvailable;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new device added event arguments.
        /// </summary>
        /// <param name="device">The underlying COM device that was added.</param>
        internal DeviceAddedEventArgs(IUPnPDevice device)
        {
            mbInterfaceGuidAvailable = false;
            mdCOMDevice = device;
        }

        /// <summary>
        /// Creates a new device added event arguments.
        /// </summary>
        /// <param name="device">The underlying COM device that was added.</param>
        /// <param name="baseArgs">The base args to get other event information from.</param>
        internal DeviceAddedEventArgs(IUPnPDevice device, DeviceAddedEventArgs baseArgs)
        {
            mbInterfaceGuidAvailable = baseArgs.InterfaceGuidAvailable;
            mgInterfaceGuid = baseArgs.mgInterfaceGuid;
            mdCOMDevice = device;
        }

        /// <summary>
        /// Creates a new device added event arguments.
        /// </summary>
        /// <param name="device">The underlying COM device that was added.</param>
        /// <param name="interfaceGuid">The network interface guid.</param>
        internal DeviceAddedEventArgs(IUPnPDevice device, Guid interfaceGuid)
        {
            mdCOMDevice = device;
            mgInterfaceGuid = interfaceGuid;
            mbInterfaceGuidAvailable = true;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// The native underlying COM device.
        /// </summary>
        internal IUPnPDevice COMDevice
        {
            get
            {
                return mdCOMDevice;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the network interface Guid if its available.
        /// </summary>
        public Guid InterfaceGuid
        {
            get
            {
                if (InterfaceGuidAvailable)
                    return mgInterfaceGuid;
                else
                    throw new InvalidOperationException("Interface GUID is not available.");
            }
        }

        /// <summary>
        /// Gets whether the network interface Guid is available.
        /// </summary>
        public bool InterfaceGuidAvailable
        {
            get
            {
                return mbInterfaceGuidAvailable;
            }
        }

        /// <summary>
        /// Gets the device for the device added event arguments.
        /// </summary>
        public Device Device
        {
            get
            {
                if (mdDevice == null) mdDevice = new Device(mdCOMDevice, mgInterfaceGuid);
                return mdDevice;
            }
        }

        #endregion
    }
}
