namespace ManagedUPnPTest
{
    partial class ctlUPnPTreeBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlUPnPTreeBrowser));
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "Blank");
            // 
            // UPnPTreeBrowser
            // 
            this.ImageIndex = 0;
            this.ImageList = this.ilIcons;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ilIcons;
    }
}
