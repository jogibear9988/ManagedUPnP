//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates the direction of an actions argument.
    /// </summary>
    public enum ArgumentDirection
    {
        /// <summary>
        /// Unknown direction.
        /// </summary>
        Unknown,

        /// <summary>
        /// Input only.
        /// </summary>
        In,

        /// <summary>
        /// Output only.
        /// </summary>
        Out
    }
}

#endif