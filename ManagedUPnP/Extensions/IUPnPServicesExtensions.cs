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
    /// Extends the IUPnPServices interface.
    /// </summary>
    internal static class IUPnPServicesExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the readable info for a native com service.
        /// </summary>
        /// <param name="services">The services to get the readable info for.</param>
        /// <param name="indent">The indent for the readable info.</param>
        /// <returns>The readable info for the native com serivce.</returns>
        public static string ReadableInfo(this IUPnPServices services, int indent = 0)
        {
            StringBuilder lsbBuilder = new StringBuilder();
            string lsIndent = new String(' ', indent * 4);

            if (services.Count > 0)
            {
                lsbBuilder.AppendLine();
                lsbBuilder.AppendLine(string.Format("{0}{{", lsIndent));

                try
                {
                    foreach (IUPnPService ldService in services)
                        lsbBuilder.AppendLine(ldService.ReadableInfo(indent + 1));
                }
                catch (Exception loE)
                {
                    if (Logging.Enabled)
                        Logging.Log(services, String.Format("Getting readable info for services failed with exception: '{0}'", loE.ToString()));
                }

                lsbBuilder.AppendLine(string.Format("{0}}}", lsIndent));

                return lsbBuilder.ToString();
            }
            else
                return "(none)" + Environment.NewLine;
        }

        #endregion
    }
}
