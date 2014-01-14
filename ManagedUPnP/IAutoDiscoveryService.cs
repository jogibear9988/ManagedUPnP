//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates an object which contains a service.
    /// </summary>
    public interface IAutoDiscoveryService
    {
        /// <summary>
        /// Gets the service for this auto discovery service.
        /// </summary>
        Service Service { get; }
    }
}
