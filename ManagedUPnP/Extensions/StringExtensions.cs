//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;

namespace ManagedUPnP
{
    /// <summary>
    /// Extends the string class.
    /// </summary>
    internal static class StringExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Appends a line character to the start of a string.
        /// </summary>
        /// <param name="value">The value to pre-pend the line end character to.</param>
        /// <returns>The prepended line.</returns>
        public static String AsLine(this string value)
        {
            if (value.Length > 0)
                return String.Concat(Environment.NewLine, value);
            else
                return String.Empty;
        }

        /// <summary>
        /// Removes the end line characters at the end of a string if needed.
        /// </summary>
        /// <param name="value">The value to remove the end line characters from.</param>
        /// <returns>The inline text.</returns>
        public static String AsInLine(this string value)
        {
            if (value.EndsWith(Environment.NewLine))
                return value.Substring(0, value.Length - Environment.NewLine.Length);
            else
                return value;
        }

        /// <summary>
        /// Appends a line character to the start of a string if needed.
        /// </summary>
        /// <param name="value">The value to pre-pend the line end character to.</param>
        /// <returns>The prepended line.</returns>
        public static String LineBefore(this string value)
        {
            if (value.Length > 0 && !value.StartsWith(Environment.NewLine))
                return String.Concat(Environment.NewLine, value);
            else
                return value;
        }

        #endregion
    }
}
