using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ManagedUPnP.CodeGen;

namespace ManagedUPnPTest.UPnPBrowser
{
    /// <summary>
    /// Encapsulates a control which allows for the selection
    /// of all available CodeGenerationProviders as a combo box.
    /// </summary>
    public class ctlCodeGenProviderComboBox : ComboBox
    {
        #region Private Static Locals

        /// <summary>
        /// Stores the instances of all available Code Generation Providers.
        /// </summary>
        private static ICodeGenProvider[] mcgCodeGenProviders = GetCodeGenProviders();

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new CodeGenProvider Selection Combo box
        /// </summary>
        public ctlCodeGenProviderComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!IsDesignMode()) AddSelectionItems();
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Gets the available code generation providers from the calling assembly
        /// and the ManagedUPnP assembly.
        /// </summary>
        /// <returns>An array of CodeGenProviders.</returns>
        public static ICodeGenProvider[] GetCodeGenProviders()
        {
            ArrayList lalList = new ArrayList();

            HashSet<Type> lhsTypes = new HashSet<Type>();

            foreach (Type ltType in FindAssignableFrom<ICodeGenProvider>(typeof(ICodeGenProvider).Assembly))
                lhsTypes.Add(ltType);

            foreach (Type ltType in FindAssignableFrom<ICodeGenProvider>(Assembly.GetCallingAssembly()))
                lhsTypes.Add(ltType);

            foreach (Type ltType in lhsTypes)
                try { lalList.Add((ICodeGenProvider)Activator.CreateInstance(ltType)); }
                catch { }

            return (ICodeGenProvider[])lalList.ToArray(typeof(ICodeGenProvider));
        }

        /// <summary>
        /// Finds all types which are assignable from another.
        /// </summary>
        /// <typeparam name="TBaseType">The base type to get all assignable types from.</typeparam>
        /// <param name="assembly">The assembly to search.</param>
        /// <returns>A IEnumerable containing the types which are available.</returns>
        private static IEnumerable<Type> FindAssignableFrom<TBaseType>(Assembly assembly)
        {
            Type baseType = typeof(TBaseType);
            return assembly.GetTypes().Where(type => (type != baseType) && baseType.IsAssignableFrom(type));
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
                ICodeGenProvider lcgOldProvider = CodeGenProvider;

                this.Items.Clear();

                foreach (ICodeGenProvider lcgCodeGenProvider in mcgCodeGenProviders)
                    this.Items.Add(lcgCodeGenProvider);

                CodeGenProvider = lcgOldProvider;
            }
            finally
            {
                this.EndUpdate();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the selected code gen provider.
        /// </summary>
        public ICodeGenProvider CodeGenProvider
        {
            get
            {
                return (ICodeGenProvider)this.SelectedItem;
            }
            set
            {
                if (!DesignMode)
                {
                    if (value != null)
                        foreach (object loProvider in this.Items)
                            if (loProvider is ICodeGenProvider && loProvider.ToString() == value.ToString())
                            {
                                SelectedItem = loProvider;
                                return;
                            }

                    if (this.Items.Count > 0)
                        SelectedItem = this.Items[0];
                    else
                        SelectedItem = null;
                }
            }
        }

        #endregion
    }
}
