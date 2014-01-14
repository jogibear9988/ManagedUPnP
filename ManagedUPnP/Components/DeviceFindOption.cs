//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Components

namespace ManagedUPnP.Components
{
    /// <summary>
    /// Specifies which devices to raise a DeviceAdded event for.
    /// </summary>
    public enum DeviceFindOption
    {
        /// <summary>
        /// Only raise event for devices directly found by the search,
        /// or devices which have services which were directly found by 
        /// the search.
        /// </summary>
        FoundDevicesOnly,

        /// <summary>
        /// Raise the DevicesAdded event for each device and child device
        /// which is found.
        /// </summary>
        AllChildrenDeivces
    }
}

#endif