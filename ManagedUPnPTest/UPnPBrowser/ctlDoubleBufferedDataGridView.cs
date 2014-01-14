//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ManagedUPnPTest
{
    /// <summary>
    /// Encapsulates a DataGridView which draws quicker due to double buffering
    /// </summary>
    public partial class ctlDoubleBufferedDataGridView : DataGridView
    {
        #region Locals

        /// <summary>
        /// Determines whether combo boxes only need a single click to be dropped down
        /// </summary>
        protected bool mbSingleClickComboBoxes = false;

        /// <summary>
        /// Determines if closing a combo box drop down causes an auto commit to occur
        /// </summary>
        protected bool mbAutoCommitComboBoxes = false;

        /// <summary>
        /// Stores the level of paint suspension.
        /// </summary>
        protected int miSuspendPaint = 0;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new buffered data grid view
        /// </summary>
        public ctlDoubleBufferedDataGridView()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether to use a single click combo box interface
        /// </summary>
        [DefaultValue(false)]
        public bool SingleClickComboBoxes
        {
            get
            {
                return mbSingleClickComboBoxes;
            }
            set
            {
                mbSingleClickComboBoxes = value;
            }
        }

        /// <summary>
        /// Gets or sets whether to auto commit combo boxes after selection
        /// </summary>
        [DefaultValue(false)]
        public bool AutoCommitComboBoxes
        {
            get
            {
                return mbAutoCommitComboBoxes;
            }
            set
            {
                mbAutoCommitComboBoxes = value;
            }
        }

        #endregion

        #region Paint Suspension

        /// <summary>
        /// Suppresses painting of the grid view.
        /// </summary>
        public void BeginUpdate()
        {
            miSuspendPaint++;
        }

        /// <summary>
        /// Resumes painting of the grid view.
        /// </summary>
        public void EndUpdate()
        {
            miSuspendPaint--;
            if (miSuspendPaint == 0) Invalidate();
        }

        /// <summary>
        /// Handles when painting is needed.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Only paint if painting isnt suspended
            if(miSuspendPaint <= 0) base.OnPaint(e);
        }

        #endregion

        #region Single Click Combo Boxes

        /// <summary>
        /// Ensures that only a single click is required to drop down a combo box
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
 	         base.OnCellMouseUp(e);

             if (mbSingleClickComboBoxes && e.ColumnIndex >= 0 && e.RowIndex >= 0)
             {
                 DataGridView ldbgDataGrid = this;
                 DataGridViewColumn lcColumn = ldbgDataGrid.Columns[e.ColumnIndex];

                 if (lcColumn is DataGridViewComboBoxColumn && ldbgDataGrid != null)
                 {
                     DataGridViewComboBoxCell lcCell = (DataGridViewComboBoxCell)ldbgDataGrid[e.ColumnIndex, e.RowIndex];

                     if (lcCell != null)
                     {
                         ldbgDataGrid.CurrentCell = lcCell;

                         ldbgDataGrid.BeginEdit(true);
                         DataGridViewComboBoxEditingControl lcEditingControl = (DataGridViewComboBoxEditingControl)ldbgDataGrid.EditingControl;
                         if (lcEditingControl != null) lcEditingControl.DroppedDown = true;
                     }

                 }
             }
        }

        #endregion

        #region Auto Commit Combo Boxes

        /// <summary>
        /// Adds events to the editing control
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            // If we are dealing with a combo box
            if (mbAutoCommitComboBoxes && e.Control is DataGridViewComboBoxEditingControl)
            {
                // Get the control
                DataGridViewComboBoxEditingControl lcControl = (DataGridViewComboBoxEditingControl)e.Control;

                // Add the drop down closed event
                lcControl.DropDownClosed += new EventHandler(lcComboBoxControl_DropDownClosed);

                // Add the visible changed event
                lcControl.VisibleChanged += new EventHandler(lcComboBoxControl_VisibleChanged);

                // Add the selected index changed event
                lcControl.SelectedIndexChanged += new EventHandler(lcComboBoxControl_SelectedIndexChanged);
            }

            base.OnEditingControlShowing(e);
        }

        /// <summary>
        /// Handles when a new index is selected in a combo box control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void lcComboBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Fires an immediate timed commit event because the user selected an item
            if (tmrCommitEdit.Enabled) TimedCommitEdit(false);
        }

        /// <summary>
        /// Handles when a combo box controls visibility is changed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void lcComboBoxControl_VisibleChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl lcControl = ((DataGridViewComboBoxEditingControl)sender);

            if (!lcControl.Visible)
            {
                // Remove all attached events
                lcControl.DropDownClosed -= new EventHandler(lcComboBoxControl_DropDownClosed);
                lcControl.VisibleChanged -= new EventHandler(lcComboBoxControl_VisibleChanged);
                lcControl.SelectedIndexChanged -= new EventHandler(lcComboBoxControl_SelectedIndexChanged);
            }
        }

        /// <summary>
        /// Occurs when a combo box drop down closed event fires
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void lcComboBoxControl_DropDownClosed(object sender, EventArgs e)
        {
            // Fire a timed commit event waiting for the index to change
            TimedCommitEdit(true);
        }

        /// <summary>
        /// Fires a timed commit edit event
        /// </summary>
        /// <param name="changesCancelled">True if we are still waiting to see if the user cancelled</param>
        protected void TimedCommitEdit(bool changesCancelled)
        {
            if (changesCancelled)
                tmrCommitEdit.Interval = 100;
            else
                tmrCommitEdit.Interval = 1;

            tmrCommitEdit.Enabled = false;
            tmrCommitEdit.Enabled = true;
        }

        /// <summary>
        /// Handles the commit edit timer event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void tmrCommitEdit_Tick(object sender, EventArgs e)
        {
            // Cancel the timer
            tmrCommitEdit.Enabled = false;

            // End the edit
            this.EndEdit();
        }

        #endregion
    }
}
