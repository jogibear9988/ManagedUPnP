//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using ManagedUPnP;
using ManagedUPnP.Descriptions;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a tree item which represnets a state variable.
    /// </summary>
    public class UPnPStateVarTreeItem : UPnPServiceBasedTreeItem
    {
        #region Locals

        /// <summary>
        /// The state variable description for this tree item.
        /// </summary>
        protected StateVariableDescription mdDesc;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new state variable tree item.
        /// </summary>
        /// <param name="service">The service for which this state variable belongs.</param>
        /// <param name="desc">The state variable description.</param>
        public UPnPStateVarTreeItem(Service service, StateVariableDescription desc)
            : base(service)
        {
            mdDesc = desc;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the item text for the tree list node.
        /// </summary>
        public override string ItemText
        {
            get
            {
                return mdDesc.Name;
            }
        }

        /// <summary>
        /// Gets a newly created information control for the item.
        /// </summary>
        public override ctlUPnPInfo InfoControl
        {
            get
            {
                ctlUPnPStateVarInfo lsviInfo = new ctlUPnPStateVarInfo();
                lsviInfo.Item = this;
                return lsviInfo;
            }
        }

        /// <summary>
        /// Gets the key for teh tree list node.
        /// </summary>
        public override string ItemKey
        {
            get
            {
                return String.Format(
                    "STATEVAR:{0}.{1}.{2}",
                    msService.Device.UniqueDeviceName,
                    msService.ServiceTypeIdentifier,
                    mdDesc.Name);
            }
        }

        /// <summary>
        /// Gets the custom icon for the node.
        /// </summary>
        public override Image ItemIcon
        {
            get 
            { 
                return null; 
            }
        }

        /// <summary>
        /// Gets the state variables name.
        /// </summary>
        public string VarName
        {
            get
            {
                return mdDesc.Name;
            }
        }

        #endregion
    }
}
