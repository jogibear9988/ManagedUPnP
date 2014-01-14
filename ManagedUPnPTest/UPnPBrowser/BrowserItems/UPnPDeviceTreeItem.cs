//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using ManagedUPnP;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a tree item which represnets a device.
    /// </summary>
    public class UPnPDeviceTreeItem : UPnPTreeItem
    {
        #region Locals

        /// <summary>
        /// The device for this tree item.
        /// </summary>
        protected Device mdDevice;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets the tree item key for a device.
        /// </summary>
        /// <param name="device">The device to get the key for.</param>
        /// <returns>A string.</returns>
        public static string KeyFor(Device device)
        {
            return String.Format("DEVICE:{0}", device.UniqueDeviceName);
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new device tree item.
        /// </summary>
        /// <param name="device">The device for the tree item.</param>
        public UPnPDeviceTreeItem(Device device)
        {
            mdDevice = device;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the linked object to the item.
        /// </summary>
        public override object LinkedObject
        {
            get
            {
                return mdDevice;
            }
        }

        /// <summary>
        /// Gets a newly created information control for the item.
        /// </summary>
        public override ctlUPnPInfo InfoControl 
        {
            get
            {
                ctlUPnPDeviceInfo ldiInfo = new ctlUPnPDeviceInfo();
                ldiInfo.Item = this;
                return ldiInfo;
            }
        }

        /// <summary>
        /// Gets the key for teh tree list node.
        /// </summary>
        public override string ItemKey
        {
            get
            {
                return KeyFor(mdDevice);
            }
        }

        /// <summary>
        /// Gets the item text for the tree list node.
        /// </summary>
        public override string ItemText
        {
            get
            {
                return mdDevice.FriendlyName;
            }
        }

        /// <summary>
        /// Gets the custom icon for the node.
        /// </summary>
        public override Image ItemIcon
        {
            get
            {
                return mdDevice.GetIconImage(null, 16, 16);
            }
        }

        #endregion
    }
}
