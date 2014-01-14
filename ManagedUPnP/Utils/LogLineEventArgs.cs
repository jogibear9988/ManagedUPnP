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
    /// Encapsulates the event arguments for the logging of lines.
    /// </summary>
    public class LogLinesEventArgs : EventArgs
    {
        #region Protected Locals

        /// <summary>
        /// The lines to add to the log separated by \r\n.
        /// </summary>
        protected string msLines;

        /// <summary>
        /// The indent of the lines in the log.
        /// </summary>
        protected int miIndent;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new LogLinesEventArgs object.
        /// </summary>
        /// <param name="lines">The lines to add to the log separated by \r\n.</param>
        /// <param name="indent">The indent of the lines in the log.</param>
        public LogLinesEventArgs(string lines, int indent)
        {
            msLines = lines;
            miIndent = indent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lines to add to the log separated by \r\n.
        /// </summary>
        public string Lines
        {
            get
            {
                return msLines;
            }
        }

        /// <summary>
        /// Gets the indent of the lines in the log.
        /// </summary>
        public int Indent
        {
            get
            {
                return miIndent;
            }
        }

        #endregion
    }
}
