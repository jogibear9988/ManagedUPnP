//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using UPNPLib;
using System;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Extends the IUPnPService extensions.
    /// </summary>
    internal static class IUPnPServiceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the description for a service.
        /// </summary>
        /// <param name="service">The native com service.</param>
        /// <param name="device">The native com device.</param>
        /// <param name="rootDescription">The root description for the services device.</param>
        /// <returns>The service description.</returns>
        public static ServiceDescription Description(
            this IUPnPService service, IUPnPDevice device, RootDescription rootDescription)
        {
            if (Logging.Enabled)
                Logging.Log(service, String.Format("Getting ServiceDescription for {0} -> {1}", device.FriendlyName, service.Id), 1);

            try
            {
                DeviceServiceDescription ldsdDesc = service.DeviceServiceDescription(device, rootDescription);

                if (ldsdDesc != null)
                {
                    ServiceDescription lsdDesc = ldsdDesc.GetDescription(rootDescription);

                    if (lsdDesc != null)
                    {
                        if (Logging.Enabled)
                        {
                            if (Logging.Enabled)
                                Logging.Log(service, "ServiceDescription:", 1);

                            try
                            {
                                if (Logging.Enabled)
                                    Logging.Log(service, lsdDesc.ToString());
                            }
                            finally
                            {
                                if (Logging.Enabled)
                                    Logging.Log(service, "End ServiceDescription", -1);
                            }
                        }
                    }
                    else
                        if (Logging.Enabled)
                            Logging.Log(service, "ServiceDescription not found");

                    return lsdDesc;
                }
                else
                {
                    if (Logging.Enabled)
                        Logging.Log(service, "Failed - Device Service Description Not Located");
                    return null;
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(service, String.Format("Finished getting ServiceDescription for {0} -> {1}", device.FriendlyName, service.Id), -1);
            }
        }

        /// <summary>
        /// Gets the device service description for a service.
        /// </summary>
        /// <param name="service">The service to get the description for.</param>
        /// <param name="device">The device to get the description for.</param>
        /// <param name="rootDescription">The root description for the device.</param>
        /// <returns>The device service description.</returns>
        public static DeviceServiceDescription DeviceServiceDescription(
            this IUPnPService service, IUPnPDevice device, RootDescription rootDescription)
        {
            if (Logging.Enabled)
                Logging.Log(service, String.Format("Getting DeviceServiceDescription for {0} -> {1}", device.FriendlyName, service.Id), 1);

            try
            {
                if (Logging.Enabled)
                {
                    if (Logging.Enabled)
                        Logging.Log(service, "Using RootDescription:", 1);

                    try
                    {
                        if (Logging.Enabled)
                            Logging.Log(service, rootDescription.ToString());
                    }
                    finally
                    {
                        if (Logging.Enabled)
                            Logging.Log(service, "End RootDescription", -1);
                    }
                }

                DeviceServiceDescription ldsdDesc = null;

                if (rootDescription != null)
                {
                    if (Logging.Enabled)
                        Logging.Log(service, string.Format("Finding device by UDN: '{0}'", device.UniqueDeviceName));
                    DeviceDescription lddDevice = rootDescription.FindDevice(device.UniqueDeviceName);

                    if (lddDevice != null)
                    {
                        if (Logging.Enabled)
                            Logging.Log(service, string.Format("Device Found, finding DeviceServiceDescription by Service ID: '{0}'", service.Id));

                        if (Logging.Enabled)
                        {
                            if (Logging.Enabled)
                                Logging.Log(service, "DeviceDescription:", 1);

                            try
                            {
                                if (Logging.Enabled)
                                    Logging.Log(service, lddDevice.ToString());
                            }
                            finally
                            {
                                if (Logging.Enabled)
                                    Logging.Log(service, "End DeviceDescription", -1);
                            }
                        }

                        lddDevice.DeviceServices.TryGetValue(service.Id, out ldsdDesc);

                        if (ldsdDesc != null)
                        {
                            if (Logging.Enabled)
                            {
                                if (Logging.Enabled)
                                    Logging.Log(service, "DeviceServiceDescription found:", 1);

                                try
                                {
                                    if (Logging.Enabled)
                                        Logging.Log(service, ldsdDesc.ToString());
                                }
                                finally
                                {
                                    if (Logging.Enabled)
                                        Logging.Log(service, "End DeviceServiceDescription", -1);
                                }
                            }
                        }
                        else
                            if (Logging.Enabled)
                                Logging.Log(service, "DeviceServiceDescripton not found");
                    }
                    else
                        if (Logging.Enabled)
                            Logging.Log(service, "Device not found");
                }
                else
                    if (Logging.Enabled)
                        Logging.Log(service, "Invalid rootDescription parameter"); 

                return ldsdDesc;
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(service, String.Format("Finished getting DeviceServiceDescription for {0} -> {1}", device.FriendlyName, service.Id), -1);
            }
        }

        #endregion
    }
}

#endif