//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System.Collections.Generic;
using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a list of descriptions.
    /// </summary>
    /// <typeparam name="T">The type for the items in the list.</typeparam>
    public abstract class DescriptionList<T> : Description, IEnumerable<T>
    {
        #region Protected Locals

        /// <summary>
        /// The list of descriptions.
        /// </summary>
        protected List<T> mlList = new List<T>();

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new description list.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        public DescriptionList(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new description list from an XML reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader.</param>
        public DescriptionList(Description parent, XmlTextReader reader)
            : this(parent)
        {
            AddItemsFrom(reader);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds items from an XML reader.
        /// </summary>
        /// <param name="reader">The XML reader.</param>
        public void AddItemsFrom(XmlTextReader reader)
        {
            ProcessReader(reader);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return mlList.GetEnumerator();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of items in the description.
        /// </summary>
        public int Count
        {
            get
            {
                return mlList.Count;
            }
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The index for the item.</param>
        /// <returns>The item of type T.</returns>
        public T this[int index]
        {
            get
            {
                return mlList[index];
            }
        }

        #endregion

        #region System.Collections.IEnumerable Implementation

        /// <summary>
        /// Gets the enumerator for the list.
        /// </summary>
        /// <returns>An enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return mlList.GetEnumerator();
        }

        #endregion
    }
}

#endif