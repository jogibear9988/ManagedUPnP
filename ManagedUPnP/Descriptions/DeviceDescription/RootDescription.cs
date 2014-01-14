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
    /// Encapsulates a root description.
    /// </summary>
    public class RootDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for an icon.
        /// </summary>
        protected const string msElement = "root";

        #endregion

        #region Protected Locals

        /// <summary>
        /// The XML schema document code. 
        /// </summary>
        protected string msSchema;

        /// <summary>
        /// The document version of the specification.
        /// </summary>
        protected SpecVersionDescription msvSpecVersion;

        /// <summary>
        /// The root device for the description.
        /// </summary>
        protected DeviceDescription mdDevice;

        /// <summary>
        /// The document URL for this root description.
        /// </summary>
        protected string msDocumentURL;

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
        /// Gets the root description for a reader.
        /// </summary>
        /// <param name="documentURL">The URL for this root description.</param>
        /// <param name="reader">The XML reader to get the root description from.</param>
        public RootDescription(string documentURL, XmlTextReader reader)
            : base(null)
        {
            msvSpecVersion = new SpecVersionDescription(this);
            mdDevice = new DeviceDescription(this);

            msDocumentURL = documentURL;

            if (IsStartNodeFor(reader))
            {
                msSchema = reader.GetAttribute("xmlns") ?? String.Empty;
                ProcessReader(reader);
            }
            else
                throw new InvalidOperationException(
                    string.Format(
                        "The node {0} is not a valid node for {1}",
                        reader.Name, this.GetType().Name));
        }

        #endregion

        #region Protected Methods

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

        /// <summary>
        /// Uses an XML node for this description.
        /// </summary>
        /// <param name="reader">The XML reader to use the node from.</param>
        /// <param name="lastNodeName">The last node name.</param>
        /// <returns>True if the node was processed false otherwise.</returns>
        protected override bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            if (SpecVersionDescription.IsStartNodeFor(reader))
            {
                msvSpecVersion = new SpecVersionDescription(this, reader);
                return true;
            }
            else
                if (DeviceDescription.IsStartNodeFor(reader))
                {
                    mdDevice = new DeviceDescription(this, reader);
                    return true;
                }

            return false;
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
            return Device.FindDevice(udn);
        }

        /// <summary>
        /// Gets this device description with full service information.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation of the device.</returns>
        public string ToStringWithFullServices(int indent = 0)
        {
            return
                String.Format(
                    "{0}{1}",
                    mdDevice.ToStringWithFullServices(this, indent),
                    base.ToString(indent).LineBefore());
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
                    "{0}{1}",
                    mdDevice.ToString(indent),
                    base.ToString(indent).LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the spec version of the XML document from which this description came.
        /// </summary>
        public SpecVersionDescription SpecVersion
        {
            get
            {
                return msvSpecVersion;
            }
        }

        /// <summary>
        /// Gets the URL for this root device document description.
        /// </summary>
        public String DocumentURL
        {
            get
            {
                return msDocumentURL;
            }
        }

        /// <summary>
        /// Gets the root device description for this device.
        /// </summary>
        public DeviceDescription Device
        {
            get
            {
                return mdDevice;
            }
        }

        /// <summary>
        /// Gets the document schema link for this root description.
        /// </summary>
        public string Schema
        {
            get
            {
                return msSchema;
            }
        }

        /// <summary>
        /// Gets the base URL for this root device.
        /// </summary>
        [UsesProperty("URLBase")]
        public string URLBase
        {
            get
            {
                return GetPropertyString("URLBase");
            }
        }

        #endregion
    }
}

#endif