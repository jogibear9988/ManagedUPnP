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
    /// Used for code generation for device check comparison.
    /// </summary>
    [Flags]
    public enum ServiceCheckFlags
    {
        /// <summary>
        /// No checks are performed.
        /// </summary>
        None = 0,

        /// <summary>
        /// Check to ensure service type id matches.
        /// </summary>
        ServiceType = 1,

        /// <summary>
        /// Check to ensure service Id matches.
        /// </summary>
        ServiceId = 2
    }
}
