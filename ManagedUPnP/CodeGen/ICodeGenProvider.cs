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
    /// Encapsulates the ability to provide constants
    /// required for code generation. Classes implementing
    /// this interface are to be passed to the 
    /// <see cref="DeviceGen.GenerateClassFor"/>
    /// and the 
    /// <see cref="ServiceGen.GenerateClassFor"/>
    /// static methods.
    /// </summary>
    public interface ICodeGenProvider
    {
        #region Other Constants

        /// <summary>
        /// Gets the full file extension for a partial class.
        /// </summary>
        string PartialClassFileExtension { get; }

        /// <summary>
        /// Gets the full file extension for class.
        /// </summary>
        string ClassFileExtension { get; }

        #endregion

        #region String Constant Code

        /// <summary>Gets the code for the constant name.</summary>
        /// <remarks>Format Parameters: 0 = typeName, 1 = valueName.</remarks>
        string ConstantIdentifyer { get; }

        /// <summary>Gets the code for the constant group definition.</summary>
        /// <remarks>Format Parameters: 0 = type, 1 = constantDefinitions.</remarks>
        string ConstantGroupDefinition { get; }

        /// <summary>Gets the code for the constant comment.</summary>
        /// <remarks>Format Parameters: 0 = commentText.</remarks>
        string ConstantComment { get; }

        /// <summary>Gets the code for the constant definition.</summary>
        /// <remarks>Format Parameters: 0 = VarName, 1 = value, 2 = comments</remarks>
        string ConstantDefinition { get; }

        #endregion

        #region String Constant Groups

        /// <summary>
        /// Gets the group name for state variable constant identifiers.
        /// </summary>
        string StateVarNameGroup { get; }

        /// <summary>
        /// Gets the group name for state variable allowed value constant identifiers.
        /// </summary>
        /// <remarks>Format Parameters: 0 = StateVarFriendlyName.</remarks>
        string AllowedValueGroup { get; }

        /// <summary>
        /// Gets the group name for action constant identifiers.
        /// </summary>
        string ActionNameGroup { get; }

        /// <summary>
        /// Gets the group name for service constant identifiers.
        /// </summary>
        string ServiceGroup { get; }

        /// <summary>
        /// Gets the group name for device constant identifiers.
        /// </summary>
        /// <remarks>Format Parameters: 0 = StateVarFriendlyName</remarks>
        string DeviceGroup { get; }

        #endregion

        #region Region Constants

        /// <summary>
        /// Gets the protected constants region name.
        /// </summary>
        string ProtectedConstants { get; }

        /// <summary>
        /// Gets the public properties region name.
        /// </summary>
        string PublicProperties { get; }

        /// <summary>
        /// Gets the protected methods region name.
        /// </summary>
        string ProtectedMethods { get; }

        /// <summary>
        /// Gets the public enumerations region name.
        /// </summary>
        string PublicEnumerations { get; }

        /// <summary>
        /// Gets the event handlers region name.
        /// </summary>
        string EventHandlers { get; }

        /// <summary>
        /// Gets the event callers region name.
        /// </summary>
        string EventCallers { get; }

        /// <summary>
        /// Gets the code for a region.
        /// </summary>
        /// <remarks>Format Parameters: 0 = regionName, 1 = regionCode, 2 = indentation, 3 = beforeEnd, 4 = beforeStart.</remarks>
        string Region { get; }

        #endregion

        #region Misc Constants

        /// <summary>
        /// Gets the Null value for an object.
        /// </summary>
        string Null { get; }

        /// <summary>
        /// Gets the partial class statement.
        /// </summary>
        string PartialClass { get; }

        /// <summary>
        /// Gets the public modifier statement.
        /// </summary>
        string Public { get; }

        /// <summary>
        /// Gets the name of the service class.
        /// </summary>
        string Service { get; }

        /// <summary>
        /// Gets the name of the device class.
        /// </summary>
        string Device { get; }

        /// <summary>
        /// Gets the string to append to the string constant names for service IDs.
        /// </summary>
        string ServiceID { get; }

        /// <summary>
        /// Gets the string to append to the string constant names for device Types.
        /// </summary>
        string DeviceType { get; }

        /// <summary>
        /// Gets the string to append to the string constant names for device model names.
        /// </summary>
        string DeviceModelName { get; }

        /// <summary>
        /// Gets a space.
        /// </summary>
        char Space { get; }

        /// <summary>
        /// Gets the indent char to use for indentation of code.
        /// </summary>
        char IndentChar { get; }

        /// <summary>
        /// Gets the string representing and empty line.
        /// </summary>
        string EmptyLine { get; }

        /// <summary>
        /// Gets the type for a parameter when it cannot be determined.
        /// </summary>
        /// <remarks>This can occur if a related state variable definition is missing.</remarks>
        string UnknownType { get; }

        /// <summary>
        /// Gets a comma for delimiting parameters.
        /// </summary>
        string ParameterSeperator { get; }

        /// <summary>
        /// Gets a comma with a trailing space.
        /// </summary>
        string Comma { get; }

        /// <summary>
        /// Gets the definition for a using clause.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = Using namespace.
        /// </remarks>
        string UsingClause { get; }

        #endregion

        #region Device Code Constants

        /// <summary>
        /// Gets the definition for the service ID constant comment.
        /// </summary>
        /// <remarks>Format Parameters: 0 = service friendly name.</remarks>
        string ServiceIdConstComment { get; }

        /// <summary>
        /// Gets the definition for the device type constant comment.
        /// </summary>
        /// <remarks>Format Parameters: 0 = device friendly name.</remarks>
        string DeviceTypeConstComment { get; }

        /// <summary>
        /// Gets the definition for the generic device return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = DeviceClass, 1 = DeviceType.
        /// </remarks>
        string GenericDeviceRet { get; }

        /// <summary>
        /// Gets the definition for the generic service return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ServiceClass, 1 = ServiceId.
        /// </remarks>
        string GenericServiceRet { get; }

        /// <summary>
        /// Gets the definition for the specific device return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = DeviceClass, 1 = DeviceType.
        /// </remarks>
        string SpecificDeviceRet { get; }

        /// <summary>
        /// Gets the definition for the specific service return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ServiceClass, 0 = ServiceId.
        /// </remarks>
        string SpecificServiceRet { get; }

        /// <summary>
        /// Gets the definition for a public property.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = returnType, 1 = propStartName, 2 = propEndName,
        /// 3 = returnValue, 4 = Device/Service name, 5 = Device/Service type, 6 = "Device" / "Service"
        /// </remarks>
        string Property { get; }

        /// <summary>
        /// Gets the code for the comments as the class header.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = rootDeviceName, 1 = rootDeviceType, 2 = serialNumber
        /// 3 = deviceName, 4 = deviceType,
        /// 5 = dateTime, 6 = className,
        /// 7 = namespaceName, 8 = classScope,
        /// 9 = partialClass.
        /// </remarks>
        string DeviceClassHeaderComment { get; }

        /// <summary>
        /// Gets the definition for the entire device class.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = using, 1 = namespace, 2 = classScope, 3 = "partial " if partial class, 
        /// 4 = className, 5 = protectedConstants, 6 = DeviceType, 7 = proprties,
        /// 8 = DeviceModelName, 9 = classHeaderComment
        /// </remarks>
        string DeviceBase { get; }

        #endregion

        #region Service Code Constants

        /// <summary>
        /// Gets the comment for the allowed value constants.
        /// </summary>
        string AllowedValueComment { get; }

        /// <summary>
        /// Gets the comment for the state var name constants.
        /// </summary>
        string StateVarNameComment { get; }

        /// <summary>
        /// Gets the comment for the action name constants.
        /// </summary>
        string ActionNameComment { get; }

        /// <summary>
        /// Gets the name for an enum state var type.
        /// </summary>
        /// <remarks>Format Parameters: 0 = StateVarFriendlyName.</remarks>
        string EnumStateVarName { get; }

        /// <summary>
        /// Gets the definition for an enum state var type.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarName, 1 = stateVarValues.
        /// </remarks>
        string StateVarEnum { get; }

        /// <summary>
        /// Gets the code used to convert non enum state var value to typed value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = stateVarFriendlyName</remarks>
        string EnumStateVarEventConversion { get; }

        /// <summary>
        /// Gets an enumeration value for an enum state variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarEnumValue, 1 = stateVarName
        /// </remarks>
        string EnumStateVarValue { get; }

        /// <summary>
        /// Gets the definition for the property of a non enum state variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = returnType, 1 = stateVarFriendlyName, 2 = stateVarName, 3 = state var comment
        /// </remarks>
        string NonEnumStateVar { get; }

        /// <summary>
        /// Gets the code used to convert non enum state var value to typed value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = stateVarType</remarks>
        string NonEnumStateVarEventConversion { get; }

        /// <summary>
        /// Gets the parsing case statement for an enum type.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarValue, 1 = stateVarName, 2 = stateVarEnumValue.
        /// </remarks>
        string EnumParseCaseStatement { get; }

        /// <summary>
        /// Gets the to string case statement for an enum type.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarName, 1 = stateVarEnumValue, 2 = stateVarValue.
        /// </remarks>
        string EnumToStringCaseStatement { get; }

        /// <summary>
        /// Gets the event handler definition for state variable changes.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarType, 1 = stateVarName.
        /// </remarks>
        string StateVariableEventHandler { get; }

        /// <summary>
        /// Gets the event caller definition for state variable changes.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarType, 1 = stateVarName.
        /// </remarks>
        string StateVariableEventCaller { get; }

        /// <summary>
        /// Gets the code for converting a state var enum type from and to a string.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarFriendlyName, 1 = parseCaseStatements, 2 = toStringCaseStatements.
        /// </remarks>
        string EnumStateVarConversion { get; }

        /// <summary>
        /// Gets the definition for the property of an enum state variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarFriendlyName, 1 = stateVarName, 2 = state var description.
        /// </remarks>
        string EnumStateVar { get; }

        /// <summary>
        /// Gets the definition for an actions in parameter argument.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ArgumentType, 1 = ArgumentName, 2 = ", " if required.
        /// </remarks>
        string ActionInArgument { get; }

        /// <summary>
        /// Gets the definition for an action out parameter argument.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = ArgumentType, 1 = ArgumentName, 2 = ", " if required.
        /// </remarks>
        string ActionOutArgument { get; }

        /// <summary>
        /// Gets the definition for setting an actions in value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = InArgumentIndex, 1 = InArgumentName.
        /// </remarks>
        string InSetValue { get; }

        /// <summary>
        /// Gets the definition for setting an actions in enumerated value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = InArgumentIndex, 1 = StateVarEnumName, 2 = InArgumentName.
        /// </remarks>
        string InSetValueEnum { get; }

        /// <summary>
        /// Gets the definition for setting an actions out value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = OutArgumentName, 1 = OutArgumentType, 2 = OutArgumentIndex.
        /// </remarks>
        string OutSetValue { get; }

        /// <summary>
        /// Gets the definition for setting an actions out enumerated value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = OutArgumentName, 1 = StateVarEnumName, 2 = OutArgumentIndex.
        /// </remarks>
        string OutSetValueEnum { get; }

        /// <summary>
        /// Gets the definition for return an actions out value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = OutArgumentType, 1 = OutArgumentIndex.
        /// </remarks>
        string OutReturnValue { get; }

        /// <summary>
        /// Gets the definition for return an actions out enumerated value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = StateVarEnumName, 1 = OutArgumentIndex.
        /// </remarks>
        string OutReturnValueEnum { get; }

        /// <summary>
        /// Gets the comment line used for an actions return value.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = name, 1 = information.
        /// </remarks>
        string ActionReturnsComment { get; }

        /// <summary>
        /// Gets the action argument comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = actionName, 1 = extra state var Comments.
        /// </remarks>
        string ArgumentComment { get; }

        /// <summary>
        /// Gets the state var comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarName, 1 = extra state var comments.
        /// </remarks>
        string StateVarComment { get; }

        /// <summary>
        /// Gets the state var allowed range comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = minimum range, 1 = maximum range.
        /// </remarks>
        string StateVarAllowedRangeComment { get; }

        /// <summary>
        /// Gets the state var step comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = increment step value.
        /// </remarks>
        string StateVarStepComment { get; }

        /// <summary>
        /// Gets the state var allowed values comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = state var allowed values list.
        /// </remarks>
        string StateVarAllowedValues { get; }

        /// <summary>
        /// Gets the state var default value comment.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = state var default value
        /// </remarks>
        string StateVarDefaultValueComment { get; }

        /// <summary>
        /// Gets the state var allowed range when no minimum comment.
        /// </summary>
        string ArgMinimum { get; }

        /// <summary>
        /// Gets the state var allowed range when no maximum comment.
        /// </summary>
        string ArgMaximum { get; }

        /// <summary>
        /// Gets the comment for an allowed values list.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = comma with space if needed, 1 = allowedValueText.
        /// </remarks>
        string AllowedValue { get; }

        /// <summary>
        /// Gets the comment line used for an actions in parameter value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = name, 1 = information.</remarks>
        string ActionInParamComment { get; }

        /// <summary>
        /// Gets the comment line used for an actions out parameter value.
        /// </summary>
        /// <remarks>Format Parameters: 0 = name, 1 = information.</remarks>
        string ActionOutParamComment { get; }

        /// <summary>
        /// Gets the code for the out variables declaration in an action method.
        /// </summary>
        string OutVar { get; }

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
        string Action { get; }

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
        string ReturnAction { get; }

        /// <summary>
        /// Gets the overrided state var redirection code - sends the event for actual state variable
        /// as well as the for the Object type variable.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = stateVarNameConstant, 1 = StateVarName, 2 = StateVarType, 3 = stateVarConvertedValue.
        /// </remarks>
        string StateVarChangedEventHandlerCaseStatement { get; }

        /// <summary>
        /// Gets the overrided state var changed event handler to pass through real state changed event handlers.
        /// </summary>
        /// <remarks>Format Parameters: 0 = case code for each state variable.</remarks>
        string StateVarChangedEventHandler { get; }

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
        string ServiceClassHeaderComment { get; }

        /// <summary>
        /// Gets the definition for the entire service class.
        /// </summary>
        /// <remarks>
        /// Format Parameters:
		/// 0 = namespace, 1 = classname, 2 = serviceType, 3 = enuemrations, 
        /// 4 = "partial " if partial class, else string.empty, 5 = StateVarConversions,
        /// 6 = ActionMethods, 7 = StateVarProps, 8 = stringConstants, 9 = classModifiers,
        /// 10 = eventHandlers, 11 = eventCallers, 12 = classHeaderComment.
        /// </remarks>
        string ServiceBase { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Generates code for a region if content is available.
        /// </summary>
        /// <param name="regionName">The name of the region.</param>
        /// <param name="regionContent">The content of the region with one empty line at the end and none at the start.</param>
        /// <param name="addEmptyLineBeforeEnd">True to add empty line before region ending line.</param>
        /// <param name="addEmptyLineBeforeStart">True to add empty line before region start line.</param>
        /// <param name="indentation">The indentation (0 = non embedded class).</param>
        /// <returns>The string for the region.</returns>
        string GenerateRegion(
            string regionName, string regionContent,
            bool addEmptyLineBeforeEnd = false, 
            bool addEmptyLineBeforeStart = true, int indentation = 0);

        /// <summary>
        /// Gets a code friendly identifier for a string.
        /// </summary>
        /// <param name="name">The string.</param>
        /// <param name="parameter">True if identifier is a parameter (ie. starts with lower case letter).</param>
        /// <returns>The code friendly identifier.</returns>
        string CodeFriendlyIdentifier(string name, bool parameter);

        /// <summary>
        /// Gets a code friendly identifier for a string.
        /// </summary>
        /// <param name="name">The string.</param>
        /// <param name="parameter">True if identifier is a parameter (ie. starts with lower case letter).</param>
        /// <param name="allowUnderscores">True to allow underscores from the string.</param>
        /// <returns>The code friendly identifier.</returns>
        string CodeFriendlyIdentifier(string name, bool parameter, bool allowUnderscores);

        /// <summary>
        /// Gets the class scope for a ClassScope enumeration.
        /// </summary>
        /// <param name="classScope">The class scope enumeration.</param>
        /// <param name="addAfter">The string to add after the scope if it is available.</param>
        /// <returns>The class scope as a string.</returns>
        string GetClassScope(ClassScope classScope, string addAfter);

        /// <summary>
        /// Gets the array initialiser size for a specific count.
        /// </summary>
        /// <param name="inArgumentCount">The number of elements to be in the array.</param>
        /// <returns>A string defining the number of elements in the array.</returns>
        string ArraySizeForCount(int inArgumentCount);

        #endregion
    }
}

#endif