//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using UPNPLib;
using System.Threading;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates the ability to discover devices and services.
    /// </summary>
    /// <remarks>
    /// Create an instance of the discovery class for asynchronous
    /// discoveries. Or use the static methods for synchronous 
    /// discoveries or quick asynchronous discoveries.
    /// </remarks>
    /// <example>
    ///     Asynchronous discovery using an instance of the Discovery class.
    ///     <code>
    ///        Discovery mdDiscovery;
    /// 
    ///        public void DiscoveryExample()
    ///        {
    ///             mdDiscovery = new Discovery(null);
    ///             ldDiscovery.DeviceAdded += new DeviceAddedEventHandler(mdDiscovery_DeviceAdded);
    ///             ldDiscovery.DeviceRemoved += new DeviceRemovedEventHandler(mdDiscovery_DeviceRemoved);
    ///             ldDiscovery.SearchComplete += new SearchCompleteEventHandler(mdDiscovery_SearchComplete);
    /// 
    ///             ldDiscovery.Start();
    ///         }
    /// 
    ///         void mdDiscovery_SearchComplete(object sender, SearchCompleteEventArgs e)
    ///         {
    ///             // NOTE: Event handler is not running in GUI thread
    ///             Console.WriteLine("Initial Search complete, scanning for new devices.");
    ///         }
    /// 
    ///         void mdDiscovery_DeviceRemoved(object sender, DeviceRemovedEventArgs e)
    ///         {
    ///             // NOTE: Event handler is not running in GUI thread
    ///             Console.WriteLine(string.Format("Device Removed: {0}", e.UDN));
    ///         }
    /// 
    ///         void mdDiscovery_DeviceAdded(object sender, DeviceAddedEventArgs e)
    ///         {
    ///             // NOTE: Event handler is not running in GUI thread
    ///             Console.WriteLine(string.Format("Device Added: {0}", e.Device.ToString()));
    ///         }
    ///     </code>
    ///     
    ///     Synchronous discovery by UDN using the static methods:
    ///     <code>
    ///         public void DiscoveryExample()
    ///         {
    ///             // Discovery by Unique Device Name
    ///             Device ldDevice = FindDeviceByUDN("uuid:abcdefab-abcd-abcd-abcd-abcdefabcdef")
    ///             
    ///             if (ldDevice != null)
    ///                 Console.WriteLine(string.Format("Device Found by UDN: {0}", ldDevice.FriendlyName));
    ///         }
    ///     </code>
    /// </example>
    public class Discovery : IDisposable
    {
        #region Public Delegates

        /// <summary>
        /// Called when a synchronous device find operation completes its initial search.
        /// </summary>
        /// <param name="devices">The devices that were found.</param>
        /// <param name="searchCompleted">True if the search completed and all available devices were returned.</param>
        public delegate void DevicesFoundDelegate(Devices devices, bool searchCompleted);

        /// <summary>
        /// Called when a synchronous service find operation completes its initial search.
        /// </summary>
        /// <param name="services">The services that were found.</param>
        /// <param name="searchCompleted">True if the search completed and all available services were returned.</param>
        public delegate void ServicesFoundDelegate(Services services, bool searchCompleted);

        #endregion

        #region Protected Constants

        /// <summary>
        /// All Devices search URI.
        /// </summary>
        protected const string csAllDevices = "upnp:rootdevice";

        #endregion

        #region Internal Classes

        /// <summary>
        /// Implements the IUPnPDeviceFinderCallback interface and
        /// raises events in the owned discovery class.
        /// </summary>
        internal class DeviceFinderCallback : IUPnPDeviceFinderCallback
        {
            #region Private Locals

            /// <summary>
            /// The discovery class linked to this callback, or null if
            /// the discovery class no longer wants the events raised.
            /// </summary>
            private Discovery mdDiscovery;

            #endregion

            #region Initialisation

            /// <summary>
            /// Creates a new device finder callback object.
            /// </summary>
            /// <param name="discovery">The discovery object for which the events should be raised.</param>
            public DeviceFinderCallback(Discovery discovery)
            {
                mdDiscovery = discovery;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Ignores all events from this point forward.
            /// </summary>
            public void Ignore()
            {
                mdDiscovery = null;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the discovery object linked to this callback object.
            /// </summary>
            public Discovery Discovery
            {
                get
                {
                    return mdDiscovery;
                }
            }

            #endregion

            #region IUPnPDeviceFinderCallback Interface Methods

            /// <summary>
            /// Called when a new device is discovered.
            /// </summary>
            /// <param name="findData">The search for which the UPnP framework is returning results.</param>
            /// <param name="device">The new device which was added.</param>
            void IUPnPDeviceFinderCallback.DeviceAdded(int findData, UPnPDevice device)
            {
                if (Discovery != null)
                    Discovery.OnDeviceAdded(new DeviceAddedEventArgs(device));
            }

            /// <summary>
            /// Called when a device is removed.
            /// </summary>
            /// <param name="findData">The search for which the UPnP framework is returning results.</param>
            /// <param name="udn">The unique device name for the device that was removed.</param>
            void IUPnPDeviceFinderCallback.DeviceRemoved(int findData, string udn)
            {
                if (Discovery != null)
                    Discovery.OnDeviceRemoved(new DeviceRemovedEventArgs(udn));
            }

            /// <summary>
            /// Called when the initial search is complete.
            /// </summary>
            /// <param name="findData">The search for which the UPnP framework is returning results.</param>
            void IUPnPDeviceFinderCallback.SearchComplete(int findData)
            {
                if (Discovery != null)
                    Discovery.OnSearchComplete(new SearchCompleteEventArgs());
            }

            #endregion
        }

        /// <summary>
        /// Implements the IUPnPDeviceFinderCallbackWithInterface interface and
        /// raises events in the owned discovery class.
        /// </summary>
        internal class DeviceFinderCallbackWithInterface : DeviceFinderCallback, IUPnPDeviceFinderAddCallbackWithInterface
        {
            #region Public Initialisation

            public DeviceFinderCallbackWithInterface(Discovery discovery)
                : base(discovery)
            {
            }

            #endregion

            #region IUPnPDeviceFinderCallbackWithInterface Interface Methods

            /// <summary>
            /// Called when a new device is discovered.
            /// </summary>
            /// <param name="findData">The search for which the UPnP framework is returning results.</param>
            /// <param name="device">The new device which was added.</param>
            /// <param name="guidInterface">The network interface Guid from which the device came.</param>
            void IUPnPDeviceFinderAddCallbackWithInterface.DeviceAddedWithInterface(
                int findData, UPnPDevice device, ref Guid guidInterface)
            {
                if(Discovery != null)
                    Discovery.OnDeviceAdded(new DeviceAddedEventArgs(device, guidInterface));
            }

            #endregion
        }

        #endregion

        #region Private Locals

        /// <summary>
        /// The callback for raising the events.
        /// </summary>
        private DeviceFinderCallback mdfcCallback = null;

        #endregion

        #region Protected Locals

        /// <summary>
        /// The finder being used to discover devices.
        /// </summary>
        protected UPnPDeviceFinder mfFinder = new UPnPDeviceFinder();
        
        /// <summary>
        /// The address family of the interfaces to search in. (Vista and above only)
        /// </summary>
        protected AddressFamilyFlags mafAddressFamily;

        /// <summary>
        /// The base device type URI string for searching.
        /// </summary>
        protected string msSearchURI;

        /// <summary>
        /// The current find handle if searching or 0 if none.
        /// </summary>
        protected int miFindHandle = 0;

        /// <summary>
        /// True to resolve network interface GUIDs.
        /// </summary>
        protected bool mbResolveNetworkInterface = false;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a new device is discoverred.
        /// </summary>
        public event DeviceAddedEventHandler DeviceAdded;

        /// <summary>
        /// Occrs when a device is removed.
        /// </summary>
        public event DeviceRemovedEventHandler DeviceRemoved;

        /// <summary>
        /// Occurs when a search has completed its initial phase.
        /// </summary>
        public event SearchCompleteEventHandler SearchComplete;

        #endregion

        #region Internal Static Methods

        /// <summary>
        /// Finds a native device by UDN.
        /// </summary>
        /// <param name="udn">The UDN for the device.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <returns>The device if one is found or null if not found.</returns>
        internal static IUPnPDevice FindNativeDeviceByUDN(
            string udn, AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth)
        {
            UPnPDeviceFinder lfFinder = new UPnPDeviceFinder();

            if (lfFinder is IUPnPAddressFamilyControl)
                ((IUPnPAddressFamilyControl)lfFinder).SetAddressFamily((int)addressFamily);

            return lfFinder.FindByUDN(udn);
        }

        /// <summary>
        /// Finds native devices by device or service type.
        /// </summary>
        /// <param name="deviceOrServiceType">The partial or full device or service type to search for.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <returns>A UPnP Devices collection containing the found devices.</returns>
        internal static IUPnPDevices FindNativeDevices(
            string deviceOrServiceType,
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth)
        {
            UPnPDeviceFinder lfFinder = new UPnPDeviceFinder();

            if (lfFinder is IUPnPAddressFamilyControl) 
                ((IUPnPAddressFamilyControl)lfFinder).SetAddressFamily((int)addressFamily);

            return lfFinder.FindByType(deviceOrServiceType, 0);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Finds a device by UDN synchronously.
        /// </summary>
        /// <param name="udn">The UDN for the device (cannot be null or empty).</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <returns>The device if one is found or null if not found.</returns>
        /// <exception cref="System.ArgumentException">Thrown when udn is null or empty.</exception>
        public static Device FindDeviceByUDN(
            string udn, AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth)
        {
            if (String.IsNullOrEmpty(udn))
                throw new ArgumentException("cannot be null or empty string", "udn");

            IUPnPDevice ldDevice = FindNativeDeviceByUDN(udn, addressFamily);

            if (ldDevice != null)
                return new Device(ldDevice, Guid.Empty);
            else
                return null;
        }

        /// <summary>
        /// Finds devices by device or service type asynchronously using a timeout period.
        /// </summary>
        /// <param name="uriString">The URI string to search for or null for all devices.</param>
        /// <param name="timeoutMS">The maximum timeout time in milliseconds to wait.</param>
        /// <param name="maxDevices">The maximum number of devices to find before returning.</param>
        /// <param name="devicesFound">
        /// The delegate to call when async operation is complete. <para /> NOTE: this delegate is executed
        /// in a different to the calling thread.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <param name="resolveNetworkInterfaces">True to resolve network interface Guids.</param>
        /// <returns>A Devices list containing the found devices.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when devicesFound delegate is null.</exception>
        public static void FindDevicesAsync(
            string uriString,
            int timeoutMS,
            int maxDevices,
            DevicesFoundDelegate devicesFound, 
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth,
            bool resolveNetworkInterfaces = false)
        {
            if(devicesFound == null) throw new ArgumentNullException("devicesFound");

            Object[] loParams = new object[] 
            { 
                uriString, timeoutMS, maxDevices, 
                addressFamily, resolveNetworkInterfaces,
                devicesFound
            };

            Devices ldDevices = null;
            bool lbSearchCompleted = false;
            Thread ltCurrent = Thread.CurrentThread;

            Thread ltThread = new Thread(
                (object args) => 
                    {
                        try
                        {
                            object[] loArgs = (object[])args;

                            ldDevices = Discovery.FindDevices(
                                (string)loArgs[0], (int)loArgs[1], (int)loArgs[2],
                                out lbSearchCompleted, (AddressFamilyFlags)loArgs[3],
                                (bool)loArgs[4]);
                        }
                        finally
                        {
                            devicesFound(ldDevices, lbSearchCompleted);
                        }
                    }
            );

            ltThread.Start(loParams);
        }

        /// <summary>
        /// Finds services by service type asynchronously using a timeout period.
        /// </summary>
        /// <param name="serviceType">The service type to search for or null for all.</param>
        /// <param name="timeoutMS">The maximum timeout time in milliseconds to wait.</param>
        /// <param name="maxDevices">The maximum number of devices to find before returning.</param>
        /// <param name="servicesFound">
        /// The delegate to call when async operation is complete.<para />NOTE: this delegate is executed
        /// in a different to the calling thread.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <param name="resolveNetworkInterfaces">True to resolve network interface Guids.</param>
        /// <returns>A Devices list containing the found devices.</returns>
        public static void FindServicesAsync(
            string serviceType,
            int timeoutMS,
            int maxDevices,
            ServicesFoundDelegate servicesFound,
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth,
            bool resolveNetworkInterfaces = false)
        {
            FindDevicesAsync(
                serviceType, timeoutMS, maxDevices,
                (devices, completed) => { servicesFound(devices.FindServices(serviceType, true), completed); },
                addressFamily, resolveNetworkInterfaces);
        }

        /// <summary>
        /// Finds all services synchronously.
        /// </summary>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <param name="resolveNetworkInterfaces">True to resolve network interface Guids.</param>
        /// <returns>A Services list containing the found services.</returns>
        public static Services FindServices(
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth,
            bool resolveNetworkInterfaces = false)
        {
            bool lbCompleted = true;

            return FindServices(
                null, int.MaxValue, int.MaxValue, 
                out lbCompleted, 
                addressFamily, resolveNetworkInterfaces); 
        }

        /// <summary>
        /// Finds services by service type synchronously using a timeout period.
        /// </summary>
        /// <param name="serviceType">The service type to search for or null for all.</param>
        /// <param name="timeoutMS">The maximum timeout time in milliseconds to wait.</param>
        /// <param name="maxDevices">The maximum number of devices to find before returning.</param>
        /// <param name="searchCompleted">True if the search completed and all available devices were returned.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <param name="resolveNetworkInterfaces">True to resolve network interface Guids.</param>
        /// <returns>A Services list containing the found services.</returns>
        public static Services FindServices(
            string serviceType,
            int timeoutMS,
            int maxDevices,
            out bool searchCompleted,
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth,
            bool resolveNetworkInterfaces = false)
        {
            Devices ldDevices = 
                FindDevices(
                    serviceType, timeoutMS, maxDevices, out searchCompleted, 
                    addressFamily, resolveNetworkInterfaces);

            return ldDevices.FindServices(serviceType, true);
        }

        /// <summary>
        /// Finds devices by device or service type synchronously using a timeout period.
        /// </summary>
        /// <param name="uriString">The URI string to search for or null / empty for all.</param>
        /// <param name="timeoutMS">The maximum timeout time in milliseconds to wait or -1 for wait forever.</param>
        /// <param name="maxDevices">The maximum number of devices to find before returning or 0 for as many as possible.</param>
        /// <param name="searchCompleted">True if the search completed and all available devices were returned.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <param name="resolveNetworkInterfaces">True to resolve network interface Guids.</param>
        /// <returns>A Devices list containing the found devices.</returns>
        public static Devices FindDevices(
            string uriString,
            int timeoutMS,
            int maxDevices,
            out bool searchCompleted,
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth,
            bool resolveNetworkInterfaces = false)
        {
            Discovery ldDiscovery = new Discovery(uriString, addressFamily, resolveNetworkInterfaces);
            Devices ldDevices = new Devices();
            bool lbSearchCompleted = false;
            ManualResetEvent lmreComplete = new ManualResetEvent(false);

            ldDiscovery.DeviceAdded += 
                (sender, args) => 
                { 
                    ldDevices.Add(args.Device);
                    if (maxDevices > 0 && ldDevices.Count >= maxDevices)
                        lmreComplete.Set();
                };
            
            ldDiscovery.SearchComplete += (sender, args) => 
                {
                    lbSearchCompleted = true;
                    lmreComplete.Set();
                };

            ldDiscovery.Start();

            lmreComplete.WaitOne(timeoutMS);

            searchCompleted = lbSearchCompleted;
            ldDiscovery.Dispose();

            return ldDevices;
        }

        /// <summary>
        /// Finds devices by URI synchronously waiting for the entire search to complete.
        /// </summary>
        /// <param name="deviceOrServiceType">The partial or full device or service type to search for (cannot be null or empty).</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <returns>A Devices list containing the found devices.</returns>
        /// <exception cref="System.ArgumentException">Thrown when deviceOrServiceType is null or empty.</exception>
        public static Devices FindDevices(
            string deviceOrServiceType,
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth)
        {
            if (String.IsNullOrEmpty(deviceOrServiceType))
                throw new ArgumentException("cannot be null or empty string", "deviceOrServiceType");

            IUPnPDevices ldDevices = FindNativeDevices(deviceOrServiceType, 0);

            if (ldDevices != null)
                return new Devices(ldDevices, Guid.Empty);
            else
                return null;
        }

        /// <summary>
        /// Finds services by URI synchronously waiting for the entire search to complete.
        /// </summary>
        /// <param name="serviceType">The partial or full service type to search for (cannot be null or empty).</param>
        /// <returns>A list of services containing the found services.</returns>
        /// <exception cref="System.ArgumentException">Thrown when serviceType is null or empty.</exception>
        public static Services FindServices(string serviceType)
        {
            if (String.IsNullOrEmpty(serviceType))
                throw new ArgumentException("cannot be null or empty string", "serviceType");

            IUPnPDevices ldDevices = FindNativeDevices(serviceType);
            return ldDevices.FindServices(serviceType);
        }

        /// <summary>
        /// Finds all (recursively) services of a certain type for a specific device by udn.
        /// </summary>
        /// <param name="udn">The udn of the device to search for services in (cannot be null or empty).</param>
        /// <param name="serviceType">The partial or full service type of the service.</param>
        /// <param name="addressFamily">The address family to search in. (Vista or above only).</param>
        /// <returns>A list of services containing the found services on the device.</returns>
        /// <exception cref="System.ArgumentException">Thrown when udn is null or empty.</exception>
        public static Services FindServicesByUDN(
            string udn, string serviceType,
            AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth)
        {
            if (String.IsNullOrEmpty(udn))
                throw new ArgumentException("cannot be null or empty string", "udn");

            IUPnPDevice ldDevice = FindNativeDeviceByUDN(udn, addressFamily);

            if (ldDevice != null)
                return ldDevice.FindServices(Guid.Empty, serviceType);
            else
                return new Services();
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new discovery class.
        /// </summary>
        /// <param name="searchURI">
        /// The URI string to filter deviecs by or null / empty for all. 
        /// Gets or sets the Search URI to filter devices by. 
        /// A search uri can be a Device.Type, Device.UDN, or Service.ServiceTypeIdentifier in the form
        /// of urn:schemas-upnp-org:deviceTypeName:deviceTypeVersion, uuid:00000000-0000-0000-0000-000000000000, or
        /// urn:schemas-upnp-org:service:serviceTypeName:serviceTypeVersion.
        /// </param>
        /// <param name="addressFamily">The address family of the interfaces to search in (Vista and above only).</param>
        /// <param name="resolveNetworkInterfaces">True to resolve network interface Guids.</param>
        public Discovery(string searchURI, AddressFamilyFlags addressFamily = AddressFamilyFlags.IPvBoth, bool resolveNetworkInterfaces = false)
        {
            mbResolveNetworkInterface = resolveNetworkInterfaces;
            msSearchURI = searchURI;
            mafAddressFamily = addressFamily;
        }

        #endregion

        #region Event Callers

        /// <summary>
        /// Raises the DeviceAdded event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDeviceAdded(DeviceAddedEventArgs e)
        {
            if (Logging.Enabled)
                Logging.Log(this, string.Format("Async discovery device added: '{0}' ('UDN:{1}')", e.COMDevice.FriendlyName, e.COMDevice.UniqueDeviceName));
            if (DeviceAdded != null) DeviceAdded(this, e);
        }

        /// <summary>
        /// Raises the DeviceRemoved event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDeviceRemoved(DeviceRemovedEventArgs e)
        {
            if (Logging.Enabled)
                Logging.Log(this, string.Format("Async discovery device removed: 'UDN:{0}'", e.UDN));
            if (DeviceRemoved != null) DeviceRemoved(this, e);
        }

        /// <summary>
        /// Raises the search complete event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnSearchComplete(SearchCompleteEventArgs e)
        {
            if (Logging.Enabled)
                Logging.Log(this, "Async discovery search completed");
            if (SearchComplete != null) SearchComplete(this, e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts an asynchronous search using current parameters if one isnt already in progress.
        /// </summary>
        /// <returns>True if a new asynchronous search was started, false otherwise.</returns>
        public bool Start()
        {
            if (Logging.Enabled)
                Logging.Log(
                    this,
                    string.Format(
                        "Async discovery starting with - SearchURI:'{0}', ResolveNetworkInterface:'{1}', AddressFamily:'{2}'",
                        msSearchURI, mbResolveNetworkInterface, mafAddressFamily.ToString()), 1);

            try
            {
                if (miFindHandle == 0)
                {
                    // Set the address family if its available
                    if (mfFinder is IUPnPAddressFamilyControl)
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, "Setting address family");
                        ((IUPnPAddressFamilyControl)mfFinder).SetAddressFamily((int)mafAddressFamily);
                    }

                    // If we want to resolve the network interface
                    if (Logging.Enabled)
                        Logging.Log(this, "Creating callback");
                    if (mbResolveNetworkInterface)
                        // Then use the with interface device finder callback
                        mdfcCallback = new DeviceFinderCallbackWithInterface(this);
                    else
                        // Otherwise use the standard device finder callback
                        mdfcCallback = new DeviceFinderCallback(this);

                    string lsSearchURI;

                    // If search uri isnt specified
                    if (string.IsNullOrEmpty(msSearchURI))
                    {
                        // Search for all devices
                        if (Logging.Enabled)
                            Logging.Log(this, "Search URI not specified using all devices");
                        lsSearchURI = csAllDevices;
                    }
                    else
                        // Otherwise use search uri
                        lsSearchURI = msSearchURI;

                    // Create the find handle
                    if (Logging.Enabled)
                        Logging.Log(this, "Creating find handle");
                    miFindHandle = mfFinder.CreateAsyncFind(lsSearchURI, 0, mdfcCallback);

                    // Start the search 
                    if (miFindHandle != 0)
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, "Starting async find");
                        mfFinder.StartAsyncFind(miFindHandle);
                    }
                    else
                        if (Logging.Enabled)
                            Logging.Log(this, "Creation of find handle failed");

                    // Return true if the search was started
                    return miFindHandle != 0;
                }
                else
                {
                    // Already searching, return false
                    if (Logging.Enabled) 
                        Logging.Log(this, "Discovery already in progress");
                    return false;
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Finished async discovery start", -1);
            }
        }

        /// <summary>
        /// Stops the current asynchronous search.
        /// </summary>
        public void Stop()
        {
            if (Logging.Enabled)
                Logging.Log(
                    this,
                    string.Format(
                        "Async discovery stopping with - SearchURI:'{0}', ResolveNetworkInterface:'{1}', AddressFamily:'{2}'",
                        msSearchURI, mbResolveNetworkInterface, mafAddressFamily.ToString()), 1);

            try
            {
                // If we are searching
                if (miFindHandle != 0)
                {
                    // Cancel the search
                    if (Logging.Enabled)
                        Logging.Log(this, "Cancelling async find");
                    mfFinder.CancelAsyncFind(miFindHandle);

                    // Ignore the callback
                    if (Logging.Enabled)
                        Logging.Log(this, "Notifying callback to ignore");
                    mdfcCallback.Ignore();

                    // Clear the callback
                    mdfcCallback = null;

                    // Reset the find handle
                    miFindHandle = 0;
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Finished async discovery stop", -1);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets whether to resolve the network interface Gui for the devices.
        /// </summary>
        public bool ResolveNetworkInterface
        {
            get
            {
                return mbResolveNetworkInterface;
            }
        }

        /// <summary>
        /// Gets the address family to search in (Vista or above only).
        /// </summary>
        public AddressFamilyFlags AddressFamily
        {
            get
            {
                return mafAddressFamily;
            }
        }

        /// <summary>
        /// Gets the search URI to search for.
        /// </summary>
        public string SearchURI
        {
            get
            {
                return msSearchURI;
            }
        }

        /// <summary>
        /// Gets whether this object is currently searching.
        /// </summary>
        public bool Searching
        {
            get
            {
                return miFindHandle != 0;
            }
        }

        #endregion

        #region Finalisation

        /// <summary>
        /// Finalisation for disposal.
        /// </summary>
        ~Discovery()
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
                if (mfFinder != null)
                {
                    Stop();
                    mfFinder = null;
                }
            }
        }

        #endregion
    }
}
