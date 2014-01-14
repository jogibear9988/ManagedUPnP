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
    /// Encapsulates a tree item which represnets a service.
    /// </summary>
    public class UPnPServiceTreeItem : UPnPServiceBasedTreeItem
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the tree item key for a service.
        /// </summary>
        /// <param name="service">The service to get the key for.</param>
        /// <returns>A string.</returns>
        public static string KeyFor(Service service)
        {
            return String.Format("SERVICE:{0}", service.Key);
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new service tree item.
        /// </summary>
        /// <param name="service">The service for the tree item.</param>
        public UPnPServiceTreeItem(Service service)
            : base(service)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a newly created information control for the item.
        /// </summary>
        public override ctlUPnPInfo InfoControl
        {
            get
            {
                ctlUPnPServiceInfo lsiInfo = new ctlUPnPServiceInfo();
                lsiInfo.Item = this;
                return lsiInfo;
            }
        }

        /// <summary>
        /// Gets the item text for the tree list node.
        /// </summary>
        public override string ItemText
        {
            get
            {
                return msService.FriendlyServiceTypeIdentifier;
            }
        }

        /// <summary>
        /// Gets the key for teh tree list node.
        /// </summary>
        public override string ItemKey
        {
            get
            {
                return KeyFor(msService);
            }
        }

        /// <summary>
        /// Gets the custom icon for the node.
        /// </summary>
        public override Image ItemIcon
        {
            get
            {
                return null;
            }
        }

        #endregion
    }

}
