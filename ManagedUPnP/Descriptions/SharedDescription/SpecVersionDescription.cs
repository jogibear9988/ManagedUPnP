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
    /// Encapsulates the spec version.
    /// </summary>
    public class SpecVersionDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for the spec version.
        /// </summary>
        protected const string msSpecVersionElement = "specVersion";

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        public static bool IsStartNodeFor(XmlTextReader reader)
        {
            return (reader.NodeType == XmlNodeType.Element && reader.Name == msSpecVersionElement);
        }

        /// <summary>
        /// Gets whether the current node for a reader marks the end of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        public static bool IsEndNodeFor(XmlTextReader reader)
        {
            return (reader.NodeType == XmlNodeType.EndElement && reader.Name == msSpecVersionElement);
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new spec version description.
        /// </summary>
        public SpecVersionDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new spec version description from an XML reason.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to get the spec version from.</param>
        public SpecVersionDescription(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        #endregion

        #region Protected Methods

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

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation for the description.</returns>
        public override string ToString(int indent)
        {
            return
                String.Format(
                    "{0}Spec Version: {0}.{1}",
                    Indent(indent), Major, Minor);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the minor version number.
        /// </summary>
        public int Minor
        {
            get
            {
                return GetPropertyInt("minor");
            }
        }

        /// <summary>
        /// Gets the major version number.
        /// </summary>
        public int Major
        {
            get
            {
                return GetPropertyInt("major");
            }
        }

        #endregion
    }
}

#endif