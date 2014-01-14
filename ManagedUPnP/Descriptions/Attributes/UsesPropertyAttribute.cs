//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Attribute which signifies that a property has been used by the description object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple=true)] 
    public class UsesPropertyAttribute : Attribute
    {
        #region Protected Locals

        /// <summary>
        /// The name of the proeperty that is used.
        /// </summary>
        protected string msName;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new uses property attribute.
        /// </summary>
        /// <param name="name">The name of the property being used.</param>
        public UsesPropertyAttribute(string name)
        {
            msName = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the property being used.
        /// </summary>
        public string Name
        {
            get
            {
                return msName;
            }
        }

        #endregion
    }
}

#endif