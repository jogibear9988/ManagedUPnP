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
    /// Encapsulates an description for an icon.
    /// </summary>
    public class IconDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for an icon.
        /// </summary>
        protected const string msElement = "icon";

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
        /// Creates a new icon description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The reader to load the icon from.</param>
        public IconDescription(Description parent, XmlTextReader reader)
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
        /// Gets the icon description as a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The icon description as a string.</returns>
        public override string ToString(int indent)
        {
            return
                String.Format(
                    "{0}Icon: {1} (W:{2} x H:{3} x Colors:{4} - '{5}'){6}",
                    Indent(indent),
                    URL, Width, Height, ColorDepth, MimeType,
                    base.ToString(indent).LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the mime type for the icon.
        /// </summary>
        [UsesProperty("mimetype")]
        public string MimeType
        {
            get
            {
                return GetPropertyString("mimetype");
            }
        }

        /// <summary>
        /// Gets the width for the icon.
        /// </summary>
        [UsesProperty("width")]
        public int Width
        {
            get
            {
                return GetPropertyInt("width");
            }
        }

        /// <summary>
        /// Gets the height for the icon.
        /// </summary>
        [UsesProperty("height")]
        public int Height
        {
            get
            {
                return GetPropertyInt("height");
            }
        }

        /// <summary>
        /// Gets the color depth of the icon.
        /// </summary>
        [UsesProperty("depth")]
        public string ColorDepth
        {
            get
            {
                return GetPropertyString("depth");
            }
        }

        /// <summary>
        /// Gets the URL for the icon.
        /// </summary>
        [UsesProperty("URL")]
        public string URL
        {
            get
            {
                return GetPropertyString("url");
            }
        }

        #endregion
    }
}

#endif