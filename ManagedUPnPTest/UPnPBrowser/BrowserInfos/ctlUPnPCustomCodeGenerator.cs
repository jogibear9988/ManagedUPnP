//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ManagedUPnP;
using ManagedUPnP.CodeGen;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a control to allow the user to generate a set
    /// of classes for devices / services.
    /// </summary>
    public partial class ctlUPnPCustomCodeGenerator : UserControl
    {
        #region Protected Locals

        /// <summary>
        /// The current device for which to generate classes.
        /// </summary>
        protected Device mdDevice;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Cerates a new Custom Code Generator.
        /// </summary>
        public ctlUPnPCustomCodeGenerator()
        {
            InitializeComponent();

            cbScope.Value = ClassScope.Public;

            UpdateEnabled();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the service and device class names for 
        /// the checked nodes in the tree.
        /// </summary>
        /// <param name="services">The dictionary to contain the ClassNames, and Services.</param>
        /// <param name="devices">The dictionary to contain the ClassNames, and Devices.</param>
        /// <param name="nodes">The nodes to add the service and device class names for.</param>
        private void GetServiceDeviceClassNames(
            Dictionary<string, Service> services,
            Dictionary<string, Device> devices,
            TreeNodeCollection nodes)
        {
            foreach (TreeNode lnNode in nodes)
            {
                if (lnNode.Checked)
                {
                    IUPnPTreeItem liItem = (IUPnPTreeItem)(lnNode.Tag);

                    if (liItem is UPnPServiceTreeItem)
                        services[liItem.VisibleItemText] = (Service)(liItem.LinkedObject);

                    if (liItem is UPnPDeviceTreeItem)
                        devices[liItem.VisibleItemText] = (Device)(liItem.LinkedObject);
                }
            }

            foreach (TreeNode lnNode in nodes)
                GetServiceDeviceClassNames(services, devices, lnNode.Nodes);
        }

        /// <summary>
        /// Gets the file names for the device and service classes.
        /// </summary>
        /// <param name="services">The ClassName, Service dictionary identifying the services.</param>
        /// <param name="devices">The ClassName, Device dictionary identifying the devices.</param>
        /// <returns>A dictionary containing Serve/Device, FileNames.</returns>
        private Dictionary<object, string> GetFileNames(
            Dictionary<string, Service> services,
            Dictionary<string, Device> devices)
        {
            Dictionary<object, string> ldRet = new Dictionary<object, string>();

            // Get the file extension
            string lsFileExt;
            if (cbPartialClasses.Checked)
                lsFileExt = CodeGenProvider.PartialClassFileExtension;
            else
                lsFileExt = CodeGenProvider.ClassFileExtension;

            // For each device
            foreach (KeyValuePair<string, Device> lkvValue in devices)
            {
                // Generate the file name
                string lsFileName = Path.ChangeExtension(Path.Combine(
                    tbDeviceDestinationFolder.Text,
                    CodeGenProvider.CodeFriendlyIdentifier(lkvValue.Key, false)), lsFileExt);

                // If its a duplicate then tell the user
                if (ldRet.ContainsKey(lsFileName))
                    throw new Exception(
                        string.Format(
                            "The classname filename for Device '{0}' of '{1}' is a duplicate.\r\nPlease revise the class names.",
                            lkvValue.Key, lsFileName));

                // Set the filename
                ldRet[lkvValue.Value] = lsFileName;
            }

            // For each service
            foreach (KeyValuePair<string, Service> lkvValue in services)
            {
                // Generate the file name
                string lsFileName = Path.ChangeExtension(Path.Combine(
                    tbServiceDestinationFolder.Text,
                    CodeGenProvider.CodeFriendlyIdentifier(lkvValue.Key, false)), lsFileExt);

                // If its a duplicate then tell the user
                if (ldRet.ContainsKey(lsFileName))
                    throw new Exception(
                        string.Format(
                            "The classname filename for Service '{0}' or '{1}' is a duplicate.\r\nPlease revise the class names.",
                            lkvValue.Key, lsFileName));

                // Set the file name
                ldRet[lkvValue.Value] = lsFileName;
            }

            // Return the file names
            return ldRet;
        }

        /// <summary>
        /// Gets the selected scope for the classes.
        /// </summary>
        /// <returns>The selected scope.</returns>
        private string GetSelectedScope()
        {
            return cbScope.Items[cbScope.SelectedIndex].ToString();
        }

        /// <summary>
        /// Performs the code generation.
        /// </summary>
        private void PerformCodeGeneration()
        {
            try
            {
                try
                {
                    pbProgress.Value = 0;
                    pbProgress.Visible = true;
                    this.Cursor = Cursors.WaitCursor;

                    Dictionary<string, Service> ldServices = new Dictionary<string, Service>();
                    Dictionary<string, Device> ldDevices = new Dictionary<string, Device>();

                    // Get the class names and file names
                    GetServiceDeviceClassNames(ldServices, ldDevices, tbSelection.Nodes);
                    Dictionary<object, string> ldFileNames = GetFileNames(ldServices, ldDevices);

                    //Build a list of to overwrite files
                    StringBuilder lsbToOverwrite = new StringBuilder();
                    foreach (string lsFileName in ldFileNames.Values)
                        if (File.Exists(lsFileName))
                            lsbToOverwrite.AppendLine(lsFileName);
                    if (lsbToOverwrite.Length > 0)
                    {
                        // Inform user of overwrites and give them a chance to cancel
                        if (MessageBox.Show(
                            string.Format("These files will be overwritten:\r\n\r\n{0}\r\nDo you want to continue?", lsbToOverwrite),
                            "Confirm Overwrite",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            MessageBox.Show("No files were created.", "Complete", MessageBoxButtons.OK);
                            return;
                        }
                    }

                    string lsScope = GetSelectedScope();

                    // Get the service specific class names (all references to non generated services are returned as a Service object)
                    Dictionary<string, string> ldServiceClassNames = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, Service> lkvValue in ldServices)
                        ldServiceClassNames[lkvValue.Value.Id] = lkvValue.Key;

                    // Get the device specific class names (all references to non generated devices are returned as a Device object)
                    Dictionary<string, string> ldDeviceClassNames = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, Device> lkvValue in ldDevices)
                        ldDeviceClassNames[lkvValue.Value.UniqueDeviceName] = lkvValue.Key;

                    pbProgress.Maximum = ldDevices.Count + ldServices.Count;

                    // Genereate devices
                    foreach (KeyValuePair<string, Device> lkvClass in ldDevices)
                    {
                        string lsCode = lkvClass.Value.GenerateClassFor(
                            CodeGenProvider,
                            lkvClass.Key,
                            tbDeviceNamespace.Text,
                            cbScope.Value,
                            cbPartialClasses.Checked,
                            true,
                            tbServiceNamespace.Text,
                            ldDeviceClassNames,
                            ldServiceClassNames
                        );

                        string lsFileName = ldFileNames[lkvClass.Value];
                        File.WriteAllText(lsFileName, lsCode);

                        pbProgress.Value += 1;
                    }

                    // Generate services
                    foreach (KeyValuePair<string, Service> lkvClass in ldServices)
                    {
                        string lsCode = lkvClass.Value.GenerateClassFor(
                            CodeGenProvider,
                            lkvClass.Key,
                            tbServiceNamespace.Text,
                            cbScope.Value,
                            cbPartialClasses.Checked,
                            cbTestStateVars.Checked);

                        string lsFileName = ldFileNames[lkvClass.Value];
                        File.WriteAllText(lsFileName, lsCode);

                        pbProgress.Value += 1;
                    }
                }
                finally
                {
                    pbProgress.Visible = false;
                    this.Cursor = Cursors.Arrow;
                }

                MessageBox.Show("All files were created successfully.", "Complete", MessageBoxButtons.OK);
            }
            catch (Exception loE)
            {
                MessageBox.Show(
                    string.Format(
                        "An error occurred while generateing the code:\r\n\r\n{0}\r\n\r\n" +
                        "Pleaes rectify the error and try again.",
                        loE.Message),
                        "Error Occurred",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the enabled properties of controls.
        /// </summary>
        private void UpdateEnabled()
        {
            bool lbEnabled = true;

            lblRootNamespace.Visible = cgpProvider.CodeGenProvider is VBNetCodeGenProvider;
            tbRootNamespace.Visible = cgpProvider.CodeGenProvider is VBNetCodeGenProvider;

            if (tbRootNamespace.Visible)
                tbRootNamespace.Text = ((VBNetCodeGenProvider)cgpProvider.CodeGenProvider).DefaultRootNamespace;

            // Ensure code generation provider is selected
            if (cgpProvider.CodeGenProvider == null)
                lbEnabled = false;

            // Check device destination folder
            if (tbDeviceDestinationFolder.Text.Length == 0 || !Directory.Exists(tbDeviceDestinationFolder.Text))
                lbEnabled = false;

            // Check service destination folder
            if (tbServiceDestinationFolder.Text.Length == 0 || !Directory.Exists(tbServiceDestinationFolder.Text))
                lbEnabled = false;

            // Check namespace names
            if (tbDeviceNamespace.Text.Length == 0) lbEnabled = false;
            if (tbServiceNamespace.Text.Length == 0) lbEnabled = false;

            // Set the enabled
            cmdGenerate.Enabled = lbEnabled;
        }

        /// <summary>
        /// Adds a device to the selection tree view.
        /// </summary>
        /// <param name="device">The device to add.</param>
        private void AddDevice(Device device)
        {
            foreach(Service lsService in device.Services)
                tbSelection.AddService(lsService);

            if(device.HasChildren)
                foreach(Device ldDevice in device.Children)
                    AddDevice(ldDevice);
        }

        /// <summary>
        /// Sets the device for the entire operation.
        /// </summary>
        /// <param name="device">The new device.</param>
        /// <remarks>Clears all user settings.</remarks>
        private void SetDevice(Device device)
        {
            tbSelection.Nodes.Clear();
            AddDevice(device);
            DefaultNodes(tbSelection.Nodes);
            tbSelection.ExpandAll();

            tbDeviceNamespace.Text = "UPnP" + device.DefaultCodeGenClassName(CodeGenProvider) + ".Devices";
            tbServiceNamespace.Text = "UPnP" + device.DefaultCodeGenClassName(CodeGenProvider) + ".Services";

            UpdateEnabled();
        }

        /// <summary>
        /// Defaults all the values for the nodes including class names.
        /// </summary>
        /// <param name="nodes">The nodes to default.</param>
        private void DefaultNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode lnNode in nodes)
            {
                IUPnPTreeItem liItem = (IUPnPTreeItem)(lnNode.Tag);

                if (liItem.LinkedObject is Service)
                    liItem.VisibleItemText = ((Service)(liItem.LinkedObject)).DefaultCodeGenClassName(CodeGenProvider);

                if (liItem.LinkedObject is Device)
                    liItem.VisibleItemText = ((Device)(liItem.LinkedObject)).DefaultCodeGenClassName(CodeGenProvider);

                tbSelection.UpdateNode(lnNode);
                lnNode.Checked = true;

                DefaultNodes(lnNode.Nodes);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the device for the code generation.
        /// </summary>
        /// <remarks>Changing value resets all user settings.</remarks>
        public Device Device
        {
            get
            {
                return mdDevice;
            }
            set
            {
                if (mdDevice != value)
                {
                    mdDevice = value;
                    SetDevice(mdDevice);
                }
            }
        }

        /// <summary>
        /// Gets or sets the code generation provider to use.
        /// </summary>
        public ICodeGenProvider CodeGenProvider
        {
            get
            {
                return cgpProvider.CodeGenProvider;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles when the user changes a label in the treeview.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void tbSelection_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!e.CancelEdit && e.Label != null)
            {
                IUPnPTreeItem liItem = (IUPnPTreeItem)(e.Node.Tag);
                liItem.VisibleItemText = e.Label;
                tbSelection.UpdateNode(e.Node);
            }
        }

        /// <summary>
        /// Handles when the user presses a key in the treeview.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void tbSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && tbSelection.SelectedNode != null)
                tbSelection.SelectedNode.BeginEdit();
        }

        /// <summary>
        /// Handles when the user clicks a browse button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            // Get the text box for the browse button.
            TextBox ltbTextBox = (sender == cmdDeviceBrowse ? tbDeviceDestinationFolder : tbServiceDestinationFolder);

            // Keep the last location if its not set for this box
            if(ltbTextBox.TextLength != 0)
                fbdBrowse.SelectedPath = ltbTextBox.Text;

            // Show the dialog
            if (fbdBrowse.ShowDialog(this) == DialogResult.OK)
                ltbTextBox.Text = fbdBrowse.SelectedPath;

            UpdateEnabled();
        }

        /// <summary>
        /// Handles when the user clicks the generate button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            UpdateEnabled();
            if (cmdGenerate.Enabled) PerformCodeGeneration();
        }

        /// <summary>
        /// Handles when the user changes the text in a destination foler.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void tbDesintationFolder_TextChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        /// <summary>
        /// Handles when the user changes the Code Generator Provider.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cgpProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        /// <summary>
        /// Occurs when the user changes the root namespace for VB.NET projects.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void tbRootNamespace_TextChanged(object sender, EventArgs e)
        {
            if (cgpProvider.CodeGenProvider is VBNetCodeGenProvider)
                ((VBNetCodeGenProvider)(cgpProvider.CodeGenProvider)).DefaultRootNamespace = tbRootNamespace.Text;
        }

        #endregion
    }
}
