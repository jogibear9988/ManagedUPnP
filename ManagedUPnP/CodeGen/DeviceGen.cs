//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_CodeGen

using System.Collections.Generic;
using System.Text;
using System;

namespace ManagedUPnP.CodeGen
{
    /// <summary>
    /// Encapsulates a static class which can generate class code for a device.
    /// </summary>
    public class DeviceGen
    {
        #region Protected Locals

        /// <summary>
        /// The code generation constant provider to use.
        /// </summary>
        protected ICodeGenProvider mcgCodeGenProvider;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new device class code generator.
        /// </summary>
        /// <param name="codeGenProvider">The provider to use when generating the code.</param>
        public DeviceGen(ICodeGenProvider codeGenProvider)
        {
            mcgCodeGenProvider = codeGenProvider;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates the code for the service properties.
        /// </summary>
        /// <param name="device">The device to generate for.</param>
        /// <param name="specificServices">True if specific service classes are being used, false if using the Service class.</param>
        /// <param name="consts">The string constants list.</param>
        /// <param name="specificServiceClasses">The dictionary of IDs, ClassNames for the services or null for default.</param>
        /// <param name="properties">A StringBuilder to append the properties code to.</param>
        private void GenerateServicePropertyCode(
            Device device, bool specificServices, StringConstants consts, 
            StringBuilder properties, Dictionary<string, string> specificServiceClasses)
        {
            // For each service
            foreach (Service lsService in device.Services)
            {
                string lsServiceName = ServiceGen.DefaultCodeGenClassName(lsService, CodeGenProvider);
                bool lbSpecificService = specificServices;
                string lsSpecificClass = string.Empty;

                // If using specific service names
                if (lbSpecificService)
                {
                    // And the services dictionary is null
                    if (specificServiceClasses == null)
                        // Then use the service name for its name
                        lsSpecificClass = lsServiceName;
                    else
                        // Otherwise reference the specific service classes
                        if (!specificServiceClasses.TryGetValue(lsService.Id, out lsSpecificClass))
                            // If its not found then dont use specific service name for this service
                            lbSpecificService = false;
                }

                // Get the service class if specific, otherwise use generic service class
                string lsServiceClass =
                    (lbSpecificService ? lsSpecificClass : CodeGenProvider.Service);

                // Build the property for the service and append
                properties.Append(
                    string.Format(
                        CodeGenProvider.Property,
                        lsServiceClass,
                        (lbSpecificService ? lsServiceClass : lsServiceName),
                        CodeGenProvider.Service,
                        string.Format(
                            (lbSpecificService ? CodeGenProvider.SpecificServiceRet : CodeGenProvider.GenericServiceRet),
                            lsServiceClass,
                            consts[CodeGenProvider.ServiceGroup, lsServiceName + CodeGenProvider.ServiceID, lsService.Id, 
                                   string.Format(CodeGenProvider.ServiceIdConstComment,lsServiceName)]),
                        lsServiceName,
                        lsService.ServiceTypeIdentifier, 
                        CodeGenProvider.Service
                    )
                );
            }
        }

        /// <summary>
        /// Generates the code for the device properties.
        /// </summary>
        /// <param name="device">The device to generate for.</param>
        /// <param name="specificDevices">True if specific device classes are being used, false if using the Device class.</param>
        /// <param name="consts">The string constants list.</param>
        /// <param name="specificDeviceClasses">The dictionary of UDNs, ClassNames for the devices or null for default.</param>
        /// <param name="properties">A StringBuilder to append the properties code to.</param>
        private void GenerateDevicePropertyCode(
            Device device, bool specificDevices, StringConstants consts,
            StringBuilder properties, Dictionary<string, string> specificDeviceClasses)
        {
            if (device.HasChildren)
                // For each service
                foreach (Device ldDevice in device.Children)
                {
                    string lsDeviceName = DefaultCodeGenClassName(ldDevice, CodeGenProvider);
                    bool lbSpecificDevice = specificDevices;
                    string lsSpecificClass = string.Empty;

                    // If using specific device names
                    if (lbSpecificDevice)
                    {
                        // And the devices dictionary is null
                        if (specificDeviceClasses == null)
                            // Then use the device name for its name
                            lsSpecificClass = lsDeviceName;
                        else
                            // Otherwise reference the specific device classes
                            if (!specificDeviceClasses.TryGetValue(ldDevice.UniqueDeviceName, out lsSpecificClass))
                                // If its not found then dont use specific device name for this device
                                lbSpecificDevice = false;
                    }

                    // Get the device class if specific, otherwise use generic device class
                    string lsDeviceClass =
                        (lbSpecificDevice ? lsSpecificClass : CodeGenProvider.Device);

                    // Build the property for the service and append
                    properties.Append(
                        string.Format(
                            CodeGenProvider.Property,
                            lsDeviceClass,
                            (lbSpecificDevice ? lsDeviceClass : lsDeviceName),
                            CodeGenProvider.Device,
                            string.Format(
                                (lbSpecificDevice ? CodeGenProvider.SpecificDeviceRet : CodeGenProvider.GenericDeviceRet),
                                lsDeviceClass,
                                consts[CodeGenProvider.DeviceGroup, lsDeviceName + CodeGenProvider.DeviceType, ldDevice.Type,
                                       string.Format(CodeGenProvider.DeviceTypeConstComment, lsDeviceName)]
                            ),
                            lsDeviceName,
                            ldDevice.Type,
                            CodeGenProvider.Device
                        )
                    );
                }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the default code generation class name for a device.
        /// </summary>
        /// <param name="device">The device to get the class name for.</param>
        /// <param name="codeGenProvider">The code generation provider to use.</param>
        /// <returns>A string.</returns>
        public static string DefaultCodeGenClassName(Device device, ICodeGenProvider codeGenProvider)
        {
            return codeGenProvider.CodeFriendlyIdentifier(device.FriendlyName, false);
        }

        /// <summary>
        /// Generates the class code for a device.
        /// </summary>
        /// <param name="device">The device to generate code for.</param>
        /// <param name="className">The class name of the device class or null to use friendly name.</param>
        /// <param name="namespaceName">The namespace for the class.</param>
        /// <param name="classScope">The scope of the class.</param>
        /// <param name="partial">True to make the class partial, false otherwise.</param>
        /// <param name="specificDevices">True if generating properties for device specific class types, false to use device non-specific class types.</param>
        /// <param name="specificServiceNamespace">The name of the service namespace if using specific service class types, null to use service non-specific class types.</param>
        /// <param name="specificDeviceClasses">The dictionary of UDNs, ClassNames for the devices or null for default / none.</param>
        /// <param name="specificServiceClasses">The dictionary of IDs, ClassNames for the services or null for default / none.</param>
        /// <returns>The string representing the code for the class.</returns>
        public string GenerateClassFor(
            Device device, string className, string namespaceName, ClassScope classScope, bool partial, 
            bool specificDevices, string specificServiceNamespace, 
            Dictionary<string, string> specificDeviceClasses = null, Dictionary<string, string> specificServiceClasses = null) 
        {
            if (className == null)
                className = DefaultCodeGenClassName(device, CodeGenProvider); 
            else 
                className = CodeGenProvider.CodeFriendlyIdentifier(className, false);

            // No namespace specified then set it not use one
            if (string.IsNullOrEmpty(specificServiceNamespace)) specificServiceNamespace = string.Empty;
            
            // Set the specific services
            bool mbSpecificServices = specificServiceNamespace.Length > 0;

            string lsUsing;
            
            // If service namespace is different to device namespace
            if (specificServiceNamespace != namespaceName)
                // Add the services namespace to the using clause
                lsUsing = (mbSpecificServices ? string.Format(CodeGenProvider.UsingClause, specificServiceNamespace) : string.Empty);
            else
                // Otherwise dont add anything to the uses clause
                lsUsing = string.Empty;

            StringConstants lscConstants = new StringConstants(CodeGenProvider);
            StringBuilder lsbProperties = new StringBuilder();

            // Build the child devices properties
            GenerateDevicePropertyCode(device, specificDevices, lscConstants, lsbProperties, specificDeviceClasses);

            // Build the child services properties
            GenerateServicePropertyCode(device, mbSpecificServices, lscConstants, lsbProperties, specificServiceClasses);

            // Build the code for the device class
            return
                string.Format(
                    CodeGenProvider.DeviceBase,
                    lsUsing,
                    namespaceName,
                    CodeGenProvider.GetClassScope(classScope, CodeGenProvider.Space.ToString()),
                    (partial ? CodeGenProvider.PartialClass : string.Empty),
                    className,
                    CodeGenProvider.GenerateRegion(CodeGenProvider.ProtectedConstants, lscConstants.Definitions().ToString(), false, false),
                    device.Type,
                    CodeGenProvider.GenerateRegion(CodeGenProvider.PublicProperties, lsbProperties.ToString()),
                    device.ModelName,
                    string.Format(
                        CodeGenProvider.DeviceClassHeaderComment,
                        (device.RootDevice != null ? device.RootDevice.FriendlyName : CodeGenProvider.Null),
                        (device.RootDevice != null ? device.RootDevice.Type : CodeGenProvider.Null),
                        (device.RootDevice != null ? device.RootDevice.SerialNumber : CodeGenProvider.Null),
                        device.FriendlyName, device.Type,
                        DateTime.Now.ToString(),
                        className,
                        namespaceName,
                        classScope.ToString(),
                        (partial ? CodeGenProvider.PartialClass : string.Empty),
                        CodeGenProvider.ToString()
                    )
                );
        }

        #endregion

        #region Public Properties

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