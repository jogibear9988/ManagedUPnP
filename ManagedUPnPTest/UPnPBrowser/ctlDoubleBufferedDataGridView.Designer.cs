namespace ManagedUPnPTest
{
    partial class ctlDoubleBufferedDataGridView
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
            this.tmrCommitEdit = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrCommitEdit
            // 
            this.tmrCommitEdit.Interval = 1;
            this.tmrCommitEdit.Tick += new System.EventHandler(this.tmrCommitEdit_Tick);
            // 
            // ctlDoubleBufferedDataGridView
            // 
            this.Name = "ctlDoubleBufferedDataGrid";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The commit edit timer
        /// </summary>
        private System.Windows.Forms.Timer tmrCommitEdit;
    }
}
