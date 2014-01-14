//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a list of icon descriptions.
    /// </summary>
    public class IconsDescription : FormattedDescriptionList<IconDescription>
    {
        #region Protected Constants

        /// <summary>
        /// The element name for an icon.
        /// </summary>
        protected const string msElement = "iconList";

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
        /// Creates a new empty icons list description.
        /// </summary>
        public IconsDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new icons list description from an XML reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader object.</param>
        public IconsDescription(Description parent, XmlTextReader reader)
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
            if (IconDescription.IsStartNodeFor(reader))
            {
                mlList.Add(new IconDescription(this, reader));
                return true;
            }

            return false;
        }

        #endregion
    }
}

#endif