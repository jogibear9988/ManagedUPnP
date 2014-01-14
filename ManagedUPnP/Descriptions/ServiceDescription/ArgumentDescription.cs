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
    /// Encapsulates the description for an action argument.
    /// </summary>
    public class ArgumentDescription : Description
    {
        #region Protected Constants

        /// <summary>
        /// Gets the element name for the argument description.
        /// </summary>
        protected const string msElement = "argument";

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
        /// Creates the description for an argument description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to load the argument description.</param>
        public ArgumentDescription(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        #endregion

        #region Protected Overrides

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
                    "{0}Argument: [{1}] {2} => {3}{4}",
                    Indent(indent),
                    Direction, Name, RelatedStateVariable,
                    base.ToString(indent).LineBefore());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name for the argument.
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
        /// Gets the communication direction for the argument.
        /// </summary>
        [UsesProperty("direction")]
        public string Direction
        {
            get
            {
                return GetPropertyString("direction");
            }
        }

        /// <summary>
        /// Gets the direction of the argument as an enumeration.
        /// </summary>
        public ArgumentDirection DirectionValue
        {
            get
            {
                ArgumentDirection ladDir = ArgumentDirection.Unknown;
                Enum.TryParse<ArgumentDirection>(Direction, true, out ladDir);
                return ladDir;
            }
        }

        /// <summary>
        /// Gets the related state variable for the argument.
        /// </summary>
        [UsesProperty("relatedStateVariable")]
        public string RelatedStateVariable
        {
            get
            {
                return GetPropertyString("relatedStateVariable");
            }
        }

        /// <summary>
        /// Gets the state variable description which relates to this argument.
        /// </summary>
        public StateVariableDescription RelatedStateVariableDescription
        {
            get
            {
                // Get the third generation to get the service description: 
                //  ArgumentsDescription - ActionDescription - ActionsDescription - ServiceDerscription
                Description ldServiceParent = GetParentFrom(3);

                // If the third parent generation is valid and is a service decsription
                if (ldServiceParent != null && ldServiceParent is ServiceDescription)
                {
                    // Get the state variables description
                    StateVariablesDescription lsvdVarsDesc = ((ServiceDescription)ldServiceParent).StateVariables;

                    // Get the state variable for the related one
                    StateVariableDescription lsvdDesc;
                    if (lsvdVarsDesc.TryGetValue(RelatedStateVariable, out lsvdDesc))
                        return lsvdDesc;
                }

                // Not found or invalid, so return null
                return null;
            }
        }

        #endregion
    }
}

#endif