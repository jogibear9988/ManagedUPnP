namespace ManagedUPnPTest
{
    partial class ctlUPnPActionInfo
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
            this.cmdExecute = new System.Windows.Forms.Button();
            this.scTop = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpInputs = new System.Windows.Forms.TabPage();
            this.dgInputs = new ManagedUPnPTest.ctlDoubleBufferedDataGridView();
            this.clArgument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clInputType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clInputValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpOutputs = new System.Windows.Forms.TabPage();
            this.dgOutputs = new ManagedUPnPTest.ctlDoubleBufferedDataGridView();
            this.clOutputArgument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clOutputType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clOutputValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTop)).BeginInit();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInputs)).BeginInit();
            this.tpOutputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOutputs)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(776, 210);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.WordWrap = false;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.cmdExecute, 0, 1);
            this.tlpMain.Controls.Add(this.scTop, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.Size = new System.Drawing.Size(782, 544);
            this.tlpMain.TabIndex = 1;
            // 
            // cmdExecute
            // 
            this.cmdExecute.Location = new System.Drawing.Point(3, 517);
            this.cmdExecute.Name = "cmdExecute";
            this.cmdExecute.Size = new System.Drawing.Size(75, 23);
            this.cmdExecute.TabIndex = 2;
            this.cmdExecute.Text = "Execute";
            this.cmdExecute.UseVisualStyleBackColor = true;
            this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
            // 
            // scTop
            // 
            this.scTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTop.Location = new System.Drawing.Point(3, 3);
            this.scTop.Name = "scTop";
            this.scTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTop.Panel1
            // 
            this.scTop.Panel1.Controls.Add(this.rtbInfo);
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.tcMain);
            this.scTop.Size = new System.Drawing.Size(776, 508);
            this.scTop.SplitterDistance = 210;
            this.scTop.TabIndex = 3;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpInputs);
            this.tcMain.Controls.Add(this.tpOutputs);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(776, 294);
            this.tcMain.TabIndex = 2;
            // 
            // tpInputs
            // 
            this.tpInputs.Controls.Add(this.dgInputs);
            this.tpInputs.Location = new System.Drawing.Point(4, 22);
            this.tpInputs.Name = "tpInputs";
            this.tpInputs.Padding = new System.Windows.Forms.Padding(3);
            this.tpInputs.Size = new System.Drawing.Size(768, 268);
            this.tpInputs.TabIndex = 0;
            this.tpInputs.Text = "Inputs";
            this.tpInputs.UseVisualStyleBackColor = true;
            // 
            // dgInputs
            // 
            this.dgInputs.AllowUserToAddRows = false;
            this.dgInputs.AllowUserToDeleteRows = false;
            this.dgInputs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInputs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clArgument,
            this.clInputType,
            this.clInputValue});
            this.dgInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInputs.Location = new System.Drawing.Point(3, 3);
            this.dgInputs.Name = "dgInputs";
            this.dgInputs.Size = new System.Drawing.Size(762, 262);
            this.dgInputs.TabIndex = 0;
            // 
            // clArgument
            // 
            this.clArgument.HeaderText = "Input Argument";
            this.clArgument.Name = "clArgument";
            this.clArgument.ReadOnly = true;
            this.clArgument.Width = 150;
            // 
            // clInputType
            // 
            this.clInputType.HeaderText = "Type";
            this.clInputType.Name = "clInputType";
            this.clInputType.ReadOnly = true;
            // 
            // clInputValue
            // 
            this.clInputValue.HeaderText = "Input Value";
            this.clInputValue.Name = "clInputValue";
            // 
            // tpOutputs
            // 
            this.tpOutputs.Controls.Add(this.dgOutputs);
            this.tpOutputs.Location = new System.Drawing.Point(4, 22);
            this.tpOutputs.Name = "tpOutputs";
            this.tpOutputs.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputs.Size = new System.Drawing.Size(768, 268);
            this.tpOutputs.TabIndex = 1;
            this.tpOutputs.Text = "Outputs";
            this.tpOutputs.UseVisualStyleBackColor = true;
            // 
            // dgOutputs
            // 
            this.dgOutputs.AllowUserToAddRows = false;
            this.dgOutputs.AllowUserToDeleteRows = false;
            this.dgOutputs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOutputs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clOutputArgument,
            this.clOutputType,
            this.clOutputValue});
            this.dgOutputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOutputs.Location = new System.Drawing.Point(3, 3);
            this.dgOutputs.Name = "dgOutputs";
            this.dgOutputs.Size = new System.Drawing.Size(762, 262);
            this.dgOutputs.TabIndex = 1;
            // 
            // clOutputArgument
            // 
            this.clOutputArgument.Frozen = true;
            this.clOutputArgument.HeaderText = "Output Argument";
            this.clOutputArgument.Name = "clOutputArgument";
            this.clOutputArgument.ReadOnly = true;
            this.clOutputArgument.Width = 150;
            // 
            // clOutputType
            // 
            this.clOutputType.Frozen = true;
            this.clOutputType.HeaderText = "Type";
            this.clOutputType.Name = "clOutputType";
            this.clOutputType.ReadOnly = true;
            // 
            // clOutputValue
            // 
            this.clOutputValue.HeaderText = "Output Value";
            this.clOutputValue.Name = "clOutputValue";
            this.clOutputValue.ReadOnly = true;
            // 
            // UPnPActionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "UPnPActionInfo";
            this.Size = new System.Drawing.Size(782, 544);
            this.tlpMain.ResumeLayout(false);
            this.scTop.Panel1.ResumeLayout(false);
            this.scTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTop)).EndInit();
            this.scTop.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpInputs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInputs)).EndInit();
            this.tpOutputs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOutputs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctlLinkRichTextBox rtbInfo;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button cmdExecute;
        private ManagedUPnPTest.ctlDoubleBufferedDataGridView dgInputs;
        private ManagedUPnPTest.ctlDoubleBufferedDataGridView dgOutputs;
        private System.Windows.Forms.DataGridViewTextBoxColumn clArgument;
        private System.Windows.Forms.DataGridViewTextBoxColumn clInputType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clInputValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clOutputArgument;
        private System.Windows.Forms.DataGridViewTextBoxColumn clOutputType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clOutputValue;
        private System.Windows.Forms.SplitContainer scTop;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpInputs;
        private System.Windows.Forms.TabPage tpOutputs;
    }
}
