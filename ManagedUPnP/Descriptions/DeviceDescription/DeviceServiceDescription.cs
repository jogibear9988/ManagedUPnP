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
    /// Encapsulates a description for a device service.
    /// </summary>
    public class DeviceServiceDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for a service.
        /// </summary>
        protected const string msElement = "service";

        #endregion

        #region Protected Locals

        /// <summary>
        /// The device description for the service.
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
        /// Creates a new service description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="device">The device description which owns the service.</param>
        /// <param name="reader">The XML reader to get the data for the service from.</param>
        public DeviceServiceDescription(Description parent, DeviceDescription device, XmlTextReader reader)
            : base(parent, reader)
        {
            mdDevice = device;
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the base URL for all URLs.
        /// </summary>
        /// <param name="root">The root description for the service description.</param>
        /// <returns>The URL base for all URLs for this service.</returns>
        public string URLBase(RootDescription root)
        {
            if (Logging.Enabled)
                Logging.Log(this, string.Format("Getting Base URL for RootDescription: '{0}'", root.Device.FriendlyName), 1);

            try
            {
                // Try using the root descriptions URL base
                string lsURLBase = root.URLBase;

                // If thats not there
                if (lsURLBase.Length == 0)
                {
                    // Use the base of the document URL
                    lsURLBase = root.DocumentURL;

                    if (Logging.Enabled)
                        Logging.Log(this, string.Format("URLBase element is empty, using relative path from: '{0}'", lsURLBase));

                    try
                    {
                        // Create a URI
                        if (Logging.Enabled)
                            Logging.Log(this, "Building URI");
                        Uri luURI = new Uri(lsURLBase);

                        // Get its parts
                        if (Logging.Enabled)
                            Logging.Log(this, "Getting segments");
                        string[] lsParts = luURI.Segments;

                        // Exclude the last part of the URI and use that as the base
                        if (lsParts.Length > 1)
                        {
                            lsURLBase = lsURLBase.Substring(0, lsURLBase.Length - lsParts[lsParts.Length - 1].Length);
                            if (Logging.Enabled)
                                Logging.Log(this, String.Format("Resulting Base URL: '{0}'", lsURLBase));
                        }
                        else
                            if (Logging.Enabled)
                                Logging.Log(this, String.Format("Not enoughs parts in base, using: '{0}'", lsURLBase));
                    }
                    catch (Exception loE)
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, String.Format("Getting URLBase failed with exception: '{0}'", loE.ToString()));

                        // No base available (oops)
                        lsURLBase = string.Empty;
                    }
                }
                else
                    if (Logging.Enabled)
                        Logging.Log(this, string.Format("URLBase element found, using: '{0}'", lsURLBase));

                // Return the base URL
                return lsURLBase;
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Finished getting Base URL for RootDescription", -1);
            }
        }

        /// <summary>
        /// Gets the service description for this service from the root description.
        /// </summary>
        /// <param name="root">The root description to get the service description from.</param>
        /// <returns>The service description or null if it cannot be located.</returns>
        public ServiceDescription GetDescription(RootDescription root)
        {
            if (Logging.Enabled)
                Logging.Log(this, string.Format("Getting ServiceDescription for DeviceServiceDescription: '{0}'", this.ServiceId), 1);

            try
            {
                string lsURLBase = URLBase(root);

                if (lsURLBase.Length > 0 && SCPDURL.Length > 0)
                {
                    string lsURL = Utils.CombineURL(lsURLBase, SCPDURL);
                    if (Logging.Enabled)
                        Logging.Log(this, string.Format("Combined URL: {0}", lsURL));

                    ServiceDescription lsdDesc = ServiceDescriptionCache.Cache.ByURL(lsURL);
                    if (lsdDesc != null)
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, string.Format("Description Document found in cache by URL"));
                        return lsdDesc;
                    }

                    if (Logging.Enabled)
                        Logging.Log(this, String.Format("Getting Document URL: '{0}'", lsURL));

                    try
                    {
                        using (XmlTextReader lrReader = Utils.GetXMLTextReader(lsURL))
                        {
                            if (Logging.Enabled)
                                Logging.Log(this, "Finding start node");

                            while (lrReader.Read())
                                if (ServiceDescription.IsStartNodeFor(lrReader)) break;

                            if (ServiceDescription.IsStartNodeFor(lrReader))
                            {
                                if (Logging.Enabled)
                                    Logging.Log(this, "Start node found, processing description");
                                lsdDesc = new ServiceDescription(this, lsURL, Device.UDN, ServiceId, lrReader);
                            }
                        }
                    }
                    catch (Exception loE)
                    {
                        if (Logging.Enabled)
                            Logging.Log(this, String.Format("Downloading and processing of URL failed with error: {0}", loE.ToString()));
                        throw;
                    }

                    if (Logging.Enabled)
                        Logging.Log(this, "Adding to URL description document cache");
                    ServiceDescriptionCache.Cache.AddURL(lsURL, lsdDesc);

                    return lsdDesc;
                }
                else
                    return null;
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(this, "Finished getting ServiceDescription", -1);
            }
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
                    "{0}Service: {1} ({2}){3}",
                    Indent(indent),
                    ServiceId,
                    ServiceType,
                    base.ToString(indent).LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the device description for this server.
        /// </summary>
        public DeviceDescription Device
        {
            get
            {
                return mdDevice;
            }
        }

        /// <summary>
        /// Gets the type for this service.
        /// </summary>
        [UsesProperty("serviceType")]
        public string ServiceType
        {
            get
            {
                return GetPropertyString("serviceType");
            }
        }

        /// <summary>
        /// Gets the ID for this service.
        /// </summary>
        [UsesProperty("serviceId")]
        public string ServiceId
        {
            get
            {
                return GetPropertyString("serviceId");
            }
        }

        /// <summary>
        /// Gets the URL for the service description.
        /// </summary>
        [UsesProperty("SCPDURL")]
        public string SCPDURL
        {
            get
            {
                return GetPropertyString("SCPDURL");
            }
        }

        /// <summary>
        /// Gets the URL for controlling the service.
        /// </summary>
        [UsesProperty("controlURL")]
        public string ControlURL
        {
            get
            {
                return GetPropertyString("controlURL");
            }
        }

        /// <summary>
        /// Gets the event subroutine URL.
        /// </summary>
        [UsesProperty("eventSubURL")]
        public string EventSubURL
        {
            get
            {
                return GetPropertyString("eventSubURL");
            }
        }

        #endregion
    }
}

#endif