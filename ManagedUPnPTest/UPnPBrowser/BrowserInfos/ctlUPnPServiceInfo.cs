//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Text;
using ManagedUPnP;
using ManagedUPnP.CodeGen;
using ManagedUPnP.Descriptions;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates the info control for UPnP service.
    /// </summary>
    public partial class ctlUPnPServiceInfo : ctlUPnPInfo
    {
        #region Protected Locals

        /// <summary>
        /// True if code generation has already been created.
        /// </summary>
        protected bool mbCodeGenerationDone = false;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creats a new UPnP service info control.
        /// </summary>
        public ctlUPnPServiceInfo()
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

            if (miItem != null)
            {
                UPnPServiceTreeItem ldiItem = (UPnPServiceTreeItem)miItem;
                Service lsService = ((Service)(miItem.LinkedObject));
                lsbInfo.AppendLine(lsService.Description().ToString());
            }

            rtbInfo.Text = lsbInfo.ToString();
        }

        #endregion

        #region Private Methods

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

                    Service lsService = ((Service)(miItem.LinkedObject));

                    StringBuilder lsbClass = new StringBuilder();

                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    try
                    {
                        lsbClass.Append(
                            lsService.GenerateClassFor(
                                cgpProvider.CodeGenProvider,
                                null, "UPnPServices", ClassScope.Internal, false, false));

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
