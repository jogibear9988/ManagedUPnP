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
    /// Encapsulates a description for a list of actions.
    /// </summary>
    public class AllowedValueRangeDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// Gets the element name for the allowed value range description.
        /// </summary>
        protected const string msElement = "allowedValueRange";

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
        /// Creates a new allowed value range description.
        /// </summary>
        public AllowedValueRangeDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new allowed value range description from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to get the information from.</param>
        public AllowedValueRangeDescription(Description parent, XmlTextReader reader)
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation for the description.</returns>
        public override string ToString(int indent)
        {
            if (!Empty)
                return
                    String.Format(
                        "{0}(Range: '{1}' to '{2}' with step '{3}'){4}",
                        Indent(indent),
                        Minimum, Maximum, Step,
                        base.ToString(indent).LineBefore());
            else
                return String.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the minimum allowed value for this value range.
        /// </summary>
        [UsesProperty("minimum")]
        public string Minimum
        {
            get
            {
                return GetPropertyString("minimum");
            }
        }

        /// <summary>
        /// Gets the maximum allowed value for this value range.
        /// </summary>
        [UsesProperty("maximum")]
        public string Maximum
        {
            get
            {
                return GetPropertyString("maximum");
            }
        }

        /// <summary>
        /// Gets the allowed stepping value for this value range.
        /// </summary>
        [UsesProperty("step")]
        public string Step
        {
            get
            {
                return GetPropertyString("step");
            }
        }

        /// <summary>
        /// Returns true if this allowed stepping range is empty.
        /// </summary>
        public bool Empty
        {
            get
            {
                return Minimum.Length == 0 && Maximum.Length == 0 && Step.Length == 0;
            }
        }

        #endregion
    }
}

#endif