//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Collections;
using System.Collections.Generic;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a Dictionary which is guaranteed to preserve its order.
    /// </summary>
    /// <remarks>
    /// According to the Microsoft doco, the Dictionary class will not necessarily
    /// preserve its order, this is because it stores its data in order of the hash
    /// key, not in the order added. However, I have tested this behaviour with
    /// Dictionaries of int, int. Using all different values in different orders, and
    /// the order always seems preserved. Either Microsoft is wrong, or the GetHashCode
    /// functions for int are really bad. Either way, this class is designed to keep
    /// the dictionary items in the same order they were added, which is necessary for
    /// the ArgumentsDescription dictionary.
    /// 
    /// This class takes up more memory, as it has to store a Dictionary and a List of
    /// keys, but it should be reasonably close in performance.
    /// </remarks>
    /// <typeparam name="TKey">The type for the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type for the values in the dictionary.</typeparam>
    public class OrderedIndexedDictionary<TKey, TValue> : IList<TKey>, IDictionary<TKey, TValue>
    {
        #region Protected Classes

        /// <summary>
        /// Encapsulates the ordered enumerator.
        /// </summary>
        protected class OrderedIndexedDictionaryEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            #region Protected Locals

            /// <summary>
            /// The dictionary for which we want to enumerate.
            /// </summary>
            protected OrderedIndexedDictionary<TKey, TValue> mdDictionary;

            /// <summary>
            /// The current index within the dictionary.
            /// </summary>
            protected int miIndex = 0;

            #endregion

            #region Public Initialisation

            /// <summary>
            /// Creates a new enumerator.
            /// </summary>
            /// <param name="dictionary">The dictionary to enumerate.</param>
            internal OrderedIndexedDictionaryEnumerator(OrderedIndexedDictionary<TKey, TValue> dictionary)
            {
                mdDictionary = dictionary;
                miIndex = -1;
            }

            #endregion

            #region IEnumerator<KeyValuePair<TKey,TValue>> Members

            /// <summary>
            /// Gets the current key value pair being enumerated.
            /// </summary>
            public KeyValuePair<TKey, TValue> Current
            {
                get 
                {
                    return mdDictionary.PairAt(miIndex);
                }
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Disposes of the enumerator.
            /// </summary>
            public void Dispose()
            {
                mdDictionary = null;
            }

            #endregion

            #region IEnumerator Members

            /// <summary>
            /// Gets the current object being enumerated.
            /// </summary>
            object IEnumerator.Current
            {
                get 
                {
                    return this.Current;
                }
            }

            /// <summary>
            /// Moves to the next value in the enumeration.
            /// </summary>
            /// <returns>True if more values are available.</returns>
            public bool MoveNext()
            {
                miIndex++;

                if (miIndex >= mdDictionary.Count)
                    return false;
                else
                    return true;
            }

            /// <summary>
            /// Resets the enumerator to the start.
            /// </summary>
            public void Reset()
            {
                miIndex = -1;
            }

            #endregion
        }

        #endregion

        #region Protected Locals

        /// <summary>
        /// The internal dictionary used for hash lookups.
        /// </summary>
        protected Dictionary<TKey, TValue> mdDictionary;

        /// <summary>
        /// The list of keys in the order they were added.
        /// </summary>
        protected List<TKey> mlKeys;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new ordered indexed dictionary.
        /// </summary>
        public OrderedIndexedDictionary()
            : base()
        {
            mdDictionary = new Dictionary<TKey, TValue>();
            mlKeys = new List<TKey>();
        }

        /// <summary>
        /// Creates a new ordered indexed dictionary of a certain capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity for the dictionary.</param>
        public OrderedIndexedDictionary(int capacity)
            : base()
        {
            mdDictionary = new Dictionary<TKey, TValue>(capacity);
            mlKeys = new List<TKey>(capacity);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Tries to get the value for a key.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="value">The value returned, or default if key not found.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return mdDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the KeyValuePair at a specified index.
        /// </summary>
        /// <param name="index">The index for the KeyValuePair.</param>
        /// <returns>The KeyValuePair found.</returns>
        public KeyValuePair<TKey, TValue> PairAt(int index)
        {
            TKey lkKey = mlKeys[index];
            return new KeyValuePair<TKey, TValue>(lkKey, mdDictionary[lkKey]);
        }

        /// <summary>
        /// Inserts a key and value into the dictionary.
        /// </summary>
        /// <param name="index">The index to insert the new item above.</param>
        /// <param name="key">The key for the new item.</param>
        /// <param name="value">The value for the new item.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Key already exists.</exception>
        public void Insert(int index, TKey key, TValue value)
        {
            if (!mdDictionary.ContainsKey(key))
            {
                mlKeys.Insert(index, key);
                mdDictionary.Add(key, value);
            }
            else
                throw new ArgumentOutOfRangeException("key");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the value for an index.
        /// </summary>
        /// <param name="index">The index to get the value for.</param>
        /// <returns>The value found.</returns>
        public TValue this[int index]
        {
            get
            {
                return mdDictionary[mlKeys[index]];
            }
            set
            {
                mdDictionary[mlKeys[index]] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value for a key.
        /// </summary>
        /// <param name="key">The key to the get the value for. If setting and key is not found, then item is added to end of dictionary.</param>
        /// <returns>The value found.</returns>
        public TValue this[TKey key]
        {
            get
            {
                return mdDictionary[key];
            }
            set
            {
                bool lbExists = mdDictionary.ContainsKey(key);
                mdDictionary[key] = value;
                if (!lbExists) mlKeys.Add(key);
            }
        }

        #endregion

        #region IList<TKey> Members

        /// <summary>
        /// Inserts an item (not used).
        /// </summary>
        /// <param name="index">The index to insert at.</param>
        /// <param name="key">The key to insert.</param>
        /// <remarks>Invalid for this dictionary.</remarks>
        void IList<TKey>.Insert(int index, TKey key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the index of a key.
        /// </summary>
        /// <param name="key">The key to get the index for.</param>
        /// <returns>The index of the key if found, or -1 if not.</returns>
        public int IndexOf(TKey key)
        {
            return mlKeys.IndexOf(key);
        }

        /// <summary>
        /// Removes the item at an index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            TKey lkKey = mlKeys[index];
            mlKeys.RemoveAt(index);
            mdDictionary.Remove(lkKey);
        }

        /// <summary>
        /// Gets the key for an index.
        /// </summary>
        /// <param name="index">The index to get the key for.</param>
        /// <returns>The key at the index.</returns>
        /// <remarks>Setting is invalid for this dictionary.</remarks>
        TKey IList<TKey>.this[int index]
        {
            get
            {
                return mlKeys[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<TKey> Members

        /// <summary>
        /// Adds a key to dictionary.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <remarks>Invalid for this dictionary.</remarks>
        void ICollection<TKey>.Add(TKey key)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets whether a key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to check for existence.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        public bool Contains(TKey key)
        {
            return mdDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        public void Clear()
        {
            mlKeys.Clear();
            mdDictionary.Clear();
        }

        /// <summary>
        /// Copys keys in the preserved order to an array.
        /// </summary>
        /// <param name="keys">The array to receive the keys.</param>
        /// <param name="arrayIndex">The first arrayIndex in array to receive the keys.</param>
        public void CopyTo(TKey[] keys, int arrayIndex)
        {
            mlKeys.CopyTo(keys, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return mlKeys.Count;
            }
        }

        /// <summary>
        /// Gets whether this list is readonly.
        /// </summary>
        /// <remarks>As a list this dictionary IS readonly.</remarks>
        public bool IsReadOnly
        {
            get 
            {
                return true;
            }
        }

        /// <summary>
        /// Removes an item by key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to remove.</param>
        /// <returns>True if the key was found and removed, false otherwise.</returns>
        bool ICollection<TKey>.Remove(TKey key)
        {
            if (mdDictionary.ContainsKey(key))
            {
                mlKeys.Remove(key);
                mdDictionary.Remove(key);
                return true;
            }
            else
                return false;
        }

        #endregion

        #region IEnumerable<TKey> Members

        /// <summary>
        /// Gets the enumerator for the keys in the correct order.
        /// </summary>
        /// <returns>The enumerator for the keys.</returns>
        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
        {
            return mlKeys.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Gets the standard object enumerator (keys only)
        /// </summary>
        /// <returns>The keys enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return mlKeys.GetEnumerator();
        }

        #endregion

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Adds a new key and value to the end of the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to add.</param>
        /// <param name="value">The value of the item to add.</param>
        public void Add(TKey key, TValue value)
        {
            // Make sure we add to the dictionary first to make sure the
            // exception is raised if it already exists
            mdDictionary.Add(key, value);
            mlKeys.Add(key);
        }

        /// <summary>
        /// Gets whether a key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to check for existence.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return mdDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the keys as a collection in the correct order.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return mlKeys;
            }
        }

        /// <summary>
        /// Removes an item by key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to remove.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            if (mdDictionary.ContainsKey(key))
            {
                mdDictionary.Remove(key);
                mlKeys.Remove(key);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Gets a collection of the values for the dictionary in the correct order.
        /// </summary>
        public ICollection<TValue> Values
        {
            get 
            { 
                // Create list for return
                List<TValue> lvValues = new List<TValue>();

                // Make sure we add them in the order of the list
                foreach(TKey lkKey in mlKeys)
                    lvValues.Add(mdDictionary[lkKey]);

                // Return the values
                return lvValues;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Adds a new key value pair to the end of the dictionary.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            // Make sure we add to the dictionary first to make sure the
            // exception is raised if it already exists
            mdDictionary.Add(item.Key, item.Value);
            mlKeys.Add(item.Key);
        }

        /// <summary>
        /// Gets whether a key and value exists in the dictionary.
        /// </summary>
        /// <param name="item">The item containing the key and value.</param>
        /// <returns>True if the key and its value match, false otherwise.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)mdDictionary).Contains(item);
        }

        /// <summary>
        /// Copys key value pairs in the preserved order to an array.
        /// </summary>
        /// <param name="array">The array to receive the key value pairs.</param>
        /// <param name="arrayIndex">The first arrayIndex in array to receive the keys.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex cannot be less than 0.</exception>
        /// <exception cref="System.ArgumentException">array does not have enough space.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex", "cannot be less than 0");
            if (arrayIndex + this.Count > array.Length) throw new ArgumentException("not enough space in array", "array");

            for (int liCounter = 0; liCounter < mlKeys.Count; liCounter++)
                array[liCounter + arrayIndex] = PairAt(liCounter);
        }

        /// <summary>
        /// Removes an item from the dictionary if the key and value matches.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed, false otherwise.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (((IDictionary<TKey, TValue>)mdDictionary).Remove(item))
            {
                mlKeys.Remove(item.Key);
                return true;
            }
            else
                return false;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Gets the KeyValuePair enumerator which returns the items in the correct order.
        /// </summary>
        /// <returns>The Enumerator of KeyValuePairs.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new OrderedIndexedDictionaryEnumerator(this);
        }

        #endregion
    }
}

#endif