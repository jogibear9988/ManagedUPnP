//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a rich text box with built in link execution when the user clicks a link.
    /// </summary>
    public partial class ctlLinkRichTextBox : RichTextBox
    {
        #region Initialisation

        /// <summary>
        /// Creates a new link rich text box.
        /// </summary>
        public ctlLinkRichTextBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles when a link is clicked.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnLinkClicked(LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
            base.OnLinkClicked(e);
        }

        #endregion
    }
}
