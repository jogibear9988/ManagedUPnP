namespace ManagedUPnPTest
{
    partial class ctlUPnPStateVarInfo
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
            this.rtbInfo = new ManagedUPnPTest.ctlLinkRichTextBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.lblValueHeading = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfo.Location = new System.Drawing.Point(3, 3);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbInfo.Size = new System.Drawing.Size(776, 498);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.rtbInfo, 0, 0);
            this.tlpMain.Controls.Add(this.pnlControls, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.Size = new System.Drawing.Size(782, 544);
            this.tlpMain.TabIndex = 1;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.tbValue);
            this.pnlControls.Controls.Add(this.cmdRefresh);
            this.pnlControls.Controls.Add(this.lblValueHeading);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(3, 507);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(776, 34);
            this.pnlControls.TabIndex = 1;
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.Location = new System.Drawing.Point(47, 6);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(645, 20);
            this.tbValue.TabIndex = 2;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(698, 4);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lblValueHeading
            // 
            this.lblValueHeading.AutoSize = true;
            this.lblValueHeading.Location = new System.Drawing.Point(4, 9);
            this.lblValueHeading.Name = "lblValueHeading";
            this.lblValueHeading.Size = new System.Drawing.Size(37, 13);
            this.lblValueHeading.TabIndex = 0;
            this.lblValueHeading.Text = "Value:";
            // 
            // UPnPStateVarInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "UPnPStateVarInfo";
            this.Size = new System.Drawing.Size(782, 544);
            this.tlpMain.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctlLinkRichTextBox rtbInfo;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label lblValueHeading;
    }
}
