//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_CodeGen

namespace ManagedUPnP.CodeGen
{
    /// <summary>
    /// Provides extensions methods for the Device
    /// class pertaining to code generation.
    /// </summary>
    public static class ServiceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the default code generation class name for a service.
        /// </summary>
        /// <param name="service">The service to get the class name for.</param>
        /// <param name="codeGenProvider">The code generation provider to use.</param>
        /// <returns>A string.</returns>
        public static string DefaultCodeGenClassName(this Service service, ICodeGenProvider codeGenProvider)
        {
            return ServiceGen.DefaultCodeGenClassName(service, codeGenProvider);
        }

        /// <summary>
        /// Generates the class code for a service.
        /// </summary>
        /// <param name="service">The service to generate for.</param>
        /// <param name="codeGenProvider">The code generator provider to use.</param>
        /// <param name="className">The class name of the service or null to use the service type.</param>
        /// <param name="namespaceName">The namespace for the class.</param>
        /// <param name="classScope">The scope for the class.</param>
        /// <param name="partial">True to make the class partial, false otherwise.</param>
        /// <param name="testStateVars">
        /// True to test each state variable to ensure it is 
        /// usuable for accessing as property, false to include
        /// all state variables as properties.</param>
        /// <returns>The string representing the code for the class.</returns>
        public static string GenerateClassFor(
            this Service service, ICodeGenProvider codeGenProvider,
            string className, string namespaceName,
            ClassScope classScope, bool partial, bool testStateVars)
        {
            ServiceGen ldgGenerator = new ServiceGen(codeGenProvider);

            return ldgGenerator.GenerateClassFor(
                service, className, namespaceName, classScope, partial, testStateVars);
        }

        #endregion
    }
}

#endif