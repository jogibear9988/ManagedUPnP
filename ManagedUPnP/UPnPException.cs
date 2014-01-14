//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;

namespace ManagedUPnP
{
    /// <summary>
    /// Encpasulates an exception which occurs with a UPnP call.
    /// </summary>
    public class UPnPException : Exception
    {
        #region Public Enumerations

        /// <summary>
        /// The UPnP error codes.
        /// </summary>
        public enum UPnPErrorCode : int
        {
            /// <summary>
            /// Base for severity error.
            /// </summary>
            SEVERITY_ERROR = 1,

            /// <summary>
            /// Base value for underlying COM errors.
            /// </summary>
            FACILITY_ITF = 4,

            /// <summary>
            /// The base for all COM errors.
            /// </summary>
            COM_ERROR_BASE = (SEVERITY_ERROR << 31) | (FACILITY_ITF << 16),

            /// <summary>
            /// XML document does not have a root element at the top level of 
            /// the document.
            /// </summary>
            UPNP_E_ROOT_ELEMENT_EXPECTED = COM_ERROR_BASE | 0x0200,

            /// <summary>
            /// XML document does not have a device element. It is missing either 
            /// from the root element or the DeviceList element.
            /// </summary>
            UPNP_E_DEVICE_ELEMENT_EXPECTED = COM_ERROR_BASE | 0x0201,

            /// <summary>
            /// XML document does not have a service element. It is missing from the 
            /// ServiceList element, or the DeviceList element does not contain a 
            /// ServiceList element.
            /// </summary>
            UPNP_E_SERVICE_ELEMENT_EXPECTED = COM_ERROR_BASE | 0x0202,

            /// <summary>
            /// XML document is missing one of the required elements from the 
            /// Service element.
            /// </summary>
            UPNP_E_SERVICE_NODE_INCOMPLETE = COM_ERROR_BASE | 0x0203,

            /// <summary>
            /// XML document is missing one of the required elements from the 
            /// Device element.
            /// </summary>
            UPNP_E_DEVICE_NODE_INCOMPLETE = COM_ERROR_BASE | 0x0204,

            /// <summary>
            /// XML document does not have an icon element. It is missing from 
            /// the IconList element, or the DeviceList element does not 
            /// contain an IconList element.
            /// </summary>
            UPNP_E_ICON_ELEMENT_EXPECTED = COM_ERROR_BASE | 0x0205,

            /// <summary>
            /// XML document is missing one of the required elements from 
            /// the Icon element.
            /// </summary>
            UPNP_E_ICON_NODE_INCOMPLETE = COM_ERROR_BASE | 0x0206,

            /// <summary>
            /// Invalid action invoked.
            /// </summary>
            UPNP_E_INVALID_ACTION = COM_ERROR_BASE | 0x0207,

            /// <summary>
            /// Invalid arguments while invoking action.
            /// </summary>
            UPNP_E_INVALID_ARGUMENTS = COM_ERROR_BASE | 0x0208,

            /// <summary>
            /// UPnP Device is out of sync.
            /// </summary>
            UPNP_E_OUT_OF_SYNC = COM_ERROR_BASE | 0x0209,

            /// <summary>
            /// Action request failed.
            /// </summary>
            UPNP_E_ACTION_REQUEST_FAILED = COM_ERROR_BASE | 0x0210,

            /// <summary>
            /// Underlying network transport error.
            /// </summary>
            UPNP_E_TRANSPORT_ERROR = COM_ERROR_BASE | 0x0211,

            /// <summary>
            /// Could not get variable value.
            /// </summary>
            UPNP_E_VARIABLE_VALUE_UNKNOWN = COM_ERROR_BASE | 0x0212,

            /// <summary>
            /// State variable does not exist.
            /// </summary>
            UPNP_E_INVALID_VARIABLE = COM_ERROR_BASE | 0x0213,

            /// <summary>
            /// Device error.
            /// </summary>
            UPNP_E_DEVICE_ERROR = COM_ERROR_BASE | 0x0214,

            /// <summary>
            /// Protocol error.
            /// </summary>
            UPNP_E_PROTOCOL_ERROR = COM_ERROR_BASE | 0x0215,

            /// <summary>
            /// Response error.
            /// </summary>
            UPNP_E_ERROR_PROCESSING_RESPONSE = COM_ERROR_BASE | 0x0216,

            /// <summary>
            /// Device timeout.
            /// </summary>
            UPNP_E_DEVICE_TIMEOUT = COM_ERROR_BASE | 0x0217,

            /// <summary>
            /// Invalid document.
            /// </summary>
            UPNP_E_INVALID_DOCUMENT = COM_ERROR_BASE | 0x0500,

            /// <summary>
            /// Event subscription failed.
            /// </summary>
            UPNP_E_EVENT_SUBSCRIPTION_FAILED = COM_ERROR_BASE | 0x0501,

            /// <summary>
            /// Invalid action.
            /// </summary>
            FAULT_INVALID_ACTION = 401,

            /// <summary>
            /// Invalid argument.
            /// </summary>
            FAULT_INVALID_ARG = 402,

            /// <summary>
            /// Invalid sequence.
            /// </summary>
            FAULT_INVALID_SEQUENCE_NUMBER = 403,

            /// <summary>
            /// Invalid variable.
            /// </summary>
            FAULT_INVALID_VARIABLE = 404,

            /// <summary>
            /// Device internal error.
            /// </summary>
            FAULT_DEVICE_INTERNAL_ERROR = 501,

            /// <summary>
            /// Base value for action faults.
            /// </summary>
            FAULT_ACTION_SPECIFIC_BASE = 600,

            /// <summary>
            /// Max value for action faults.
            /// </summary>
            FAULT_ACTION_SPECIFIC_MAX = 899,

            /// <summary>
            /// Base value for specific action faults.
            /// </summary>
            UPNP_E_ACTION_SPECIFIC_BASE = COM_ERROR_BASE | 0x0300,

            /// <summary>
            /// Max value for specific action faults.
            /// </summary>
            UPNP_E_ACTION_SPECIFIC_MAX = (UPNP_E_ACTION_SPECIFIC_BASE + (FAULT_ACTION_SPECIFIC_MAX - FAULT_ACTION_SPECIFIC_BASE)),
        }

        #endregion

        #region Protected Locals

        /// <summary>
        /// The UPnPErrorCode for the error
        /// </summary>
        protected UPnPErrorCode mecCode;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new UPnP exception.
        /// </summary>
        /// <param name="code">The code for the error (COMException.ErrorCode).</param>
        /// <param name="innerException">The innder exception or null for none.</param>
        public UPnPException(int code, Exception innerException = null)
            : base(String.Format("UPnP Error #{0}: {1}", code, ((UPnPErrorCode)code).ToString()), innerException)
        {
        }

        /// <summary>
        /// Creates a new UPnPException.
        /// </summary>
        /// <param name="comException">The underlying COM Exception.</param>
        public UPnPException(COMException comException)
            : base(
                String.Format(
                    "UPnP Error #{0}: {1}", 
                    comException.ErrorCode, 
                    ((UPnPErrorCode)(comException.ErrorCode)).ToString()), 
                comException)
        {
            mecCode = (UPnPErrorCode)(comException.ErrorCode);
        }

        #endregion

        #region Internal Initialisation

        /// <summary>
        /// Creates a new UPnPException.
        /// </summary>
        /// <param name="code">The code for the error (COMException.ErrorCode).</param>
        /// <param name="innerException">The innder exception or null for none.</param>
        internal UPnPException(UPnPErrorCode code, Exception innerException = null)
            : base(String.Format("UPnP Error #{0}: {1}", (int)code, code.ToString()), innerException)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the underlying UPnPErrorCode
        /// </summary>
        public UPnPErrorCode Code
        {
            get
            {
                return mecCode;
            }
        }

        #endregion
    }
}
