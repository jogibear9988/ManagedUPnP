//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Windows.Forms;
using ManagedUPnP;
using ManagedUPnP.Descriptions;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates the info control for a UPnP action.
    /// </summary>
    public partial class ctlUPnPActionInfo : ctlUPnPInfo
    {
        #region Protected Structures

        /// <summary>
        /// Encapsulates row data for a row in the input or output data grids.
        /// </summary>
        protected struct RowInfo
        {
            #region Locals

            /// <summary>
            /// The argument description for the input or output.
            /// </summary>
            private ArgumentDescription madArgDesc;

            /// <summary>
            /// The state variable description for the input or output.
            /// </summary>
            private StateVariableDescription msvStateVarDesc;

            /// <summary>
            /// The index of the input or output.
            /// </summary>
            private int miIndex;

            #endregion

            #region Initialisation

            /// <summary>
            /// Creates a new row info structure.
            /// </summary>
            /// <param name="argDesc">The argument description for the input or output.</param>
            /// <param name="stateVarDesc">The state variable description for the input or output.</param>
            /// <param name="index">The index of the input or output.</param>
            public RowInfo(ArgumentDescription argDesc, StateVariableDescription stateVarDesc, int index)
            {
                madArgDesc = argDesc;
                msvStateVarDesc = stateVarDesc;
                miIndex = index;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the argument description for the input or output.
            /// </summary>
            public ArgumentDescription ArgDesc
            {
                get
                {
                    return madArgDesc;
                }
            }

            /// <summary>
            /// Gets state variable description for the input or output.
            /// </summary>
            public StateVariableDescription StateVarDesc
            {
                get
                {
                    return msvStateVarDesc;
                }
            }

            /// <summary>
            /// Gets the index of the input or output.
            /// </summary>
            public int Index
            {
                get
                {
                    return miIndex;
                }
            }

            #endregion
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new action info control.
        /// </summary>
        public ctlUPnPActionInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adds an input or output value to the data grids.
        /// </summary>
        /// <param name="argDesc">The argument description for the argument.</param>
        /// <param name="svDesc">The state variable description for the linked state variable to the argument.</param>
        /// <param name="inputIndex">The current input index.</param>
        /// <param name="outputIndex">The current output index.</param>
        protected void AddIOValue(ArgumentDescription argDesc, StateVariableDescription svDesc, ref int inputIndex, ref int outputIndex)
        {
            StateVariableDataType ldtType;

            // Get the data type for the state variable description
            if (svDesc == null)
                ldtType = StateVariableDataType.tunknown;
            else
                ldtType = svDesc.DataTypeValue;

            switch (argDesc.DirectionValue)
            {
                case ArgumentDirection.In:
                    // If its an input then add it
                    int liIndex = dgInputs.Rows.Add(
                        argDesc.Name,
                        ldtType.Description(),
                        ldtType.StringFromValue(ldtType.Default()));

                    // Set its row info
                    dgInputs.Rows[liIndex].Tag = new RowInfo(argDesc, svDesc, inputIndex);

                    // Increment the input index
                    inputIndex++;

                    break;

                case ArgumentDirection.Out:
                    // If its an output then add it
                    liIndex = dgOutputs.Rows.Add(
                        argDesc.Name,
                        ldtType.Description(),
                        String.Empty);

                    // Set its row info
                    dgOutputs.Rows[liIndex].Tag = new RowInfo(argDesc, svDesc, outputIndex);

                    // Increment the output index
                    outputIndex++;

                    break;
            }
        }

        /// <summary>
        /// Converts an exception (including inner exceptions) into a readable string.
        /// </summary>
        /// <param name="e">The exception to convert.</param>
        /// <returns>A readable string for the exception.</returns>
        protected string ExceptionToString(Exception e)
        {
            StringBuilder lsbBuilder = new StringBuilder();

            while (e != null)
            {
                lsbBuilder.AppendLine(e.Message);
                e = e.InnerException;
            }

            return lsbBuilder.ToString();
        }

        /// <summary>
        /// Executes the action based on the inputs and outputs data grids.
        /// </summary>
        protected void Execute()
        {
            try
            {
                // Get setup values
                Object[] loParams = new Object[dgInputs.Rows.Count];
                UPnPActionTreeItem lavItem = (UPnPActionTreeItem)miItem;
                Service lsService = ((Service)(miItem.LinkedObject));
                ServiceDescription ldDesc = lsService.Description();

                // For each input row
                for (int liCounter = 0; liCounter < dgInputs.Rows.Count; liCounter++)
                {
                    // Get the row info
                    RowInfo liInfo = (RowInfo)(dgInputs.Rows[liCounter].Tag);

                    try
                    {
                        object loValue = dgInputs[clInputValue.Index, liCounter].Value;
                        string lsValue;
                        if (loValue == null) lsValue = string.Empty; else lsValue = loValue.ToString();
                        loValue = liInfo.StateVarDesc.DataTypeValue.ValueFromString(lsValue);
                        if (loValue == null) loValue = liInfo.StateVarDesc.DataTypeValue.Default();

                        // Set the value of the parameter
                        loParams[liInfo.Index] = loValue;
                    }
                    catch (Exception loE)
                    {
                        // Raise exception on conversion error
                        throw new Exception(
                            String.Format("Error converting input parameter '{0}'.", dgInputs[0, liCounter].Value.ToString()),
                            loE);
                    }
                }

                // Call the action and get the outputs
                Object[] loOut = lsService.InvokeAction(lavItem.ActionName, loParams);

                // Set the user to the outputs tab
                tcMain.SelectedTab = tpOutputs;

                // For each output parameter
                for (int liCounter = 0; liCounter < dgOutputs.Rows.Count; liCounter++)
                {
                    // Get the row info
                    RowInfo liInfo = (RowInfo)(dgOutputs.Rows[liCounter].Tag);

                    // Set the value in the output grid
                    dgOutputs[clOutputValue.Index, liCounter].Value = 
                        liInfo.StateVarDesc.DataTypeValue.StringFromValue(
                        loOut[liInfo.Index]);
                }


            }
            catch (Exception loE)
            {
                // Notify user of any errors
                MessageBox.Show(
                    ExceptionToString(loE),
                    "Error Occured executing action",
                    MessageBoxButtons.OK
                );
            }
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
                // Get setup vars
                UPnPActionTreeItem lavItem = (UPnPActionTreeItem)miItem;
                Service lsService = ((Service)(miItem.LinkedObject));
                ServiceDescription ldDesc = lsService.Description();

                // If description is available
                if (ldDesc != null)
                {
                    // Get the action description for this action
                    ActionDescription laDesc;
                    if (ldDesc.Actions.TryGetValue(lavItem.ActionName, out laDesc))
                        lsbBuilder.AppendLine(laDesc.ToString());

                    lsbBuilder.AppendLine();

                    // Lock the inputs
                    dgInputs.BeginUpdate();

                    try
                    {
                        // Lock the outputs
                        dgOutputs.BeginUpdate();

                        try
                        {
                            int liInputIndex = 0;
                            int liOutputIndex = 0;

                            // For each argument in the action
                            foreach (ArgumentDescription ladDesc in laDesc.Arguments.Values)
                            {
                                // Get the state variable if there is one linked
                                StateVariableDescription lsvDesc = ladDesc.RelatedStateVariableDescription;

                                // Add the input or output argument to the grids
                                AddIOValue(ladDesc, lsvDesc, ref liInputIndex, ref liOutputIndex);

                                // Append a line in the text information for the argument
                                lsbBuilder.AppendLine(
                                    String.Format(
                                    "{0} {1} => {2}",
                                    ladDesc.DirectionValue.ToString(),
                                    ladDesc.Name,
                                    (lsvDesc == null ? "No Related Var" : lsvDesc.ToString())));
                            }

                            // Auto size the input and output grids
                            dgInputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                            dgOutputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                        }
                        finally
                        {
                            // Unlock outputs
                            dgOutputs.EndUpdate();
                        }
                    }
                    finally
                    {
                        // Unlock inputs
                        dgInputs.EndUpdate();
                    }
                }
            }

            // Set the info text
            rtbInfo.Text = lsbBuilder.ToString();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Occurs when the user clicks the execute button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void cmdExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

        #endregion
    }
}
