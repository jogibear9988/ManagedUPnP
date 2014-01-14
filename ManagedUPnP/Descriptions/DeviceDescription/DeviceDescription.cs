//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Text;
using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a description for a device.
    /// </summary>
    public class DeviceDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for a device.
        /// </summary>
        protected const string msElement = "device";

        #endregion

        #region Protected Locals

        /// <summary>
        /// The list of immediate devices for this device description.
        /// </summary>
        protected DevicesDescription mdDevices;

        /// <summary>
        /// The list of immediate services for this device description.
        /// </summary>
        protected DeviceServicesDescription mdDeviceServices;

        /// <summary>
        /// The list of icons for this device description.
        /// </summary>
        protected IconsDescription midIcons;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        public static bool IsStartNodeFor(XmlTextReader reader)
        {
            return (reader.NodeType == XmlNodeType.Element && reader.Name == msElement);
        }

        /// <summary>
        /// Gets whether the current node for a reader marks the end of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        public static bool IsEndNodeFor(XmlTextReader reader)
        {
            return (reader.NodeType == XmlNodeType.EndElement && reader.Name == msElement);
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new empty device description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        public DeviceDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new device description from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML text reader.</param>
        public DeviceDescription(Description parent, XmlTextReader reader)
            : base(parent)
        {
            base.ProcessReader(reader);
        }

        /// <summary>
        /// Initialises the device description.
        /// </summary>
        protected override void Initialise()
        {
            base.Initialise();

            mdDevices = new DevicesDescription(this);
            midIcons = new IconsDescription(this);
            mdDeviceServices = new DeviceServicesDescription(this, this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Uses an XML node for this description.
        /// </summary>
        /// <param name="reader">The XML reader to use the node from.</param>
        /// <param name="lastNodeName">The last node name.</param>
        /// <returns>True if the node was processed false otherwise.</returns>
        protected override bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            if (DeviceServicesDescription.IsStartNodeFor(reader))
            {
                mdDeviceServices.AddItemsFrom(reader);
                return true;
            }
            else
                if (DevicesDescription.IsStartNodeFor(reader))
                {
                    mdDevices.AddItemsFrom(reader);
                    return true;
                }
                else
                    if (IconsDescription.IsStartNodeFor(reader))
                    {
                        midIcons.AddItemsFrom(reader);
                        return true;
                    }

            return false;
        }

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        protected override bool IsStartNode(XmlTextReader reader)
        {
            return IsStartNodeFor(reader);
        }

        /// <summary>
        /// Gets whether the current node for a reader marks the end of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        protected override bool IsEndNode(XmlTextReader reader)
        {
            return IsEndNodeFor(reader);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds a device description recursively by UDN.
        /// </summary>
        /// <param name="udn">The UDN to search for.</param>
        /// <returns>The device description found or null if not found.</returns>
        public DeviceDescription FindDevice(string udn)
        {
            if (udn == this.UDN)
                return this;
            else
                return Devices.FindDevice(udn);
        }

        /// <summary>
        /// Gets this device description with full service information.
        /// </summary>
        /// <param name="rootDescription">The root description.</param>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation of the device.</returns>
        public string ToStringWithFullServices(RootDescription rootDescription, int indent = 0)
        {
            StringBuilder lsbBuilder = new StringBuilder();

            lsbBuilder.Append(ToString(indent).AsLine());

            foreach (DeviceServiceDescription lsService in DeviceServices.Values)
            {
                ServiceDescription lsdDesc = lsService.GetDescription(rootDescription);
                if (lsdDesc != null) lsbBuilder.Append(lsdDesc.ToString(indent).AsLine());

                foreach (DeviceDescription ldDevice in Devices.Values)
                    lsbBuilder.Append(ldDevice.ToStringWithFullServices(rootDescription, indent + 1).AsLine());
            }

            return lsbBuilder.ToString();
        }

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation for the description.</returns>
        public override string ToString(int indent)
        {
            return
                String.Format(
                    "{0}Device: {1} ({2}) (UDN:{3})" +
                    "{4}",
                    Indent(indent),
                    FriendlyName,
                    DeviceType,
                    UDN, 
                    string.Format(
                        "{0}{1}{2}{3}",
                        base.ToString(indent).AsLine(),
                        mdDeviceServices.ToString(indent).AsLine(),
                        midIcons.ToString(indent).AsLine(),
                        mdDevices.ToString(indent).AsLine()).AsInLine().LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the immediate devices for the description.
        /// </summary>
        public DevicesDescription Devices
        {
            get
            {
                return mdDevices;
            }
        }

        /// <summary>
        /// Gets the immediate services for the device.
        /// </summary>
        public DeviceServicesDescription DeviceServices
        {
            get
            {
                return mdDeviceServices;
            }
        }

        /// <summary>
        /// Gets the icons for the device.
        /// </summary>
        public IconsDescription Icons
        {
            get
            {
                return midIcons;
            }
        }

        /// <summary>
        /// Gets the device type.
        /// </summary>
        [UsesProperty("deviceType")]
        public string DeviceType
        {
            get
            {
                return GetPropertyString("deviceType");
            }
        }

        /// <summary>
        /// Gets the friendly name of the device.
        /// </summary>
        [UsesProperty("friendlyName")]
        public string FriendlyName
        {
            get
            {
                return GetPropertyString("friendlyName");
            }
        }

        /// <summary>
        /// Gets the manufacturer of the device.
        /// </summary>
        [UsesProperty("manufacturer")]
        public string Manufacturer
        {
            get
            {
                return GetPropertyString("manufacturer");
            }
        }

        /// <summary>
        /// Gets the manufacturer URL for the device.
        /// </summary>
        [UsesProperty("manufacturerURL")]
        public string ManufacturerURL
        {
            get
            {
                return GetPropertyString("manufacturerURL");
            }
        }

        /// <summary>
        /// Gets the model description text for the device.
        /// </summary>
        [UsesProperty("modelDescription")]
        public string ModelDescription
        {
            get
            {
                return GetPropertyString("modelDescription");
            }
        }

        /// <summary>
        /// Gets the model number for the device.
        /// </summary>
        [UsesProperty("modelNumber")]
        public string ModelNumber
        {
            get
            {
                return GetPropertyString("modelNumber");
            }
        }

        /// <summary>
        /// Gets the model name for the device.
        /// </summary>
        [UsesProperty("modelName")]
        public string ModelName
        {
            get
            {
                return GetPropertyString("modelName");
            }
        }

        /// <summary>
        /// Gets the URL for the device model.
        /// </summary>
        [UsesProperty("modelURL")]
        public string ModelURL
        {
            get
            {
                return GetPropertyString("modelURL");
            }
        }

        /// <summary>
        /// Gets the serial number for the device.
        /// </summary>
        [UsesProperty("serialNumber")]
        public string SerialNumber
        {
            get
            {
                return GetPropertyString("serialNumber");
            }
        }

        /// <summary>
        /// Gets the Unique Device Number for the device.
        /// </summary>
        [UsesProperty("UDN")]
        public string UDN
        {
            get
            {
                return GetPropertyString("UDN");
            }
        }

        /// <summary>
        /// Gets the Universal Product Code for the device.
        /// </summary>
        [UsesProperty("UPC")]
        public string UniversalProductCode
        {
            get
            {
                return GetPropertyString("UPC");
            }
        }

        /// <summary>
        /// Gets the presentation URL for the device.
        /// </summary>
        [UsesProperty("presentationURL")]
        public string PresentationURL
        {
            get
            {
                return GetPropertyString("presentationURL");
            }
        }

        #endregion
    }
}

#endif