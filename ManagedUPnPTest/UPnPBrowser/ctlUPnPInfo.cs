//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates the base control for UPnP information.
    /// </summary>
    public partial class ctlUPnPInfo : UserControl
    {
        #region Locals

        /// <summary>
        /// The tree item for this information.
        /// </summary>
        protected IUPnPTreeItem miItem;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new UPnP information control.
        /// </summary>
        public ctlUPnPInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Virtual Methods

        /// <summary>
        /// Updates the data for the control.
        /// </summary>
        protected virtual void UpdateData()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the tree item for the control.
        /// </summary>
        public IUPnPTreeItem Item
        {
            get
            {
                return miItem;
            }
            set
            {
                if (miItem != value)
                {
                    miItem = value;
                    UpdateData();
                }
            }
        }

        #endregion
    }
}
