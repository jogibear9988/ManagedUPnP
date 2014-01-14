//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a base UPnP tree item.
    /// </summary>
    public abstract class UPnPTreeItem : IUPnPTreeItem
    {
        #region Protected Locals

        /// <summary>
        /// The current visible item text.
        /// </summary>
        protected string msVisibleItemText = null;

        #endregion

        #region Abstract Properties

        /// <summary>
        /// Gets the linked object to the item.
        /// </summary>
        public abstract object LinkedObject { get; }

        /// <summary>
        /// Gets a newly created information control for the item.
        /// </summary>
        public abstract ctlUPnPInfo InfoControl { get; }

        /// <summary>
        /// Gets the item text for the tree list node.
        /// </summary>
        public abstract string ItemText { get; }

        /// <summary>
        /// Gets the key for teh tree list node.
        /// </summary>
        public abstract string ItemKey { get; }

        /// <summary>
        /// Gets the custom icon for the node.
        /// </summary>
        public abstract System.Drawing.Image ItemIcon { get; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the overrided item text.
        /// </summary>
        public string VisibleItemText
        {
            get
            {
                return (msVisibleItemText == null ? ItemText : msVisibleItemText);
            }
            set
            {
                msVisibleItemText = value;
            }
        }

        #endregion
    }
}
