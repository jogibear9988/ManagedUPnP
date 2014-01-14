//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates a converter to and from US ENG strings.
    /// </summary>
    internal static class USENGConverter
    {
        #region Private Static Locals

        /// <summary>
        /// The culture to use for conversion (US ENG).
        /// </summary>
        private static CultureInfo mcUSENGCulture = CultureInfo.CreateSpecificCulture("en-US");

        /// <summary>
        /// The dictionary of types and their converters.
        /// </summary>
        private static Dictionary<Type, TypeConverter> mdConverters = new Dictionary<Type, TypeConverter>();

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the converter for a managed type.
        /// </summary>
        /// <param name="dataType">The managed type to get the converter for.</param>
        /// <returns>The type converter for the type.</returns>
        public static TypeConverter GetConverter(Type dataType)
        {
            TypeConverter mtcConverter;
            if (!mdConverters.TryGetValue(dataType, out mtcConverter))
            {
                mtcConverter = TypeDescriptor.GetConverter(dataType);
                mdConverters[dataType] = mtcConverter;
            }

            return mtcConverter;
        }

        /// <summary>
        /// Converts an object to a US ENG string.
        /// </summary>
        /// <param name="dataType">The managed type for the object.</param>
        /// <param name="value">The value for the object to convert.</param>
        /// <returns>A string.</returns>
        public static string ToString(Type dataType, object value)
        {
            return GetConverter(dataType).ConvertToString(null, mcUSENGCulture, value);
        }

        /// <summary>
        /// Converts an US ENG string to an object.
        /// </summary>
        /// <param name="dataType">The managed type for the object.</param>
        /// <param name="value">The string value to convert.</param>
        /// <returns>An object of type Type.</returns>
        public static object FromString(Type dataType, string value)
        {
            return GetConverter(dataType).ConvertFromString(null, mcUSENGCulture, value);
        }

        #endregion
    }
}
