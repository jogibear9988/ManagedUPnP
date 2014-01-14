//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Components

using System;
using System.ComponentModel;

namespace ManagedUPnP.Components
{
    /// <summary>
    /// Encapsulates a GUI thread safe discovery component, for discovering Devices and/or
    /// Services by Device.Type, Device.UDN or Service.ServiceTypeIdentifier.
    /// </summary>
    /// <example>
    ///     <code>
    ///         UPnPDiscovery ldDiscovery = new UPnPDiscovery();
    ///         
    ///         ldDiscovery.SearchURI = "";
    ///         ldDiscovery.DeviceAdded += new ManagedUPnP.DeviceAddedEventHandler(UPnP_DeviceAdded);
    ///         ldDiscovery.ServiceAdded += new ManagedUPnP.ServiceAddedEventHandler(UPnP_ServiceAdded);
    ///         ldDiscovery.SearchComplete += new ManagedUPnP.SearchCompleteEventHandler(UPnP_SearchComplete);
    ///         ldDiscovery.SearchStarted += new System.EventHandler(UPnP_SearchStarted);
    ///         ldDiscovery.SearchFailed += new System.EventHandler(UPnP_SearchFailed);
    ///         ldDiscovery.SearchEnded += new System.EventHandler(UPnP_SearchEnded);
    ///         
    ///         ldDiscovery.Active = true;
    ///     </code>
    /// </example>
    public class UPnPDiscovery : Component, ISupportInitialize 
    {
        #region Property Defaults

        /// <summary>
        /// Default value for SearchURI property.
        /// </summary>
        private const string msdefSearchUri = null;

        /// <summary>
        /// Default value for DeviceFindOption property.
        /// </summary>
        private const DeviceFindOption mdfodefDeviceFindOption = DeviceFindOption.AllChildrenDeivces;

        /// <summary>
        /// Default value for ServiceFindOption property.
        /// </summary>
        private const ServiceFindOption msfodefServiceFindOption = ServiceFindOption.AllDeviceChildrenServices;

        /// <summary>
        /// Default value for ResolveNetworkInterface property.
        /// </summary>
        private const bool mbdefResolveNetworkInterface = false;

        /// <summary>
        /// Default value for active property.
        /// </summary>
        private const bool mbdefActive = false;

        /// <summary>
        /// Defaults value for AddressFamilyFlagsProperty.
        /// </summary>
        private const AddressFamilyFlags maffdefAddressFamilyFlags = AddressFamilyFlags.IPvBoth;

        #endregion

        #region Protected Locals

        /// <summary>
        /// The Search URI to filter devices by. 
        /// A search uri can be a Device.Type, Device.UDN, or Service.ServiceTypeIdentifier in the form
        /// of urn:schemas-upnp-org:type:ver, uuid:00000000-0000-0000-0000-000000000000, or
        /// urn:schemas-upnp-org:service:type:ver. If Service.ServiceTypeIdentifier is used, all devices which 
        /// have a direct service of that type will be returned. 
        /// </summary>
        protected string msSearchUri = msdefSearchUri;

        /// <summary>
        /// Determines which devices the DeviceAdded event handler should be raised for.
        /// </summary>
        protected DeviceFindOption mdfoDeviceFindOption = mdfodefDeviceFindOption;

        /// <summary>
        /// Determines which services the ServiceAdded event handler should be raised for.
        /// </summary>
        protected ServiceFindOption msfoServiceFindOption = msfodefServiceFindOption;

        /// <summary>
        /// Determines whether to resolve network interfaces of devices.
        /// </summary>
        protected bool mbResolveNetworkInterface = mbdefResolveNetworkInterface;

        /// <summary>
        /// Determines which type of network protocols search for devices on. 
        /// This fill is only effective on Windows Vista and above.
        /// </summary>
        protected AddressFamilyFlags maffAddressFamilyFlags = maffdefAddressFamilyFlags;

        /// <summary>
        /// The current discovery object running, or null if none running.
        /// </summary>
        protected Discovery mdDiscovery = null;

        /// <summary>
        /// The current active status, before or after initialisation.
        /// </summary>
        protected bool mbActive = mbdefActive;

        /// <summary>
        /// The initialisation level, 0 = initialised.
        /// </summary>
        protected int miInitialised = 0;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a new device is discoverred.
        /// </summary>
        public event DeviceAddedEventHandler DeviceAdded;

        /// <summary>
        /// Occurs when a new service is discoverred.
        /// </summary>
        public event ServiceAddedEventHandler ServiceAdded;

        /// <summary>
        /// Occrs when a device is removed.
        /// </summary>
        public event DeviceRemovedEventHandler DeviceRemoved;

        /// <summary>
        /// Occurs when a search has completed its initial phase.
        /// </summary>
        public event SearchCompleteEventHandler SearchComplete;

        /// <summary>
        /// Occurs when a search is started.
        /// </summary>
        public event EventHandler SearchStarted;

        /// <summary>
        /// Occurs when a search fails to start. The SearchEnded event 
        /// is still fired after this event on failure.
        /// </summary>
        public event EventHandler SearchFailed;

        /// <summary>
        /// Occurs when the search ends, either as a result of a failure, or
        /// as a result of the search being stopped.
        /// </summary>
        public event EventHandler SearchEnded;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new UPnPDiscovery component.
        /// </summary>
        public UPnPDiscovery()
        {
        }

        #endregion

        #region ISupportInitialize Implementation

        /// <summary>
        /// Occurs when the component begins its initialization.
        /// </summary>
        void ISupportInitialize.BeginInit()
        {
            miInitialised++;
        }

        /// <summary>
        /// Occurs when the component ends its initialization.
        /// </summary>
        void ISupportInitialize.EndInit()
        {
            if (miInitialised > 0) miInitialised--;

            if (Initialised)
            {
                if (Active && !DesignMode) SetActive(true);
            }
        }

        #endregion

        #region Event Callers

        /// <summary>
        /// Raises the DeviceAdded event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDeviceAdded(DeviceAddedEventArgs e)
        {
            if (DeviceAdded != null)
                DeviceAdded.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }

        /// <summary>
        /// Raises the DeviceAdded event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnServiceAdded(ServiceAddedEventArgs e)
        {
            if (ServiceAdded != null)
                ServiceAdded.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }

        /// <summary>
        /// Raises the DeviceRemoved event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDeviceRemoved(DeviceRemovedEventArgs e)
        {
            if (DeviceRemoved != null) 
                DeviceRemoved.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }

        /// <summary>
        /// Raises the search complete event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnSearchComplete(SearchCompleteEventArgs e)
        {
            if (SearchComplete != null)
                SearchComplete.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }
        /// <summary>
        /// Raises the search started event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnSearchStarted(EventArgs e)
        {
            if (SearchStarted != null)
                SearchStarted.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }
        /// <summary>
        /// Raises the search ended event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnSearchEnded(EventArgs e)
        {
            if (SearchEnded != null)
                SearchEnded.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }

        /// <summary>
        /// Raises the search failed event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnSearchFailed(EventArgs e)
        {
            if (SearchFailed != null)
                SearchFailed.GetInvocationList().InvokeEventGUIThreadSafe(this, e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Returns true if the discover is running.
        /// </summary>
        /// <returns>True if running, false otherwise.</returns>
        protected bool IsRunning()
        {
            return (mdDiscovery != null && mdDiscovery.Searching); 
        }

        /// <summary>
        /// Checks to see if the discovery is running and raises an exception if it is.
        /// </summary>
        /// <returns>True if teh discovery is not running.</returns>
        protected bool CheckNotRunning()
        {
            if (IsRunning())
                throw new InvalidOperationException(
                    "Search parameters cannot be changed while search is in progress, set active to false first.");

            return true;
        }

        /// <summary>
        /// Stops the UPnP discovery.
        /// </summary>
        protected void Stop()
        {
            // If the discovery is available
            if (mdDiscovery != null)
            {
                // Stop it if its searching
                if (mdDiscovery.Searching) mdDiscovery.Stop();

                // Remove events
                mdDiscovery.DeviceAdded -= new DeviceAddedEventHandler(mdDiscovery_DeviceAdded);
                mdDiscovery.DeviceRemoved -= new DeviceRemovedEventHandler(mdDiscovery_DeviceRemoved);
                mdDiscovery.SearchComplete -= new SearchCompleteEventHandler(mdDiscovery_SearchComplete);

                // Dispose of the discover and clear it
                mdDiscovery.Dispose();
                mdDiscovery = null;

                // Raise the SearchEnded event
                OnSearchEnded(new EventArgs());
            }
        }

        /// <summary>
        /// Starts the UPnP discovery.
        /// </summary>
        protected void Start()
        {
            if (!IsRunning())
            {
                // Create the discovery
                mdDiscovery = new Discovery(msSearchUri, maffAddressFamilyFlags, mbResolveNetworkInterface);

                // Add events
                mdDiscovery.DeviceAdded += new DeviceAddedEventHandler(mdDiscovery_DeviceAdded);
                mdDiscovery.DeviceRemoved += new DeviceRemovedEventHandler(mdDiscovery_DeviceRemoved);
                mdDiscovery.SearchComplete += new SearchCompleteEventHandler(mdDiscovery_SearchComplete);

                // Raise search started event
                OnSearchStarted(new EventArgs());

                // If the discover didnt start
                if (!mdDiscovery.Start())
                {
                    // Raise the search failed event
                    OnSearchFailed(new EventArgs());

                    // Stop and raise the search stopped event
                    Stop();
                }
            }
        }

        /// <summary>
        /// Sets the active property (post initialisation).
        /// </summary>
        /// <param name="value">The new value for the active property.</param>
        protected void SetActive(bool value)
        {
            mbActive = value;

            try
            {
                if (IsRunning() != value)
                    if (value) Start(); else Stop();
            }
            finally
            {
                mbActive = IsRunning();
            }
        }

        /// <summary>
        /// Raises events for when a new device is found.
        /// </summary>
        /// <param name="e">The event arguments containing the device information for the device that was found.</param>
        /// <param name="foundRoot">True if the device came from the discovery, false if it came from child devices.</param>
        protected void DeviceFound(DeviceAddedEventArgs e, bool foundRoot)
        {
            if (foundRoot || mdfoDeviceFindOption == Components.DeviceFindOption.AllChildrenDeivces)
                OnDeviceAdded(e);

            if (msfoServiceFindOption != ServiceFindOption.None)
            {
                if (msfoServiceFindOption == ServiceFindOption.AllDeviceChildrenServices ||
                    msfoServiceFindOption == ServiceFindOption.AllSearchURIMatchesServiceTypeId ||
                    (foundRoot || mdfoDeviceFindOption == DeviceFindOption.AllChildrenDeivces))
                    try
                    {
                        foreach (UPNPLib.IUPnPService lsService in e.COMDevice.Services)
                        {
                                if (
                                    (
                                        msfoServiceFindOption != ServiceFindOption.AllSearchURIMatchesServiceTypeId &&
                                        msfoServiceFindOption != ServiceFindOption.FoundDeviceDirectChildrenOnlySearchURIMatchesServiceTypeId
                                    )
                                    ||
                                    lsService.ServiceTypeIdentifier == msSearchUri)
                                    OnServiceAdded(new ServiceAddedEventArgs(e, lsService));
                        }
                    }
                    catch (Exception loE)
                    {
                        if (Logging.Enabled)
                            Logging.Log(
                                this,
                                string.Format("Exception occurred while enumerating Services: {0}", loE.ToString()));
                    }
            }

            if (mdfoDeviceFindOption == DeviceFindOption.AllChildrenDeivces ||
                msfoServiceFindOption == ServiceFindOption.AllDeviceChildrenServices ||
                msfoServiceFindOption == ServiceFindOption.AllSearchURIMatchesServiceTypeId)
                try
                {
                    foreach (UPNPLib.IUPnPDevice ldCOMDevice in e.COMDevice.Children)
                            DeviceFound(new DeviceAddedEventArgs(ldCOMDevice, e), false);
                }
                catch (Exception loE)
                {
                    if (Logging.Enabled)
                        Logging.Log(
                            this,
                            string.Format("Exception occurred while enumerating Devices: {0}", loE.ToString()));
                }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the SearchComplete event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void mdDiscovery_SearchComplete(object sender, SearchCompleteEventArgs e)
        {
            OnSearchComplete(new SearchCompleteEventArgs());
        }

        /// <summary>
        /// Handles the device removed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void mdDiscovery_DeviceRemoved(object sender, DeviceRemovedEventArgs e)
        {
            OnDeviceRemoved(e);
        }

        /// <summary>
        /// Handles the DeviceAdded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void mdDiscovery_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            DeviceFound(e, true);
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets whether initialisation is finished or not.
        /// </summary>
        protected bool Initialised
        {
            get
            {
                return miInitialised == 0;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Search URI to filter devices by. 
        /// A search uri can be a Device.Type, Device.UDN, or Service.ServiceTypeIdentifier in the form
        /// of urn:schemas-upnp-org:type:ver, uuid:00000000-0000-0000-0000-000000000000, or
        /// urn:schemas-upnp-org:service:type:ver. If Service.ServiceTypeIdentifier is used, all devices which 
        /// have a direct service of that type will be returned. This property can only be changed when search is
        /// not Active.
        /// </summary>
        [DefaultValue(msdefSearchUri)]
        [Description(
            "The search uri to filter Devices (and only devices) by, can be Device.Type, Device.UDN, or Service.ServiceTypeIdentifier in the form of:\r\n" +
            "  Device.Type - urn:schemas-upnp-org:type:ver\r\n" +
            "  Device.UDN - uuid:ffffffff-ffff-ffff-ffff-ffffffffffff\r\n" +
            "  Service.ServiceTypeIdentifier - urn:schemas-upnp-org:service:type:ver\r\n" + 
            "\r\n" + 
            "If Service.ServiceTypeIdentifier is used, all devices which have a direct service of that type will be returned.\r\n")]
        public string SearchURI
        {
            get
            {
                return msSearchUri;
            }
            set
            {
                if (msSearchUri != value && CheckNotRunning())  
                    msSearchUri = value;
            }
        }

        /// <summary>
        /// Gets or sets which devices the DeviceAdded event handler should be raised for. This property
        /// can only be changed when search is not Active.
        /// </summary>
        [DefaultValue(mdfodefDeviceFindOption)]
        [Description("Determines which devices the DeviceAdded event handler should be raised for.")]
        public DeviceFindOption DeviceFindOption
        {
            get
            {
                return mdfoDeviceFindOption;
            }
            set
            {
                if (mdfoDeviceFindOption != value && CheckNotRunning())
                    mdfoDeviceFindOption = value;
            }
        }

        /// <summary>
        /// Gets or sets which services the ServiceAdded event handler should be raised for. This property
        /// can only be changed when search is not Active.
        /// </summary>
        [DefaultValue(msfodefServiceFindOption)]
        [Description("Determines which services the ServiceAdded event handler should be raised for.")]
        public ServiceFindOption ServiceFindOption
        {
            get
            {
                return msfoServiceFindOption;
            }
            set
            {
                if (msfoServiceFindOption != value && CheckNotRunning())
                    msfoServiceFindOption = value;
            }
        }

        /// <summary>
        /// Gets or sets whether to resolve network interfaces of devices. This property
        /// can only be changed when search is not Active.
        /// </summary>
        [DefaultValue(mbdefResolveNetworkInterface)]
        [Description("Determines whether the network interface from which a Device came is resolved or not.")]
        public bool ResolveNetworkInterface
        {
            get
            {
                return mbResolveNetworkInterface;
            }
            set
            {
                if (mbResolveNetworkInterface != value && CheckNotRunning())
                    mbResolveNetworkInterface = value;
            }
        }

        /// <summary>
        /// Gets or sets which type of network protocols search for devices on. This property
        /// can only be changed when search is not Active. This parameter will only be effective
        /// on Windows Vista and above.
        /// </summary>
        [DefaultValue(maffdefAddressFamilyFlags)]
        [Description("Determines which type of network protocols search for devices on (only effective on Windows Vista and above).")]
        public AddressFamilyFlags AddressFamilyFlags
        {
            get
            {
                return maffAddressFamilyFlags;
            }
            set
            {
                if (maffAddressFamilyFlags != value && CheckNotRunning())
                    maffAddressFamilyFlags = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the search is active.
        /// </summary>
        [DefaultValue(mbdefActive)]
        [Description("Set this property to true to activate search as soon as component is created.")]
        public bool Active
        {
            get
            {
                return mbActive;
            }
            set
            {
                if (DesignMode || !Initialised)
                    mbActive = value;
                else
                    SetActive(value);
            }
        }

        #endregion
    }
}

#endif