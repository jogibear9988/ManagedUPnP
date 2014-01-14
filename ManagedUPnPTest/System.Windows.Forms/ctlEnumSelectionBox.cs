//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.Reflection;

namespace System.Windows.Forms
{
    /// <summary>
    /// Encapsulates a combo box which allows the user to select from the fields in an enumeration
    /// </summary>
    /// <typeparam name="EnumType">The enumeration type to base the selection on</typeparam>
    public partial class ctlEnumSelectionBox<EnumType> : ComboBox where EnumType : struct
    {
        #region Classes

        /// <summary>
        /// Encapsulates a selection item within the combo box
        /// </summary>
        public class EnumSelectionItem
        {
            #region Locals

            /// <summary>
            /// The text for the selection item
            /// </summary>
            private string msText;

            /// <summary>
            /// The value for the selection item
            /// </summary>
            private EnumType mtValue;

            #endregion

            #region Initialisation

            /// <summary>
            /// Creates a new EnumSelectionItem
            /// </summary>
            /// <param name="text">The text for the selection item</param>
            /// <param name="value">The underlying value for the selection item</param>
            public EnumSelectionItem(string text, EnumType value)
            {
                if (!typeof(EnumType).IsEnum)
                    throw new ArgumentException("The generic type must be of type System.Enum");

                msText = text;
                mtValue = value;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the text for the selection item
            /// </summary>
            /// <returns>The text for the item</returns>
            public override string ToString()
            {
                return msText;
            }

            /// <summary>
            /// Gets the underlying value for the selection item
            /// </summary>
            public EnumType Value
            {
                get
                {
                    return mtValue;
                }
            }

            #endregion
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new ctlEnumSelectionBox
        /// </summary>
        public ctlEnumSelectionBox()
        {
            InitializeComponent();

            // If we are not in design mode then add the selection items
            if (!IsDesignMode()) AddSelectionItems();
        }

        #endregion

        #region Protected

        /// <summary>
        /// Returns true if the project is in design mode
        /// </summary>
        /// <returns>True if the project is in design mode</returns>
        protected bool IsDesignMode()
        {
            return
                System.ComponentModel.LicenseManager.UsageMode == 
                System.ComponentModel.LicenseUsageMode.Designtime;
        }

        /// <summary>
        /// Adds the selection items to the combo box
        /// </summary>
        protected void AddSelectionItems()
        {
            this.BeginUpdate();

            try
            {
                this.DropDownStyle = ComboBoxStyle.DropDownList;
                this.Items.Clear();

                Type ltDataType = Enum.GetUnderlyingType(typeof(EnumType));

                bool lbDefaultSet = false;
                EnumType ltDefault = default(EnumType);

                foreach (FieldInfo lfField in typeof(EnumType).GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
                {
                    string lsFriendlyText = lfField.Name;
                    bool lbIncludeInFriendly = true;

                    foreach (Attribute laAttrib in lfField.GetCustomAttributes(true))
                    {
                        if (laAttrib is EnumSelectionDefaultAttribute)
                        {
                            ltDefault = (EnumType)(lfField.GetValue(lfField));
                            lbDefaultSet = true;
                        }

                        if (laAttrib is EnumSelectionTextAttribute) 
                            lsFriendlyText = laAttrib.ToString();

                        if (laAttrib is EnumSelectionHideAttribute)
                            lbIncludeInFriendly = false;
                    }

                    if(lbIncludeInFriendly)
                        this.Items.Add(new EnumSelectionItem(lsFriendlyText, (EnumType)(lfField.GetValue(lfField))));
                }

                if (lbDefaultSet) Value = ltDefault;
            }
            finally
            {
                this.EndUpdate();
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Gets or sets the value of the combo box as a boxed value.
        /// </summary>
        /// <remarks>
        /// Setting to null will deselect any value, null is returned
        /// if no value is selected
        /// </remarks>
        public EnumType? BoxedValue
        {
            get
            {
                if (this.SelectedIndex != -1)
                    return Value;
                else
                    return null;
            }
            set
            {
                if (value != null && value.HasValue)
                    Value = value.Value;
                else
                    SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Gets or sets the value of the combo box
        /// </summary>
        /// <remarks>
        /// Setting to hidden value will deselect combo box, default
        /// value is returned if no value is selected
        /// </remarks>
        public EnumType Value
        {
            get
            {
                if (this.SelectedIndex != -1)
                    return ((EnumSelectionItem)(Items[SelectedIndex])).Value;
                else
                    return default(EnumType);
            }
            set
            {
                for(int liCounter = 0; liCounter < Items.Count; liCounter++)
                    if (((EnumSelectionItem)(Items[liCounter])).Value.Equals(value))
                    {
                        SelectedIndex = liCounter;
                        return;
                    }

                SelectedIndex = -1;
            }
        }

        #endregion
    }
}
