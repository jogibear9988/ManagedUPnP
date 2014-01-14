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
    /// Encapsulates an action for a service.
    /// </summary>
    public class ActionDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// The element name for an action.
        /// </summary>
        protected const string msElement = "action";

        #endregion

        #region Protected Locals

        /// <summary>
        /// Contains the arguments for the action.
        /// </summary>
        protected ArgumentsDescription maArguments;

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
        /// Creates a new action description object.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The reader to get the action information from.</param>
        public ActionDescription(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        /// <summary>
        /// Initialises the action description.
        /// </summary>
        protected override void Initialise()
        {
            base.Initialise();

            maArguments = new ArgumentsDescription(this);
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
            if (ArgumentsDescription.IsStartNodeFor(reader))
            {
                maArguments.AddItemsFrom(reader);
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
                    "{0}Action: {1}" +
                    "{2}",
                    Indent(indent),
                    Name,
                    maArguments.ToString(indent).AsInLine().LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description for the arguments.
        /// </summary>
        public ArgumentsDescription Arguments
        {
            get
            {
                return maArguments;
            }
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        [UsesProperty("name")]
        public string Name
        {
            get
            {
                return GetPropertyString("name");
            }
        }

        #endregion
    }
}

#endif