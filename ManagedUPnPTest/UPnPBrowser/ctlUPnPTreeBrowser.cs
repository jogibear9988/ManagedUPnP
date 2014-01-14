//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ManagedUPnP;
using ManagedUPnP.Descriptions;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a tree view which lists devices, service, actions and state variables.
    /// </summary>
    public partial class ctlUPnPTreeBrowser : TreeView
    {
        #region Protected Static Locals

        /// <summary>
        /// The device icon bitmap.
        /// </summary>
        protected static Bitmap mbmpDevice;

        /// <summary>
        /// The service icon bitmap.
        /// </summary>
        protected static Bitmap mbmpService;

        /// <summary>
        /// The state variable icon bitmap.
        /// </summary>
        protected static Bitmap mbmpStateVar;

        /// <summary>
        /// The state action icon bitmap.
        /// </summary>
        protected static Bitmap mbmpAction;

        #endregion

        #region Locals

        /// <summary>
        /// The index in the image list to the device icon bitmap.
        /// </summary>
        protected int miDeviceImage;

        /// <summary>
        /// The index in the image list to the service icon bitmap.
        /// </summary>
        protected int miServiceImage;

        /// <summary>
        /// The index in the image list to the state variable icon bitmap.
        /// </summary>
        protected int miStateVarImage;

        /// <summary>
        /// The index in the image list to the action icon bitmap.
        /// </summary>
        protected int miActionImage;

        /// <summary>
        /// True to add service detail for all added devices and services from this point.
        /// </summary>
        protected bool mbAddServiceDetail = true;

        #endregion

        #region Static Initialisation

        /// <summary>
        /// Occurs when static initialisation is required
        /// </summary>
        static ctlUPnPTreeBrowser()
        {
            // Create the standard icon bitmaps
            mbmpDevice = CreateDefaultIcon('D', Color.Blue);
            mbmpService = CreateDefaultIcon('S', Color.Green);
            mbmpStateVar = CreateDefaultIcon('V', Color.Orange);
            mbmpAction = CreateDefaultIcon('A', Color.Red);
        }

        #endregion

        #region Protected Static Methods

        /// <summary>
        /// Creates a default icon bitmap.
        /// </summary>
        /// <param name="text">The char to draw in the icon.</param>
        /// <param name="color">The color of the char in the icon.</param>
        /// <returns>A 16x16 32bpp ARGB bitmap for the icon.</returns>
        protected static Bitmap CreateDefaultIcon(char text, Color color)
        {
            // Create the bitmap
            Bitmap lbmpBitmap = new Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Create the graphics
            using (Graphics lgGraphics = Graphics.FromImage(lbmpBitmap))
            {
                // Create the font
                using (Font lfFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel)) 
                {
                    // Create the brush
                    using (Brush lbBrush = new SolidBrush(color))
                    {
                        // No anti-aliasing - transparency only (no alpha)
                        lgGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                        // Setup a centered string format (horizontally and vertically)
                        StringFormat lsfFormat = new StringFormat();
                        lsfFormat.LineAlignment = StringAlignment.Center;
                        lsfFormat.Alignment = StringAlignment.Center;

                        // Draw the char on the bitmap
                        lgGraphics.DrawString(
                            text.ToString(),
                            lfFont,
                            lbBrush,
                            new Point(7, 7),
                            lsfFormat);
                    }
                }
            }

            return lbmpBitmap;
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new UPnP browser tree view.
        /// </summary>
        public ctlUPnPTreeBrowser()
        {
            InitializeComponent();

            // Add the standard icons
            ilIcons.Images.Add(mbmpDevice);
            miDeviceImage = ilIcons.Images.Count - 1;
            ilIcons.Images.Add(mbmpService);
            miServiceImage = ilIcons.Images.Count - 1;
            ilIcons.Images.Add(mbmpStateVar);
            miStateVarImage = ilIcons.Images.Count - 1;
            ilIcons.Images.Add(mbmpAction);
            miActionImage = ilIcons.Images.Count - 1;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adds a UPnP tree item to the tree view.
        /// </summary>
        /// <param name="nodes">The nodes collection to add the item to.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>The new tree node containing the item.</returns>
        protected TreeNode AddUPnPItem(TreeNodeCollection nodes, IUPnPTreeItem item)
        {
            TreeNode ltnNode = nodes.Add(item.ItemKey, item.VisibleItemText);

            // Get custom icon
            Image liImage = item.ItemIcon;
            int liIndex = 0;

            // If custom icon is available
            if (liImage != null)
                // Then use its index
                liIndex = ilIcons.Images.Add(liImage, Color.White);
            else
            {
                // Otherwise, set the index based on its type
                if (item is UPnPDeviceTreeItem) liIndex = miDeviceImage;
                if (item is UPnPServiceTreeItem) liIndex = miServiceImage;
                if (item is UPnPStateVarTreeItem) liIndex = miStateVarImage;
                if (item is UPnPActionTreeItem) liIndex = miActionImage;
            }

            // Set the image indexes
            ltnNode.ImageIndex = liIndex;
            ltnNode.SelectedImageIndex = liIndex;
            ltnNode.StateImageIndex = liIndex;

            // Set the tag
            ltnNode.Tag = item;

            // Return the node
            return ltnNode;
        }

        /// <summary>
        /// Gets a tree node collection for a device.
        /// </summary>
        /// <param name="device">The device to get the collection for.</param>
        /// <returns>The tree node collection for the device, or null if not found.</returns>
        protected TreeNodeCollection GetDeviceNodeFor(Device device)
        {
            List<Device> ldDevices = new List<Device>();

            // Build device heirarchy list
            Device ldParent = device;
            do
            {
                ldDevices.Insert(0, ldParent);
            } while ((ldParent = ldParent.ParentDevice) != null);

            // Find or add each device to the list
            TreeNodeCollection lnCurrentNodes = this.Nodes;
            bool lbAdding = false;
            foreach (Device ldDevice in ldDevices)
            {
                // Have we found it already
                if (!lbAdding)
                {
                    TreeNode[] lnFoundNodes = lnCurrentNodes.Find(UPnPDeviceTreeItem.KeyFor(ldDevice), false);
                    if (lnFoundNodes.Length == 1) lnCurrentNodes = lnFoundNodes[0].Nodes; else lbAdding = true;
                }

                // Are we adding already
                if (lbAdding)
                {
                    lbAdding = true;
                    lnCurrentNodes = AddUPnPItem(lnCurrentNodes, new UPnPDeviceTreeItem(ldDevice)).Nodes;
                }
            }

            // Return the last collection of nodes for the last node added / found
            return lnCurrentNodes;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates a nodes details from its item.
        /// </summary>
        /// <param name="node">The node to update.</param>
        public void UpdateNode(TreeNode node)
        {
            IUPnPTreeItem liItem = (IUPnPTreeItem)(node.Tag);
            node.Text = liItem.VisibleItemText;
        }

        /// <summary>
        /// Adds a service to the tree view at the correct location.
        /// </summary>
        /// <param name="service">The service to add.</param>
        public void AddService(Service service)
        {
            // Get the device and device node for the service
            Device ldDevice = service.Device;
            TreeNodeCollection lnDeviceNode = GetDeviceNodeFor(service.Device);

            // Add the service node
            TreeNode lnServiceNode = AddUPnPItem(lnDeviceNode, new UPnPServiceTreeItem(service));

            // Get the service description
            ServiceDescription lsdDesc = service.Description();

            // If the description is available
            if (lsdDesc != null && mbAddServiceDetail)
            {
                // Add the actions
                if (lsdDesc.Actions.Count > 0)
                    foreach (ActionDescription laAction in lsdDesc.Actions.Values)
                        AddUPnPItem(lnServiceNode.Nodes, new UPnPActionTreeItem(service, laAction));

                // Add the state variables
                if (lsdDesc.StateVariables.Count > 0)
                    foreach (StateVariableDescription lsvVar in lsdDesc.StateVariables.Values)
                        AddUPnPItem(lnServiceNode.Nodes, new UPnPStateVarTreeItem(service, lsvVar));
            }
        }

        /// <summary>
        /// Removes a device from the tree view.
        /// </summary>
        /// <param name="device">The device to remove.</param>
        public void RemoveDevice(Device device)
        {
            TreeNode[] lnFoundNodes = this.Nodes.Find(UPnPDeviceTreeItem.KeyFor(device), true);

            foreach (TreeNode lnNode in lnFoundNodes)
                lnNode.Remove();
        }

        /// <summary>
        /// Removes a device from the tree view.
        /// </summary>
        /// <param name="udn">The udn for the device to remove.</param>
        public void RemoveDevice(string udn)
        {
            TreeNode[] lnFoundNodes = this.Nodes.Find(udn, true);

            foreach (TreeNode lnNode in lnFoundNodes)
                lnNode.Remove();
        }

        /// <summary>
        /// Removes a service from the tree view.
        /// </summary>
        /// <param name="service">The service to remove.</param>
        public void RemoveService(Service service)
        {
            TreeNode[] lnFoundNodes = this.Nodes.Find(UPnPServiceTreeItem.KeyFor(service), true);

            foreach(TreeNode lnNode in lnFoundNodes)
                lnNode.Remove();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the selected tree item or null if none selected.
        /// </summary>
        public IUPnPTreeItem SelectedItem
        {
            get
            {
                if (this.SelectedNode != null)
                    return (IUPnPTreeItem)this.SelectedNode.Tag;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or sets whether to add actions and state variables for the services.
        /// </summary>
        [DefaultValue(true)]
        public bool AddServiceDetail
        {
            get
            {
                return mbAddServiceDetail;
            }
            set
            {
                mbAddServiceDetail = value;
            }
        }

        #endregion
    }
}
