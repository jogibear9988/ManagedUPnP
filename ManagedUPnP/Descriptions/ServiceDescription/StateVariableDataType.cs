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
    /// Encapsulates an state variable data type.
    /// </summary>
    public enum StateVariableDataType
    {
        /// <summary>
        /// Unknown type.
        /// </summary>
        [StateVariableDataTypeInfo("Unknown type", "{unknown}", typeof(String))]
        tunknown,

        /// <summary>
        /// URI type.
        /// </summary>
        [StateVariableDataTypeInfo("Uniform Resource Identifier", "uri", typeof(String), false)]
        turi,

        /// <summary>
        /// Boolean type.
        /// </summary>
        [StateVariableDataTypeInfo("Boolean type", "boolean", typeof(Boolean))]
        tboolean,

        /// <summary>
        /// Unsigned 1-byte integer.
        /// </summary>
        [StateVariableDataTypeInfo("Unsigned 1-byte integer", "ui1", typeof(Byte))]
        tui1,

        /// <summary>
        /// Unsigned 2-byte integer.
        /// </summary>
        [StateVariableDataTypeInfo("Unsigned 2-byte integer", "ui2", typeof(UInt16))]
        tui2,

        /// <summary>
        /// Unsigned 4-byte integer.
        /// </summary>
        [StateVariableDataTypeInfo("Unsigned 4-byte integer", "ui4", typeof(UInt32))]
        tui4,

        /// <summary>
        /// Signed 1-byte integer. 
        /// </summary>
        [StateVariableDataTypeInfo("Signed 1-byte integer", "i1", typeof(SByte))]
        ti1,

        /// <summary>
        /// Signed 2-byte integer. 
        /// </summary>
        [StateVariableDataTypeInfo("Signed 2-byte integer", "i2", typeof(Int16))]
        ti2,

        /// <summary>
        /// Signed 4-byte integer, same format as int. 
        /// </summary>
        [StateVariableDataTypeInfo("Signed 4-byte integer", "i4", typeof(Int32))]
        ti4,
        
        /// <summary>
        /// 4-byte ﬁxed point integer, between –2147483648 and 2147483647, may have leading zeros, no commas.
        /// </summary>
        [StateVariableDataTypeInfo("4-byte ﬁxed point integer", "int", typeof(Int32))]
        tint,
        
        /// <summary>
        /// 4-byte ﬂoating point, same format as ﬂoat.
        /// </summary>
        [StateVariableDataTypeInfo("4-byte ﬂoating point", "r4", typeof(Single))]
        tr4,

        /// <summary>
        /// 8-byte ﬂoating point, same format as ﬂoat number Same as r8.
        /// </summary>
        [StateVariableDataTypeInfo("8-byte ﬂoating point", "r8", typeof(Double))]
        tr8,

        /// <summary>
        /// Same as r8, but a maximum of 14 digits to the left of the decimal, and a maximum of 4 to the right.
        /// </summary>
        [StateVariableDataTypeInfo("Fixed 14.4 places", "fixed.14.4", typeof(Double))]
        tﬁxed_14_4,

        /// <summary>
        /// Floating-point number. Mantissa and/or exponent may have leading sign or leading zeros.
        /// </summary>
        [StateVariableDataTypeInfo("Floating point number", "float", typeof(Double))]
        tﬂoat,
        
        /// <summary>
        /// Unicode string, one character in length.
        /// </summary>
        [StateVariableDataTypeInfo("Single unicode character", "char", typeof(Char))]
        tchar,

        /// <summary>
        ///  Unicode string, unlimited length.
        /// </summary>
        [StateVariableDataTypeInfo("Unicode string", "string", typeof(String))]
        tstring,

        /// <summary>
        /// Month, day, year conforming to ISO 8601, without time data.
        /// </summary>
        [StateVariableDataTypeInfo("Date", "date", typeof(DateTime))]
        tdate,

        /// <summary>
        /// ISO8601 date with time, but no time zone.
        /// </summary>
        [StateVariableDataTypeInfo("Date and Time", "dateTime", typeof(DateTime))]
        tdateTime,

        /// <summary>
        /// ISO8601 date with optional time and time zone.
        /// </summary>
        [StateVariableDataTypeInfo("Date and Time with time zone", "dateTime.tz", typeof(DateTime))]
        tdateTime_tz,
        
        /// <summary>
        /// Time as speciﬁed in ISO8601 with no date and time zone.
        /// </summary>
        [StateVariableDataTypeInfo("Time", "time", typeof(DateTime))]
        ttime,

        /// <summary>
        /// Time as speciﬁed in ISO8601 with optional time zone, but no date.
        /// </summary>
        [StateVariableDataTypeInfo("Time with timezone", "time.tz", typeof(DateTime))]
        ttime_tz,

        /// <summary>
        /// Binary base64
        /// </summary>
        [StateVariableDataTypeInfo("Binary (Base64)", "bin.base64", typeof(Byte[]))]
        tbin_base64,

        /// <summary>
        /// Number
        /// </summary>
        [StateVariableDataTypeInfo("Number (8-byte float)", "number", typeof(Double))]
        tnumber,

        /// <summary>
        /// UUID
        /// </summary>
        [StateVariableDataTypeInfo("UUID", "uuid", typeof(String), false)]
        tuuid,

        /// <summary>
        /// Hex
        /// </summary>
        [StateVariableDataTypeInfo("Hex", "bin.hex", typeof(string))]
        tbin_hex
    }
}

#endif