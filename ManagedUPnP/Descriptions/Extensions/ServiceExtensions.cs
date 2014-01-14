//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates extension methods for the service class relating to descriptions.
    /// </summary>
    public static class ServiceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the description for this service from a root description.
        /// </summary>
        /// <param name="service">The service to get the service description for.</param>
        /// <param name="useCache">True to use cached version, false otherwise.</param>
        /// <returns>A ServiceDescription.</returns>
        public static ServiceDescription Description(this Service service, bool useCache = true)
        {
            if (!useCache)
                ServiceDescriptionCache.Cache.RemoveCacheFor(service);

            return ServiceDescriptionCache.Cache[service];
        }

        /// <summary>
        /// Gets the device service description from a root description.
        /// </summary>
        /// <param name="service">The service to get the device service description for.</param>
        /// <param name="rootDescription">The root description for the service.</param>
        /// <returns>A DeviceServiceDescription.</returns>
        public static DeviceServiceDescription DeviceServiceDescription(this Service service, RootDescription rootDescription)
        {
            return service.COMService.DeviceServiceDescription(service.COMDevice, rootDescription);
        }

        #endregion
    }
}

#endif