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
    /// Encapsulates the description for a list of action arguments.
    /// </summary>
    public class ArgumentsDescription : FormattedOrderedDescriptionDictionary<string, ArgumentDescription>
    {
        #region Protected Constants

        /// <summary>
        /// The element name for the arguments list.
        /// </summary>
        protected const string msElement = "argumentList";

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets whether the current node for a reader marks the end of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        public static bool IsEndNodeFor(XmlTextReader reader)
        {
            return (reader.NodeType == XmlNodeType.EndElement && reader.Name == msElement);
        }

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        public static bool IsStartNodeFor(XmlTextReader reader)
        {
            return (reader.NodeType == XmlNodeType.Element && reader.Name == msElement);
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new list of arguments description.
        /// </summary>
        public ArgumentsDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new list of arguments description from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to load the arguments from.</param>
        public ArgumentsDescription(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Counts the number of arguments with a specified direction.
        /// </summary>
        /// <param name="dir">The direction of the arguments.</param>
        /// <returns>The number of arguments matching the direction.</returns>
        protected int ArgDirCount(ArgumentDirection dir)
        {
            int liCount = 0;

            foreach (ArgumentDescription ladDesc in this.Values)
                if (ladDesc.DirectionValue == dir)
                    liCount++;

            return liCount;
        }

        /// <summary>
        /// Uses an XML node for this description.
        /// </summary>
        /// <param name="reader">The XML reader to use the node from.</param>
        /// <param name="lastNodeName">The last node name.</param>
        /// <returns>True if the node was processed false otherwise.</returns>
        protected override bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            if (ArgumentDescription.IsStartNodeFor(reader))
            {
                ArgumentDescription ladDesc = new ArgumentDescription(this, reader);

                if (ladDesc.Name.Length > 0)
                    mdDictionary[ladDesc.Name] = ladDesc;
                else
                    mdDictionary[Guid.NewGuid().ToString()] = ladDesc;

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

        #region Public Properties

        /// <summary>
        /// Gets the number of arguments that have an In direction.
        /// </summary>
        public int InArgCount
        {
            get
            {
                return ArgDirCount(ArgumentDirection.In);
            }
        }

        /// <summary>
        /// Gets the number of arguments that have an Out direction.
        /// </summary>
        public int OutArgCount
        {
            get
            {
                return ArgDirCount(ArgumentDirection.Out);
            }
        }

        #endregion
    }
}

#endif