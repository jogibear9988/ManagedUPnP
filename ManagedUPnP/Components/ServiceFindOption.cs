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
    /// Specifies which services to raise the service added event for.
    /// </summary>
    public enum ServiceFindOption
    {
        /// <summary>
        /// Do not raise ServiceAdded event.
        /// </summary>
        None,

        /// <summary>
        /// Only raise ServiceAdded event for direct child services 
        /// of each Device which has had a DeviceAdded event 
        /// raised for it.
        /// </summary>
        FoundDeviceDirectChildrenOnly,

        /// <summary>
        /// Raise ServiceAdded event for every child service of every 
        /// device and child device, while not raising the event more
        /// than once for a particular service.
        /// </summary>
        AllDeviceChildrenServices,

        /// <summary>
        /// Raise ServiceAdded event for every child service which matches
        /// its ServiceTypeId to the SearchURI of every 
        /// device and child device, while not raising the event more
        /// than once for a particular service.
        /// </summary>
        AllSearchURIMatchesServiceTypeId,

        /// <summary>
        /// Only raise ServiceAdded event for direct child service
        /// which matches its ServiceTypeId to the SearchURI 
        /// of each Device which has had a DeviceAdded event 
        /// raised for it.
        /// </summary>
        FoundDeviceDirectChildrenOnlySearchURIMatchesServiceTypeId
    }
}

#endif