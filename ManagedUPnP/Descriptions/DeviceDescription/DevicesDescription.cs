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
    /// Encapsulates a description for a list of devices.
    /// </summary>
    public class DevicesDescription : FormattedDescriptionDictionary<string, DeviceDescription>
    {
        #region Protected Constants

        /// <summary>
        /// The element name for a device.
        /// </summary>
        protected const string msElement = "deviceList";

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new empty devices description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        public DevicesDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new devices description from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML text reader.</param>
        public DevicesDescription(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Uses an XML node for this description.
        /// </summary>
        /// <param name="reader">The XML reader to use the node from.</param>
        /// <param name="lastNodeName">The last node name.</param>
        /// <returns>True if the node was processed false otherwise.</returns>
        protected override bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            if (DeviceDescription.IsStartNodeFor(reader))
            {
                DeviceDescription lddDesc = new DeviceDescription(this, reader);

                if (lddDesc.UDN.Length > 0)
                    mdDictionary[lddDesc.UDN] = lddDesc;
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

        #region Public Methods

        /// <summary>
        /// Finds a device description recursively by UDN.
        /// </summary>
        /// <param name="udn">The UDN to search for.</param>
        /// <returns>The device description found or null if not found.</returns>
        public DeviceDescription FindDevice(string udn)
        {
            DeviceDescription lddDesc;

            if (TryGetValue(udn, out lddDesc))
                return lddDesc;
            else
            {
                foreach (DeviceDescription lddDevice in this.Values)
                {
                    DeviceDescription lddReturn = lddDevice.FindDevice(udn);
                    if (lddReturn != null)
                        return lddReturn;
                }
            }

            return null;
        }

        #endregion
    }
}

#endif