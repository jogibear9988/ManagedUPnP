//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Text;
using ManagedUPnP;
using ManagedUPnP.Descriptions;
using ManagedUPnP.CodeGen;
using System.Net;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates the info control for UPnP device.
    /// </summary>
    public partial class ctlUPnPDeviceInfo : ctlUPnPInfo
    {
        #region Protected Locals

        /// <summary>
        /// True if code generation has already been created.
        /// </summary>
        protected bool mbCodeGenerationDone = false;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creats a new UPnP device info control.
        /// </summary>
        public ctlUPnPDeviceInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Updates the data in the control from its item.
        /// </summary>
        protected override void UpdateData()
        {
            StringBuilder lsbInfo = new StringBuilder();
            cgMain.Device = null;

            if (miItem != null)
            {
                UPnPDeviceTreeItem ldiItem = (UPnPDeviceTreeItem)miItem;
                Device ldDevice = ((Device)(miItem.LinkedObject));

                lsbInfo.AppendLine(AdapterAddressInformation(ldDevice));
                lsbInfo.AppendLine(ldDevice.GetDescription(ldDevice.RootDeviceDescription()).ToString());
                lsbInfo.AppendLine();
                lsbInfo.AppendLine(ldDevice.ToString());

                cgMain.Device = ldDevice;
            }

            rtbInfo.Text = lsbInfo.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the adapter address information.
        /// </summary>
        /// <param name="device">The device to get the adapter address information for.</param>
        /// <returns>The address information for the device.</returns>
        private static string AdapterAddressInformation(Device device)
        {
            StringBuilder lsbDestination = new StringBuilder();
            IPAddress[] lipAddresses = device.AdapterIPAddresses;
            if (lipAddresses != null && lipAddresses.Length > 0)
            {
                string lsIndent = new string(' ', 2);
                lsbDestination.AppendLine("Available on Network Interface IP Addresses: ");
                foreach (IPAddress lipAddress in lipAddresses)
                {
                    lsbDestination.Append(lsIndent);
                    lsbDestination.AppendLine(lipAddress.ToString());
                }
            }

            return lsbDestination.ToString();
        }
        
        /// <summary>
        /// Generates code for the device and displays it.
        /// </summary>
        /// <param name="force">True to force generation even if its already been done once.</param>
        private void GenerateCode(bool force = false)
        {
            if (!mbCodeGenerationDone || force)
            {
                if (miItem != null)
                {
                    mbCodeGenerationDone = true;

                    Device ldDevice = ((Device)(miItem.LinkedObject));

                    StringBuilder lsbClass = new StringBuilder();

                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    try
                    {
                        lsbClass.Append(
                            ldDevice.GenerateClassFor(
                                cgpProvider.CodeGenProvider,
                                null, "UPnPDevices", ClassScope.Internal, false, false, null));
                        rtbClass.Text = lsbClass.ToString();
                    }
                    finally
                    {
                        this.Cursor = this.DefaultCursor;
                    }

                }
                else
                    rtbClass.Text = "(Code generation not available)";
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Occurs when the tab control page is changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void tcMain_Selected(object sender, System.Windows.Forms.TabControlEventArgs e)
        {
            if (e.TabPage == tpClass)
                GenerateCode();
        }

        /// <summary>
        /// Occurs when the user changes the code generation provider.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cgpProvider_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tcMain.SelectedTab == tpClass)
                GenerateCode(true);
        }

        #endregion
    }
}
