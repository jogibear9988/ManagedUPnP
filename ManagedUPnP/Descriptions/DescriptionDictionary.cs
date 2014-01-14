//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a description dictionary using a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public abstract class DescriptionDictionary<TKey, TValue> : Description, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        #region Protected Locals

        /// <summary>
        /// The dictionary for the keys and descriptions.
        /// </summary>
        protected Dictionary<TKey, TValue> mdDictionary = new Dictionary<TKey, TValue>();

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new description dictionary.
        /// </summary>
        public DescriptionDictionary(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new description dictionary from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader.</param>
        public DescriptionDictionary(Description parent, XmlTextReader reader)
            : this(parent)
        {
            AddItemsFrom(reader);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the items from an XML reader.
        /// </summary>
        /// <param name="reader">The XML reader to read the items from.</param>
        public void AddItemsFrom(XmlTextReader reader)
        {
            ProcessReader(reader);
        }

        /// <summary>
        /// Returns true if the disctionary contains a key.
        /// </summary>
        /// <param name="key">The key to determine existence of.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return mdDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type of the
        /// value parameter. This parameter is passed uninitialized.
        /// </param>
        /// <returns>True if the value was set.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return mdDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns true if a key value pair exists.
        /// </summary>
        /// <param name="item">The item to test for existence.</param>
        /// <returns>True if the key value pair exists.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return mdDictionary.Contains(item);
        }

        /// <summary>
        /// Gets the key value pair enumerator.
        /// </summary>
        /// <returns>The enumerator for the key value pair.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return mdDictionary.GetEnumerator();
        }

        /// <summary>
        /// Gets the key value pair enumerator.
        /// </summary>
        /// <returns>The enumerator for the key value pair.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return mdDictionary.GetEnumerator();
        }

        /// <summary>
        /// Copies the key value pairs to an array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The array index to start copying to.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)mdDictionary).CopyTo(array, arrayIndex);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of items in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return mdDictionary.Count;
            }
        }

        /// <summary>
        /// Gets the keys in the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return mdDictionary.Keys;
            }
        }

        /// <summary>
        /// Gets the values in the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return mdDictionary.Values;
            }
        }

        /// <summary>
        /// Gets the value for a dictionary key.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <returns>The value for the key.</returns>
        public TValue this[TKey key]
        {
            get
            {
                return mdDictionary[key];
            }
        }

        /// <summary>
        /// Gets the value for an index.
        /// </summary>
        /// <param name="index">The index to get the value for.</param>
        /// <returns>The value for the index.</returns>
        public TValue this[int index]
        {
            get
            {
                return mdDictionary.ElementAt(index).Value;
            }
        }

        #endregion
    }
}

#endif