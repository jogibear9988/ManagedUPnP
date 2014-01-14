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
    /// Encapsulates the description for the state variable.
    /// </summary>
    public class StateVariableDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for the state variable description.
        /// </summary>
        protected const string msElement = "stateVariable";

        #endregion

        #region Protected Locals

        /// <summary>
        /// True if the state variable is evented.
        /// </summary>
        protected string msSendEvents;

        /// <summary>
        /// The allowed values for the state variable.
        /// </summary>
        protected AllowedValuesDescription maAllowedValues;

        /// <summary>
        /// The allowed range for the state variable.
        /// </summary>
        protected AllowedValueRangeDescription marAllowedRange;

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
        /// Creates a new state variable description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        public StateVariableDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new state variable description from an XML reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML text reader to create the description from.</param>
        public StateVariableDescription(Description parent, XmlTextReader reader)
            : base(parent)
        {
            if (IsStartNodeFor(reader))
            {
                msSendEvents = reader.GetAttribute("sendEvents") ?? String.Empty;
                ProcessReader(reader);
            }
            else
                throw new InvalidOperationException(
                    string.Format(
                        "The node {0} is not a valid node for {1}",
                        reader.Name, this.GetType().Name));
        }

        /// <summary>
        /// Initialises the state variable description.
        /// </summary>
        protected override void Initialise()
        {
            base.Initialise();

            maAllowedValues = new AllowedValuesDescription(this);
            marAllowedRange = new AllowedValueRangeDescription(this);
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
            if (AllowedValuesDescription.IsStartNodeFor(reader))
            {
                maAllowedValues.AddItemsFrom(reader);
                return true;
            }
            else
                if (AllowedValueRangeDescription.IsStartNodeFor(reader))
                {
                    marAllowedRange = new AllowedValueRangeDescription(this, reader);
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
                    "{0}State Variable: {1}{2} {3}{4} {5} {6}{7}",
                    Indent(indent),
                    (SendEvents ? "(evnt) " : "(    ) "),
                    DataType,
                    Name,
                    (DefaultValue.Length > 0 ? String.Format(" [default='{0}'] ", DefaultValue) : string.Empty),
                    marAllowedRange.ToString(0),
                    maAllowedValues.ToString(0),
                    base.ToString(indent).LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the default value for the state variable.
        /// </summary>
        [UsesProperty("defaultValue")]
        public string DefaultValue
        {
            get
            {
                return GetPropertyString("defaultValue");
            }
        }

        /// <summary>
        /// Gets the name of the state variable.
        /// </summary>
        [UsesProperty("name")]
        public string Name
        {
            get
            {
                return GetPropertyString("name");
            }
        }

        /// <summary>
        /// Gets the data type for this state variable.
        /// </summary>
        public StateVariableDataType DataTypeValue
        {
            get
            {
                return ManagedUPnP.Descriptions.StateVariableDataType.tunknown.ForTypeName(DataType);
            }
        }

        /// <summary>
        /// Gets the data type of the state variable.
        /// </summary>
        [UsesProperty("dataType")]
        public string DataType
        {
            get
            {
                return GetPropertyString("dataType");
            }
        }

        /// <summary>
        /// Gets whether this state variable is evented.
        /// </summary>
        public bool SendEvents
        {
            get
            {
                return msSendEvents == "yes";
            }
        }

        /// <summary>
        /// Gets the allowed values description for this state variable.
        /// </summary>
        public AllowedValuesDescription AllowedValues
        {
            get
            {
                return maAllowedValues;
            }
        }

        /// <summary>
        /// Gets the allowed range description for this state variable.
        /// </summary>
        public AllowedValueRangeDescription AllowedRange
        {
            get
            {
                return marAllowedRange;
            }
        }

        #endregion
    }
}

#endif