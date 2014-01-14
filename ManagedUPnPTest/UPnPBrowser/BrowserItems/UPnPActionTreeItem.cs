//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using ManagedUPnP;
using ManagedUPnP.Descriptions;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a tree item which represents an action.
    /// </summary>
    public class UPnPActionTreeItem : UPnPServiceBasedTreeItem
    {
        #region Locals

        /// <summary>
        /// The action description for the action for this tree item.
        /// </summary>
        protected ActionDescription mdDesc;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new action tree item.
        /// </summary>
        /// <param name="service">The service for this action tree item.</param>
        /// <param name="desc">The action description for the action for this tree item.</param>
        public UPnPActionTreeItem(Service service, ActionDescription desc)
            : base(service)
        {
            mdDesc = desc;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the item text for the tree list node.
        /// </summary>
        public override string ItemText
        {
            get
            {
                return mdDesc.Name;
            }
        }

        /// <summary>
        /// Gets a newly created information control for the item.
        /// </summary>
        public override ctlUPnPInfo InfoControl
        {
            get
            {
                ctlUPnPActionInfo laiInfo = new ctlUPnPActionInfo();
                laiInfo.Item = this;
                return laiInfo;
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

        /// <summary>
        /// Gets the key for teh tree list node.
        /// </summary>
        public override string ItemKey
        {
            get
            {
                return String.Format(
                    "ACTION:{0}.{1}.{2}",
                    msService.Device.UniqueDeviceName,
                    msService.ServiceTypeIdentifier,
                    mdDesc.Name);
            }
        }

        /// <summary>
        /// Gets the name of the action for this action tree item.
        /// </summary>
        public string ActionName
        {
            get
            {
                return mdDesc.Name;
            }
        }

        #endregion
    }
}
