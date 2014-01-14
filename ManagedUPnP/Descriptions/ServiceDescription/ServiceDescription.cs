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
    /// Encapsulates the description for a service.
    /// </summary>
    public class ServiceDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for the service description.
        /// </summary>
        protected const string msElement = "scpd";

        #endregion

        #region Protected Locals

        /// <summary>
        /// The schema url description.
        /// </summary>
        protected string msSchema;

        /// <summary>
        /// The service ID.
        /// </summary>
        protected string msServiceId;

        /// <summary>
        /// The device unique device name.
        /// </summary>
        protected string msDeviceUDN;

        /// <summary>
        /// The document version number description.
        /// </summary>
        protected SpecVersionDescription msvVersion;

        /// <summary>
        /// The description for the list of actions.
        /// </summary>
        protected ActionsDescription maActions;

        /// <summary>
        /// The description for the list of state variables.
        /// </summary>
        protected StateVariablesDescription msvStateVariables;

        /// <summary>
        /// The document URL for the description.
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
        /// Creates a new service description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="documentURL">The document URL for the service.</param>
        /// <param name="deviceUDN">The unique device number for the device.</param>
        /// <param name="serviceId">The service ID for the service.</param>
        /// <param name="reader">The reader to load the service description from.</param>
        public ServiceDescription(Description parent, string documentURL, string deviceUDN, string serviceId, XmlTextReader reader)
            : base(parent)
        {
            msDocumentURL = documentURL;

            if (IsStartNodeFor(reader))
            {
                msDeviceUDN = deviceUDN;
                msServiceId = serviceId;
                msSchema = reader.GetAttribute("xmlns") ?? String.Empty;
                ProcessReader(reader);
            }
            else
                throw new InvalidOperationException(
                    string.Format(
                        "The node {0} is not a valid node for {1}",
                        reader.Name, this.GetType().Name));
        }

        /// <summary>
        /// Initialises the service description.
        /// </summary>
        protected override void Initialise()
        {
            base.Initialise();

            msvVersion = new SpecVersionDescription(this);
            maActions = new ActionsDescription(this);
            msvStateVariables = new StateVariablesDescription(this);
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
            if (SpecVersionDescription.IsStartNodeFor(reader))
            {
                msvVersion = new SpecVersionDescription(this, reader);
                return true;
            }
            else
                if (ActionsDescription.IsStartNodeFor(reader))
                {
                    maActions.AddItemsFrom(reader);
                    return true;
                }
                else
                    if (StateVariablesDescription.IsStartNodeFor(reader))
                    {
                        msvStateVariables.AddItemsFrom(reader);
                        return true;
                    }

            return false;
        }

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        protected override bool IsEndNode(XmlTextReader reader)
        {
            return IsEndNodeFor(reader);
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation for the description.</returns>
        public override string ToString(int indent)
        {
            return
                String.Format(
                    "{0}Service: {1} on {2}" +
                    "{3}",
                    Indent(indent),
                    ServiceId,
                    DeviceUDN,
                    string.Format(
                        "{0}{1}{2}",
                        base.ToString(indent).AsLine(),
                        maActions.ToString(indent).AsLine(),
                        msvStateVariables.ToString(indent).AsLine()).AsInLine().LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the version specification description.
        /// </summary>
        public SpecVersionDescription SpecVersion
        {
            get
            {
                return msvVersion;
            }
        }

        /// <summary>
        /// Gets the document URL for the description.
        /// </summary>
        public string DocumentURL
        {
            get
            {
                return msDocumentURL;
            }
        }

        /// <summary>
        /// Gets the actions list description.
        /// </summary>
        public ActionsDescription Actions
        {
            get
            {
                return maActions;
            }
        }

        /// <summary>
        /// Gets the state variables list description.
        /// </summary>
        public StateVariablesDescription StateVariables
        {
            get
            {
                return msvStateVariables;
            }
        }

        /// <summary>
        /// The schema url description.
        /// </summary>
        public string Schema
        {
            get
            {
                return msSchema;
            }
        }

        /// <summary>
        /// The device unique device name.
        /// </summary>
        public string DeviceUDN
        {
            get
            {
                return msDeviceUDN;
            }
        }

        /// <summary>
        /// Gets the ID for the service.
        /// </summary>
        public string ServiceId
        {
            get
            {
                return msServiceId;
            }
        }

        #endregion
    }
}

#endif