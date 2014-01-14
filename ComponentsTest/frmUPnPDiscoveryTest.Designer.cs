namespace ComponentsTest
{
    partial class frmUPnPDiscoveryTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUPnPDiscoveryTest));
            this.tbLog = new System.Windows.Forms.TextBox();
            this.cmdStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdShowIPAddress = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.UPnP = new ManagedUPnP.Components.UPnPDiscovery();
            this.LogIntercept = new ManagedUPnP.Components.UPnPLogInterceptor();
            this.panel1.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UPnP)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Font = new System.Drawing.Font("Courier New", 10F);
            this.tbLog.Location = new System.Drawing.Point(3, 3);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(693, 451);
            this.tbLog.TabIndex = 0;
            this.tbLog.Text = resources.GetString("tbLog.Text");
            this.tbLog.WordWrap = false;
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(3, 3);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 23);
            this.cmdStart.TabIndex = 1;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdShowIPAddress);
            this.panel1.Controls.Add(this.cmdStop);
            this.panel1.Controls.Add(this.cmdStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 30);
            this.panel1.TabIndex = 2;
            // 
            // cmdShowIPAddress
            // 
            this.cmdShowIPAddress.Location = new System.Drawing.Point(165, 3);
            this.cmdShowIPAddress.Name = "cmdShowIPAddress";
            this.cmdShowIPAddress.Size = new System.Drawing.Size(113, 23);
            this.cmdShowIPAddress.TabIndex = 2;
            this.cmdShowIPAddress.Text = "Show IP Address";
            this.cmdShowIPAddress.UseVisualStyleBackColor = true;
            this.cmdShowIPAddress.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(84, 3);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(75, 23);
            this.cmdStop.TabIndex = 1;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panel1, 0, 1);
            this.tlpMain.Controls.Add(this.tbLog, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(699, 493);
            this.tlpMain.TabIndex = 3;
            // 
            // UPnP
            // 
            this.UPnP.SearchURI = "";
            this.UPnP.DeviceAdded += new ManagedUPnP.DeviceAddedEventHandler(this.UPnP_DeviceAdded);
            this.UPnP.ServiceAdded += new ManagedUPnP.ServiceAddedEventHandler(this.UPnP_ServiceAdded);
            this.UPnP.SearchComplete += new ManagedUPnP.SearchCompleteEventHandler(this.UPnP_SearchComplete);
            this.UPnP.SearchStarted += new System.EventHandler(this.UPnP_SearchStarted);
            this.UPnP.SearchFailed += new System.EventHandler(this.UPnP_SearchFailed);
            this.UPnP.SearchEnded += new System.EventHandler(this.UPnP_SearchEnded);
            // 
            // LogIntercept
            // 
            this.LogIntercept.Enabled = true;
            this.LogIntercept.LogLines += new ManagedUPnP.LogLinesEventHandler(this.LogIntercept_LogLines);
            // 
            // frmUPnPDiscoveryTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 493);
            this.Controls.Add(this.tlpMain);
            this.Name = "frmUPnPDiscoveryTest";
            this.Text = "UPnPDiscovery Test";
            this.Load += new System.EventHandler(this.frmComponentsTest_Load);
            this.panel1.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UPnP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ManagedUPnP.Components.UPnPDiscovery UPnP;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button cmdShowIPAddress;
        private ManagedUPnP.Components.UPnPLogInterceptor LogIntercept;
    }
}

