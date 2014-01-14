namespace ManagedUPnPTest
{
    partial class frmUPnPBrowser
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tvUPnP = new ManagedUPnPTest.ctlUPnPTreeBrowser();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.ctlLogBox();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilIcons
            // 
            this.ilIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(490, 496);
            this.pnlInfo.TabIndex = 1;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tvUPnP);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.tcMain);
            this.scMain.Size = new System.Drawing.Size(809, 528);
            this.scMain.SplitterDistance = 301;
            this.scMain.TabIndex = 2;
            // 
            // tvUPnP
            // 
            this.tvUPnP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUPnP.ImageIndex = 0;
            this.tvUPnP.Location = new System.Drawing.Point(0, 0);
            this.tvUPnP.Name = "tvUPnP";
            this.tvUPnP.SelectedImageIndex = 0;
            this.tvUPnP.Size = new System.Drawing.Size(301, 528);
            this.tvUPnP.TabIndex = 0;
            this.tvUPnP.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvUPnP_AfterSelect);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpInfo);
            this.tcMain.Controls.Add(this.tpLog);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(504, 528);
            this.tcMain.TabIndex = 1;
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.pnlInfo);
            this.tpInfo.Location = new System.Drawing.Point(4, 22);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(496, 502);
            this.tpInfo.TabIndex = 0;
            this.tpInfo.Text = "Selected Item Info";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.txtLog);
            this.tpLog.Location = new System.Drawing.Point(4, 22);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(496, 502);
            this.tpLog.TabIndex = 1;
            this.tpLog.Text = "UPnP Log";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(490, 496);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            this.txtLog.WordWrap = false;
            // 
            // frmUPnPBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 528);
            this.Controls.Add(this.scMain);
            this.Name = "frmUPnPBrowser";
            this.Text = "Managed UPnP Browser Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUPnPBrowser_FormClosing);
            this.Load += new System.EventHandler(this.frmManagedUPnPTest_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ctlUPnPTreeBrowser tvUPnP;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.ctlLogBox txtLog;
    }
}

