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
    public class ActionsDescription : FormattedDescriptionDictionary<string, ActionDescription>
    {
        #region Protected Constants

        /// <summary>
        /// Gets the element name for the action list.
        /// </summary>
        protected const string msElement = "actionList";

        #endregion

        #region Public Static

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
        /// Creates the actions list description.
        /// </summary>
        public ActionsDescription(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new actions list description from the reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to get the actions descripton from</param>
        public ActionsDescription(Description parent, XmlTextReader reader)
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
        protected override bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            if (ActionDescription.IsStartNodeFor(reader))
            {
                ActionDescription ladDesc = new ActionDescription(this, reader);

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
    }
}

#endif