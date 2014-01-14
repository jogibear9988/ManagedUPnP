//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Xml;
using UPNPLib;

namespace ManagedUPnP
{
    /// <summary>
    /// Extends the IUPnPDevice interface.
    /// </summary>
    internal static class IUPnPDeviceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets all readable info as text for a device.
        /// </summary>
        /// <param name="device">The com device to get the info for.</param>
        /// <param name="indent">The indent for the information.</param>
        /// <returns>The readable info for the device.</returns>
        public static string ReadableInfo(this IUPnPDevice device, int indent = 0)
        {
            return string.Format(
                "{1}Name: {2} ({3}){0}" +
                "{1}Description: {4}{0}" +
                "{1}Manufacturer: {5} ({6}){0}" +
                "{1}Model: {7} - {8} ({9}){0}" +
                "{1}Serial: {10}{0}" +
                "{1}Unique Device Name: {11}{0}" +
                "{1}UPC: {12}{0}" +
                "{1}Description URL: {13}{0}" +
                "{1}Devices{14}" +
                "{1}Services{15}",
                Environment.NewLine,
                new String(' ', indent * 4),
                device.FriendlyName,
                device.Type,
                device.Description,
                device.ManufacturerName,
                device.ManufacturerURL,
                device.ModelName,
                device.ModelNumber,
                device.ModelURL,
                device.SerialNumber,
                device.UniqueDeviceName,
                device.UPC,
                device.GetDocumentURL(),
                device.Children.ReadableInfo(indent),
                device.Services.ReadableInfo(indent));
        }

        /// <summary>
        /// Gets the document URL for a native com device.
        /// </summary>
        /// <param name="device">The native com device to get the document URL for.</param>
        /// <returns>The URL for the document.</returns>
        public static string GetDocumentURL(this IUPnPDevice device)
        {
            if (device is IUPnPDeviceDocumentAccess)
                return ((IUPnPDeviceDocumentAccess)device).GetDocumentURL();
            else
                return String.Empty;
        }

        /// <summary>
        /// Finds services for a device.
        /// </summary>
        /// <param name="device">The device to get the services for.</param>
        /// <param name="interfaceGuid">The network guid for any new devices if available.</param>
        /// <param name="serviceType">The service type for the services to search for.</param>
        /// <returns>The services found.</returns>
        public static Services FindServices(this IUPnPDevice device, Guid interfaceGuid, string serviceType)
        {
            Services lsServices = new Services();

            foreach (IUPnPService lsService in device.Services)
                if (lsService.ServiceTypeIdentifier == serviceType)
                    lsServices.Add(new Service(device, interfaceGuid, lsService));

            foreach (IUPnPDevice ldDevice in device.Children)
                lsServices.AddRange(ldDevice.FindServices(interfaceGuid, serviceType));

            return lsServices;
        }

        #endregion
    }
}
