//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Components

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ManagedUPnP.Components
{
    internal static class IEnumerableDelegateExtensions
    {
        /// <summary>
        /// Invokes an event handlers invocation list by calling it on the GUI thread if available.
        /// </summary>
        /// <param name="invocationList">The invocation list for the event handler.</param>
        /// <param name="args">The arguments to pass to the event handlers delegates.</param>
        /// <example>
        ///     <code>
        ///         EventField.GetInvocationList().InvokeEventGUIThreadSafe(thia, eventArgs);
        ///     </code>
        /// </example>
        public static void InvokeEventGUIThreadSafe(this IEnumerable<Delegate> invocationList, params object[] args)
        {
            foreach (Delegate ldDelegate in invocationList)
            {
                ISynchronizeInvoke lsiTarget = ldDelegate.Target as ISynchronizeInvoke;

                if (lsiTarget != null && lsiTarget.InvokeRequired)
                    lsiTarget.Invoke(ldDelegate, args);
                else
                    ldDelegate.DynamicInvoke(args);
            }
        }

    }
}

#endif