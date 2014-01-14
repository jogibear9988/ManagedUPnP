namespace ManagedUPnPTest
{
    partial class ctlUPnPCustomCodeGenerator
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tbSelection = new ManagedUPnPTest.ctlUPnPTreeBrowser();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbScope = new ManagedUPnPTest.UPnPBrowser.ctlClassScopeComboBox();
            this.cgpProvider = new ManagedUPnPTest.UPnPBrowser.ctlCodeGenProviderComboBox();
            this.lblCodeGenProvider = new System.Windows.Forms.Label();
            this.lblScope = new System.Windows.Forms.Label();
            this.lblServiceNamespace = new System.Windows.Forms.Label();
            this.lblDeviceNamespace = new System.Windows.Forms.Label();
            this.tbServiceNamespace = new System.Windows.Forms.TextBox();
            this.tbDeviceNamespace = new System.Windows.Forms.TextBox();
            this.cbTestStateVars = new System.Windows.Forms.CheckBox();
            this.cbPartialClasses = new System.Windows.Forms.CheckBox();
            this.gbSetup = new System.Windows.Forms.GroupBox();
            this.cmdServiceBrowse = new System.Windows.Forms.Button();
            this.cmdDeviceBrowse = new System.Windows.Forms.Button();
            this.tbServiceDestinationFolder = new System.Windows.Forms.TextBox();
            this.lblServiceDestination = new System.Windows.Forms.Label();
            this.tbDeviceDestinationFolder = new System.Windows.Forms.TextBox();
            this.lblDeviceDestination = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.fbdBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.lblRootNamespace = new System.Windows.Forms.Label();
            this.tbRootNamespace = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.gbSetup.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tbSelection, 0, 2);
            this.tlpMain.Controls.Add(this.gbOptions, 0, 1);
            this.tlpMain.Controls.Add(this.gbSetup, 0, 0);
            this.tlpMain.Controls.Add(this.pnlBottom, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tlpMain.Size = new System.Drawing.Size(811, 613);
            this.tlpMain.TabIndex = 0;
            // 
            // tbSelection
            // 
            this.tbSelection.AddServiceDetail = false;
            this.tbSelection.CheckBoxes = true;
            this.tbSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSelection.ImageIndex = 0;
            this.tbSelection.LabelEdit = true;
            this.tbSelection.Location = new System.Drawing.Point(3, 298);
            this.tbSelection.Name = "tbSelection";
            this.tbSelection.SelectedImageIndex = 0;
            this.tbSelection.Size = new System.Drawing.Size(805, 275);
            this.tbSelection.TabIndex = 2;
            this.tbSelection.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tbSelection_AfterLabelEdit);
            this.tbSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSelection_KeyDown);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.lblRootNamespace);
            this.gbOptions.Controls.Add(this.tbRootNamespace);
            this.gbOptions.Controls.Add(this.cbScope);
            this.gbOptions.Controls.Add(this.cgpProvider);
            this.gbOptions.Controls.Add(this.lblCodeGenProvider);
            this.gbOptions.Controls.Add(this.lblScope);
            this.gbOptions.Controls.Add(this.lblServiceNamespace);
            this.gbOptions.Controls.Add(this.lblDeviceNamespace);
            this.gbOptions.Controls.Add(this.tbServiceNamespace);
            this.gbOptions.Controls.Add(this.tbDeviceNamespace);
            this.gbOptions.Controls.Add(this.cbTestStateVars);
            this.gbOptions.Controls.Add(this.cbPartialClasses);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOptions.Location = new System.Drawing.Point(3, 76);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(805, 216);
            this.gbOptions.TabIndex = 1;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbScope
            // 
            this.cbScope.BoxedValue = null;
            this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScope.FormattingEnabled = true;
            this.cbScope.Location = new System.Drawing.Point(109, 190);
            this.cbScope.Name = "cbScope";
            this.cbScope.Size = new System.Drawing.Size(157, 21);
            this.cbScope.TabIndex = 9;
            this.cbScope.Value = ManagedUPnP.CodeGen.ClassScope.Private;
            // 
            // cgpProvider
            // 
            this.cgpProvider.CodeGenProvider = null;
            this.cgpProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cgpProvider.FormattingEnabled = true;
            this.cgpProvider.Location = new System.Drawing.Point(70, 19);
            this.cgpProvider.Name = "cgpProvider";
            this.cgpProvider.Size = new System.Drawing.Size(334, 21);
            this.cgpProvider.TabIndex = 1;
            this.cgpProvider.SelectedIndexChanged += new System.EventHandler(this.cgpProvider_SelectedIndexChanged);
            // 
            // lblCodeGenProvider
            // 
            this.lblCodeGenProvider.AutoSize = true;
            this.lblCodeGenProvider.Location = new System.Drawing.Point(9, 22);
            this.lblCodeGenProvider.Name = "lblCodeGenProvider";
            this.lblCodeGenProvider.Size = new System.Drawing.Size(55, 13);
            this.lblCodeGenProvider.TabIndex = 0;
            this.lblCodeGenProvider.Text = "Language";
            // 
            // lblScope
            // 
            this.lblScope.AutoSize = true;
            this.lblScope.Location = new System.Drawing.Point(37, 193);
            this.lblScope.Name = "lblScope";
            this.lblScope.Size = new System.Drawing.Size(66, 13);
            this.lblScope.TabIndex = 8;
            this.lblScope.Text = "Class Scope";
            // 
            // lblServiceNamespace
            // 
            this.lblServiceNamespace.AutoSize = true;
            this.lblServiceNamespace.Location = new System.Drawing.Point(2, 166);
            this.lblServiceNamespace.Name = "lblServiceNamespace";
            this.lblServiceNamespace.Size = new System.Drawing.Size(103, 13);
            this.lblServiceNamespace.TabIndex = 6;
            this.lblServiceNamespace.Text = "Service Namespace";
            // 
            // lblDeviceNamespace
            // 
            this.lblDeviceNamespace.AutoSize = true;
            this.lblDeviceNamespace.Location = new System.Drawing.Point(2, 140);
            this.lblDeviceNamespace.Name = "lblDeviceNamespace";
            this.lblDeviceNamespace.Size = new System.Drawing.Size(101, 13);
            this.lblDeviceNamespace.TabIndex = 4;
            this.lblDeviceNamespace.Text = "Device Namespace";
            // 
            // tbServiceNamespace
            // 
            this.tbServiceNamespace.Location = new System.Drawing.Point(109, 163);
            this.tbServiceNamespace.Name = "tbServiceNamespace";
            this.tbServiceNamespace.Size = new System.Drawing.Size(242, 20);
            this.tbServiceNamespace.TabIndex = 7;
            // 
            // tbDeviceNamespace
            // 
            this.tbDeviceNamespace.Location = new System.Drawing.Point(109, 137);
            this.tbDeviceNamespace.Name = "tbDeviceNamespace";
            this.tbDeviceNamespace.Size = new System.Drawing.Size(242, 20);
            this.tbDeviceNamespace.TabIndex = 5;
            // 
            // cbTestStateVars
            // 
            this.cbTestStateVars.AutoSize = true;
            this.cbTestStateVars.Checked = true;
            this.cbTestStateVars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTestStateVars.Location = new System.Drawing.Point(3, 103);
            this.cbTestStateVars.Name = "cbTestStateVars";
            this.cbTestStateVars.Size = new System.Drawing.Size(286, 17);
            this.cbTestStateVars.TabIndex = 3;
            this.cbTestStateVars.Text = "Test service state variables before adding as properties";
            this.cbTestStateVars.UseVisualStyleBackColor = true;
            // 
            // cbPartialClasses
            // 
            this.cbPartialClasses.AutoSize = true;
            this.cbPartialClasses.Location = new System.Drawing.Point(3, 80);
            this.cbPartialClasses.Name = "cbPartialClasses";
            this.cbPartialClasses.Size = new System.Drawing.Size(153, 17);
            this.cbPartialClasses.TabIndex = 2;
            this.cbPartialClasses.Text = "Generate as partial classes";
            this.cbPartialClasses.UseVisualStyleBackColor = true;
            // 
            // gbSetup
            // 
            this.gbSetup.Controls.Add(this.cmdServiceBrowse);
            this.gbSetup.Controls.Add(this.cmdDeviceBrowse);
            this.gbSetup.Controls.Add(this.tbServiceDestinationFolder);
            this.gbSetup.Controls.Add(this.lblServiceDestination);
            this.gbSetup.Controls.Add(this.tbDeviceDestinationFolder);
            this.gbSetup.Controls.Add(this.lblDeviceDestination);
            this.gbSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSetup.Location = new System.Drawing.Point(3, 3);
            this.gbSetup.Name = "gbSetup";
            this.gbSetup.Size = new System.Drawing.Size(805, 67);
            this.gbSetup.TabIndex = 0;
            this.gbSetup.TabStop = false;
            this.gbSetup.Text = "Setup";
            // 
            // cmdServiceBrowse
            // 
            this.cmdServiceBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdServiceBrowse.Location = new System.Drawing.Point(724, 39);
            this.cmdServiceBrowse.Name = "cmdServiceBrowse";
            this.cmdServiceBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdServiceBrowse.TabIndex = 5;
            this.cmdServiceBrowse.Text = "Browse";
            this.cmdServiceBrowse.UseVisualStyleBackColor = true;
            this.cmdServiceBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // cmdDeviceBrowse
            // 
            this.cmdDeviceBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeviceBrowse.Location = new System.Drawing.Point(724, 15);
            this.cmdDeviceBrowse.Name = "cmdDeviceBrowse";
            this.cmdDeviceBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdDeviceBrowse.TabIndex = 2;
            this.cmdDeviceBrowse.Text = "Browse";
            this.cmdDeviceBrowse.UseVisualStyleBackColor = true;
            this.cmdDeviceBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // tbServiceDestinationFolder
            // 
            this.tbServiceDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServiceDestinationFolder.Location = new System.Drawing.Point(172, 41);
            this.tbServiceDestinationFolder.Name = "tbServiceDestinationFolder";
            this.tbServiceDestinationFolder.Size = new System.Drawing.Size(546, 20);
            this.tbServiceDestinationFolder.TabIndex = 4;
            this.tbServiceDestinationFolder.TextChanged += new System.EventHandler(this.tbDesintationFolder_TextChanged);
            // 
            // lblServiceDestination
            // 
            this.lblServiceDestination.AutoSize = true;
            this.lblServiceDestination.Location = new System.Drawing.Point(9, 44);
            this.lblServiceDestination.Name = "lblServiceDestination";
            this.lblServiceDestination.Size = new System.Drawing.Size(159, 13);
            this.lblServiceDestination.TabIndex = 3;
            this.lblServiceDestination.Text = "Service Class Destination Folder";
            // 
            // tbDeviceDestinationFolder
            // 
            this.tbDeviceDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeviceDestinationFolder.Location = new System.Drawing.Point(172, 17);
            this.tbDeviceDestinationFolder.Name = "tbDeviceDestinationFolder";
            this.tbDeviceDestinationFolder.Size = new System.Drawing.Size(546, 20);
            this.tbDeviceDestinationFolder.TabIndex = 1;
            this.tbDeviceDestinationFolder.TextChanged += new System.EventHandler(this.tbDesintationFolder_TextChanged);
            // 
            // lblDeviceDestination
            // 
            this.lblDeviceDestination.AutoSize = true;
            this.lblDeviceDestination.Location = new System.Drawing.Point(9, 20);
            this.lblDeviceDestination.Name = "lblDeviceDestination";
            this.lblDeviceDestination.Size = new System.Drawing.Size(157, 13);
            this.lblDeviceDestination.TabIndex = 0;
            this.lblDeviceDestination.Text = "Device Class Destination Folder";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pbProgress);
            this.pnlBottom.Controls.Add(this.cmdGenerate);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(3, 579);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(805, 31);
            this.pnlBottom.TabIndex = 4;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(102, 2);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(697, 23);
            this.pbProgress.TabIndex = 1;
            this.pbProgress.Visible = false;
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Location = new System.Drawing.Point(3, 3);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(93, 23);
            this.cmdGenerate.TabIndex = 0;
            this.cmdGenerate.Text = "Generate Files";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // fbdBrowse
            // 
            this.fbdBrowse.Description = "Select Files Location";
            // 
            // lblRootNamespace
            // 
            this.lblRootNamespace.AutoSize = true;
            this.lblRootNamespace.Location = new System.Drawing.Point(65, 49);
            this.lblRootNamespace.Name = "lblRootNamespace";
            this.lblRootNamespace.Size = new System.Drawing.Size(90, 13);
            this.lblRootNamespace.TabIndex = 10;
            this.lblRootNamespace.Text = "Root Namespace";
            // 
            // tbRootNamespace
            // 
            this.tbRootNamespace.Location = new System.Drawing.Point(161, 46);
            this.tbRootNamespace.Name = "tbRootNamespace";
            this.tbRootNamespace.Size = new System.Drawing.Size(243, 20);
            this.tbRootNamespace.TabIndex = 11;
            this.tbRootNamespace.TextChanged += new System.EventHandler(this.tbRootNamespace_TextChanged);
            // 
            // ctlUPnPCustomCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ctlUPnPCustomCodeGenerator";
            this.Size = new System.Drawing.Size(811, 613);
            this.tlpMain.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.gbSetup.ResumeLayout(false);
            this.gbSetup.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ctlUPnPTreeBrowser tbSelection;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbPartialClasses;
        private System.Windows.Forms.CheckBox cbTestStateVars;
        private System.Windows.Forms.Label lblServiceNamespace;
        private System.Windows.Forms.Label lblDeviceNamespace;
        private System.Windows.Forms.TextBox tbServiceNamespace;
        private System.Windows.Forms.TextBox tbDeviceNamespace;
        private System.Windows.Forms.GroupBox gbSetup;
        private System.Windows.Forms.Button cmdDeviceBrowse;
        private System.Windows.Forms.TextBox tbDeviceDestinationFolder;
        private System.Windows.Forms.Label lblDeviceDestination;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowse;
        private System.Windows.Forms.Button cmdServiceBrowse;
        private System.Windows.Forms.TextBox tbServiceDestinationFolder;
        private System.Windows.Forms.Label lblServiceDestination;
        private System.Windows.Forms.Label lblScope;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.ProgressBar pbProgress;
        private UPnPBrowser.ctlCodeGenProviderComboBox cgpProvider;
        private System.Windows.Forms.Label lblCodeGenProvider;
        private UPnPBrowser.ctlClassScopeComboBox cbScope;
        private System.Windows.Forms.Label lblRootNamespace;
        private System.Windows.Forms.TextBox tbRootNamespace;
    }
}
