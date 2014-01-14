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
    /// Encapsulates extension methods for the device class relating to descriptions.
    /// </summary>
    public static class DeviceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the device description for this device from a root description.
        /// </summary>
        /// <param name="device">The device to get the device description for.</param>
        /// <param name="rootDescription">The root description to get the device description for.</param>
        /// <returns>A device description or null if not found.</returns>
        public static DeviceDescription GetDescription(this Device device, RootDescription rootDescription)
        {
            if (rootDescription != null)
                return rootDescription.FindDevice(device.COMDevice.UniqueDeviceName);
            else
                return null;
        }

        /// <summary>
        /// Gets the device description for this devices root device.
        /// </summary>
        /// <param name="device">The device to get the root device description for.</param>
        /// <param name="useCache">True to use cached version, false otherwise.</param>
        /// <returns>The root description for this device or null if not available.</returns>
        public static RootDescription RootDeviceDescription(this Device device, bool useCache = true)
        {
            if (!useCache)
                RootDescriptionCache.Cache.RemoveCacheFor(device.RootDevice);

            return RootDescriptionCache.Cache[device.RootDevice];
        }

        #endregion
    }
}

#endif