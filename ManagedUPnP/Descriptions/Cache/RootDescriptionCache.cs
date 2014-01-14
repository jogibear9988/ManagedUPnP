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
    /// Encapsulates a cache for the root descriptions of devices.
    /// </summary>
    public class RootDescriptionCache
    {
        #region Private Static Locals

        /// <summary>
        /// The static root description cache.
        /// </summary>
        private static RootDescriptionCache mrdcCache = new RootDescriptionCache();

        #endregion

        #region Private Locals

        /// <summary>
        /// The cache dictionary using the UDN as a key.
        /// </summary>
        private Dictionary<string, RootDescription> mdCache = new Dictionary<string, RootDescription>();

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets the root description cache.
        /// </summary>
        public static RootDescriptionCache Cache
        {
            get
            {
                return mrdcCache;
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the key for a device.
        /// </summary>
        /// <param name="device">The com native device to get the key for.</param>
        /// <returns>The key for a device.</returns>
        internal string KeyFor(IUPnPDevice device)
        {
            return device.RootDevice.UniqueDeviceName;
        }

        /// <summary>
        /// Gets the root description for a com native device.
        /// </summary>
        /// <param name="device">The device to get root description for.</param>
        /// <returns>The root description for the device.</returns>
        internal RootDescription this[IUPnPDevice device]
        {
            get
            {
                RootDescription lrdDesc;
                string lsKey = KeyFor(device);

                if (Logging.Enabled)
                    Logging.Log(this, string.Format("Getting RootDescription from cache for: '{0}' root of '{1}' key '{2}'", device.RootDevice.FriendlyName, device.FriendlyName, lsKey), 1);

                try
                {
                    if (!mdCache.TryGetValue(lsKey, out lrdDesc))
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, "Cache missed");
                        lrdDesc = device.RootDeviceDescription();

                        if (Logging.Enabled)
                            Logging.Log(this, "Adding to cache");
                        mdCache[lsKey] = lrdDesc;
                    }
                    else
                        if (Logging.Enabled)
                            Logging.Log(this, "Cache hit");

                    return lrdDesc;
                }
                finally
                {
                    if (Logging.Enabled)
                        Logging.Log(this, "Finished getting RootDescription from cache", -1);
                }
            }
        }

        /// <summary>
        /// Revmoves the cache for a com native device.
        /// </summary>
        /// <param name="device">The device to remove the cache for.</param>
        internal void RemoveCacheFor(IUPnPDevice device)
        {
            string lsKey = KeyFor(device);
            if (Logging.Enabled)
                Logging.Log(this, string.Format("Removing RootDescription cache for: '{0}' root of '{1}' key '{2}'", device.RootDevice.FriendlyName, device.FriendlyName, lsKey));
            mdCache.Remove(lsKey);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            if (Logging.Enabled)
                Logging.Log(this, "Clearing RootDescription cache");
            mdCache.Clear();
        }

        /// <summary>
        /// Removes the cache item for a device
        /// </summary>
        /// <param name="device">The device to remove the cache for.</param>
        public void RemoveCacheFor(Device device)
        {
            RemoveCacheFor(device.COMDevice);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the root description for a device.
        /// </summary>
        /// <param name="device">The device to get the root description for.</param>
        /// <returns>The root description for the device.</returns>
        public RootDescription this[Device device]
        {
            get
            {
                return this[device.COMDevice];
            }
        }

        #endregion
    }
}

#endif