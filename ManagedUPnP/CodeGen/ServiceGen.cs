//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_CodeGen

using System;
using System.Collections.Generic;
using System.Text;
using ManagedUPnP.Descriptions;

namespace ManagedUPnP.CodeGen
{
    /// <summary>
    /// Encapsulates a static class which can generate class code for a service.
    /// </summary>
    public class ServiceGen
    {
        #region Protected Locals

        /// <summary>
        /// The code generation constant provider to use.
        /// </summary>
        protected ICodeGenProvider mcgCodeGenProvider;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new service class code generator.
        /// </summary>
        /// <param name="codeGenProvider">The provider to use when generating the code.</param>
        public ServiceGen(ICodeGenProvider codeGenProvider)
        {
            mcgCodeGenProvider = codeGenProvider;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates the code for all actions.
        /// </summary>
        /// <param name="consts">The stirng constants created.</param>
        /// <param name="actionMethods">The StringBuilder to contain the action call methods.</param>
        /// <param name="enumStateVars">A HashSet of the state variables which are enumerations.</param>
        /// <param name="stateVarTypes">A Dictionary containing the state variables and their data types.</param>
        /// <param name="desc">The service description to generate the action code for.</param>
        private void GenerateActionCode(
            StringConstants consts, StringBuilder actionMethods, HashSet<string> enumStateVars,
            Dictionary<string, string> stateVarTypes, ServiceDescription desc)
        {
            // For each action
            foreach (ActionDescription ladDesc in desc.Actions.Values)
            {
                string lsFriendlyName = CodeGenProvider.CodeFriendlyIdentifier(ladDesc.Name, false);
                int liInArgumentCount = 0;
                int liInArgumentIndex = 0;
                int liOutArgumentIndex = 0;
                StringBuilder lsbInArguments = new StringBuilder();
                StringBuilder lsbOutArguments = new StringBuilder();
                StringBuilder lsbInSetValues = new StringBuilder();
                StringBuilder lsbOutSetValues = new StringBuilder();
                StringBuilder lsbInParamComments = new StringBuilder();
                StringBuilder lsbOutParamComments = new StringBuilder();
                StringBuilder lsbReturnsComments = new StringBuilder();
                int liOutArguments = ladDesc.Arguments.OutArgCount;
                string lsLastOutType = CodeGenProvider.UnknownType;

                // For each argument
                foreach (ArgumentDescription ladArgDesc in ladDesc.Arguments.Values)
                {
                    bool lbEnumStateVar = enumStateVars.Contains(ladArgDesc.RelatedStateVariable);
                    string lsArgFriendlyName = CodeGenProvider.CodeFriendlyIdentifier(ladArgDesc.Name, true);
                    string lsRelatedStateVarFriendlyName = CodeGenProvider.CodeFriendlyIdentifier(ladArgDesc.RelatedStateVariable, false);
                    string lsType;

                    // Get the type of the related state variable, or use unknown type if state variable cannot be found
                    if (!stateVarTypes.TryGetValue(ladArgDesc.RelatedStateVariable, out lsType))
                        lsType = CodeGenProvider.UnknownType;

                    // Determine direction
                    if (ladArgDesc.DirectionValue == ArgumentDirection.Out)
                    {
                        lsLastOutType = lsType;

                        // If there is more than 1 out argument then use out parameters
                        if (liOutArguments > 1)
                            GenerateOutArgumentCode(
                                ladArgDesc, liOutArgumentIndex, lsbOutArguments, lsbOutSetValues, lbEnumStateVar,
                                lsArgFriendlyName, lsRelatedStateVarFriendlyName, lsType, lsbOutParamComments);
                        else
                            // Otherwise use a returning function
                            GenerateOutArgumentReturnCode(
                                ladArgDesc, liOutArgumentIndex, lsbOutSetValues, lbEnumStateVar,
                                lsRelatedStateVarFriendlyName, lsType, lsbReturnsComments);

                        // Increment the out argument index
                        liOutArgumentIndex++;
                    }
                    else
                    {
                        // Generate the in argument code
                        GenerateInArgumentCode(
                            ladArgDesc, liInArgumentIndex, lsbInArguments, lsbInSetValues,
                            lbEnumStateVar, lsArgFriendlyName, lsRelatedStateVarFriendlyName,
                            lsType, lsbInParamComments);

                        // Increment in argument count and index
                        liInArgumentCount++;
                        liInArgumentIndex++;
                    }
                }

                // Generate the method code
                actionMethods.Append(
                    string.Format(
                        (liOutArguments == 1 ? CodeGenProvider.ReturnAction : CodeGenProvider.Action),
                        lsFriendlyName,
                        lsbInArguments,
                        lsbOutArguments,
                        CodeGenProvider.ArraySizeForCount(liInArgumentCount),
                        lsbInSetValues,
                        consts[CodeGenProvider.ActionNameGroup, ladDesc.Name, string.Format(CodeGenProvider.ActionNameComment, ladDesc.Name)],
                        lsbOutSetValues,
                        (lsbInArguments.Length > 0 && lsbOutArguments.Length > 0 ? CodeGenProvider.ParameterSeperator : String.Empty),
                        lsLastOutType,
                        lsbInParamComments, 
                        lsbOutParamComments, 
                        lsbReturnsComments,
                        (liOutArguments > 0 ? CodeGenProvider.OutVar : string.Empty)
                    )
                );
            }
        }

        /// <summary>
        /// Generates the descriptive details for a state variable, eg. AllowedRange, Defulat, AllowedValues.
        /// </summary>
        /// <param name="stateVarDesc">The state variable description describing the state variable.</param>
        /// <returns>A string containing a single line comment.</returns>
        private string GenerateStateVariableDescriptionComment(StateVariableDescription stateVarDesc)
        {
            if (stateVarDesc != null)
            {
                StringBuilder lsbComment = new StringBuilder();

                // Allowed range
                if (stateVarDesc.AllowedRange != null)
                {
                    // Allowed range - min and max
                    if (
                        !string.IsNullOrEmpty(stateVarDesc.AllowedRange.Minimum) ||
                        !string.IsNullOrEmpty(stateVarDesc.AllowedRange.Maximum)
                       )
                        lsbComment.Append(
                            string.Format(
                                CodeGenProvider.StateVarAllowedRangeComment,
                                (string.IsNullOrEmpty(stateVarDesc.AllowedRange.Minimum) ? CodeGenProvider.ArgMinimum : stateVarDesc.AllowedRange.Minimum),
                                (string.IsNullOrEmpty(stateVarDesc.AllowedRange.Minimum) ? CodeGenProvider.ArgMaximum : stateVarDesc.AllowedRange.Maximum)
                            )
                        );

                    // Allowed range - step
                    if (!string.IsNullOrEmpty(stateVarDesc.AllowedRange.Step)
                       )
                        lsbComment.Append(
                            string.Format(
                                CodeGenProvider.StateVarStepComment,
                                stateVarDesc.AllowedRange.Step
                            )
                        );
                }

                // Allowed values - ecvluding Enum types 
                if (stateVarDesc.AllowedValues != null && 
                    stateVarDesc.AllowedValues.Count > 0 &&
                    stateVarDesc.DataTypeValue != StateVariableDataType.tstring)
                {
                    StringBuilder lsbAllowedValues = new StringBuilder();

                    foreach (string lsAllowedValue in stateVarDesc.AllowedValues)
                        lsbAllowedValues.Append(
                            string.Format(
                                CodeGenProvider.AllowedValue,
                                (lsbAllowedValues.Length == 0 ? string.Empty : CodeGenProvider.Comma),
                                lsAllowedValue
                            )
                        );

                    if (lsbAllowedValues.Length > 0)
                        lsbComment.Append(
                            string.Format(
                                CodeGenProvider.StateVarAllowedValues,
                                lsbAllowedValues.ToString()
                            )
                        );
                }

                // Default value
                if (!string.IsNullOrEmpty(stateVarDesc.DefaultValue))
                    lsbComment.Append(
                        string.Format(
                            CodeGenProvider.StateVarDefaultValueComment,
                            stateVarDesc.DefaultValue
                        )
                    );

                return lsbComment.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Generates the comments for a state variable including its name.
        /// </summary>
        /// <param name="stateVarDesc">The state variable description describing the state variable.</param>
        /// <returns>A string containing a single line comment.</returns>
        private string GenerateStateVarComment(StateVariableDescription stateVarDesc)
        {
            return string.Format(
                CodeGenProvider.StateVarComment,
                stateVarDesc.Name,
                GenerateStateVariableDescriptionComment(stateVarDesc));
        }

        /// <summary>
        /// Generates the comments for an actions method argument including its name.
        /// </summary>
        /// <param name="argumentDesc">The argument description describing the argument of the action.</param>
        /// <returns>A string containing a single line comment.</returns>
        private string GenerateArgumentDescriptionComment(ArgumentDescription argumentDesc)
        {
            StateVariableDescription lsdDesc = argumentDesc.RelatedStateVariableDescription;

            return string.Format(
                CodeGenProvider.ArgumentComment,
                argumentDesc.Name,
                GenerateStateVariableDescriptionComment(lsdDesc));
        }

        /// <summary>
        /// Generates the code for an in argument.
        /// </summary>
        /// <param name="argumentDesc">The argument description.</param>
        /// <param name="inArgumentIndex">The argument index.</param>
        /// <param name="inArguments">The StringBuilder to append the argument definition to.</param>
        /// <param name="inSetValues">The StringBuilder to append the argument set values to.</param>
        /// <param name="enumStateVar">True if the state variable is an enumeration type.</param>
        /// <param name="argFriendlyName">The code friendly argument name.</param>
        /// <param name="relatedStateVarFriendlyName">The related state variable code friendly name.</param>
        /// <param name="type">The data type for the argument.</param>
        /// <param name="comments">A StringBuilder to append the comments line for the parameter to.</param>
        private void GenerateInArgumentCode(
            ArgumentDescription argumentDesc,
            int inArgumentIndex, StringBuilder inArguments, StringBuilder inSetValues,
            bool enumStateVar, string argFriendlyName, string relatedStateVarFriendlyName,
            string type, StringBuilder comments)
        {
            // Generate argument comment line
            comments.Append(
                string.Format(
                    CodeGenProvider.ActionInParamComment,
                    argFriendlyName,
                    GenerateArgumentDescriptionComment(argumentDesc)
                )
            );

            // Generate argument definition
            inArguments.Append(
                string.Format(
                    CodeGenProvider.ActionInArgument,
                    type, argFriendlyName,
                    (inArguments.Length > 0 ? CodeGenProvider.ParameterSeperator : String.Empty)
                )
            );

            // Generate in set value code
            if (enumStateVar)
                inSetValues.Append(
                    string.Format(
                        CodeGenProvider.InSetValueEnum,
                        inArgumentIndex,
                        relatedStateVarFriendlyName,
                        argFriendlyName
                    )
                );
            else
                inSetValues.Append(
                    string.Format(
                        CodeGenProvider.InSetValue,
                        inArgumentIndex,
                        argFriendlyName
                    )
                );
        }

        /// <summary>
        /// Generates the code for an out argument which is on its own in the action.
        /// </summary>
        /// <param name="argumentDesc">The argument description.</param>
        /// <param name="outArgumentIndex">The out argument index (0).</param>
        /// <param name="outSetValues">A StringBuilder to append the out set value code to.</param>
        /// <param name="enumStateVar">True if the argument is an enumeration type.</param>
        /// <param name="relatedStateVarFriendlyName">The related state variable code friendly name.</param>
        /// <param name="type">The data type for the argument.</param>
        /// <param name="comments">A StringBuilder to append the comments line for the return parameter to.</param>
        private void GenerateOutArgumentReturnCode(
            ArgumentDescription argumentDesc,
            int outArgumentIndex, StringBuilder outSetValues, bool enumStateVar,
            string relatedStateVarFriendlyName, string type, StringBuilder comments)
        {
            // Generate argument comment line
            comments.Append(
                string.Format(
                    CodeGenProvider.ActionReturnsComment,
                    relatedStateVarFriendlyName,
                    GenerateArgumentDescriptionComment(argumentDesc)
                )
            );

            // Generate the return statement code
            if (enumStateVar)
                outSetValues.Append(string.Format(CodeGenProvider.OutReturnValueEnum, relatedStateVarFriendlyName, outArgumentIndex));
            else
                outSetValues.Append(string.Format(CodeGenProvider.OutReturnValue, type, outArgumentIndex));
        }

        /// <summary>
        /// Generates the code for an out argument which is NOT on its own in the action.
        /// </summary>
        /// <param name="argumentDesc">The argument description.</param>
        /// <param name="outArgumentIndex">The out argument index (0).</param>
        /// <param name="outArguments">A StringBuilder to append the out arguments to.</param>
        /// <param name="outSetValues">A StringBuilder to append the out set value code to.</param>
        /// <param name="enumStateVar">True if the argument is an enumeration type.</param>
        /// <param name="argFriendlyName">The code friendly name of the out argument.</param>
        /// <param name="relatedStateVarFriendlyName">The related state variable code friendly name.</param>
        /// <param name="type">The data type for the argument.</param>
        /// <param name="comments">A StringBuilder to append the comments line for the parameter to.</param>
        private void GenerateOutArgumentCode(
            ArgumentDescription argumentDesc,
            int outArgumentIndex, StringBuilder outArguments, StringBuilder outSetValues, bool enumStateVar,
            string argFriendlyName, string relatedStateVarFriendlyName, string type, StringBuilder comments)
        {
            // Generate argument comment line
            comments.Append(
                string.Format(
                    CodeGenProvider.ActionOutParamComment,
                    argFriendlyName,
                    GenerateArgumentDescriptionComment(argumentDesc)
                )
            );

            // Generate argument definition
            outArguments.Append(
                string.Format(
                    CodeGenProvider.ActionOutArgument,
                    type, argFriendlyName,
                    (outArguments.Length > 0 ? CodeGenProvider.ParameterSeperator : String.Empty)
                )
            );

            // Generate out set value code
            if (enumStateVar)
                outSetValues.Append(
                    string.Format(
                        CodeGenProvider.OutSetValueEnum,
                        argFriendlyName,
                        relatedStateVarFriendlyName,
                        outArgumentIndex
                    )
                );
            else
                outSetValues.Append(
                    string.Format(
                        CodeGenProvider.OutSetValue,
                        argFriendlyName,
                        type,
                        outArgumentIndex
                    )
                );
        }

        /// <summary>
        /// Generates the code for all state variables.
        /// </summary>
        /// <param name="service">The service containing the state variables.</param>
        /// <param name="testStateVars">
        /// True to test each state variable to ensure it is 
        /// usuable for accessing as property, false to include
        /// all state variables as properties.</param>
        /// <param name="consts">The string constants created.</param>
        /// <param name="stateVarProps">A StringBuilder to contain property definitions.</param>
        /// <param name="stateVarConversion">A StringBuilder to contain the conversion method definitions.</param>
        /// <param name="stateVarEnums">A StringBuilder to contain the enumerations for the state variables.</param>
        /// <param name="eventHandlers">A StringBuilder to contain event handler declarations for the state variable changes.</param>
        /// <param name="eventCallers">A StringBuilder to contain event caller methods for the state variable changes.</param>
        /// <param name="stateVarEventIntercept">A StringBuilder to contain case statement code for each event state variable.</param>
        /// <param name="enumStateVars">A HashSet containing the names of all enumerated state variables.</param>
        /// <param name="stateVarTypes">A Dictionary to contain each state variaible name and its code data type.</param>
        /// <param name="desc">The service description to create the state variable code from.</param>
        private void GenerateStateVarCode(
            Service service, bool testStateVars, StringConstants consts, StringBuilder stateVarProps, StringBuilder stateVarConversion,
            StringBuilder stateVarEnums, StringBuilder eventHandlers, StringBuilder eventCallers, StringBuilder stateVarEventIntercept,
            HashSet<string> enumStateVars, Dictionary<string, string> stateVarTypes,
            ServiceDescription desc)
        {
            // For each state variaible description
            foreach (StateVariableDescription lsdStateVarDesc in desc.StateVariables.Values)
            {
                string lsFriendlyName = CodeGenProvider.CodeFriendlyIdentifier(lsdStateVarDesc.Name, false);

                // Determine if we actually want the property accessor for the state variable
                bool lbAddProp = IsStateVarQueryable(service, testStateVars, lsdStateVarDesc);

                // If the state variable is a string and has an allowed value list
                if (lsdStateVarDesc.DataTypeValue == StateVariableDataType.tstring && lsdStateVarDesc.AllowedValues.Count > 0)
                    // Generate it using an enumeration
                    GenerateEnumStateVarCode(
                        consts, stateVarProps, stateVarConversion, stateVarEnums, 
                        eventHandlers, eventCallers, stateVarEventIntercept, enumStateVars,
                        stateVarTypes, lsdStateVarDesc, lsFriendlyName, lbAddProp);
                else
                    // Otherwise generate it using simple properties
                    GenerateStateVarCode(
                        consts, stateVarProps, eventHandlers, eventCallers, stateVarEventIntercept,
                        stateVarTypes, lsdStateVarDesc, lsFriendlyName, lbAddProp);
            }
        }

        private void GenerateStateVarEventCode(
            string type, string stateVarFriendlyName, string conversionCode, StringConstants consts, StringBuilder eventHandlers,
            StringBuilder eventCallers, StringBuilder stateVarEventIntercept,
            StateVariableDescription stateVarDesc)
        {
            string lsConstName = 
                consts[
                    CodeGenProvider.StateVarNameGroup, stateVarDesc.Name,
                    string.Format(CodeGenProvider.StateVarNameComment, stateVarDesc.Name)
                ];

            eventHandlers.Append(
                string.Format(
                    CodeGenProvider.StateVariableEventHandler,
                    type,
                    stateVarFriendlyName));

            eventCallers.Append(
                string.Format(
                    CodeGenProvider.StateVariableEventCaller,
                    type,
                    stateVarFriendlyName));

            stateVarEventIntercept.Append(
                string.Format(
                    CodeGenProvider.StateVarChangedEventHandlerCaseStatement,
                    lsConstName,
                    stateVarFriendlyName,
                    type,
                    conversionCode));
        }

        /// <summary>
        /// Generates the code for a NON enumerated state variable.
        /// </summary>
        /// <param name="consts">The string constants created.</param>
        /// <param name="stateVarProps">A StringBuilder to contain property definitions.</param>
        /// <param name="eventHandlers">A StringBuilder to contain event handler declarations for the state variable changes.</param>
        /// <param name="eventCallers">A StringBuilder to contain event caller methods for the state variable changes.</param>
        /// <param name="stateVarEventIntercept">A StringBUilder to contain the case statements for the state variable changed event.</param>
        /// <param name="stateVarTypes">A Dictionary to contain each state variaible name and its code data type.</param>
        /// <param name="stateVarDesc">The state variable description.</param>
        /// <param name="friendlyName">The code friendly name of the state variaible.</param>
        /// <param name="addProp">True to add the actual property accessor.</param>
        private void GenerateStateVarCode(
            StringConstants consts, StringBuilder stateVarProps,
            StringBuilder eventHandlers, StringBuilder eventCallers, StringBuilder stateVarEventIntercept,
            Dictionary<string, string> stateVarTypes, StateVariableDescription stateVarDesc, 
            string friendlyName, bool addProp)
        {
            // Get the data type
            string lsType = stateVarDesc.DataTypeValue.BaseType().Name;

            // Append the accessor for the state variaible
            if (addProp)
                stateVarProps.Append(
                    string.Format(
                        CodeGenProvider.NonEnumStateVar,
                        lsType, friendlyName, 
                        consts[
                            CodeGenProvider.StateVarNameGroup, stateVarDesc.Name,
                            string.Format(CodeGenProvider.StateVarNameComment, stateVarDesc.Name)
                        ],
                        GenerateStateVarComment(stateVarDesc)));

            // Add the type to the list for the actions
            stateVarTypes[stateVarDesc.Name] = lsType;

            // If the state variable is evented
            if (stateVarDesc.SendEvents)
                GenerateStateVarEventCode(
                    lsType, 
                    friendlyName,
                    String.Format(CodeGenProvider.NonEnumStateVarEventConversion, lsType),
                    consts,
                    eventHandlers, 
                    eventCallers, 
                    stateVarEventIntercept,
                    stateVarDesc);
        }

        /// <summary>
        /// Generates the code for an enumerated state variable.
        /// </summary>
        /// <param name="consts">The string constants created.</param>
        /// <param name="stateVarProps">A StringBuilder to contain property definitions.</param>
        /// <param name="stateVarConversion">A StringBuilder to contain the conversion method definitions.</param>
        /// <param name="stateVarEnums">A StringBuilder to contain the enumerations for the state variables.</param>
        /// <param name="eventHandlers">A StringBuilder to contain event handler declarations for the state variable changes.</param>
        /// <param name="eventCallers">A StringBuilder to contain event caller methods for the state variable changes.</param>
        /// <param name="stateVarEventIntercept">A StringBUilder to contain the case statements for the state variable changed event.</param>
        /// <param name="enumStateVars">A HashSet containing the names of all enumerated state variables.</param>
        /// <param name="stateVarTypes">A Dictionary to contain each state variaible name and its code data type.</param>
        /// <param name="stateVarDesc">The state variable description.</param>
        /// <param name="friendlyName">The code friendly name of the state variaible.</param>
        /// <param name="addProp">True to add the actual property accessor.</param>
        private void GenerateEnumStateVarCode(
            StringConstants consts, StringBuilder stateVarProps, StringBuilder stateVarConversion, StringBuilder stateVarEnums,
            StringBuilder eventHandlers, StringBuilder eventCallers, StringBuilder stateVarEventIntercept,
            HashSet<string> enumStateVars, Dictionary<string, string> stateVarTypes,
            StateVariableDescription stateVarDesc, string friendlyName, bool addProp)
        {
            StringBuilder lsbStateVarEnumValues = new StringBuilder();
            StringBuilder lsbEnumStateVarParse = new StringBuilder();
            StringBuilder lsbEnumStateVarToString = new StringBuilder();

            // Add the state var to the enum list
            enumStateVars.Add(stateVarDesc.Name);

            // Generate allowed values code
            GenerateEnumAllowedValuesCode(
                consts, stateVarDesc, friendlyName, lsbStateVarEnumValues,
                lsbEnumStateVarParse, lsbEnumStateVarToString);

            // Generate enumeration
            stateVarEnums.Append(string.Format(CodeGenProvider.StateVarEnum, friendlyName, lsbStateVarEnumValues));

            // Generate conversion functions
            stateVarConversion.Append(string.Format(CodeGenProvider.EnumStateVarConversion, friendlyName, lsbEnumStateVarParse, lsbEnumStateVarToString));

            // Generate property accessor if required
            if (addProp) 
                stateVarProps.Append(
                    string.Format(
                        CodeGenProvider.EnumStateVar, friendlyName, 
                        consts[
                            CodeGenProvider.StateVarNameGroup, stateVarDesc.Name,
                            string.Format(CodeGenProvider.StateVarNameComment, stateVarDesc.Name)
                        ],
                        GenerateStateVarComment(stateVarDesc)));

            string lsEnumTypeName = string.Format(CodeGenProvider.EnumStateVarName, friendlyName);

            // Add the type to the list for the actions 
            stateVarTypes[stateVarDesc.Name] = lsEnumTypeName;

            // If the state variable is evented
            if (stateVarDesc.SendEvents)
                GenerateStateVarEventCode(
                    lsEnumTypeName,
                    friendlyName,
                    String.Format(CodeGenProvider.EnumStateVarEventConversion, friendlyName),
                    consts,
                    eventHandlers,
                    eventCallers,
                    stateVarEventIntercept,
                    stateVarDesc);
        }

        /// <summary>
        /// Generates the case statements for the conversion functions 
        /// for an enumerated state variaible.
        /// </summary>
        /// <param name="consts">The string constants created.</param>
        /// <param name="stateVarDesc">The state variable description.</param>
        /// <param name="friendlyName">The code friendly name of the state variaible.</param>
        /// <param name="stateVarEnumValues">A StringBuilder to contain the list of enumeration values.</param>
        /// <param name="enumStateVarParse">A StringBuilder to contain the list of state variable parse case statements.</param>
        /// <param name="enumStateVarToString">A StringBuilder to contain the list of state variable to string case statements.</param>
        private void GenerateEnumAllowedValuesCode(
            StringConstants consts, StateVariableDescription stateVarDesc, string friendlyName,
            StringBuilder stateVarEnumValues, StringBuilder enumStateVarParse, StringBuilder enumStateVarToString)
        {
            // For each allowed value
            foreach (string lsAllowedValue in stateVarDesc.AllowedValues)
            {
                // Get the code friendly name
                string lsFriendlyValue = CodeGenProvider.CodeFriendlyIdentifier(lsAllowedValue, false);

                // Create the constant for the allowed value
                string lsAllowedValueConst = consts[
                    string.Format(CodeGenProvider.AllowedValueGroup, CodeGenProvider.CodeFriendlyIdentifier(stateVarDesc.Name, false)),
                    lsAllowedValue,
                    string.Format(CodeGenProvider.AllowedValueComment, lsAllowedValue, stateVarDesc.Name)
                ];

                // Generate the enumeration values
                stateVarEnumValues.Append(string.Format(CodeGenProvider.EnumStateVarValue, lsFriendlyValue, friendlyName));

                // Generate the parse case statement
                enumStateVarParse.Append(string.Format(CodeGenProvider.EnumParseCaseStatement, lsAllowedValueConst, friendlyName, lsFriendlyValue));

                // Generate the to string case statment
                enumStateVarToString.Append(string.Format(CodeGenProvider.EnumToStringCaseStatement, friendlyName, lsFriendlyValue, lsAllowedValueConst));
            }
        }

        /// <summary>
        /// Determines whether a state variable is queryable and therefore 
        /// should have its property accessor created.
        /// </summary>
        /// <param name="service">The service the state variable belongs to.</param>
        /// <param name="testStateVars">True to test for query ability, false to allow.</param>
        /// <param name="stateVarDesc">The state variaible description for which needs to be tested.</param>
        /// <returns>True if the state variable should have its accessor property created.</returns>
        private bool IsStateVarQueryable(Service service, bool testStateVars, StateVariableDescription stateVarDesc)
        {
            // Do we want to test for queryable ability
            if (testStateVars)
            {
                bool lbAddProp = false;

                try
                {
                    // Attempt to query the state variable
                    service.QueryStateVariable(stateVarDesc.Name);

                    // If succeeded then allow the property
                    lbAddProp = true;
                }
                catch (UPnPException loE)
                {
                    // If its variable value unknown then its probably a valid state variable
                    if (loE.Code == UPnPException.UPnPErrorCode.UPNP_E_VARIABLE_VALUE_UNKNOWN)
                        lbAddProp = true;
                }
                catch (Exception)
                {
                }

                // Return whether the property should be added
                return lbAddProp;
            }
            else
                // We want to add it irrespective
                return true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the default code generation class name for a service.
        /// </summary>
        /// <param name="service">The service to get the class name for.</param>
        /// <param name="codeGenProvider">The code generation provider to use.</param>
        /// <returns>A string.</returns>
        public static string DefaultCodeGenClassName(Service service, ICodeGenProvider codeGenProvider)
        {
            return codeGenProvider.CodeFriendlyIdentifier(service.FriendlyServiceTypeIdentifier, false);
        }

        /// <summary>
        /// Generates the class code for a service.
        /// </summary>
        /// <param name="service">The service to generate for.</param>
        /// <param name="className">The class name of the service or null to use the service type.</param>
        /// <param name="namespaceName">The namespace for the class.</param>
        /// <param name="classScope">The scope for the class.</param>
        /// <param name="partial">True to make the class partial, false otherwise.</param>
        /// <param name="testStateVars">
        /// True to test each state variable to ensure it is 
        /// usuable for accessing as property, false to include
        /// all state variables as properties.</param>
        /// <returns>The string representing the code for the class.</returns>
        public string GenerateClassFor(
            Service service, string className, string namespaceName, 
            ClassScope classScope, bool partial, bool testStateVars)
        {
            // If classname is not specified then default to service information
            if (className == null) 
                className = DefaultCodeGenClassName(service, CodeGenProvider); 
            else 
                // Otherwise ensure classname is Identifier compatible
                className = CodeGenProvider.CodeFriendlyIdentifier(className, false);

            StringBuilder lsbStateVarProps = new StringBuilder();
            StringBuilder lsbStateVarConversion = new StringBuilder();
            StringBuilder lsbStateVarEnums = new StringBuilder();
            StringBuilder lsbActionMethods = new StringBuilder();
            StringBuilder lsbEventHandlers = new StringBuilder();
            StringBuilder lsbEventCallers = new StringBuilder();
            StringBuilder lsbStateVarEventIntercept = new StringBuilder();
            HashSet<string> lhsEnumStateVars = new HashSet<string>();
            StringConstants lscConsts = new StringConstants(CodeGenProvider);
            Dictionary<string, string> ldStateVarTypes = new Dictionary<string,string>();

            // Get the service description
            ServiceDescription lsdDesc = service.Description();

            if (lsdDesc != null)
            {
                // Generate the state variable property declarations
                GenerateStateVarCode(
                    service, testStateVars, lscConsts, lsbStateVarProps,
                    lsbStateVarConversion, lsbStateVarEnums, lsbEventHandlers, lsbEventCallers,
                    lsbStateVarEventIntercept, lhsEnumStateVars, ldStateVarTypes, lsdDesc);

                // Generate the action methods
                GenerateActionCode(
                    lscConsts, lsbActionMethods, lhsEnumStateVars, 
                    ldStateVarTypes, lsdDesc);
            }

            if (lsbStateVarEventIntercept.Length > 0)
                lsbEventCallers.Append(
                    String.Format(CodeGenProvider.StateVarChangedEventHandler, lsbStateVarEventIntercept)
                );

            return
                String.Format(
                    CodeGenProvider.ServiceBase,
                    namespaceName,
                    className,
                    service.ServiceTypeIdentifier,
                    CodeGenProvider.GenerateRegion(CodeGenProvider.PublicEnumerations, lsbStateVarEnums.ToString()),
                    (partial ? CodeGenProvider.PartialClass : String.Empty),
                    CodeGenProvider.GenerateRegion(CodeGenProvider.ProtectedMethods, lsbStateVarConversion.ToString()),
                    lsbActionMethods,
                    CodeGenProvider.GenerateRegion(CodeGenProvider.PublicProperties, lsbStateVarProps.ToString()),
                    CodeGenProvider.GenerateRegion(CodeGenProvider.ProtectedConstants, lscConsts.Definitions().ToString(), false, false),
                    CodeGenProvider.GetClassScope(classScope, CodeGenProvider.Space.ToString()),
                    CodeGenProvider.GenerateRegion(CodeGenProvider.EventHandlers, lsbEventHandlers.ToString(), true),
                    CodeGenProvider.GenerateRegion(CodeGenProvider.EventCallers, lsbEventCallers.ToString()),
                    string.Format(
                        CodeGenProvider.ServiceClassHeaderComment,
                        (service.Device != null && service.Device.RootDevice != null ? service.Device.RootDevice.FriendlyName : CodeGenProvider.Null),
                        (service.Device != null && service.Device.RootDevice != null ? service.Device.RootDevice.Type : CodeGenProvider.Null),
                        (service.Device != null && service.Device.RootDevice != null ? service.Device.RootDevice.SerialNumber : CodeGenProvider.Null),
                        (service.Device != null ? service.Device.FriendlyName : CodeGenProvider.Null),
                        (service.Device != null ? service.Device.Type : CodeGenProvider.Null),
                        service.Id, service.ServiceTypeIdentifier, 
                        DateTime.Now.ToString(),
                        className,
                        namespaceName,
                        classScope.ToString(),
                        (partial ? CodeGenProvider.PartialClass : string.Empty),
                        testStateVars,
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