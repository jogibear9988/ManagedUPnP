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
    /// Encapsulates the event arguments for when a device is removed.
    /// </summary>
    public class DeviceRemovedEventArgs : EventArgs
    {
        #region Protected Locals

        /// <summary>
        /// The UDN of the device removed.
        /// </summary>
        protected string msUDN;

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new device removed event arguments.
        /// </summary>
        /// <param name="udn">The UDN of the device being removed.</param>
        public DeviceRemovedEventArgs(string udn)
        {
            msUDN = udn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the UDN for the device that ws removed.
        /// </summary>
        public string UDN
        {
            get
            {
                return msUDN;
            }
        }

        #endregion
    }
}
