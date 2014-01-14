//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UPNPLib;
using System;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates a list of services.
    /// </summary>
    public class Services : List<Service>
    {
        #region Internal Initialisation

        /// <summary>
        /// Creates a new services list from a native device.
        /// </summary>
        /// <param name="device">The device to add the services for.</param>
        /// <param name="interfaceGuid">The network interface Guid for the device.</param>
        /// <param name="serviceType">The service type to add or null for all.</param>
        /// <param name="includingChildDevices">True to add all child devices services.</param>
        internal Services(IUPnPDevice device, Guid interfaceGuid, string serviceType = null, bool includingChildDevices = false)
            : base()
        {
            AddFrom(device, interfaceGuid, serviceType, includingChildDevices);
        }

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates an empty services list.
        /// </summary>
        public Services()
        {
        }

        /// <summary>
        /// Creates a new services list from a device.
        /// </summary>
        /// <param name="device">The device to add the services for.</param>
        /// <param name="serviceType">The service type to add or null for all.</param>
        /// <param name="includingChildDevices">True to add all child devices services.</param>
        public Services(Device device, string serviceType = null, bool includingChildDevices = false)
            : base()
        {
            AddFrom(device, serviceType, includingChildDevices);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Adds services from a native device.
        /// </summary>
        /// <param name="device">The device to add the services for.</param>
        /// <param name="interfaceGuid">The network interface Guid for the device.</param>
        /// <param name="serviceType">The service type to add or null for all.</param>
        /// <param name="includingChildDevices">True to add all child devices services.</param>
        internal void AddFrom(IUPnPDevice device, Guid interfaceGuid, string serviceType = null, bool includingChildDevices = false)
        {
            if (Logging.Enabled)
                Logging.Log(
                    this, 
                    string.Format(
                        "Adding services for device: '{0}', ServiceType:'{1}' ChildDevices:'{2}'", 
                        device.FriendlyName, 
                        (serviceType == null ? "(null)" : serviceType),
                        includingChildDevices), 
                    1);

            try
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Scanning services", 1);

                try
                {
                    foreach (IUPnPService lsService in device.Services)
                    {
                        try
                        {
                            if (Logging.Enabled)
                                Logging.Log(this, string.Format("Service found: ID:'{0}', Type:'{1}'", lsService.Id, lsService.ServiceTypeIdentifier));

                            if (serviceType == null || lsService.ServiceTypeIdentifier == serviceType)
                            {
                                if (Logging.Enabled)
                                    Logging.Log(this, "Service accepted - matched");
                                Add(new Service(device, interfaceGuid, lsService));
                                if (Logging.Enabled)
                                    Logging.Log(this, "Service added");
                            }
                            else
                                if (Logging.Enabled)
                                    Logging.Log(this, "Service rejected - not matched");
                        }
                        catch (Exception loE)
                        {
                            if (Logging.Enabled)
                                Logging.Log(this, string.Format("Error occurred while adding service: '{0}'", loE.ToString()));
                        }
                    }
                }
                catch (Exception loE)
                {
                    if (Logging.Enabled)
                        Logging.Log(this, string.Format("Error occurred while scanning services: '{0}'", loE.ToString()));
                }

                if (Logging.Enabled)
                    Logging.Log(this, "Finished scanning services", -1);

                if (includingChildDevices)
                {
                    if (Logging.Enabled)
                        Logging.Log(this, "Scanning child devices", 1);

                    try
                    {
                        foreach (IUPnPDevice ldDevice in device.Children)
                        {
                            try
                            {
                                if (Logging.Enabled)
                                    Logging.Log(this, string.Format("Device found: Name:'{0}', UDN:'{1}'", ldDevice.FriendlyName, ldDevice.UniqueDeviceName));
                                AddFrom(ldDevice, interfaceGuid, serviceType, includingChildDevices);
                            }
                            catch (Exception loE)
                            {
                                if (Logging.Enabled)
                                    Logging.Log(this, string.Format("Error occurred while adding device: '{0}'", loE.ToString()));
                            }
                        }
                    }
                    catch (Exception loE)
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, string.Format("Error occurred while scanning child devices: '{0}'", loE.ToString()));
                    }

                    if (Logging.Enabled)
                        Logging.Log(this, "Finished scanning child devices", -1);
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Finished adding services for device", -1);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds services from a device.
        /// </summary>
        /// <param name="device">The device to add the services for.</param>
        /// <param name="serviceType">The service type to add or null for all.</param>
        /// <param name="includingChildDevices">True to add all child devices services.</param>
        public void AddFrom(Device device, string serviceType = null, bool includingChildDevices = false)
        {
            AddFrom(device.COMDevice, device.InterfaceGuid, serviceType, includingChildDevices);
        }

        /// <summary>
        /// Finds all services of a certain type.
        /// </summary>
        /// <param name="serviceType">The type to get the services for.</param>
        /// <returns>A new Services object.</returns>
        public Services FindByType(string serviceType)
        {
            Services lsRet = new Services();

            foreach (Service lsService in this)
                if (string.IsNullOrEmpty(serviceType) || serviceType == lsService.ServiceTypeIdentifier)
                    lsRet.Add(lsService);

            return lsRet;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the service by Id for this list.
        /// </summary>
        /// <param name="id">The id for the service to get.</param>
        /// <returns>The Service if id is found or null if not.</returns>
        public Service this[string id]
        {
            get
            {
                foreach (Service lsService in this)
                    if (lsService.Id == id)
                        return lsService;

                return null;
            }
        }

        #endregion
    }
}
