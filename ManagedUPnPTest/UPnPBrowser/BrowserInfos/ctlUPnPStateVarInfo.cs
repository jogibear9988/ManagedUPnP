//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using ManagedUPnP;
using ManagedUPnP.Descriptions;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates the info control for UPnP state variable.
    /// </summary>
    public partial class ctlUPnPStateVarInfo : ctlUPnPInfo
    {
        #region Initialisation

        /// <summary>
        /// Creats a new UPnP state variable info control.
        /// </summary>
        public ctlUPnPStateVarInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Updates the value of the state variable.
        /// </summary>
        protected void UpdateValue()
        {
            if (miItem != null)
            {
                UPnPStateVarTreeItem lsvItem = (UPnPStateVarTreeItem)miItem;
                Service lsService = ((Service)(miItem.LinkedObject));

                try
                {
                    tbValue.Text = lsService.QueryStateVariable(lsvItem.VarName).ToString();
                }
                catch (Exception loE)
                {
                    tbValue.Text = loE.Message;
                }
            }
            else
                tbValue.Text = "(NOT AVAILABLE)";
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Updates the data in the control from its item.
        /// </summary>
        protected override void UpdateData()
        {
            StringBuilder lsbBuilder = new StringBuilder();

            if (miItem != null)
            {
                UPnPStateVarTreeItem lsvItem = (UPnPStateVarTreeItem)miItem;
                Service lsService = ((Service)(miItem.LinkedObject));
                ServiceDescription ldDesc = lsService.Description();

                if (ldDesc != null)
                {
                    StateVariableDescription lsvDesc;
                    if (ldDesc.StateVariables.TryGetValue(lsvItem.VarName, out lsvDesc))
                        lsbBuilder.AppendLine(lsvDesc.ToString());
                }

                UpdateValue();
            }

            rtbInfo.Text = lsbBuilder.ToString();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles when the refresh button is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            UpdateValue();
        }

        #endregion
    }
}
