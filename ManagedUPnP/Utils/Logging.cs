//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace ManagedUPnP
{
    /// <summary>
    /// Provides a simple static class to
    /// enable logging for debugging purposes.
    /// </summary>
    public static class Logging
    {
        #region Private Static Locals

        /// <summary>
        /// True if the logging is enabled.
        /// </summary>
        private static bool mbEnabled = false;

        /// <summary>
        /// The current indent for the logging text.
        /// </summary>
        private static int miIndent = 0;

        #endregion

        #region Public Static Events

        /// <summary>
        /// Occurs when log lines are appended to the log.
        /// </summary>
        public static event LogLinesEventHandler LogLines;

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Calls the LogLines event handler.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private static void OnLogLines(object sender, LogLinesEventArgs e)
        {
            if (LogLines != null)
                LogLines(sender, e);
        }

        #endregion

        #region Internal Static Methods

        /// <summary>
        /// Appends lines to the log.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="lines">The lines for the log (separated by \r\n).</param>
        /// <param name="indent">The indent of the log lines.</param>
        internal static void Log(object sender, string lines, int indent = 0)
        {
            if (indent < 0) miIndent += indent;

            if (Enabled)
                OnLogLines(sender, new LogLinesEventArgs(lines, miIndent));

            if (indent > 0) miIndent += indent;
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets or sets whether logging is enabled.
        /// </summary>
        public static bool Enabled
        {
            get
            {
                return mbEnabled;
            }
            set
            {
                mbEnabled = value;
            }
        }

        #endregion
    }
}
