//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace ManagedUPnP
{
    #region Public Delegates

    /// <summary>
    /// Event handler for when a service instance dies.
    /// </summary>
    /// <param name="sender">The sender of the events.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void ServiceInstanceDiedEventHandler(object sender, ServiceInstanceDiedEventArgs e);

    #endregion
}
