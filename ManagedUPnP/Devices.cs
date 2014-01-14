//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using UPNPLib;
using System.Collections;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates a list of devices.
    /// </summary>
    public class Devices : List<Device>
    {
        #region Internal Initialisation

        /// <summary>
        /// Creates a new list of devices from a devices child devices.
        /// </summary>
        /// <param name="device">The device to add the children for.</param>
        /// <param name="interfaceGuid">The network interface Guid of the device or Guid.Empty if unknown.</param>
        /// <param name="includingChildDevices">True to recursively get all devices or false to get immediate devices.</param>
        internal Devices(IUPnPDevice device, Guid interfaceGuid, bool includingChildDevices = false)
            : base()
        {
            AddFrom(device, interfaceGuid, includingChildDevices);
        }

        /// <summary>
        /// Creates a new list of devices from a native devices list.
        /// </summary>
        /// <param name="devices">The native devices to add to the list.</param>
        /// <param name="interfaceGuid">The network interface Guid for the devices or Guid.Empty is unknown.</param>
        internal Devices(IUPnPDevices devices, Guid interfaceGuid)
        {
            foreach(IUPnPDevice ldDevice in devices)
                this.Add(new Device(ldDevice, interfaceGuid));
        }

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates an empty list of devices.
        /// </summary>
        public Devices()
        {
        }

        /// <summary>
        /// Creates a new list of devices from a devices child devices.
        /// </summary>
        /// <param name="device">The device to add the children for.</param>
        /// <param name="includingChildDevices">True to recursively get all devices or false to get immediate devices.</param>
        public Devices(Device device, bool includingChildDevices = false)
            : base()
        {
            AddFrom(device, includingChildDevices);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Adds devices from a devices child devices.
        /// </summary>
        /// <param name="device">The device to add the children for.</param>
        /// <param name="interfaceGuid">The network interface Guid of the device or Guid.Empty if unknown.</param>
        /// <param name="includingChildDevices">True to recursively get all devices or false to get immediate devices.</param>
        internal void AddFrom(IUPnPDevice device, Guid interfaceGuid, bool includingChildDevices = false)
        {
            if(device.HasChildren)
                foreach (UPnPDevice ldDevice in device.Children)
                    Add(new Device(ldDevice, interfaceGuid));

            if (includingChildDevices)
                foreach (IUPnPDevice ldDevice in device.Children)
                    AddFrom(ldDevice, interfaceGuid, includingChildDevices);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds devices from a devices child devices.
        /// </summary>
        /// <param name="device">The device to add the children for.</param>
        /// <param name="includingChildDevices">True to recursively get all devices or false to get immediate devices.</param>
        public void AddFrom(Device device, bool includingChildDevices = false)
        {
            AddFrom(device.COMDevice, device.InterfaceGuid, includingChildDevices);
        }

        /// <summary>
        /// Finds services within this devices list by service type.
        /// </summary>
        /// <param name="serviceType">The service type to get.</param>
        /// <param name="recursive">True to search recursively in device children.</param>
        /// <returns>A new Services object.</returns>
        public Services FindServices(string serviceType, bool recursive)
        {
            Services lsRet = new Services();

            foreach (Device ldDevice in this)
                lsRet.AddRange(ldDevice.Services.FindByType(serviceType));

            if(recursive)
                foreach (Device ldDevice in this)
                    if(ldDevice.HasChildren)
                        lsRet.AddRange(ldDevice.Children.FindServices(serviceType, recursive));

            return lsRet;
        }

        /// <summary>
        /// Adds devices by Type for this list.
        /// </summary>
        /// <param name="type">The Type for the devices to get.</param>
        /// <param name="list">The list to add the devices to.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the type.</returns>
        public void AddDevicesByType(string type, IList list, bool recursive = true)
        {
            foreach (Device ldDevice in this)
                if (ldDevice.Type == type)
                    list.Add(ldDevice);
                else
                {
                    if (recursive && ldDevice.HasChildren)
                        ldDevice.Children.AddDevicesByType(type, list, recursive);
                }
        }

        /// <summary>
        /// Gets the devices by Type for this list recusively.
        /// </summary>
        /// <param name="type">The Type for the devices to get.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the type.</returns>
        public Devices DevicesByType(string type, bool recursive = true)
        {
            Devices ldDevices = new Devices();

            AddDevicesByType(type, ldDevices, recursive);

            return ldDevices;
        }

        /// <summary>
        /// Adds devices by ModelName for this list.
        /// </summary>
        /// <param name="modelName">The ModelName for the devices to get.</param>
        /// <param name="list">The list to add the devices to.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the modelName.</returns>
        public void AddDevicesByModelName(string modelName, IList list, bool recursive = true)
        {
            foreach (Device ldDevice in this)
                if (ldDevice.ModelName == modelName)
                    list.Add(ldDevice);
                else
                {
                    if (recursive && ldDevice.HasChildren)
                        ldDevice.Children.AddDevicesByModelName(modelName, list, recursive);
                }
        }

        /// <summary>
        /// Gets the devices by ModelName for this list recusively.
        /// </summary>
        /// <param name="modelName">The ModelName for the devices to get.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the modelName.</returns>
        public Devices DevicesByModelName(string modelName, bool recursive = true)
        {
            Devices ldDevices = new Devices();

            AddDevicesByModelName(modelName, ldDevices, recursive);

            return ldDevices;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the device by UDN for this list recusively.
        /// </summary>
        /// <param name="udn">The UDN for the device to get.</param>
        /// <returns>The Device if UDN is found or null if not.</returns>
        public Device this[string udn]
        {
            get
            {
                foreach (Device ldDevice in this)
                    if (ldDevice.UniversalProductCode == udn)
                        return ldDevice;
                    else
                    {
                        if (ldDevice.HasChildren)
                        {
                            Device ldSearch = ldDevice.Children[udn];
                            if (ldSearch != null) return ldSearch;
                        }
                    }

                return null;
            }
        }

        #endregion
    }
}
