//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;
using ManagedUPnP;
using System.Diagnostics;
using System;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a form which allows the user to browse UPnP devices.
    /// </summary>
    public partial class frmUPnPBrowser : Form
    {
        #region Private Locals

        /// <summary>
        /// Handles discovery of the services.
        /// </summary>
        private AutoEventedDiscoveryServices<Service> mdsServices;

        /// <summary>
        /// The current UPnP info control being displayed.
        /// </summary>
        private ctlUPnPInfo miInfo = null;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new UPnP browser.
        /// </summary>
        public frmUPnPBrowser()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Occurs when the form loads.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void frmManagedUPnPTest_Load(object sender, System.EventArgs e)
        {
            // Setup Managed UPnP Logging
            ManagedUPnP.Logging.LogLines += new LogLinesEventHandler(Logging_LogLines);
            ManagedUPnP.Logging.Enabled = true;

            // Create discovery for all service and device types
            mdsServices = new AutoEventedDiscoveryServices<Service>(null);

            // Try to resolve network interfaces if OS supports it
            mdsServices.ResolveNetworkInterfaces = true;

            // Assign events
            mdsServices.CanCreateServiceFor += new AutoEventedDiscoveryServices<Service>.
                CanCreateServiceForEventHandler(dsServices_CanCreateServiceFor);

            mdsServices.CreateServiceFor += new AutoEventedDiscoveryServices<Service>.
                CreateServiceForEventHandler(dsServices_CreateServiceFor);

            mdsServices.StatusNotifyAction += new AutoEventedDiscoveryServices<Service>.
                StatusNotifyActionEventHandler(dsServices_StatusNotifyAction);

            ManagedUPnP.WindowsFirewall.CheckUPnPFirewallRules(null);

            // Start async discovery
            mdsServices.ReStartAsync();
        }

        /// <summary>
        /// Occurs when the main form is closing.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void frmUPnPBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Disable Managed UPnP Logging
            ManagedUPnP.Logging.Enabled = false;
            ManagedUPnP.Logging.LogLines -= new LogLinesEventHandler(Logging_LogLines);
        }

        /// <summary>
        /// Occurs when a Managed UPnP log entry is raised.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void Logging_LogLines(object sender, LogLinesEventArgs a)
        {
            string lsDateTime = DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] ");
            string lsLineStart = lsDateTime + new String(' ', a.Indent * 4);
            txtLog.AppendLog(lsLineStart + a.Lines.Replace("\r\n", "\r\n" + lsLineStart) + "\r\n");
        }

        /// <summary>
        /// Occurs when a notify action occurs for the dicovery object.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void dsServices_StatusNotifyAction(object sender, AutoEventedDiscoveryServices<Service>.StatusNotifyActionEventArgs a)
        {
            switch (a.NotifyAction)
            {
                case AutoDiscoveryServices<Service>.NotifyAction.ServiceAdded:
                    // A new service was found, add it
                    tvUPnP.AddService((Service)(a.Data));
                    break;

                case AutoDiscoveryServices<Service>.NotifyAction.DeviceRemoved:
                    // A device has been removed, remove it and all services
                    tvUPnP.RemoveDevice((String)(a.Data));
                    break;

                case AutoDiscoveryServices<Service>.NotifyAction.ServiceRemoved:
                    // A service was removed, remove it
                    tvUPnP.RemoveService((Service)(a.Data));
                    break;
            }
        }

        /// <summary>
        /// Occurs when the discovery object wants a new auto service created.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void dsServices_CreateServiceFor(object sender, AutoEventedDiscoveryServices<Service>.CreateServiceForEventArgs a)
        {
            a.CreatedAutoService = a.Service;
        }

        /// <summary>
        /// Occurs when the discovery object needs to determine if an auto service can be created.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void dsServices_CanCreateServiceFor(object sender, AutoEventedDiscoveryServices<Service>.CanCreateServiceForEventArgs a)
        {
            a.CanCreate = true;
        }

        /// <summary>
        /// Occurs after an item in the UPnP browser tree view is selected.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void tvUPnP_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get the selection tree item.
            IUPnPTreeItem liItem = tvUPnP.SelectedItem;

            // Save the currently showed control
            ctlUPnPInfo liPrev = miInfo;

            // Default to no new control
            miInfo = null;

            try
            {
                // If the new item is available
                if (liItem != null)
                {
                    // Get the control
                    miInfo = liItem.InfoControl;

                    // If the control is available
                    if (miInfo != null)
                    {
                        // Dock it and add it
                        miInfo.Dock = DockStyle.Fill;
                        pnlInfo.Controls.Add(miInfo);
                    }
                }
            }
            finally
            {
                // If the old control was available
                if (liPrev != null)
                {
                    // Remove it and dispose it
                    pnlInfo.Controls.Remove(liPrev);
                    liPrev.Dispose();
                }
            }
        }

        #endregion
    }
}
