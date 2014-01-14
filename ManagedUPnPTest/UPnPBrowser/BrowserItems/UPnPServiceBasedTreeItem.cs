//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Drawing;
using ManagedUPnP;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a UPnPTreeItem which is linked to a service
    /// </summary>
    public abstract class UPnPServiceBasedTreeItem : UPnPTreeItem
    {
        #region Locals

        /// <summary>
        /// The service linked to this item.
        /// </summary>
        protected Service msService;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new service based tree item.
        /// </summary>
        /// <param name="service">The service for the item.</param>
        public UPnPServiceBasedTreeItem(Service service)
        {
            msService = service;
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
                return msService;
            }
        }

        #endregion
    }

}
