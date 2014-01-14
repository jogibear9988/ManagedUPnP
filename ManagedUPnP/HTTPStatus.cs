//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

namespace ManagedUPnP
{
    /// <summary>
    /// Holds all http status values.
    /// </summary>
    public enum HTTPStatus
    {
        /// <summary>
        /// Continue.
        /// </summary>
        Continue = 100,

        /// <summary>
        /// Switching protocols.
        /// </summary>
        SwitchingProtocols = 101,

        /// <summary>
        /// OK.
        /// </summary>
        OK = 200,

        /// <summary>
        /// Created.
        /// </summary>
        Created = 201,

        /// <summary>
        /// Accepted.
        /// </summary>
        Accepted = 202,

        /// <summary>
        /// Non-authoritative information.
        /// </summary>
        NonAuthoritativeInfo = 203,

        /// <summary>
        /// No content.
        /// </summary>
        NoContent = 204,

        /// <summary>
        /// Reset content.
        /// </summary>
        ResetContent = 205,

        /// <summary>
        /// Partial content.
        /// </summary>
        PartialContent = 206,
        
        /// <summary>
        /// Object moved.
        /// </summary>
        ObjectMoved = 302,
        
        /// <summary>
        /// Not modified. 
        /// </summary>
        NotModified = 304,
        
        /// <summary>
        /// Temporary redirect.
        /// </summary>
        TemporaryRedirect = 307,
        
        /// <summary>
        /// Bad request.
        /// </summary>
        BadRequest = 400,
        
        /// <summary>
        /// Access denied.
        /// </summary>
        AccessDenied = 401,

        /// <summary>
        /// Invalid arguments.
        /// </summary>
        InvalidArgs = 402,

        /// <summary>
        /// Forbidden.
        /// </summary>
        Forbidden = 403,
        
        /// <summary>
        /// File or directory not found.
        /// </summary>
        FileNotFound = 404,
        
        /// <summary>
        /// Method not allowed.
        /// </summary>
        MethodNotAllowed = 405,
        
        /// <summary>
        /// Client browser does not accept the MIME type of the requested page.
        /// </summary>
        InvalidMimeType = 406,
        
        /// <summary>
        /// Proxy authentication required.
        /// </summary>
        ProxyAuthenticationRequired = 407,
        
        /// <summary>
        /// Precondition failed.
        /// </summary>
        PreconditionFailed = 412,
        
        /// <summary>
        /// Request entity too large.
        /// </summary>
        RequestTooLarge = 413,
        
        /// <summary>
        /// Request-URL too long.
        /// </summary>
        RequestURLTooLong = 414,
        
        /// <summary>
        /// Unsupported media type.
        /// </summary>
        UnsupportedMediaType = 415,
        
        /// <summary>
        /// Requested range not satisfiable.
        /// </summary>
        RangeNotSatisfiable = 416,
        
        /// <summary>
        /// Execution failed.
        /// </summary>
        ExecutionFailed = 417,
        
        /// <summary>
        /// Locked error.
        /// </summary>
        Locked = 423,
        
        /// <summary>
        /// Internal server error.
        /// </summary>
        InternalError = 500,
        
        /// <summary>
        /// An action failed on the service.
        /// </summary>
        ActionFailed = 501,
        
        /// <summary>
        /// Bad Gateway.
        /// </summary>
        BadGateway = 502,
        
        /// <summary>
        /// Service unavailable.
        /// </summary>
        ServiceUnavailable = 503,
        
        /// <summary>
        /// Gateway timeout.
        /// </summary>
        GatewayTimeout = 504,
        
        /// <summary>
        /// HTTP version not supported.
        /// </summary>
        HTTPVerNotSupported = 505
    }
}
