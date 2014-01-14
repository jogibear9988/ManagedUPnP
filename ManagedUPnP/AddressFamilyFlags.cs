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
    /// Flags to determine which IP Version to search for devices in. (Vista and above only)
    /// </summary>
    [Flags]
    public enum AddressFamilyFlags : uint
    {
        /// <summary>
        /// IP Version 4.0 only.
        /// </summary>
        IPv4 = 1,

        /// <summary>
        /// IP Version 6.0 only.
        /// </summary>
        IPv6 = 2,

        /// <summary>
        /// Any IP Version.
        /// </summary>
        IPvBoth = 3
    }
}
