//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_CodeGen

using System.Collections.Generic;

namespace ManagedUPnP.CodeGen
{
    /// <summary>
    /// Provides extensions methods for the Device
    /// class pertaining to code generation.
    /// </summary>
    public static class DeviceExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the default code generation class name for a device.
        /// </summary>
        /// <param name="device">The device to get the class name for.</param>
        /// <param name="codeGenProvider">The code generation provider to use.</param>
        /// <returns>A string.</returns>
        public static string DefaultCodeGenClassName(this Device device, ICodeGenProvider codeGenProvider)
        {
            return DeviceGen.DefaultCodeGenClassName(device, codeGenProvider);
        }

        /// <summary>
        /// Generates the class code for a device.
        /// </summary>
        /// <param name="device">The device to generate code for.</param>
        /// <param name="codeGenProvider">The code generator provider to use.</param>
        /// <param name="className">The class name of the device class or null to use friendly name.</param>
        /// <param name="namespaceName">The namespace for the class.</param>
        /// <param name="classScope">The scope of the class.</param>
        /// <param name="partial">True to make the class partial, false otherwise.</param>
        /// <param name="specificDevices">True if generating properties for device specific class types, false to use device non-specific class types.</param>
        /// <param name="specificServiceNamespace">The name of the service namespace if using specific service class types, null to use service non-specific class types.</param>
        /// <param name="specificDeviceClasses">The dictionary of UDNs, ClassNames for the devices or null for default / none.</param>
        /// <param name="specificServiceClasses">The dictionary of IDs, ClassNames for the services or null for default / none.</param>
        /// <returns>The string representing the code for the class.</returns>
        public static string GenerateClassFor(
            this Device device, ICodeGenProvider codeGenProvider,
            string className, string namespaceName, ClassScope classScope, bool partial,
            bool specificDevices, string specificServiceNamespace,
            Dictionary<string, string> specificDeviceClasses = null, Dictionary<string, string> specificServiceClasses = null)
        {
            DeviceGen ldgGenerator = new DeviceGen(codeGenProvider);

            return ldgGenerator.GenerateClassFor(
                device, className, namespaceName, classScope, partial, specificDevices,
                specificServiceNamespace, specificDeviceClasses, specificServiceClasses);
        }

        #endregion
    }
}

#endif