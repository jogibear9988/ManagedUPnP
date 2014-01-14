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
    /// Extends the IUPnPService extensions.
    /// </summary>
    internal static class IUPnPServiceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the readable info for a native com service.
        /// </summary>
        /// <param name="service">The service to get the readable info for.</param>
        /// <param name="indent">The indent for the readable info.</param>
        /// <returns>The readable info for the native com serivce.</returns>
        public static string ReadableInfo(this IUPnPService service, int indent = 0)
        {
            return string.Format(
                "{0}{1} ({2})",
                new String(' ', indent * 4),
                service.Id,
                service.ServiceTypeIdentifier);
        }

        /// <summary>
        /// Gets the friendly service type identifier for a native com service.
        /// </summary>
        /// <param name="service">The native com service.</param>
        /// <returns>The service type identifier for the native com service.</returns>
        public static string GetFriendlyServiceTypeIdentifier(this IUPnPService service)
        {
            string[] lsComponents = service.ServiceTypeIdentifier.Split(':');

            if (lsComponents.Length > 2)
                return String.Format("{0}:{1}", lsComponents[lsComponents.Length - 2], lsComponents[lsComponents.Length - 1]);
            else
                return service.ServiceTypeIdentifier;
        }

        #endregion
    }
}
