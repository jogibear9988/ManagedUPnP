//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Drawing;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates the methods and properties for UPnP tree list node.
    /// </summary>
    public interface IUPnPTreeItem
    {
        /// <summary>
        /// Gets the linked object to the item.
        /// </summary>
        object LinkedObject { get; }

        /// <summary>
        /// Gets the item text for the tree list node.
        /// </summary>
        string ItemText { get; }

        /// <summary>
        /// Gets or sets the overrided item text.
        /// </summary>
        string VisibleItemText { get; set; }
        
        /// <summary>
        /// Gets the key for teh tree list node.
        /// </summary>
        string ItemKey { get; }

        /// <summary>
        /// Gets the custom icon for the node.
        /// </summary>
        Image ItemIcon { get; }

        /// <summary>
        /// Gets a newly created information control for the item.
        /// </summary>
        ctlUPnPInfo InfoControl { get; }
    }
}
