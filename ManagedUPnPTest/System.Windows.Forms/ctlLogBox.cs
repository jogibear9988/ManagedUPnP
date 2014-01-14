//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Encapsulates a thread safe auto scroll rich text box for logging purposes.
    /// </summary>
    public partial class ctlLogBox : ctlAutoScrollRichTextBox
    {
        #region Protected Locals

        /// <summary>
        /// The text to append once the control has been created.
        /// </summary>
        protected StringBuilder msbToAppend = new StringBuilder();

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new log box control.
        /// </summary>
        public ctlLogBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Appends text to the log using thread safe methods.
        /// </summary>
        /// <param name="text">The text to append.</param>
        protected void AppendLogText(string text)
        {
            this.BeginInvoke(new Action<string>(AppendText), text);
        }

        /// <summary>
        /// Appends all outstanding text that couldnt be appended
        /// before the creation of the control.
        /// </summary>
        protected void AppendOutstanding()
        {
            string lsText = string.Empty;

            // If there is text to append
            if (msbToAppend != null)
            {
                // Lock for threading
                lock (msbToAppend)
                {
                    // Get the text
                    lsText = msbToAppend.ToString();

                    // Clear the text
                    msbToAppend = null;
                }
            }

            // If text to append
            if (lsText.Length > 0)
                // Append it
                AppendLogText(lsText);
        }

        /// <summary>
        /// Appends text to the outstanding text to append.
        /// </summary>
        /// <param name="text">The text to append.</param>
        protected void AppendToOutstanding(string text)
        {
            lock (msbToAppend)
                msbToAppend.Append(text);
        }

        #endregion

        #region Event Callers

        /// <summary>
        /// Occurs when a link is clicked in the rich text box.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnLinkClicked(LinkClickedEventArgs e)
        {
            // Start the link in browser
            System.Diagnostics.Process.Start(e.LinkText);
            base.OnLinkClicked(e);
        }

        /// <summary>
        /// Occurs when the handle for the text box is initially created.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Append outstanding log entries that were appended
            // before the control handle was created.
            AppendOutstanding();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Occurs before the context menu opens.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void cmMenu_Opening(object sender, CancelEventArgs e)
        {
            // Enable / disable menu items as needed
            miCopy.Enabled = this.SelectionLength > 0;
            miCopyAll.Enabled = this.TextLength > 0;
            miSelectAll.Enabled = this.TextLength > 0;
            miAutoScroll.Checked = this.AutoScroll;
        }

        /// <summary>
        /// Occurs when the user clicks the copy menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void miCopy_Click(object sender, EventArgs e)
        {
            this.Copy();
            this.Focus();
        }

        /// <summary>
        /// Occurs when the user click the Copy all menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void miCopyAll_Click(object sender, EventArgs e)
        {
            // Start formatting update to prevent flicker.
            BeginFormattingUpdate();

            try
            {
                this.SelectAll();
                this.Copy();
                this.Focus();
            }
            finally
            {
                // End formatting update
                EndFormattingUpdate();
            }
        }

        /// <summary>
        /// Selects all text in the text box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void miSelectAll_Click(object sender, EventArgs e)
        {
            this.SelectAll();
            this.Focus();
        }

        /// <summary>
        /// Occurs when the user changes the auto scroll menu item checked property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void miAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            this.AutoScroll = miAutoScroll.Checked;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends text to the log in using thread safe methods.
        /// </summary>
        /// <param name="text">The text to append to the log.</param>
        public void AppendLog(string text)
        {
            if (!IsHandleCreated)
                AppendToOutstanding(text);
            else
            {
                if (msbToAppend != null) AppendOutstanding();
                AppendLogText(text);
            }
        }

        #endregion
    }
}
