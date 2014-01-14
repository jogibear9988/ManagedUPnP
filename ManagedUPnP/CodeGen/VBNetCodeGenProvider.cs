//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_CodeGen

using System;
using System.Text;

namespace ManagedUPnP.CodeGen
{
    /// <summary>
    /// Provides the code generation constants for generating
    /// VB .Net code.
    /// </summary>
    public class VBNetCodeGenProvider : ICodeGenProvider
    {
        #region Protected Locals

        /// <summary>
        /// The default root namespace to use for VB.Net projects or null for none.
        /// </summary>
        protected string msDefaultRootNamespace = null;

        #endregion

        #region Other Constants

        /// <summary>
        /// Gets the full file extension for a partial class.
        /// </summary>
        public string PartialClassFileExtension { get { return ".Partial.vb"; } }

        /// <summary>
        /// Gets the full file extension for class.
        /// </summary>
        public string ClassFileExtension { get { return ".vb"; } }

        #endregion

        #region String Constant Code

        /// <summary>Gets the code for the constant name.</summary>
        /// <remarks>Format Parameters: 0 = typeName, 1 = valueName.</remarks>
        public string ConstantIdentifyer { get { return "cs{0}_{1}"; } }

        /// <summary>Gets the code for the constant group definition.</summary>
        /// <remarks>Format Parameters: 0 = type, 1 = constantDefinitions.</remarks>
        public string ConstantGroupDefinition { get { return "{1}"; } }

        /// <summary>Gets the code for the constant comment.</summary>
        /// <remarks>Format Parameters: 0 = commentText.</remarks>
        public string ConstantComment
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' {0}\r\n" +
                    "\t\t''' </summary>\r\n";
            }
        }

        /// <summary>Gets the code for the constant definition.</summary>
        /// <remarks>Format Parameters: 0 = VarName, 1 = value, 2 = comments</remarks>
        public string ConstantDefinition
        {
            get
            {
                return
                    "{2}" +
                    "\t\tProtected Const {0} As String = \"{1}\"\r\n" +
                    "\r\n";
            }
        }

        #endregion

        #region String Constant Groups

        /// <summary>
        /// Gets the group name for state variable constant identifiers.
        /// </summary>
        public string StateVarNameGroup { get { return "StateVar"; } }

        /// <summary>
        /// Gets the group name for state variable allowed value constant identifiers.
        /// </summary>
        /// <remarks>Format Parameters: 0 = StateVarFriendlyName.</remarks>
        public string AllowedValueGroup { get { return "AllowedVal_{0}"; } }

        /// <summary>
        /// Gets the group name for action constant identifiers.
        /// </summary>
        public string ActionNameGroup { get { return "Action"; } }

        /// <summary>
        /// Gets the group name for service constant identifiers.
        /// </summary>
        public string ServiceGroup { get { return "Service"; } }

        /// <summary>
        /// Gets the group name for device constant identifiers.
        /// </summary>
        /// <remarks>Format Parameters: 0 = StateVarFriendlyName</remarks>
        public string DeviceGroup { get { return "Device"; } }

        #endregion

        #region Region Constants

        /// <summary>
        /// Gets the protected constants region name.
        /// </summary>
        public string ProtectedConstants { get { return "Protected Constants"; } }

        /// <summary>
        /// Gets the public properties region name.
        /// </summary>
        public string PublicProperties { get { return "Public Properties"; } }

        /// <summary>
        /// Gets the protected methods region name.
        /// </summary>
        public string ProtectedMethods { get { return "Protected Methods"; } }

        /// <summary>
        /// Gets the public enumerations region name.
        /// </summary>
        public string PublicEnumerations { get { return "Public Enumerations"; } }

        /// <summary>
        /// Gets the event handlers region name.
        /// </summary>
        public string EventHandlers { get { return "Event Handlers"; } }

        /// <summary>
        /// Gets the event callers region name.
        /// </summary>
        public string EventCallers { get { return "Event Callers"; } }

        /// <summary>
        /// Gets the code for a region.
        /// </summary>
        /// <remarks>Format Parameters: 0 = regionName, 1 = regionCode, 2 = indentation, 3 = beforeEnd, 4 = beforeStart.</remarks>
        public string Region
        {
            get
            {
                return
                    "{4}" +
                    "{2}\t\t#Region \"{0}\"\r\n" +
                    "\r\n" +
                    "{1}" +
                    "{3}" +
                    "{2}\t\t#End Region\r\n";
            }
        }

        #endregion

        #region Misc Constants

        /// <summary>
        /// Gets the Null value for a object.
        /// </summary>
        public string Null { get { return "Nothing"; } }

        /// <summary>
        /// Gets the partial class statement.
        /// </summary>
        public string PartialClass { get { return "Partial "; } }

        /// <summary>
        /// Gets the public modifier statement.
        /// </summary>
        public string Public { get { return "Public"; } }

        /// <summary>
        /// Gets the name of the service class.
        /// </summary>
        public string Service { get { return "Service"; } }

        /// <summary>
        /// Gets the name of the device class.
        /// </summary>
        public string Device { get { return "Device"; } }

        /// <summary>
        /// Gets the string to append to the string constant names for service IDs.
        /// </summary>
        public string ServiceID { get { return "ID"; } }

        /// <summary>
        /// Gets the string to append to the string constant names for device Types.
        /// </summary>
        public string DeviceType { get { return  "Type"; } }

        /// <summary>
        /// Gets the string to append to the string constant names for device model names.
        /// </summary>
        public string DeviceModelName { get { return  "ModelName"; } }

        /// <summary>
        /// Gets the indent char to use for indentation of code.
        /// </summary>
        public char IndentChar { get { return '\t'; } }

        /// <summary>
        /// Gets the string representing and empty line.
        /// </summary>
        public string EmptyLine { get { return "\r\n"; } }

        /// <summary>
        /// Gets a space.
        /// </summary>
        public char Space { get { return  ' '; } }

        /// <summary>
        /// Gets the type for a parameter when it cannot be determined.
        /// </summary>
        /// <remarks>This can occur if a related state variable definition is missing.</remarks>
        public string UnknownType { get { return "Object"; } }

        /// <summary>
        /// Gets a comma for delimiting parameters.
        /// </summary>
        public string ParameterSeperator { get { return ", "; } }

        /// <summary>
        /// Gets a comma with a trailing space.
        /// </summary>
        public string Comma { get { return ", "; } }

        /// <summary>
        /// Gets the definition for a using clause.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = Using namespace.
        /// </remarks>
        public string UsingClause
        {
            get
            {
                if (String.IsNullOrEmpty(msDefaultRootNamespace)) 
                    return "Imports {0}\r\n";
                else
                    return String.Format("Imports {0}.{{0}}\r\n", msDefaultRootNamespace);
            }
        }

        #endregion

        #region Device Code Constants

        /// <summary>
        /// Gets the definition for the service ID constant comment.
        /// </summary>
        /// <remarks>Format Parameters: 0 = service friendly name.</remarks>
        public string ServiceIdConstComment
        { 
            get 
            { 
                return 
                    "Stores the constant ID value for child service {0}."; 
            }
        }

        /// <summary>
        /// Gets the definition for the device type constant comment.
        /// </summary>
        /// <remarks>Format Parameters: 0 = device friendly name.</remarks>
        public string DeviceTypeConstComment  
        { 
            get 
            { 
                return 
                    "Stores the constant type value for child device {0}."; 
            } 
        }

        /// <summary>
        /// Gets the definition for the generic device return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = DeviceClass, 1 = DeviceType.
        /// </remarks>
        public string GenericDeviceRet  
        { 
            get 
            { 
                return 
                    "FirstDeviceByType({1}, False)"; 
            } 
        }

        /// <summary>
        /// Gets the definition for the generic service return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ServiceClass, 1 = ServiceId.
        /// </remarks>
        public string GenericServiceRet
        { 
            get 
            { 
                return 
                    "Services({1})"; 
            } 
        }

        /// <summary>
        /// Gets the definition for the specific device return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = DeviceClass, 1 = DeviceType.
        /// </remarks>
        public string SpecificDeviceRet  
        { 
            get 
            { 
                return 
                    "New {0}(FirstDeviceByType({1}, False), mdcDeviceCheck)"; 
            } 
        }

        /// <summary>
        /// Gets the definition for the specific service return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ServiceClass, 0 = ServiceId.
        /// </remarks>
        public string SpecificServiceRet
        { 
            get 
            { 
                return 
                    "New {0}(Services({1}))"; 
            } 
        }

        /// <summary>
        /// Gets the definition for a public property.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = returnType, 1 = propStartName, 2 = propEndName,
        /// 3 = returnValue, 4 = Device/Service name, 5 = Device/Service type, 6 = "Device" / "Service"
        /// </remarks>
        public string Property
        { 
            get 
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Gets a new {4} ({5}) child {6} for the device.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic ReadOnly Property {1}{2}() As {0}\r\n" +
                    "\t\t\tGet\r\n" +
                    "\t\t\t\tReturn {3}\r\n" +
                    "\t\t\tEnd Get\r\n" +
                    "\t\tEnd Property\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the code for the comments as the class header.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = rootDeviceName, 1 = rootDeviceType, 2 = serialNumber
        /// 3 = deviceName, 4 = deviceType,
        /// 5 = dateTime, 6 = className,
        /// 7 = namespaceName, 8 = classScope,
        /// 9 = partialClass, 10 = codeGenerationProvider
        /// </remarks>
        public string DeviceClassHeaderComment  
        { 
            get 
            { 
                return 
                    "' Generated by the ManagedUPnP Framework\r\n" +
                    "' http://managedupnp.codeplex.com\r\n" +
                    "'\r\n" +
                    "' For Root Device: {0} ({1}) - Serial: {2}\r\n" +
                    "' Using Device: {3} ({4})\r\n" +
                    "'\r\n" +
                    "' On: {5}\r\n" +
                    "'\r\n" +
                    "' Code Generation Provider: {10}\r\n" +
                    "' Class Name: {6}\r\n" +
                    "' Namespace Name: {7}\r\n" +
                    "' Class Scope: {8}\r\n" +
                    "' Partial Class: {9}\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the definition for the entire device class.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = using, 1 = namespace, 2 = classScope, 3 = "Partial " if partial class, 
        /// 4 = className, 5 = protectedConstants, 6 = DeviceType, 7 = proprties,
        /// 8 = DeviceModelName, 9 = classHeaderComment
        /// </remarks>
        public string DeviceBase
        {
            get
            {
                return
                    "{9}" +
                    "Imports System\r\n" +
                    "Imports System.Collections\r\n" +
                    "Imports ManagedUPnP\r\n" +
                    "{0}" +
                    "\r\n" +
                    "Namespace {1}\r\n" +
                    "\r\n" +
                    "\t''' <summary>\r\n" +
                    "\t''' Encapsulates a specific Device class for the {8} device ({6}).\r\n" +
                    "\t''' </summary>\r\n" +
                    "\t{2}{3}Class {4}\r\n" +
                    "\t\tInherits Device\r\n" +
                    "\r\n" +
                    "{5}" +
                    "\r\n" +
                    "\t\t#Region \"Public Constants\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' The device type string for this device.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic Const DeviceType As String = \"{6}\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' The device model name string for this device.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic Const DeviceModelName As String = \"{8}\"\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "\r\n" +
                    "\t\t#Region \"Protected Locals\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' The device check flags that were initially used to created the device object.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tProtected mdcDeviceCheck As DeviceCheckFlags\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "\r\n" +
                    "\t\t#Region \"Initialisation\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Creates a new instance of the {8} device from a base device.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"device\">The base device to create the {8} device from.</param>\r\n" +
                    "\t\t''' <param name=\"deviceCheck\">The flags specifying what parameters to check of the base device before allowing creation of the {8} device.</param>\r\n" +
                    "\t\tPublic Sub New(device As Device, deviceCheck As DeviceCheckFlags)\r\n" +
                    "\t\t\tMyBase.New(device)\r\n" +
                    "\t\t\tIf Not CanAccess(device, deviceCheck) Then\r\n" +
                    "\t\t\t\tThrow New NotSupportedException()\r\n" +
                    "\t\t\tEnd If\r\n" +
                    "\r\n" +
                    "\t\t\tmdcDeviceCheck = deviceCheck\r\n" +
                    "\t\tEnd Sub\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "\r\n" +
                    "\t\t#Region \"Public Static Methods\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Determines if a {8} device can use a device as its base.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"device\">The base device to test for compatible functionality.</param>\r\n" +
                    "\t\t''' <param name=\"deviceCheck\">The flags specifying what parameters to check of the base device.</param>\r\n" +
                    "\t\t''' <returns>True if the device can be used as a base device, false otherwise.</returns>\r\n" +
                    "\t\tPublic Shared Function CanAccess(device As Device, deviceCheck As DeviceCheckFlags) As Boolean\r\n" +
                    "\t\t\tReturn _\r\n" +
                    "\t\t\t\t((deviceCheck And DeviceCheckFlags.DeviceType) = DeviceCheckFlags.None OrElse device.Type = DeviceType) AndAlso _\r\n" +
                    "\t\t\t\t((deviceCheck And DeviceCheckFlags.DeviceModelName) = DeviceCheckFlags.None OrElse device.ModelName = DeviceModelName)\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Returns {4} objects for each compatible device in a collection of Devices as an array.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"devices\">The base devices to create the {4} object for.</param>\r\n" +
                    "\t\t''' <param name=\"deviceCheck\">The flags specifying what parameters to check of the base devices.</param>\r\n" +
                    "\t\t''' <returns>An array of {4} objects containing the newly created devices.</returns>\r\n" +
                    "\t\tPublic Shared Function FromDevices(devices As ManagedUPnP.Devices, deviceCheck As DeviceCheckFlags) As {4}()\r\n" +
                    "\t\t\tDim lalReturn As ArrayList = New ArrayList()\r\n" +
                    "\r\n" +
                    "\t\t\tFor Each ldDevice As Device In devices\r\n" +
                    "\t\t\t\tIf ldDevice IsNot Nothing AndAlso CanAccess(ldDevice, deviceCheck) Then\r\n" +
                    "\t\t\t\t\tlalReturn.Add(new {4}(ldDevice, deviceCheck))\r\n" +
                    "\t\t\t\tEnd If\r\n" +
                    "\t\t\tNext\r\n" +
                    "\r\n" +
                    "\t\t\tReturn DirectCast(lalReturn.ToArray(GetType({4})), {4}())\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Returns {4} objects for each compatible device found in a device and all its children.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"baseDevice\">The base device and recursive children to consider.</param>\r\n" +
                    "\t\t''' <param name=\"deviceCheck\">The flags specifying what parameters to check of the base devices.</param>\r\n" +
                    "\t\t''' <returns>An array of {4} objects containing the newly created devices.</returns>\r\n" +
                    "\t\tPublic Shared Function SearchAndCreate(baseDevice As Device, deviceCheck As DeviceCheckFlags) As {4}()\r\n" +
                    "\t\t\tReturn FromDevices(baseDevice.DevicesByType(DeviceType), deviceCheck)\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Returns {4} objects for each compatible device discovered in a synchronous manner.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"deviceCheck\">The flags specifying what parameters to check discovered devices.</param>\r\n" +
                    "\t\t''' <returns>An array of {4} objects containing the newly created devices.</returns>\r\n" +
                    "\t\tPublic Shared Function DiscoverAndCreate(deviceCheck As DeviceCheckFlags) As {4}()\r\n" +
                    "\t\t\tReturn FromDevices(Discovery.FindDevices(DeviceType), deviceCheck)\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "{7}" +
                    "\tEnd Class\r\n" +
                    "End Namespace\r\n";
            }
        }

        #endregion

        #region Service Code Constants

        /// <summary>
        /// Gets the comment for the allowed value constants.
        /// </summary>
        public string AllowedValueComment
        {
            get
            {
                return
                    "The string value for the allowed value {0} of the {1} state variable.";
            }
        }

        /// <summary>
        /// Gets the comment for the state var name constants.
        /// </summary>
        public string StateVarNameComment
        {
            get
            {
                return
                    "The name for the {0} state variable.";
            }
        }

        /// <summary>
        /// Gets the comment for the action name constants.
        /// </summary>
        public string ActionNameComment
        {
            get
            {
                return
                    "The name for the {0} action.";
            }
        }

        /// <summary>
        /// Gets the name for an enum state var type.
        /// </summary>
        /// <remarks>Format Parameters: 0 = StateVarFriendlyName.</remarks>
        public string EnumStateVarName { get { return "{0}Enum"; } }

        /// <summary>
        /// Gets the definition for an enum state var type.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarName, 1 = stateVarValues.
        /// </remarks>
        public string StateVarEnum
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' The enumeration type to hold a value for the {0} state variable.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic Enum {0}Enum\r\n" +
                    "\t\t\r\n" +
                    "{1}" +
                    "\t\t\t''' <summary>\r\n" +
                    "\t\t\t''' Value describing an invalid or unknown {0} value.\r\n" +
                    "\t\t\t''' </summary>\r\n" +
                    "\t\t\t_Unknown\r\n" +
                    "\t\tEnd Enum\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the code used to convert non enum state var value to typed value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = stateVarFriendlyName</remarks>
        public string EnumStateVarEventConversion
        {
            get
            {
                return
                    "Parse{0}(DirectCast(a.StateVarValue, String))";
            }
        }

        /// <summary>
        /// Gest the enumeration value for an enum state variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarEnumValue, 1 = stateVarName
        /// </remarks>
        public string EnumStateVarValue
        {
            get
            {
                return
                    "\t\t\t''' <summary>\r\n" +
                    "\t\t\t''' The {1} state var '{0}' value.\r\n" +
                    "\t\t\t''' </summary>\r\n" +
                    "\t\t\t{0}\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the definition for the property of a non enum state variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = returnType, 1 = stateVarFriendlyName, 2 = stateVarName, 3 = state var comment
        /// </remarks>
        public string NonEnumStateVar
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Gets {3}.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic ReadOnly Property {1}() As {0}\r\n" +
                    "\t\t\tGet\r\n" +
                    "\t\t\t\tReturn QueryStateVariable(Of String)({2})\r\n" +
                    "\t\t\tEnd Get\r\n" +
                    "\t\tEnd Property\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the code used to convert non enum state var value to typed value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = stateVarType</remarks>
        public string NonEnumStateVarEventConversion
        {
            get
            {
                return
                    "DirectCast(a.StateVarValue, {0})";
            }
        }

        /// <summary>
        /// Gets the parsing case statement for an enum type.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarValue, 1 = stateVarName, 2 = stateVarEnumValue.
        /// </remarks>
        public string EnumParseCaseStatement
        {
            get
            {
                return
                    "\t\t\t\tCase {0}\r\n" +
                    "\t\t\t\t\tReturn {1}Enum.{2}\r\n";
            }
        }

        /// <summary>
        /// Gets the to string case statement for an enum type.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarName, 1 = stateVarEnumValue, 2 = stateVarValue.
        /// </remarks>
        public string EnumToStringCaseStatement
        {
            get
            {
                return
                    "\t\t\t\tCase {0}Enum.{1}\r\n" +
                    "\t\t\t\t\t\tReturn {2}\r\n";
            }
        }

        /// <summary>
        /// Gets the event handler definition for state variable changes.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarType, 1 = stateVarName.
        /// </remarks>
        public string StateVariableEventHandler
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Occurs when the service notifies that the {1} state variable has changed its value.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic Event {1}Changed As StateVariableChangedEventHandler(Of {0})\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the event caller definition for state variable changes.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarType, 1 = stateVarName.
        /// </remarks>
        public string StateVariableEventCaller
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Raises the {1}Changed event.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"e\">The event arguments.</param>\r\n" +
                    "\t\tProtected Overridable Sub On{1}Changed(e As StateVariableChangedEventArgs(Of {0}))\r\n" +
                    "\t\t\tRaiseEvent {1}Changed(Me, e)\r\n" +
                    "\t\tEnd Sub\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the code for converting a state var enum type from and to a string.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarFriendlyName, 1 = parseCaseStatements, 2 = toStringCaseStatements.
        /// </remarks>
        public string EnumStateVarConversion
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Parses a string value from the {0} state var and returns the enumeration value for it.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"value\">The string value to parse.</param>\r\n" +
                    "\t\t''' <returns>The parsed value or {0}Enum._Unknown if not parsable.</returns>\r\n" +
                    "\t\tProtected Function Parse{0}(value as String) As {0}Enum \r\n" +
                    "\t\t\tSelect Case value\r\n" +
                    "{1}" +
                    "\t\t\t\tCase Else\r\n" +
                    "\t\t\t\t\tReturn {0}Enum._Unknown\r\n" +
                    "\r\n" + 
                    "\t\t\tEnd Select\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Gets the string value for the {0} state var from its enumeration value.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"value\">The enumeration value to get the string value for.</param>\r\n" +
                    "\t\t''' <returns>The string value for the enumeration, or string.empty if {0}Enum._Unknown or out of range.</returns>\r\n" +
                    "\t\tProtected Function ToString{0}(value As {0}Enum) As String\r\n" +
                    "\t\t\tSelect Case value\r\n" +
                    "{2}" +
                    "\t\t\t\tCase Else\r\n" +
                    "\t\t\t\t\tReturn String.Empty\r\n" +
                    "\r\n" +
                    "\t\t\tEnd Select\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the definition for the property of an enum state variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarFriendlyName, 1 = stateVarName, 2 = state var description.
        /// </remarks>
        public string EnumStateVar
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Gets the raw string value for {2}.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic ReadOnly Property {0}String() As String\r\n" +
                    "\t\t\tGet\r\n" +
                    "\t\t\t\tReturn QueryStateVariable(Of String)({1})\r\n" +
                    "\t\t\tEnd Get\r\n" +
                    "\t\tEnd Property\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Gets {2}.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <remarks>Returns {0}Enum._Unknown on error or if unparsable.</remarks>\r\n" +
                    "\t\tPublic Readonly Property {0}() As {0}Enum\r\n" +
                    "\t\t\tGet\r\n" +
                    "\t\t\t\tTry\r\n" +
                    "\t\t\t\t\tReturn Parse{0}({0}String)\r\n" +
                    "\t\t\t\tCatch\r\n" +
                    "\t\t\t\tEnd Try\r\n" +
                    "\r\n" +
                    "\t\t\t\tReturn {0}Enum._Unknown\r\n" +
                    "\t\t\tEnd Get\r\n" +
                    "\t\tEnd Property\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the definition for an actions in parameter argument.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ArgumentType, 1 = ArgumentName, 2 = ", " if required.
        /// </remarks>
        public string ActionInArgument { get { return "{2}{1} As {0}"; } }

        /// <summary>
        /// Gets the definition for an action out parameter argument.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ArgumentType, 1 = ArgumentName, 2 = ", " if required.
        /// </remarks>
        public string ActionOutArgument { get { return "{2}ByRef {1} As {0}"; } }

        /// <summary>
        /// Gets the definition for setting an actions in value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = InArgumentIndex, 1 = InArgumentName.
        /// </remarks>
        public string InSetValue { get { return "\t\t\tloIn({0}) = {1}\r\n"; } }

        /// <summary>
        /// Gets the definition for setting an actions in enumerated value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = InArgumentIndex, 1 = StateVarEnumName, 2 = InArgumentName.
        /// </remarks>
        public string InSetValueEnum { get { return "\t\t\tloIn({0}) = ToString{1}({2})\r\n"; } }

        /// <summary>
        /// Gets the definition for setting an actions out value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = OutArgumentName, 1 = OutArgumentType, 2 = OutArgumentIndex.
        /// </remarks>
        public string OutSetValue { get { return "\t\t\t{0} = DirectCast(loOut({2}), {1})\r\n"; } }

        /// <summary>
        /// Gets the definition for setting an actions out enumerated value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = OutArgumentName, 1 = StateVarEnumName, 2 = OutArgumentIndex.
        /// </remarks>
        public string OutSetValueEnum { get { return "\t\t\t{0} = Parse{1}(DirectCast(loOut({2}), String))\r\n"; } }

        /// <summary>
        /// Gets the definition for return an actions out value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = OutArgumentType, 1 = OutArgumentIndex.
        /// </remarks>
        public string OutReturnValue { get { return "\t\t\tReturn DirectCast(loOut({1}), {0})\r\n"; } }

        /// <summary>
        /// Gets the definition for return an actions out enumerated value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = StateVarEnumName, 1 = OutArgumentIndex.
        /// </remarks>
        public string OutReturnValueEnum { get { return "\t\t\tReturn Parse{0}(DirectCast(loOut({1}), String))\r\n"; } }

        /// <summary>
        /// Gets the comment line used for an actions return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = name, 1 = information.
        /// </remarks>
        public string ActionReturnsComment { get { return "\t\t''' <returns>Out value for {1}.</returns>\r\n"; } }

        /// <summary>
        /// Gets the action argument comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = actionName, 1 = extra state var Comments.
        /// </remarks>
        public string ArgumentComment { get { return "the {0} action parameter{1}"; } }

        /// <summary>
        /// Gets the state var comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarName, 1 = extra state var comments.
        /// </remarks>
        public string StateVarComment { get { return "the {0} state variable{1}"; } }

        /// <summary>
        /// Gets the state var allowed range comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = minimum range, 1 = maximum range.
        /// </remarks>
        public string StateVarAllowedRangeComment { get { return ". With range of {0} to {1}"; } }

        /// <summary>
        /// Gets the state var step comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = increment step value.
        /// </remarks>
        public string StateVarStepComment { get { return ". Increment of {0}"; } }

        /// <summary>
        /// Gets the state var allowed values comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = state var allowed values list.
        /// </remarks>
        public string StateVarAllowedValues { get { return ". Allowed values are {0}"; } }

        /// <summary>
        /// Gets the state var default value comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = state var default value
        /// </remarks>
        public string StateVarDefaultValueComment { get { return ". Default value of {0}"; } }

        /// <summary>
        /// Gets the state var allowed range when no minimum comment.
        /// </summary>
        public string ArgMinimum { get { return "data type minimum"; } }

        /// <summary>
        /// Gets the state var allowed range when no maximum comment.
        /// </summary>
        public string ArgMaximum { get { return "data type maximum"; } }

        /// <summary>
        /// Gets the comment for an allowed values list.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = comma with space if needed, 1 = allowedValueText.
        /// </remarks>
        public string AllowedValue { get { return "{0}'{1}'"; } }

        /// <summary>
        /// Gets the comment line used for an actions in parameter value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = name, 1 = information.</remarks>
        public string ActionInParamComment 
        { 
            get 
            { 
                return "\t\t''' <param name=\"{0}\">In value for {1}.</param>\r\n"; 
            } 
        }

        /// <summary>
        /// Gets the comment line used for an actions out parameter value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = name, 1 = information.</remarks>
        public string ActionOutParamComment
        {
            get
            {
                return
                    "\t\t''' <param name=\"{0}\">Out value for {1}.</param>\r\n";
            }
        }

        /// <summary>
        /// Gets the code for the out variables declaration in an action method.
        /// </summary>
        public string OutVar { get { return "Dim loOut() As Object = "; } }

        /// <summary>
        /// Gets the definition for a multi out parameter action.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ActionFunctionName, 1 = InArguments, 2 = OutArguments
        /// 3 = CountInArguments, 4 = InSetValues, 5 = ActionName,
        /// 6 = OutSetValues, 7 = InOutArgumentsComma, 8 = return type,
        /// 9 = in params comments, 10 = out params comments, 11 = returns comments,
        /// 12 = out var or string.empty if not used
        /// </remarks>
        public string Action
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Executes the {0} action.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "{9}{10}" +
                    "\t\tPublic Sub {0}({1}{7}{2})\r\n" +
                    "\t\t\tDim loIn({3}) As Object\r\n" +
                    "\r\n" +
                    "{4}" +
                    "\t\t\t{12}InvokeAction({5}, loIn)\r\n" +
                    "\r\n" +
                    "{6}" +
                    "\t\tEnd Sub\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the definition for a single out parameter action.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ActionFunctionName, 1 = InArguments, 2 = OutArguments (string.Empty)
        /// 3 = CountInArguments, 4 = InSetValues, 5 = ActionName,
        /// 6 = OutSetValues, 7 = InOutArgumentsComma, 8 = OutReturnType, 
        /// 9 = in params comments, 10 = out params comments, 11 = returns comments.
        /// 12 = out var or string.empty if not used
        /// </remarks>
        public string ReturnAction
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Executes the {0} action.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "{9}{10}{11}" +
                    "\t\tPublic Function {0}({1}{7}{2}) As {8}\r\n" +
                    "\t\t\tDim loIn({3}) As Object\r\n" +
                    "\r\n" +
                    "{4}" +
                    "\t\t\t{12}InvokeAction({5}, loIn)\r\n" +
                    "\r\n" +
                    "{6}" +
                    "\t\tEnd Function\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the overrided state var redirection code - sends the event for actual state variable
        /// as well as the for the Object type variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarNameConstant, 1 = StateVarName, 2 = StateVarType, 3 = stateVarConvertedValue.
        /// </remarks>
        public string StateVarChangedEventHandlerCaseStatement
        {
            get
            {
                return
                    "\t\t\t\t\tCase {0}\r\n" +
                    "\t\t\t\t\t\t' Raise the event for the {1} state variable\r\n" +
                    "\t\t\t\t\t\tOn{1}Changed( _\r\n" +
                    "\t\t\t\t\t\t\tNew StateVariableChangedEventArgs(Of {2})( _\r\n" +
                    "\t\t\t\t\t\t\t\t{0}, _\r\n" +
                    "\t\t\t\t\t\t\t\t{3}))\r\n" +
                    "\t\t\t\t\t\tExit Select\r\n" +
                    "\t\t\t\t\t\r\n";
            }
        }

        /// <summary>
        /// Gets the overrided state var changed event handler to pass through real state changed event handlers.
        /// </summary>
        /// <remarks>Format Parameters: 0 = case code for each state variable.</remarks>
        public string StateVarChangedEventHandler
        {
            get
            {
                return
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Raises the StateVariableChanged event.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"a\">The event arguments.</param>\r\n" +
                    "\t\tProtected Overrides Sub OnStateVariableChanged(a As StateVariableChangedEventArgs)\r\n" +
                    "\t\t\tTry\r\n" +
                    "\t\t\t\t' Determine state variable that is changing\r\n" +
                    "\t\t\t\tSelect Case a.StateVarName\r\n" +
                    "\t\t\t\t\t\r\n" +
                    "{0}" +
                    "\t\t\t\t\tCase Else\r\n" +
                    "\t\t\t\t\t\tExit Select\r\n" +
                    "\r\n" +
                    "\t\t\t\tEnd Select\r\n" +
                    "\t\t\tCatch\r\n" +
                    "\t\t\tEnd Try\r\n" +
                    "\t\t\t\r\n" +
                    "\t\t\tMyBase.OnStateVariableChanged(a)\r\n" +
                    "\t\tEnd Sub\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the code for the comments as the class header.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = rootDeviceName, 1 = rootDeviceType, 2 = serialNumber
        /// 3 = deviceName, 4 = deviceType,
        /// 5 = serviceName, 6 = serviceType
        /// 7 = dateTime, 8 = className,
        /// 9 = namespaceName, 10 = classScope,
        /// 11 = partialClass, 12 = testStateVars.
        /// </remarks>
        public string ServiceClassHeaderComment
        {
            get
            {
                return
                    "' Generated by the ManagedUPnP Framework\r\n" +
                    "' http://managedupnp.codeplex.com\r\n" +
                    "'\r\n" +
                    "' For Root Device: {0} ({1}) - Serial: {2}\r\n" +
                    "' For Device: {3} ({4})\r\n" +
                    "' Using Service: {5} ({6})\r\n" +
                    "'\r\n" +
                    "' On: {7}\r\n" +
                    "'\r\n" +
                    "' Code Generation Provider: {13}\r\n" +
                    "' Class Name: {8}\r\n" +
                    "' Namespace Name: {9}\r\n" +
                    "' Class Scope: {10}\r\n" +
                    "' Partial Class: {11}\r\n" +
                    "' Test State Vars for Properties: {12}\r\n" +
                    "\r\n";
            }
        }

        /// <summary>
        /// Gets the definition for the entire service class.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = namespace, 1 = classname, 2 = serviceType, 3 = enuemrations, 
        /// 4 = "Partial " if partial class, else string.empty, 5 = StateVarConversions,
        /// 6 = ActionMethods, 7 = StateVarProps, 8 = stringConstants, 9 = classModifiers,
        /// 10 = eventHandlers, 11 = eventCallers, 12 = classHeaderComment.
        /// </remarks>
        public string ServiceBase
        {
            get
            {
                return
                    "{12}" +
                    "Imports System\r\n" +
                    "Imports System.Collections\r\n" +
                    "Imports ManagedUPnP\r\n" +
                    "\r\n" +
                    "Namespace {0}\r\n" +
                    "\r\n" +
                    "\t''' <summary>\r\n" +
                    "\t''' Encapsulates a specific Service class for the {1} ({2}) service.\r\n" +
                    "\t''' </summary>\r\n" +
                    "\t{9}{4}Class {1}\r\n" +
                    "\t\tInherits Service\r\n" +
                    "\t\r\n" +
                    "{8}" +
                    "\r\n" +
                    "\t\t#Region \"Public Constants\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' The service type identifier for the {1} service.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\tPublic Const ServiceType As String = \"{2}\"\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "{3}" +
                    "\r\n" +
                    "\t\t#Region \"Initialisation\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Creates a new instance of the {1} service from a base service.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"service\">The base service to create the {1} service from.</param>\r\n" +
                    "\t\tPublic Sub New(service As Service)\r\n" +
                    "\t\t\tMyBase.New(service)\r\n" +
                    "\t\t\r\n" +
                    "\t\t\tIf Not CanAccess(service) Then\r\n" +
                    "\t\t\t\tThrow New NotSupportedException()\r\n" +
                    "\t\t\tEnd If\r\n" +
                    "\t\tEnd Sub\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "\r\n" +
                    "\t\t#Region \"Public Static Methods\"\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Determines if a service is compatible with this service class.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"service\">The base service to test for compatibility with.</param>\r\n" +
                    "\t\t''' <returns>True if the service type is compatible, false otherwise.</returns>\r\n" +
                    "\t\tPublic Shared Function CompatibleWith(service As Service) As Boolean\r\n" +
                    "\t\t\tReturn service.ServiceTypeIdentifier = ServiceType\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Returns {1} objects for each compatible service in a collection of Services as an array.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"services\">The base services to create the {1} object for.</param>\r\n" +
                    "\t\t''' <returns>An array of {1} objects containing the newly created services.</returns>\r\n" +
                    "\t\tPublic Shared Function FromServices(services As ManagedUPnP.Services) As {1}()\r\n" +
                    "\t\t\tDim lalReturn As New ArrayList()\r\n" +
                    "\r\n" +
                    "\t\t\tFor Each lsService As Service in services\r\n" +
                    "\t\t\t\tIf lsService IsNot Nothing AndAlso CompatibleWith(lsService) Then\r\n" +
                    "\t\t\t\t\tlalReturn.Add(New {1}(lsService))\r\n" +
                    "\t\t\t\tEnd If\r\n" +
                    "\t\t\tNext\r\n" +
                    "\r\n" +
                    "\t\t\tReturn DirectCast(lalReturn.ToArray(GetType({1})), {1}())\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Returns {1} objects for each compatible service found in a devices child services.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"baseDevice\">The base device to consider.</param>\r\n" +
                    "\t\t''' <param name=\"includingChildDevices\">True to search all child devices recursively, false to use direct children only.</param>\r\n" +
                    "\t\t''' <returns>An array of {1} objects containing the newly created services.</returns>\r\n" +
                    "\t\tPublic Shared Function SearchAndCreate(baseDevice As Device, includingChildDevices As Boolean) As {1}()\r\n" +
                    "\t\t\tReturn FromServices(New ManagedUPnP.Services(baseDevice, ServiceType, includingChildDevices))\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Returns {1} objects for each compatible services discovered in a synchronous manner.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <returns>An array of {1} objects containing the newly created services.</returns>\r\n" +
                    "\t\tPublic Shared Function DiscoverAndCreate() As {1}()\r\n" +
                    "\t\t\tReturn FromServices(Discovery.FindServices(ServiceType))\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "{10}" +
                    "{11}" +
                    "{5}" +
                    "\r\n" +
                    "\t\t#Region \"Public Methods\"\r\n" +
                    "\r\n" +
                    "{6}" +
                    "\t\t''' <summary>\r\n" +
                    "\t\t''' Determines if a base service can access this service class.\r\n" +
                    "\t\t''' </summary>\r\n" +
                    "\t\t''' <param name=\"service\">The base service to test for compatibility with.</param>\r\n" +
                    "\t\t''' <returns>True if the service can be used to access this class, false otherwise.</returns>\r\n" +
                    "\t\tPublic Overrides Function CanAccess(service As Service) As Boolean\r\n" +
                    "\t\t\tReturn MyBase.CanAccess(service) AndAlso CompatibleWith(service)\r\n" +
                    "\t\tEnd Function\r\n" +
                    "\r\n" +
                    "\t\t#End Region\r\n" +
                    "{7}" +
                    "\tEnd Class\r\n" +
                    "End Namespace\r\n";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates code for a region if content is available.
        /// </summary>
        /// <param name="regionName">The name of the region.</param>
        /// <param name="regionContent">The content of the region with one empty line at the end and none at the start.</param>
        /// <param name="addEmptyLineBeforeEnd">True to add empty line before region ending line.</param>
        /// <param name="addEmptyLineBeforeStart">True to add empty line before region start line.</param>
        /// <param name="indentation">The indentation (0 = non embedded class).</param>
        /// <returns>The string for the region.</returns>
        public string GenerateRegion(string regionName, string regionContent,
            bool addEmptyLineBeforeEnd = false, bool addEmptyLineBeforeStart = true, int indentation = 0)
        {
            if (regionContent.Length > 0)
                return String.Format(
                    Region,
                    regionName,
                    regionContent,
                    new string(IndentChar, indentation),
                    (addEmptyLineBeforeEnd ? EmptyLine : string.Empty),
                    (addEmptyLineBeforeStart ? EmptyLine : string.Empty));
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets a code friendly identifier for a string.
        /// </summary>
        /// <param name="name">The string.</param>
        /// <param name="parameter">True if identifier is a parameter (ie. starts with lower case letter).</param>
        /// <returns>The code friendly identifier.</returns>
        public string CodeFriendlyIdentifier(string name, bool parameter)
        {
            return CodeFriendlyIdentifier(name, parameter, false);
        }

        /// <summary>
        /// Gets a code friendly identifier for a string.
        /// </summary>
        /// <param name="name">The string.</param>
        /// <param name="parameter">True if identifier is a parameter (ie. starts with lower case letter).</param>
        /// <param name="allowUnderscores">True to allow underscores from the string.</param>
        /// <returns>The code friendly identifier.</returns>
        public string CodeFriendlyIdentifier(string name, bool parameter, bool allowUnderscores)
        {
            StringBuilder lsbReturn = new StringBuilder();

            foreach (Char lchChar in name)
            {
                // Allow only A..Z, a..z, 0..9, and underscore if wanted
                if (
                    (lchChar >= 'A' && lchChar <= 'Z') ||
                    (lchChar >= 'a' && lchChar <= 'z') ||
                    (lchChar >= '0' && lchChar <= '9') ||
                    (lchChar == '_' && allowUnderscores))
                {
                    // If this is the first character
                    if (lsbReturn.Length == 0)
                    {
                        // If its an underscore or a numeric then append an underscore to the start first
                        if (lchChar == '_' || (lchChar >= '0' && lchChar <= '9')) lsbReturn.Append("_" + lchChar);
                        else
                            // If its a letter then convert it to upper if this isnt a parameter
                            if (!parameter) lsbReturn.Append(lchChar.ToString().ToUpper());
                            else
                                // If its a letter then convert it to lower if this is a parameter
                                if (parameter) lsbReturn.Append(lchChar.ToString().ToLower());
                    }
                    else
                        // Otherwise just append the character
                        lsbReturn.Append(lchChar);
                }
            }

            // Return the identifier
            return lsbReturn.ToString();
        }

        /// <summary>
        /// Gets the class scope for a ClassScope enumeration.
        /// </summary>
        /// <param name="classScope">The class scope enumeration.</param>
        /// <param name="addAfter">The string to add after the scope if it is available.</param>
        /// <returns>The class scope as a string.</returns>
        public string GetClassScope(ClassScope classScope, string addAfter)
        {
            string lsScope = string.Empty;

            switch (classScope)
            {
                case ClassScope.Internal:
                    lsScope = "Friend";
                    break;

                case ClassScope.Private:
                    lsScope = "Private";
                    break;

                case ClassScope.Public:
                    lsScope = "Public";
                    break;
            }

            if (lsScope.Length > 0)
                return string.Format("{0}{1}", lsScope, addAfter);
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the array initialiser size for a specific count.
        /// </summary>
        /// <param name="inArgumentCount">The number of elements to be in the array.</param>
        /// <returns>A string defining the number of elements in the array.</returns>
        public string ArraySizeForCount(int inArgumentCount)
        {
            // VB.Net arrays are defined by last valid index not count
            // so reduce by one to compensate, this also works
            // with 0 element arrays by using -1
            return (inArgumentCount-1).ToString();
        }

        /// <summary>
        /// Converts the code provider to a string.
        /// </summary>
        /// <returns>The code provider name as a string.</returns>
        public override string ToString()
        {
            return "VB.Net";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the default root namespace to use in
        /// using clauses for VB.NET projects.
        /// </summary>
        public string DefaultRootNamespace
        {
            get
            {
                return msDefaultRootNamespace;
            }
            set
            {
                msDefaultRootNamespace = CodeFriendlyIdentifier(value, false);
            }
        }

        #endregion
    }
}

#endif