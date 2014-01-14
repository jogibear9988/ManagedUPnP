//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System.Collections.Generic;
using UPNPLib;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Gets the description cache for a service.
    /// </summary>
    public class ServiceDescriptionCache
    {
        #region Private Static Locals

        /// <summary>
        /// The static cache for the service descriptions.
        /// </summary>
        private static ServiceDescriptionCache msdcCache = new ServiceDescriptionCache();

        #endregion

        #region Private Locals

        /// <summary>
        /// The cache dictionary using the UDN and service ID as a key.
        /// </summary>
        private Dictionary<string, ServiceDescription> mdCache = new Dictionary<string, ServiceDescription>();

        /// <summary>
        /// The cache dictionary by URL insead of ID.
        /// </summary>
        private Dictionary<string, ServiceDescription> mdURLCache = new Dictionary<string, ServiceDescription>();

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Public gets the service description cache object.
        /// </summary>
        public static ServiceDescriptionCache Cache
        {
            get
            {
                return msdcCache;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the key for a native device and service.
        /// </summary>
        /// <param name="device">The native device.</param>
        /// <param name="service">The native service.</param>
        /// <returns>The key for the device and service.</returns>
        private string KeyFor(IUPnPDevice device, IUPnPService service)
        {
            return device.RootDevice.UniqueDeviceName + "|" + service.Id;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Removes the cache for a native dvice and service.
        /// </summary>
        /// <param name="device">The native device.</param>
        /// <param name="service">The native service.</param>
        internal void RemoveCacheFor(IUPnPDevice device, IUPnPService service)
        {
            string lsKey = KeyFor(device, service);

            if (Logging.Enabled)
                Logging.Log(this, string.Format("Getting ServiceDescription cache for: '{0}' root of '{1}' key '{2}'", device.RootDevice.FriendlyName, service.Id, lsKey), 1);

            ServiceDescription lsdDesc = null;
            if (mdCache.TryGetValue(lsKey, out lsdDesc))
                mdURLCache.Remove(lsdDesc.DocumentURL);

            mdCache.Remove(lsKey);
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the service description for a native device and service.
        /// </summary>
        /// <param name="device">The native device.</param>
        /// <param name="service">The native service.</param>
        /// <returns>The service description for the cache or null if not available.</returns>
        internal ServiceDescription this[IUPnPDevice device, IUPnPService service]
        {
            get
            {
                ServiceDescription lsdDesc;
                string lsKey = KeyFor(device, service);

                if (Logging.Enabled)
                    Logging.Log(this, string.Format("Getting ServiceDescription from cache for: '{0}' root of '{1}' key '{2}'", device.RootDevice.FriendlyName, service.Id, lsKey), 1);

                try
                {
                    if (!mdCache.TryGetValue(lsKey, out lsdDesc))
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, "Cache missed");
                        lsdDesc = service.Description(device, RootDescriptionCache.Cache[device.RootDevice]);

                        if (lsdDesc != null)
                        {
                            if (Logging.Enabled)
                                Logging.Log(this, "Adding to cache");
                            mdCache[lsKey] = lsdDesc;
                            mdURLCache[lsdDesc.DocumentURL] = lsdDesc;
                        }
                        else
                            if (Logging.Enabled)
                                Logging.Log(this, "Failed, nothing to add to cache");
                    }

                    return lsdDesc;
                }
                finally
                {
                    if (Logging.Enabled)
                        Logging.Log(this, "Finished getting ServiceDescription from cache", -1);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            if (Logging.Enabled)
                Logging.Log(this, "Clearing ServiceDescription cache");
            mdCache.Clear();
        }

        /// <summary>
        /// Removes the cache for a service.
        /// </summary>
        /// <param name="service">The service to remove the cache for.</param>
        public void RemoveCacheFor(Service service)
        {
            RemoveCacheFor(service.COMDevice, service.COMService);
        }

        /// <summary>
        /// Gets an item from the cache by URL.
        /// </summary>
        /// <param name="url">The URL to get the item from.</param>
        /// <returns>The service description found or null if none.</returns>
        public ServiceDescription ByURL(string url)
        {
            ServiceDescription lsdDesc = null;
            mdURLCache.TryGetValue(url, out lsdDesc);
            return lsdDesc;
        }

        /// <summary>
        /// Gets an item from the cache by URL.
        /// </summary>
        /// <param name="url">The URL to get the item from.</param>
        /// <param name="desc">The description to add the URL cache.</param>
        /// <returns>The service description found or null if none.</returns>
        public void AddURL(string url, ServiceDescription desc)
        {
            mdURLCache[url] = desc;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the service description for service.
        /// </summary>
        /// <param name="service">The service to get the cache for.</param>
        public ServiceDescription this[Service service]
        {
            get
            {
                return this[service.COMDevice, service.COMService];
            }
        }

        #endregion
    }
}

#endif