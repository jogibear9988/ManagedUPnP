//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using ManagedUPnP.Extensions;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UPNPLib;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates a UPnP Device.
    /// </summary>
    public class Device 
    {
        #region Private Locals

        /// <summary>
        /// Hold the handle to the Native COM Device
        /// </summary>
        private IUPnPDevice mdCOMDevice;

        #endregion

        #region Protected Locals

        /// <summary>
        /// Contains the Guid for the Network interface this device is on.
        /// </summary>
        /// <remarks>Only valid if mbInterfaceGuidAvailable is true.</remarks>
        protected Guid mgInterfaceGuid;

        /// <summary>
        /// True if mgInterfaceGuid is set.
        /// </summary>
        protected bool mbInterfaceGuidAvailable;

        #endregion

        #region Internal Initialisation

        /// <summary>
        /// Creates a new device.
        /// </summary>
        /// <param name="comDevice">The Native COM Device for which this device is linked to.</param>
        /// <param name="interfaceGuid">The Network interface Guid ID for the device or Guid.Empty for unknown.</param>
        internal Device(IUPnPDevice comDevice, Guid interfaceGuid)
        {
            mbInterfaceGuidAvailable = !interfaceGuid.Equals(Guid.Empty);
            mgInterfaceGuid = interfaceGuid;
            mdCOMDevice = comDevice;
        }

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new device from an already created device.
        /// </summary>
        /// <param name="device">The device to create from.</param>
        public Device(Device device)
            : this(device.COMDevice, device.InterfaceGuid) 
        {
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the Native COM Device interface for this device.
        /// </summary>
        internal IUPnPDevice COMDevice
        {
            get
            {
                return mdCOMDevice;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the devices by Type for this list.
        /// </summary>
        /// <param name="type">The Type for the devices to get.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the type.</returns>
        public Devices DevicesByType(string type, bool recursive = true)
        {
            Devices ldDevices = new Devices();

            if (Type == type) ldDevices.Add(this);
            if (HasChildren) Children.AddDevicesByType(type, ldDevices, recursive);

            return ldDevices;
        }

        /// <summary>
        /// Gets the devices by ModelName for this list.
        /// </summary>
        /// <param name="modelName">The ModelName for the devices to get.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the type.</returns>
        public Devices DevicesByModelName(string modelName, bool recursive = true)
        {
            Devices ldDevices = new Devices();

            if (ModelName == modelName) ldDevices.Add(this);
            if (HasChildren) Children.AddDevicesByModelName(modelName, ldDevices, recursive);

            return ldDevices;
        }

        /// <summary>
        /// Gets the first device by Type for this list.
        /// </summary>
        /// <param name="type">The Type for the device to get.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the type.</returns>
        public Device FirstDeviceByType(string type, bool recursive = true)
        {
            Devices ldDevices = DevicesByType(type, recursive);
            return (ldDevices.Count > 0 ? ldDevices[0] : null);
        }

        /// <summary>
        /// Gets the first device by ModelName for this list.
        /// </summary>
        /// <param name="modelName">The ModelName for the devices to get.</param>
        /// <param name="recursive">True to search recursively.</param>
        /// <returns>The Devices that matched the type.</returns>
        public Device FirstDeviceByModelName(string modelName, bool recursive = true)
        {
            Devices ldDevices = DevicesByModelName(modelName, recursive);
            return (ldDevices.Count > 0 ? ldDevices[0] : null);
        }

        /// <summary>
        /// Gets all relevant information for this device in a string format.
        /// </summary>
        /// <returns>A String.</returns>
        public override string ToString()
        {
            return mdCOMDevice.ReadableInfo();
        }

        /// <summary>
        /// Gets the URL for an icon for this device.
        /// </summary>
        /// <param name="encodingFormat">The MIME type of the encoding format that is requested for the icon.</param>
        /// <param name="sizeX">Specifies the width of the icon, in pixels. Standard values are 16, 32, or 48.</param>
        /// <param name="sizeY">Specifies the height of the icon, in pixels. Standard values are 16, 32, or 48 pixels.</param>
        /// <param name="bitDepth">Specifies the bit depth of the icon. Standard values are 8, 16, or 24.</param>
        /// <returns>A string representing the URL for the icon.</returns>
        /// <remarks>
        /// An application can specify any values for lSizeX, lSizeY, and lBitDepth. However, there is no 
        /// guarantee that an icon exists with those specifications. If a matching icon does not exist, 
        /// the URL for the icon that most closely matches the size and bit depth specified is returned.
        /// </remarks>
        public string GetIconURL(string encodingFormat, int sizeX, int sizeY, int bitDepth)
        {
            return COMDevice.IconURL(encodingFormat, sizeX, sizeY, bitDepth);
        }

        /// <summary>
        /// Gets the image for an icon from the device if its available.
        /// </summary>
        /// <param name="encodingFormat">The mime encoding format eg. "image/jpeg".</param>
        /// <param name="sizeX">The requested width of the icon.</param>
        /// <param name="sizeY">The requested height of the icon.</param>
        /// <param name="bitDepth">The requested bit depth of the icon.</param>
        /// <returns>An image if found or null if not.</returns>
        public Image GetIconImage(string encodingFormat = null, int sizeX = 0, int sizeY = 0, int bitDepth = 0)
        {
            if (Logging.Enabled)
                Logging.Log(this, String.Format("Getting icon image for device: '{0}'", this.FriendlyName));

            try
            {
                if (String.IsNullOrEmpty(encodingFormat)) encodingFormat = "image/jpeg";
                if (sizeX == 0) sizeX = 32;
                if (sizeY == 0) sizeY = 32;
                if (bitDepth == 0) bitDepth = 24;

                if (Logging.Enabled)
                    Logging.Log(this, string.Format("Getting URL for icon: Size:'{0}x{1}', BitDepth'{2}'", sizeX, sizeY, bitDepth));
                string msURL = GetIconURL(encodingFormat, sizeX, sizeY, bitDepth);

                if (String.IsNullOrEmpty(msURL))
                {
                    if (Logging.Enabled)
                        Logging.Log(this, "URL is null or empty, failed");
                    return null;
                }

                if (Logging.Enabled)
                    Logging.Log(this, String.Format("Requesting icon from URL: '{0}'", msURL));

                try
                {
                    if (Logging.Enabled)
                        Logging.Log(this, "Downloading data");
                    Stream lsStream = Utils.GetURLStream(msURL);

                    if (Logging.Enabled)
                        Logging.Log(this, "Processing data to image");
                    Image limgImage = Image.FromStream(lsStream);
                    return limgImage;
                }
                catch (Exception loE)
                {
                    if (Logging.Enabled)
                        Logging.Log(this, String.Format("Getting icon image failed with exception: '{0}'", loE.ToString()));
                    return null;
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Finished getting icon image for device");
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the host name for the device.
        /// </summary>
        public string RootHostName
        {
            get
            {
                Uri luURI = new Uri(RootDevice.DocumentURL);
                return luURI.Host;
            }
        }

        /// <summary>
        /// Gets the Host IP addresses for the device.
        /// </summary>
        public IPAddress[] RootHostAddresses
        {
            get
            {
                string lsHostName = RootHostName;
                IPAddress lipAddress;

                if (!IPAddress.TryParse(lsHostName, out lipAddress))
                {
                    try
                    {
                        return Dns.GetHostEntry(lsHostName).AddressList;
                    }
                    catch
                    {
                        return new IPAddress[0];
                    }
                }
                else
                    return new IPAddress[] { lipAddress };
            }
        }

        /// <summary>
        /// Gets all network interfaces which this device is connectable from.
        /// </summary>
        public NetworkInterface[] Adapters
        {
            get
            {
                ArrayList lalReturn = new ArrayList();

                // Get the host IP Addresses
                IPAddress[] lipAddresses = null;

                // Get all the network interfaces
                NetworkInterface[] lniNics = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface lniNIC in lniNics)
                {
                    IPInterfaceProperties lipProps = lniNIC.GetIPProperties();
                    UnicastIPAddressInformationCollection lipNICAddresses = lipProps.UnicastAddresses;

                    // If Inteface Guid is available
                    if (InterfaceGuidAvailable)
                    {
                        // If guid mataches 
                        if (InterfaceGuid.Equals(new Guid(lniNIC.Id)))
                        {
                            lalReturn.Add(lniNIC);
                            break;
                        }
                    }
                    else
                    {
                        // Get host addresses if needed
                        if (lipAddresses == null) lipAddresses = RootHostAddresses;

                        // True if NIC has been added
                        bool lbAdded = false;

                        // For each address belonging to the network interface
                        foreach (UnicastIPAddressInformation lipNICAddress in lipNICAddresses)
                        {
                            // For each address belonging to the device host
                            foreach (IPAddress lipAddress in lipAddresses)
                                // If it is connectable form this interface
                                if (lipAddress.ConnectableFrom(lipNICAddress))
                                {
                                    // Add the nick
                                    lalReturn.Add(lniNIC);
                                    lbAdded = true;
                                    break;
                                }

                            // If NIC has been added then dont check other addresses for this NIC
                            if (lbAdded) break;
                        }
                    }
                }

                return (NetworkInterface[])lalReturn.ToArray(typeof(NetworkInterface));
            }
        }

        /// <summary>
        /// Returns an array of Unicast IP addresses identifying the Host adapters
        /// which can connect to this device.
        /// </summary>
        /// <returns>An array IP Addresses.</returns>
        public UnicastIPAddressInformation[] AdapterUnicastIPAddressInformations
        {
            get
            {
                if (InterfaceGuidAvailable)
                    return Adapters.SelectMany((adapter) => adapter.GetIPProperties().UnicastAddresses).ToArray();
                else
                {
                    ArrayList lalReturn = new ArrayList();

                    // Get the host IP Addresses
                    IPAddress[] lipAddresses = RootHostAddresses;

                    // Get all the network interfaces
                    NetworkInterface[] lniNics = NetworkInterface.GetAllNetworkInterfaces();

                    foreach (NetworkInterface lniNIC in lniNics)
                    {
                        IPInterfaceProperties lipProps = lniNIC.GetIPProperties();
                        UnicastIPAddressInformationCollection lipNICAddresses = lipProps.UnicastAddresses;

                        // For each address belonging to the network interface
                        foreach (UnicastIPAddressInformation lipNICAddress in lipNICAddresses)
                            // For each address belonging to the device host
                            foreach (IPAddress lipAddress in lipAddresses)
                                // If the address is connectable
                                if (lipAddress.ConnectableFrom(lipNICAddress))
                                    // Add it and dont check other addresess
                                    lalReturn.Add(lipNICAddress);
                    }

                    return (UnicastIPAddressInformation[])lalReturn.ToArray(typeof(UnicastIPAddressInformation));
                }
            }
        }

        /// <summary>
        /// Returns an array of IP addresses identifying the Host adapters
        /// which can connect to this device.
        /// </summary>
        /// <returns>An array IP Addresses.</returns>
        public IPAddress[] AdapterIPAddresses
        {
            get
            {
                return
                    AdapterUnicastIPAddressInformations.Select((unicastAddressInfo) => unicastAddressInfo.Address).ToArray();
            }
        }

        /// <summary>
        /// Gets the network interface Guid for this device.
        /// </summary>
        public Guid InterfaceGuid
        {
            get
            {
                return mgInterfaceGuid;
            }
        }

        /// <summary>
        /// Gets whether the InterfaceGuid property is set and is valid.
        /// </summary>
        public bool InterfaceGuidAvailable
        {
            get
            {
                return mbInterfaceGuidAvailable;
            }
        }

        /// <summary>
        /// Gets the immediate child devices for this device. 
        /// </summary>
        public Devices Children
        {
            get
            {
                return new Devices(COMDevice, InterfaceGuid);
            }
        }

        /// <summary>
        /// Gets the description for this device.
        /// </summary>
        public string Description
        {
            get
            {
                return COMDevice.Description;
            }
        }

        /// <summary>
        /// Gets the friendly name for this device.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                return COMDevice.FriendlyName;
            }
        }

        /// <summary>
        /// Gets whether this device has children.
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return COMDevice.HasChildren;
            }
        }

        /// <summary>
        /// Gets whether this device is the root device.
        /// </summary>
        public bool IsRootDevice
        {
            get
            {
                return COMDevice.IsRootDevice;
            }
        }

        /// <summary>
        /// Gets the manufacturer name for this device.
        /// </summary>
        public string ManufacturerName
        {
            get
            {
                return COMDevice.ManufacturerName;
            }
        }

        /// <summary>
        /// Gets the document URL for the Device.
        /// </summary>
        public string DocumentURL
        {
            get
            {
                return COMDevice.GetDocumentURL();
            }
        }

        /// <summary>
        /// Gets the manufacturer URL for this device.
        /// </summary>
        public string ManufacturerURL
        {
            get
            {
                return COMDevice.ManufacturerURL;
            }
        }

        /// <summary>
        /// Gets the model name for this device.
        /// </summary>
        public string ModelName
        {
            get
            {
                return COMDevice.ModelName;
            }
        }

        /// <summary>
        /// Gets the model number for this device.
        /// </summary>
        public string ModelNumber
        {
            get
            {
                return COMDevice.ModelNumber;
            }
        }

        /// <summary>
        /// Gets the model URL for this device.
        /// </summary>
        public string ModelURL
        {
            get
            {
                return COMDevice.ModelURL;
            }
        }

        /// <summary>
        /// Gets the parent device for this device or null if this is the top parent device.
        /// </summary>
        public Device ParentDevice
        {
            get
            {
                if (COMDevice.ParentDevice != null)
                    return new Device(COMDevice.ParentDevice, mgInterfaceGuid);
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the URL used to access the devices interface in a browser.
        /// </summary>
        public string PresentationURL
        {
            get
            {
                return COMDevice.PresentationURL;
            }
        }

        /// <summary>
        /// Gets the root device for this device.
        /// </summary>
        public Device RootDevice
        {
            get
            {
                return new Device(COMDevice.RootDevice, mgInterfaceGuid);
            }
        }

        /// <summary>
        /// Gets the serial number for this device.
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return COMDevice.SerialNumber;
            }
        }

        /// <summary>
        /// Gets the immediate services this device provides.
        /// </summary>
        public Services Services
        {
            get
            {
                return new Services(COMDevice, InterfaceGuid);
            }
        }

        /// <summary>
        /// Gets the type of this device.
        /// </summary>
        public string Type
        {
            get
            {
                return COMDevice.Type;
            }
        }

        /// <summary>
        /// Gets the UDN of this device.
        /// </summary>
        public string UniqueDeviceName
        {
            get
            {
                return COMDevice.UniqueDeviceName;
            }
        }

        /// <summary>
        /// Gets the UPC of this device.
        /// </summary>
        public string UniversalProductCode
        {
            get
            {
                return COMDevice.UPC;
            }
        }

        /// <summary>
        /// Gets the device by UDN for this device and its children recusively.
        /// </summary>
        /// <param name="udn">The UDN for the device to get.</param>
        /// <returns>The Device if UDN is found or null if not.</returns>
        public Device this[string udn]
        {
            get
            {
                if (UniqueDeviceName == udn)
                    return this;
                else
                    if (HasChildren)
                        return this.Children[udn];
                    else
                        return null;
            }
        }

        #endregion
    }
}
