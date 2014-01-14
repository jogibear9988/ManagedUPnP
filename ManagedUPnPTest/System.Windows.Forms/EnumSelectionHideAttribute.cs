//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace System.Windows.Forms
{
    /// <summary>
    /// Attribute indicating to hide the Enumeration field from the user selection box
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumSelectionHideAttribute : Attribute
    {
        #region Initialisation

        /// <summary>
        /// Creates a new EnumSelectionHide attribute
        /// </summary>
        public EnumSelectionHideAttribute()
        {
        }

        #endregion
    }
}
