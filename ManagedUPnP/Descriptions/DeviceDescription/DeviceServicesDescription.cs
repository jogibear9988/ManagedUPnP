//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a list of services for a device.
    /// </summary>
    public class DeviceServicesDescription : FormattedDescriptionDictionary<string, DeviceServiceDescription>
    {
        #region Protected Constants

        /// <summary>
        /// The element name for a service.
        /// </summary>
        protected const string msElement = "serviceList";

        #endregion

        #region Protected Locals

        /// <summary>
        /// The device description for this list of services.
        /// </summary>
        protected DeviceDescription mdDevice = null;

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
        /// Creates a new empty device services list.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="device">The device description that owns the services.</param>
        public DeviceServicesDescription(Description parent, DeviceDescription device)
            : base(parent)
        {
            mdDevice = device;
        }

        /// <summary>
        /// Creates a new device services list from an XML reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="device">The device description for the services list.</param>
        /// <param name="reader">The reader to load the service descriptions from.</param>
        public DeviceServicesDescription(Description parent, DeviceDescription device, XmlTextReader reader)
            : base(parent, reader)
        {
            mdDevice = device;
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
            if (DeviceServiceDescription.IsStartNodeFor(reader))
            {
                DeviceServiceDescription lddDesc = new DeviceServiceDescription(this, mdDevice, reader);

                if(lddDesc.ServiceId.Length > 0)
                    mdDictionary[lddDesc.ServiceId] = lddDesc;
                else
                    mdDictionary[Guid.NewGuid().ToString()] = lddDesc;

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

        #region Public Properties

        /// <summary>
        /// Gets the device description for this list of services.
        /// </summary>
        public DeviceDescription Device
        {
            get
            {
                return mdDevice;
            }
        }

        #endregion
    }
}

#endif