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
    /// Encapsulates the description for a list of allowed values.
    /// </summary>
    public class AllowedValuesDescription : DescriptionList<string>
    {
        #region Protected Constants

        /// <summary>
        /// Gets the element name for the allowed value list description.
        /// </summary>
        protected const string msElement = "allowedValueList";

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
        /// Creates a new allowed values description.
        /// </summary>
        public AllowedValuesDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new allowed values description from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to get the values from.</param>
        public AllowedValuesDescription(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Uses an XML node for this description.
        /// </summary>
        /// <param name="reader">The XML reader to use the node from.</param>
        /// <param name="lastNodeName">The last node name.</param>
        /// <returns>True if the node was processed false otherwise.</returns>
        [UsesProperty("allowedValue")]
        protected override bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            if (lastNodeName == "allowedValue")
            {
                if (reader.NodeType == XmlNodeType.Text)
                    mlList.Add(reader.Value);

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
            if (this.Count > 0)
            {
                string lsIndent = Indent(indent);
                StringBuilder lsbBuilder = new StringBuilder();

                lsbBuilder.Append(lsIndent);
                lsbBuilder.Append("(Allowed:");

                bool lbFirst = true;
                foreach (String lsItem in this)
                {
                    if (!lbFirst) lsbBuilder.Append(", ");
                    lbFirst = false;

                    lsbBuilder.Append("'");
                    lsbBuilder.Append(lsItem.ToString());
                    lsbBuilder.Append("'");
                }

                lsbBuilder.Append(")");

                lsbBuilder.Append(base.ToString(indent).LineBefore());

                return lsbBuilder.ToString();
            }
            else
                return String.Empty;
        }        

        #endregion
    }
}

#endif