//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Reflection;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Provides extension methods for the data enumeration.
    /// </summary>
    public static class StateVariableDataTypeExtensions
    {
        /// <summary>
        /// Gets the data type info attribute for a data type.
        /// </summary>
        /// <param name="dataType">The data type to get the attribute for.</param>
        /// <returns>The attribute or null if not available.</returns>
        public static StateVariableDataTypeInfoAttribute Info(this StateVariableDataType dataType)
        {
            Type ltType = typeof(StateVariableDataType);
            FieldInfo lfiInfo = ltType.GetField(dataType.ToString());
            object[] loAtts = lfiInfo.GetCustomAttributes(typeof(StateVariableDataTypeInfoAttribute), true);

            if (loAtts.Length == 1)
                return (StateVariableDataTypeInfoAttribute)loAtts[0];
            else
                return null;
        }

        /// <summary>
        /// Gets the data type for a UPnP XML type name.
        /// </summary>
        /// <param name="dataType">The data type (any, not used).</param>
        /// <param name="name">The name of the data type.</param>
        /// <returns>The data type matching the name of DataType.tunknown if not recognised.</returns>
        public static StateVariableDataType ForTypeName(this StateVariableDataType dataType, string name)
        {
            StateVariableDataType[] ldtTypes = (StateVariableDataType[])Enum.GetValues(typeof(StateVariableDataType));

            foreach (StateVariableDataType ldtType in ldtTypes)
            {
                StateVariableDataTypeInfoAttribute laAttr = ldtType.Info();

                if (laAttr != null && laAttr.Name == name)
                    return ldtType;
            }

            return StateVariableDataType.tunknown;
        }

        /// <summary>
        /// Gets the UPnP XML name for a data type.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns>The UPnP XML name.</returns>
        public static string XMLName(this StateVariableDataType dataType)
        {
            StateVariableDataTypeInfoAttribute laAtt = Info(dataType);

            if (laAtt != null)
                return laAtt.Name;
            else
                return null;
        }

        /// <summary>
        /// Gets the description for a data type.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns>The data type description or null if invalid.</returns>
        public static string Description(this StateVariableDataType dataType)
        {
            StateVariableDataTypeInfoAttribute laAtt = Info(dataType);

            if (laAtt != null)
                return laAtt.Description;
            else
                return null;
        }

        /// <summary>
        /// Gets the managed base type for a data type.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns>The managed base type or null if invalid.</returns>
        public static Type BaseType(this StateVariableDataType dataType)
        {
            StateVariableDataTypeInfoAttribute laAtt = Info(dataType);

            if (laAtt != null)
                return laAtt.BaseType;
            else
                return null;
        }

        /// <summary>
        /// Gets the default value for a data type.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns>The default value or null if invalid.</returns>
        public static object Default(this StateVariableDataType dataType)
        {
            StateVariableDataTypeInfoAttribute laAtt = Info(dataType);

            if (laAtt != null)
                return laAtt.Default;
            else
                return null;
        }

        /// <summary>
        /// Converts a data type value to a string.
        /// </summary>
        /// <param name="dataType">The data type to convert from.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The string representation of the value or null if invalid.</returns>
        public static string StringFromValue(this StateVariableDataType dataType, object value)
        {
            StateVariableDataTypeInfoAttribute laAtt = Info(dataType);

            if (laAtt != null)
                return laAtt.ConvertToString(value);
            else
                return null;
        }

        /// <summary>
        /// Converts a string to a data type value.
        /// </summary>
        /// <param name="dataType">The data type to convert to.</param>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The object value from the string or null if invalid.</returns>
        public static object ValueFromString(this StateVariableDataType dataType, string value)
        {
            StateVariableDataTypeInfoAttribute laAtt = Info(dataType);

            if (laAtt != null)
                return laAtt.ConvertFromString(value);
            else
                return null;
        }
    }
}

#endif