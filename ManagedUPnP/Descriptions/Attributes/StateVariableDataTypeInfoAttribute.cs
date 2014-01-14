//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Reflection;
using System.Text;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Provides information for a data type field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class StateVariableDataTypeInfoAttribute : Attribute
    {
        #region Protected Locals

        /// <summary>
        /// The description for the data type.
        /// </summary>
        protected string msDescription;

        /// <summary>
        /// The XML UPnP name for the data type.
        /// </summary>
        protected string msName;

        /// <summary>
        /// The managed base type for the data type.
        /// </summary>
        protected Type mtBaseType;

        /// <summary>
        /// True if the value can be null.
        /// </summary>
        protected bool mbEmptyAllowed = false;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new data type info attribute.
        /// </summary>
        /// <param name="description">The description for the data type.</param>
        /// <param name="name">The name for the data type.</param>
        /// <param name="baseType">The base managed type for the data type.</param>
        /// <param name="emptyAllowed">True if value can be empty (ie, empty string for string types).</param>
        public StateVariableDataTypeInfoAttribute(string description, string name, Type baseType, bool emptyAllowed = true)
        {
            msDescription = description;
            msName = name;
            mtBaseType = baseType;
            mbEmptyAllowed = emptyAllowed;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts a string of comma separated elements to an array value.
        /// </summary>
        /// <param name="values">The string of comma separated elements to convert.</param>
        /// <param name="arrayType">The array type to convert to.</param>
        /// <returns>A new object of type arrayType (empty array if no elements).</returns>
        private static Array ConvertStringToArray(string values, Type arrayType)
        {
            String[] lsValues = (values.Length == 0 ? new string[0] : values.Split(','));
            Type ltElementType = arrayType.GetElementType();
            Array laArray = Array.CreateInstance(ltElementType, lsValues.Length);

            int liIndex = 0;
            foreach (string lsValue in lsValues)
            {
                laArray.SetValue(USENGConverter.FromString(ltElementType, lsValue), liIndex);
                liIndex++;
            }

            return laArray;
        }

        /// <summary>
        /// Converts an array value to a string of comma separated elements.
        /// </summary>
        /// <param name="value">The array value to convert.</param>
        /// <returns>A comma separated list of elements.</returns>
        private static String ConvertArrayToString(object value)
        {
            Type ltType = value.GetType();

            if (!ltType.IsArray)
                throw new ArgumentException("must be an array type", "value");

            StringBuilder lsbBuilder = new StringBuilder();
            Type ltElementType = ltType.GetElementType();
            bool lbFirst = true;

            foreach (Object loElement in (Array)value)
            {
                if (!lbFirst)
                    lsbBuilder.Append(',');
                else
                    lbFirst = false;

                lsbBuilder.Append(USENGConverter.ToString(ltElementType, loElement));
            }

            return lsbBuilder.ToString();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts a string to a data value of this type.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The converted value or null on failure.</returns>
        public object ConvertFromString(string value)
        {
            try
            {
                if (BaseType.IsArray)
                    return ConvertStringToArray(value, BaseType);
                else
                    return USENGConverter.FromString(BaseType, value);
            }
            catch (Exception loE)
            {
                if (Logging.Enabled)
                    Logging.Log(
                        this,
                        String.Format(
                            "Converting value '{0}' from string to '{1}' failed with exception: '{2}'",
                            (value == null ? "(null)" : value),
                            BaseType.ToString(),
                            loE.ToString()));

                return null;
            }
        }

        /// <summary>
        /// Converts a data type value of this type to a string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The string representation of the value or null if value is null.</returns>
        public string ConvertToString(object value)
        {
            if (value == null)
                return null;
            else
                if (value != null && value.GetType().IsArray)
                    return ConvertArrayToString(value);
                else
                    return USENGConverter.ToString(BaseType, value);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description of the data type.
        /// </summary>
        public string Description
        {
            get
            {
                return msDescription;
            }
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        public string Name
        {
            get
            {
                return msName;
            }
        }

        /// <summary>
        /// Gets whether this data type can be empty (ie, empty string for string types).
        /// </summary>
        public bool EmptyAllowed
        {
            get
            {
                return mbEmptyAllowed;
            }
        }

        /// <summary>
        /// Gets the managed base type of the data type.
        /// </summary>
        public Type BaseType
        {
            get
            {
                return mtBaseType;
            }
        }

        /// <summary>
        /// Gets the default managed value for the data type.
        /// </summary>
        public object Default
        {
            get
            {
                // If it is an array then return an empty array of that type
                if (mtBaseType.IsArray)
                    return Array.CreateInstance(mtBaseType.GetElementType(), 0);

                try
                {
                    // Try the activator (value types)
                    return Activator.CreateInstance(mtBaseType);
                }
                catch
                {
                    // Try the parameterless constructor (non value types)
                    ConstructorInfo lciInfo = mtBaseType.GetConstructor(new Type[0]);

                    if (lciInfo != null)
                        return lciInfo.Invoke(null);
                    else
                    {
                        // Try the empty static field (string)
                        FieldInfo lfiInfo = mtBaseType.GetField("Empty", BindingFlags.Static | BindingFlags.Public);
                        return lfiInfo.GetValue(null);
                    }
                }
            }
        }

        #endregion
    }
}

#endif