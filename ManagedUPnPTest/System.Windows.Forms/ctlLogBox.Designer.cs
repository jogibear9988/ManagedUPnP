namespace System.Windows.Forms
{
    partial class ctlLogBox
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
            this.components = new System.ComponentModel.Container();
            this.cmMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.miAutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmMenu
            // 
            this.cmMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCopy,
            this.tsSep1,
            this.miSelectAll,
            this.miCopyAll,
            this.tsSep2,
            this.miAutoScroll});
            this.cmMenu.Name = "contextMenuStrip1";
            this.cmMenu.Size = new System.Drawing.Size(210, 104);
            this.cmMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmMenu_Opening);
            // 
            // miCopy
            // 
            this.miCopy.Name = "miCopy";
            this.miCopy.Size = new System.Drawing.Size(209, 22);
            this.miCopy.Text = "Copy";
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(206, 6);
            // 
            // miSelectAll
            // 
            this.miSelectAll.Name = "miSelectAll";
            this.miSelectAll.Size = new System.Drawing.Size(209, 22);
            this.miSelectAll.Text = "Select All";
            this.miSelectAll.Click += new System.EventHandler(this.miSelectAll_Click);
            // 
            // miCopyAll
            // 
            this.miCopyAll.Name = "miCopyAll";
            this.miCopyAll.Size = new System.Drawing.Size(209, 22);
            this.miCopyAll.Text = "Copy all Text to Clipboard";
            this.miCopyAll.Click += new System.EventHandler(this.miCopyAll_Click);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(206, 6);
            // 
            // miAutoScroll
            // 
            this.miAutoScroll.CheckOnClick = true;
            this.miAutoScroll.Name = "miAutoScroll";
            this.miAutoScroll.Size = new System.Drawing.Size(209, 22);
            this.miAutoScroll.Text = "Auto Scroll";
            this.miAutoScroll.CheckedChanged += new System.EventHandler(this.miAutoScroll_CheckedChanged);
            // 
            // ctlLogBox
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ContextMenuStrip = this.cmMenu;
            this.ReadOnly = true;
            this.WordWrap = false;
            this.cmMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmMenu;
        private System.Windows.Forms.ToolStripMenuItem miCopy;
        private System.Windows.Forms.ToolStripSeparator tsSep1;
        private System.Windows.Forms.ToolStripMenuItem miSelectAll;
        private System.Windows.Forms.ToolStripMenuItem miCopyAll;
        private ToolStripSeparator tsSep2;
        private ToolStripMenuItem miAutoScroll;
    }
}
