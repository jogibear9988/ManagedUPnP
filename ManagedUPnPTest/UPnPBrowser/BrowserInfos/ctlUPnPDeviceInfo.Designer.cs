namespace ManagedUPnPTest
{
    partial class ctlUPnPDeviceInfo
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
            ManagedUPnP.CodeGen.CSharpCodeGenProvider cSharpCodeGenProvider1 = new ManagedUPnP.CodeGen.CSharpCodeGenProvider();
            this.rtbInfo = new ManagedUPnPTest.ctlLinkRichTextBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.tpClass = new System.Windows.Forms.TabPage();
            this.tlpCodeGeneration = new System.Windows.Forms.TableLayoutPanel();
            this.rtbClass = new ManagedUPnPTest.ctlLinkRichTextBox();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.cgpProvider = new ManagedUPnPTest.UPnPBrowser.ctlCodeGenProviderComboBox();
            this.lblCodeGenProvider = new System.Windows.Forms.Label();
            this.tpGenerateClasses = new System.Windows.Forms.TabPage();
            this.cgMain = new ManagedUPnPTest.ctlUPnPCustomCodeGenerator();
            this.tcMain.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpClass.SuspendLayout();
            this.tlpCodeGeneration.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.tpGenerateClasses.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfo.Location = new System.Drawing.Point(3, 3);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbInfo.Size = new System.Drawing.Size(802, 514);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.WordWrap = false;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpInfo);
            this.tcMain.Controls.Add(this.tpClass);
            this.tcMain.Controls.Add(this.tpGenerateClasses);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(816, 546);
            this.tcMain.TabIndex = 2;
            this.tcMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcMain_Selected);
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.rtbInfo);
            this.tpInfo.Location = new System.Drawing.Point(4, 22);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(808, 520);
            this.tpInfo.TabIndex = 0;
            this.tpInfo.Text = "Item Description Information";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // tpClass
            // 
            this.tpClass.Controls.Add(this.tlpCodeGeneration);
            this.tpClass.Location = new System.Drawing.Point(4, 22);
            this.tpClass.Name = "tpClass";
            this.tpClass.Padding = new System.Windows.Forms.Padding(3);
            this.tpClass.Size = new System.Drawing.Size(808, 520);
            this.tpClass.TabIndex = 1;
            this.tpClass.Text = "Class (Code Generation)";
            this.tpClass.UseVisualStyleBackColor = true;
            // 
            // tlpCodeGeneration
            // 
            this.tlpCodeGeneration.ColumnCount = 1;
            this.tlpCodeGeneration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCodeGeneration.Controls.Add(this.rtbClass, 0, 1);
            this.tlpCodeGeneration.Controls.Add(this.pnlProvider, 0, 0);
            this.tlpCodeGeneration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCodeGeneration.Location = new System.Drawing.Point(3, 3);
            this.tlpCodeGeneration.Name = "tlpCodeGeneration";
            this.tlpCodeGeneration.RowCount = 2;
            this.tlpCodeGeneration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCodeGeneration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCodeGeneration.Size = new System.Drawing.Size(802, 514);
            this.tlpCodeGeneration.TabIndex = 3;
            // 
            // rtbClass
            // 
            this.rtbClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbClass.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbClass.Location = new System.Drawing.Point(3, 36);
            this.rtbClass.Name = "rtbClass";
            this.rtbClass.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbClass.Size = new System.Drawing.Size(796, 475);
            this.rtbClass.TabIndex = 1;
            this.rtbClass.Text = "";
            this.rtbClass.WordWrap = false;
            // 
            // pnlProvider
            // 
            this.pnlProvider.AutoSize = true;
            this.pnlProvider.Controls.Add(this.cgpProvider);
            this.pnlProvider.Controls.Add(this.lblCodeGenProvider);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProvider.Location = new System.Drawing.Point(3, 3);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(796, 27);
            this.pnlProvider.TabIndex = 2;
            // 
            // cgpProvider
            // 
            this.cgpProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cgpProvider.CodeGenProvider = null;
            this.cgpProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cgpProvider.FormattingEnabled = true;
            this.cgpProvider.Location = new System.Drawing.Point(64, 3);
            this.cgpProvider.Name = "cgpProvider";
            this.cgpProvider.Size = new System.Drawing.Size(732, 21);
            this.cgpProvider.TabIndex = 1;
            this.cgpProvider.SelectedIndexChanged += new System.EventHandler(this.cgpProvider_SelectedIndexChanged);
            // 
            // lblCodeGenProvider
            // 
            this.lblCodeGenProvider.AutoSize = true;
            this.lblCodeGenProvider.Location = new System.Drawing.Point(3, 6);
            this.lblCodeGenProvider.Name = "lblCodeGenProvider";
            this.lblCodeGenProvider.Size = new System.Drawing.Size(55, 13);
            this.lblCodeGenProvider.TabIndex = 0;
            this.lblCodeGenProvider.Text = "Language";
            // 
            // tpGenerateClasses
            // 
            this.tpGenerateClasses.Controls.Add(this.cgMain);
            this.tpGenerateClasses.Location = new System.Drawing.Point(4, 22);
            this.tpGenerateClasses.Name = "tpGenerateClasses";
            this.tpGenerateClasses.Padding = new System.Windows.Forms.Padding(3);
            this.tpGenerateClasses.Size = new System.Drawing.Size(808, 520);
            this.tpGenerateClasses.TabIndex = 2;
            this.tpGenerateClasses.Text = "Generate Classes (Recursive)";
            this.tpGenerateClasses.UseVisualStyleBackColor = true;
            // 
            // cgMain
            // 
            this.cgMain.Device = null;
            this.cgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgMain.Location = new System.Drawing.Point(3, 3);
            this.cgMain.Name = "cgMain";
            this.cgMain.Size = new System.Drawing.Size(802, 514);
            this.cgMain.TabIndex = 0;
            // 
            // ctlUPnPDeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcMain);
            this.Name = "ctlUPnPDeviceInfo";
            this.Size = new System.Drawing.Size(816, 546);
            this.tcMain.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpClass.ResumeLayout(false);
            this.tlpCodeGeneration.ResumeLayout(false);
            this.tlpCodeGeneration.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.tpGenerateClasses.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ctlLinkRichTextBox rtbInfo;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TabPage tpClass;
        private System.Windows.Forms.TabPage tpGenerateClasses;
        private ctlUPnPCustomCodeGenerator cgMain;
        private System.Windows.Forms.TableLayoutPanel tlpCodeGeneration;
        private ctlLinkRichTextBox rtbClass;
        private System.Windows.Forms.Panel pnlProvider;
        private UPnPBrowser.ctlCodeGenProviderComboBox cgpProvider;
        private System.Windows.Forms.Label lblCodeGenProvider;
    }
}
