//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates an abstract class which maintaines a list of services
    /// for a service type.
    /// </summary>
    /// <remarks>
    /// The services are automatically removed and added as they become
    /// available or become dead. This class is thread safe.
    /// </remarks>
    /// <typeparam name="T">The type of the auto discovery service</typeparam>
    public abstract class AutoDiscoveryServices<T> where T : IAutoDiscoveryService, IDisposable 
    {
        #region Enumerations

        /// <summary>
        /// The various notify actions raised.
        /// </summary>
        /// <remarks>
        /// The brackets in the value summary description show the type / value of the data.
        /// </remarks>
        public enum NotifyAction
        {
            /// <summary>
            /// Search Complete - Async notifications pending (Data = null).
            /// </summary>
            SearchComplete,

            /// <summary>
            /// Device Removed (Data = <see cref="System.String" />: DeviceUDN).
            /// </summary>
            DeviceRemoved,

            /// <summary>
            /// Service Removed for device (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service removed).
            /// </summary>
            ServiceRemoved,

            /// <summary>
            /// COM Device Found (Data = <see cref="System.String" />: COMDevice.ToString()).
            /// </summary>
            COMDeviceFound,

            /// <summary>
            /// Device Found (Data = <see cref="ManagedUPnP.Device" />: Device found).
            /// </summary>
            DeviceFound,

            /// <summary>
            /// Service Found (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service found).
            /// </summary>
            ServiceFound,

            /// <summary>
            /// Service Accepted (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service accepted).
            /// </summary>
            ServiceAccepted,

            /// <summary>
            /// Service Not Accepted (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service not accepted).
            /// </summary>
            ServiceNotAccepted,

            /// <summary>
            /// Service Already Added (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service already added).
            /// </summary>
            ServiceAlreadyAdded,

            /// <summary>
            /// Service Not Created (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service not created).
            /// </summary>
            ServiceNotCreated,

            /// <summary>
            /// Service Added (Data = <see cref="ManagedUPnP.IAutoDiscoveryService" />: Service added")
            /// </summary>
            ServiceAdded
        }

        #endregion

        #region Protected Locals

        /// <summary>
        /// The dictionary containing key and services.
        /// </summary>
        protected Dictionary<string, T> mdAutoServices = new Dictionary<string, T>();

        /// <summary>
        /// The address families to search.
        /// </summary>
        protected AddressFamilyFlags mafAddressFamily = AddressFamilyFlags.IPvBoth;

        /// <summary>
        /// True to resolve network interfaces in search.
        /// </summary>
        protected bool mbResolveNetworkInterfaces = false;

        /// <summary>
        /// The discovery object to discover services.
        /// </summary>
        protected Discovery mdDiscovery;

        /// <summary>
        /// The service type to discover or String.Empty for for all services.
        /// </summary>
        protected string msDiscoveryServiceType;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when services have changed.
        /// </summary>
        public event EventHandler AutoServicesChanged;

        /// <summary>
        /// Event raised when the initial search is complete.
        /// </summary>
        public event EventHandler SearchComplete;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new AutoDiscoveryServices object.
        /// </summary>
        /// <param name="discoveryServiceType">The service type to discover or String.Empty for all services.</param>
        public AutoDiscoveryServices(string discoveryServiceType)
        {
            if (string.IsNullOrEmpty(discoveryServiceType))
                discoveryServiceType = null;

            msDiscoveryServiceType = discoveryServiceType;
        }

        #endregion

        #region Event Callers

        /// <summary>
        /// Raises the SearchCompleteEvent.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnSearchComplete(EventArgs e)
        {
            if (SearchComplete != null)
                SearchComplete(this, e);
        }

        /// <summary>
        /// Raises the AutoServicesChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnAutoServicesChanged(EventArgs e)
        {
            if (AutoServicesChanged != null)
                AutoServicesChanged(this, e);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles when the discovery object finishes its initial search.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void mdDiscovery_SearchComplete(object sender, SearchCompleteEventArgs e)
        {
            StatusNotify(NotifyAction.SearchComplete);
            OnSearchComplete(new EventArgs());
        }

        /// <summary>
        /// Handles when the discovery object removes a device.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void mdDiscovery_DeviceRemoved(object sender, DeviceRemovedEventArgs e)
        {
            try
            {
                lock (mdAutoServices)
                {
                    List<string> mlRemove = new List<string>();
                    foreach (KeyValuePair<string, T> lkvValue in mdAutoServices)
                        if (lkvValue.Value.Service.Device.UniqueDeviceName == e.UDN)
                            mlRemove.Add(lkvValue.Value.Service.Key);

                    foreach (string lsRemove in mlRemove)
                    {
                        T lasRemove = mdAutoServices[lsRemove];
                        mdAutoServices.Remove(lsRemove);
                        Removed(lasRemove);
                        StatusNotify(NotifyAction.ServiceRemoved, lasRemove);
                    }

                    if (mlRemove.Count > 0) OnAutoServicesChanged(new EventArgs());
                }
            }
            finally
            {
                StatusNotify(NotifyAction.DeviceRemoved, e.UDN);
            }
        }

        /// <summary>
        /// Handles when the discovery object adds a device.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments.</param>
        private void mdDiscovery_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            StatusNotify(NotifyAction.COMDeviceFound, e.Device.ToString());
            StatusNotify(NotifyAction.DeviceFound, e.Device);
            AddAllFor(new Services(e.Device, DiscoveryServiceType, true));
        }

        #endregion

        #region Protected Abstract Methods

        /// <summary>
        /// Determines whether a service is compatible with T.
        /// </summary>
        /// <param name="service">The service to determine compatibility for.</param>
        /// <returns>True if the service is compatible false otherwise.</returns>
        protected abstract bool CanCreateAutoServiceFor(Service service);

        /// <summary>
        /// Creates a T object for the specified service.
        /// </summary>
        /// <param name="service">The service to create T for.</param>
        /// <returns>The service created or null if unable to create.</returns>
        protected abstract T CreateAutoServiceFor(Service service);

        #endregion

        #region Protected Methods

        /// <summary>
        /// Notifies of status change with no data.
        /// </summary>
        /// <param name="action">The action for the notification.</param>
        protected void StatusNotify(NotifyAction action)
        {
            StatusNotify(action, null);
        }

        /// <summary>
        /// Notifies of status change with data.
        /// </summary>
        /// <param name="action">The action for the notification.</param>
        /// <param name="data">The data for the action <see cref="ManagedUPnP.AutoDiscoveryServices{T}.NotifyAction" />.</param>
        protected virtual void StatusNotify(NotifyAction action, object data)
        {
        }

        /// <summary>
        /// Notifies that an auto service has been removed.
        /// </summary>
        /// <param name="autoService">The auto service removed.</param>
        protected virtual void Removed(T autoService)
        {
        }

        /// <summary>
        /// Notifies that an auto service has been added.
        /// </summary>
        /// <param name="autoService">The auto service added.</param>
        protected virtual void Added(T autoService)
        {
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the discovery service type or String.Empty for all services.
        /// </summary>
        protected virtual string DiscoveryServiceType 
        {
            get
            {
                return msDiscoveryServiceType;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears the services (stops and starts the async if already running).
        /// </summary>
        public virtual void Clear()
        {
            // Save the running state
            bool lbRunning = AsyncRunning;

            // Stop the search
            if (lbRunning) StopAsync();

            try
            {
                // Clear the services
                lock (mdAutoServices)
                {
                    mdAutoServices.Clear();
                    OnAutoServicesChanged(new EventArgs());
                }
            }
            finally
            {
                // Restart the search if needed
                if (lbRunning) ReStartAsync();
            }
        }

        /// <summary>
        /// Stops the current async search if running.
        /// </summary>
        public void StopAsync()
        {
            if (AsyncRunning)
            {
                mdDiscovery.Stop();
                mdDiscovery.DeviceAdded -= new DeviceAddedEventHandler(mdDiscovery_DeviceAdded);
                mdDiscovery.DeviceRemoved -= new DeviceRemovedEventHandler(mdDiscovery_DeviceRemoved);
                mdDiscovery.SearchComplete -= new SearchCompleteEventHandler(mdDiscovery_SearchComplete);
                mdDiscovery.Dispose();
                mdDiscovery = null;
            }
        }

        /// <summary>
        /// Starts or restarts the current async search.
        /// </summary>
        public void ReStartAsync()
        {
            if (AsyncRunning) StopAsync();

            if (!AsyncRunning)
            {
                mdDiscovery = new Discovery(DiscoveryServiceType, AddressFamily, ResolveNetworkInterfaces);
                mdDiscovery.DeviceAdded += new DeviceAddedEventHandler(mdDiscovery_DeviceAdded);
                mdDiscovery.DeviceRemoved += new DeviceRemovedEventHandler(mdDiscovery_DeviceRemoved);
                mdDiscovery.SearchComplete += new SearchCompleteEventHandler(mdDiscovery_SearchComplete);
                mdDiscovery.Start();
            }
        }

        /// <summary>
        /// Adds all services for a device UDN syncrhonously.
        /// </summary>
        /// <param name="udn">The UDN of the device to add the services for.</param>
        public void AddAllFor(string udn)
        {
            AddAllFor(Discovery.FindServicesByUDN(udn, DiscoveryServiceType, AddressFamily));
        }

        /// <summary>
        /// Adds all services for a service list synchronously.
        /// </summary>
        /// <param name="services">The services to add.</param>
        public void AddAllFor(Services services)
        {
            bool lbChanged = false;

            try
            {
                foreach (Service lsService in services)
                {
                    StatusNotify(NotifyAction.ServiceFound, lsService);

                    string lsKey = lsService.Key;

                    bool lbAlreadyAdded;
                    lock (mdAutoServices) lbAlreadyAdded = mdAutoServices.ContainsKey(lsKey);

                    if (!lbAlreadyAdded)
                    {
                        if (CanCreateAutoServiceFor(lsService))
                        {
                            StatusNotify(NotifyAction.ServiceAccepted, lsService);

                            lock (mdAutoServices)
                            {
                                T lasAutoService = CreateAutoServiceFor(lsService);

                                if (lasAutoService != null)
                                {
                                    mdAutoServices[lsKey] = lasAutoService;
                                    Added(lasAutoService);

                                    StatusNotify(NotifyAction.ServiceAdded, lasAutoService);
                                    lbChanged = true;
                                }
                                else
                                    StatusNotify(NotifyAction.ServiceNotCreated, lsService);
                            }
                        }
                        else
                            StatusNotify(NotifyAction.ServiceNotAccepted, lsService);
                    }
                    else
                        StatusNotify(NotifyAction.ServiceAlreadyAdded, lsService);
                }
            }
            finally
            {
                if (lbChanged)
                    OnAutoServicesChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets a dictionary containing ServiceKey, ServiceName.
        /// </summary>
        /// <returns>The Key, Name dictionary.</returns>
        public Dictionary<string, string> GetKeyNameDictionary()
        {
            Dictionary<string, string> ldReturn = new Dictionary<string, string>();

            lock (mdAutoServices)
            {
                foreach (KeyValuePair<string, T> lasAutoService in mdAutoServices)
                    ldReturn[lasAutoService.Key] = lasAutoService.Value.Service.Name;
            }

            return ldReturn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets whether the async discovery is running.
        /// </summary>
        public bool AsyncRunning
        {
            get
            {
                return mdDiscovery != null;
            }
        }

        /// <summary>
        /// Gets a thread safe snapshot list of auto services currently available.
        /// </summary>
        public List<T> AutoServices
        {
            get
            {
                lock (mdAutoServices) return mdAutoServices.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the address familys to search on, this will
        /// require a restart of the search is one is already under way.
        /// </summary>
        public AddressFamilyFlags AddressFamily
        {
            get
            {
                return mafAddressFamily;
            }
            set
            {
                mafAddressFamily = value;
            }
        }

        /// <summary>
        /// Gets or sets whether to resolve network interfaces, this will
        /// require a restart of the search is one is already under way 
        /// (Windows Vista and above only).
        /// </summary>
        public bool ResolveNetworkInterfaces
        {
            get
            {
                return mbResolveNetworkInterfaces;
            }
            set
            {
                mbResolveNetworkInterfaces = value;
            }
        }

        #endregion

        #region Finalisation

        /// <summary>
        /// Finalisation for disposal.
        /// </summary>
        ~AutoDiscoveryServices()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal dispose method.
        /// </summary>
        /// <param name="disposeManaged">True to dispose managed objects.</param>
        protected virtual void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                if (mdDiscovery != null)
                {
                    mdDiscovery.Dispose();
                    mdDiscovery = null;
                }
            }
        }

        #endregion
    }
}
