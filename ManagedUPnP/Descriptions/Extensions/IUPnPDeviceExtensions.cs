//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System.Xml;
using UPNPLib;
using System;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Extends the IUPnPDevice interface.
    /// </summary>
    internal static class IUPnPDeviceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the device description for a device.
        /// </summary>
        /// <param name="device">The device to get the description for.</param>
        /// <param name="rootDescription">The root description for the device.</param>
        /// <returns>The device description or null if not found in the root description.</returns>
        public static DeviceDescription Description(this IUPnPDevice device, RootDescription rootDescription)
        {
            if (rootDescription != null)
                return rootDescription.FindDevice(device.UniqueDeviceName);
            else
                return null;
        }

        /// <summary>
        /// Gets the root description for a device.
        /// </summary>
        /// <param name="device">The device to get the root description.</param>
        /// <returns>The root description for the device.</returns>
        public static RootDescription RootDeviceDescription(this IUPnPDevice device)
        {
            if (Logging.Enabled)
                Logging.Log(device, String.Format("Getting RootDeviceDescription for {0}", device.FriendlyName), 1);

            try
            {
                string lsURL = device.GetDocumentURL();

                if (lsURL.Length > 0)
                {
                    if (Logging.Enabled)
                        Logging.Log(device, String.Format("Getting Document URL: '{0}'", lsURL));

                    try
                    {
                        using (XmlTextReader lrReader = Utils.GetXMLTextReader(lsURL))
                        {
                            if (Logging.Enabled)
                                Logging.Log(device, "Finding start node");

                            while (lrReader.Read())
                                if (RootDescription.IsStartNodeFor(lrReader)) break;

                            if (RootDescription.IsStartNodeFor(lrReader))
                            {
                                if (Logging.Enabled)
                                    Logging.Log(device, "Start node found, processing description");
                                return new RootDescription(lsURL, lrReader);
                            }
                            else
                            {
                                if (Logging.Enabled)
                                    Logging.Log(device, "Start node NOT found");
                                return null;
                            }
                        }
                    }
                    catch (Exception loE)
                    {
                        if (Logging.Enabled)
                            Logging.Log(device, String.Format("Downloading and processing of URL failed with error: {0}", loE.ToString()));
                        throw;
                    }
                }
                else
                {
                    if (Logging.Enabled)
                        Logging.Log(device, String.Format("Document URL Invalid: '{0}'", lsURL));
                    return null;
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(device, System.String.Format("Finished getting RootDeviceDescription for {0}", device.FriendlyName), -1);
            }
        }

        #endregion
    }
}

#endif