//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using UPNPLib;

namespace ManagedUPnP
{
    /// <summary>
    /// Extends the IUPnPDevices interface.
    /// </summary>
    internal static class IUPnPDevicesExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the readable info for a devices list.
        /// </summary>
        /// <param name="devices">The list of devices to get the readable info for.</param>
        /// <param name="indent">The indent for the readable info.</param>
        /// <returns>The readable info for the devices.</returns>
        public static string ReadableInfo(this IUPnPDevices devices, int indent = 0)
        {
            StringBuilder lsbBuilder = new StringBuilder();
            string lsIndent = new String(' ', indent * 4);

            if (devices.Count > 0)
            {
                lsbBuilder.AppendLine();
                lsbBuilder.AppendLine(string.Format("{0}{{", lsIndent));

                foreach (IUPnPDevice ldDevice in devices)
                    lsbBuilder.Append(ldDevice.ReadableInfo(indent + 1));

                lsbBuilder.AppendLine(string.Format("{0}}}", lsIndent));

                return lsbBuilder.ToString();
            }
            else
                return "(none)" + Environment.NewLine;
        }

        /// <summary>
        /// Gets all the services for a list of devices.
        /// </summary>
        /// <param name="devices">The list of devices to find the services for.</param>
        /// <param name="serviceType">The type of services to search for.</param>
        /// <returns>The list of services found.</returns>
        public static Services FindServices(this IUPnPDevices devices, string serviceType)
        {
            Services lsServices = new Services();

            foreach (IUPnPDevice ldDevice in devices)
                lsServices.AddRange(ldDevice.FindServices(Guid.Empty, serviceType));

            return lsServices;
        }

        #endregion
    }
}
