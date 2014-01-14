//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates static global settings for the
    /// ManagedUPnP namespace.
    /// </summary>
    public static class Globals
    {
        #region Private Static Locals

        /// <summary>
        /// Stores the request timeout for description XML documents,
        /// and Icons from the UPnP enabled devices.
        /// </summary>
        private static int miRequestTimeoutMS = 30000;

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets or sets the request timeout for description XML documents,
        /// and Icons from the UPnP enabled devices.
        /// </summary>
        public static int RequestTimeoutMS
        {
            get
            {
                return miRequestTimeoutMS;
            }
            set
            {
                if (value <= 0) value = 1;
                miRequestTimeoutMS = value;
            }
        }

        #endregion
    }
}
