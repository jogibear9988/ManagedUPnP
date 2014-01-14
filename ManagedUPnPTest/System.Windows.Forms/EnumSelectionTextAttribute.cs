//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace System.Windows.Forms
{
    /// <summary>
    /// Attribute for the text to show in the user selection box for an Enumeration Field
    /// </summary>
    /// <remarks>
    /// If no selection text is specified then the name of the enum field is used
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumSelectionTextAttribute : Attribute
    {
        #region Locals

        /// <summary>
        /// Stores the selection text for thie enumeration field
        /// </summary>
        private readonly string msSelectionText;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new EnumSelectionText attribute
        /// </summary>
        /// <param name="selectionText">The selection text for the attribute</param>
        public EnumSelectionTextAttribute(string selectionText)
        {
            this.msSelectionText = selectionText;
        }

        #endregion

        #region Public

        /// <summary>
        /// Gets the text for this selection text attribute
        /// </summary>
        /// <returns>The text for the selection text attribute</returns>
        public override string ToString()
        {
            return msSelectionText;
        }

        #endregion
    }

}
