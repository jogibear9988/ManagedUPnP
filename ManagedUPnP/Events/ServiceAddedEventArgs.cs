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
    public class ServiceAddedEventArgs : EventArgs
    {
        #region Private Locals

        /// <summary>
        /// The native com device for the service added.
        /// </summary>
        private IUPnPDevice mdCOMDevice;

        /// <summary>
        /// The native com service for the service added.
        /// </summary>
        private IUPnPService msCOMService;

        /// <summary>
        /// The service added.
        /// </summary>
        private Service msService;

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
        /// Creates a new service added event arguments.
        /// </summary>
        /// <param name="device">The underlying COM device that was added.</param>
        /// <param name="service">The underlying COM service that was added.</param>
        internal ServiceAddedEventArgs(IUPnPDevice device, IUPnPService service)
        {
            mbInterfaceGuidAvailable = false;
            mdCOMDevice = device;
            msCOMService = service;
        }

        /// <summary>
        /// Creates a new service added event arguments.
        /// </summary>
        /// <param name="deviceArgs">The underlying device args from which the service came.</param>
        /// <param name="service">The underlying COM service that was added.</param>
        internal ServiceAddedEventArgs(DeviceAddedEventArgs deviceArgs, IUPnPService service)
        {
            mbInterfaceGuidAvailable = deviceArgs.InterfaceGuidAvailable;
            if (mbInterfaceGuidAvailable) mgInterfaceGuid = deviceArgs.InterfaceGuid; 
            mdCOMDevice = deviceArgs.COMDevice;
            msCOMService = service;
        }

        /// <summary>
        /// Creates a new service added event arguments.
        /// </summary>
        /// <param name="device">The underlying COM device that was added.</param>
        /// <param name="interfaceGuid">The network interface guid.</param>
        /// <param name="service">The service which was added.</param>
        internal ServiceAddedEventArgs(IUPnPDevice device, Guid interfaceGuid, IUPnPService service)
        {
            mdCOMDevice = device;
            msCOMService = service;
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

        /// <summary>
        /// The native underlying COM service.
        /// </summary>
        internal IUPnPService COMService
        {
            get
            {
                return msCOMService;
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
        /// Gets the service for the service added event arguments.
        /// </summary>
        public Service Service
        {
            get
            {
                if (msService == null) msService = new Service(mdCOMDevice, mgInterfaceGuid, msCOMService);
                return msService;
            }
        }

        #endregion
    }
}
