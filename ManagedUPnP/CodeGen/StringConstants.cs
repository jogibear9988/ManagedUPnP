//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_CodeGen

using System.Collections.Generic;
using System.Text;

namespace ManagedUPnP.CodeGen
{
    /// <summary>
    /// Encapsulates a class to create and manage a set of string constants
    /// where each constant can have a base group and name as part of its 
    /// code identifier.
    /// </summary>
    internal class StringConstants
    {
        #region Protected Locals

        /// <summary>
        /// The code generation constant provider to use.
        /// </summary>
        protected ICodeGenProvider mcgCodeGenProvider;

        #endregion

        #region Protected Structures

        /// <summary>
        /// Encapsulates the information for a constant.
        /// </summary>
        protected struct ConstantData
        {
            #region Public Fields

            /// <summary>
            /// The identifier for the constant.
            /// </summary>
            public string Identifier;

            /// <summary>
            /// The comment text for the constant.
            /// </summary>
            public string CommentText;

            #endregion

            #region Public Initialisation

            /// <summary>
            /// Creates a new constant data structure.
            /// </summary>
            /// <param name="identifier">The identifier for the constant.</param>
            /// <param name="commentText">The comment text for the constant.</param>
            public ConstantData(string identifier, string commentText)
            {
                Identifier = identifier;
                CommentText = commentText;
            }

            #endregion
        }

        #endregion

        #region Protected Locals

        /// <summary>The constants currently added.</summary>
        /// <remarks>Keys - Group, [Value, ConstantData].</remarks>
        protected Dictionary<string, Dictionary<string, ConstantData>> mdConstants = new Dictionary<string, Dictionary<string, ConstantData>>();

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new device class code generator.
        /// </summary>
        /// <param name="codeGenProvider">The provider to use when generating the code.</param>
        public StringConstants(ICodeGenProvider codeGenProvider)
        {
            mcgCodeGenProvider = codeGenProvider;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Makes a constant name based on group and value.
        /// </summary>
        /// <param name="group">The group for the constant.</param>
        /// <param name="name">The value for the constant.</param>
        /// <returns>The name of the constant.</returns>
        protected string MakeConstantName(string group, string name)
        {
            return
                string.Format(
                    CodeGenProvider.ConstantIdentifyer,
                    CodeGenProvider.CodeFriendlyIdentifier(group, false, true),
                    CodeGenProvider.CodeFriendlyIdentifier(name, false));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the definitions for all constants.
        /// </summary>
        /// <returns>The definitions for all the constants.</returns>
        public StringBuilder Definitions()
        {
            StringBuilder lsbRet = new StringBuilder();

            // For each group
            foreach(KeyValuePair<string, Dictionary<string, ConstantData>> lkvGroups in mdConstants)
            {
                StringBuilder lsbGroup = new StringBuilder(); 

                // For each constant
                foreach (KeyValuePair<string, ConstantData> lkvConsts in lkvGroups.Value)
                    lsbGroup.Append(
                        string.Format(
                            CodeGenProvider.ConstantDefinition,
                            lkvConsts.Value.Identifier,
                            lkvConsts.Key,
                            (
                                string.IsNullOrEmpty(lkvConsts.Value.CommentText) ?
                                string.Empty :
                                string.Format(CodeGenProvider.ConstantComment, lkvConsts.Value.CommentText)
                            )
                        )
                    );

                // Append the group
                lsbRet.Append(string.Format(CodeGenProvider.ConstantGroupDefinition, lkvGroups.Key, lsbGroup));
            }

            // Return the definitions
            return lsbRet;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets and adds if necessary, the constant name for a group, name and value.
        /// </summary>
        /// <param name="group">The group for the constant.</param>
        /// <param name="name">The name for the constant.</param>
        /// <param name="value">The value for the constant.</param>
        /// <param name="commentText">The comment for the constant.</param>
        /// <returns>The name of the constant.</returns>
        public string this[string group, string name, string value, string commentText]
        {
            get
            {
                // Check to see if group is created
                Dictionary<string, ConstantData> ldConsts;
                if (!mdConstants.TryGetValue(group, out ldConsts))
                {
                    // If not create and add it
                    ldConsts = new Dictionary<string, ConstantData>();
                    mdConstants[group] = ldConsts;
                }

                // Check to see if constant is created
                ConstantData lcdConstant;
                if (!ldConsts.TryGetValue(value, out lcdConstant))
                {
                    // If not create and add it
                    lcdConstant = new ConstantData(MakeConstantName(group, name), commentText);
                    ldConsts[value] = lcdConstant;
                }

                // Return constant name
                return lcdConstant.Identifier;
            }
        }

        /// <summary>
        /// Gets and adds if necessary, the constant name for a group and value.
        /// </summary>
        /// <param name="group">The group for the constant.</param>
        /// <param name="value">The value for the constant.</param>
        /// <param name="commentText">The comment for the constant.</param>
        /// <returns>The name of the constant.</returns>
        public string this[string group, string value, string commentText]
        {
            get
            {
                return this[group, value, value, commentText];
            }
        }

        /// <summary>
        /// Gets and adds if necessary, the constant name for a group and value.
        /// </summary>
        /// <param name="group">The group for the constant.</param>
        /// <param name="value">The value for the constant.</param>
        /// <returns>The name of the constant.</returns>
        public string this[string group, string value]
        {
            get
            {
                return this[group, value, value, string.Empty];
            }
        }

        /// <summary>
        /// Gets the code generation provider.
        /// </summary>
        public ICodeGenProvider CodeGenProvider
        {
            get
            {
                return mcgCodeGenProvider;
            }
        }

        #endregion
    }
}

#endif