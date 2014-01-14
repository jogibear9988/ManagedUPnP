//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace System.Windows.Forms
{
    /// <summary>
    /// Attribute indicating whether or not this Enumeration field is the default in the selection box
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumSelectionDefaultAttribute : Attribute
    {
        #region Initialisation

        /// <summary>
        /// Creates a new EnumSelectionDefault attribute
        /// </summary>
        public EnumSelectionDefaultAttribute()
        {
        }

        #endregion
    }
}
