using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Galagram.Collections
{
    /// <summary>
    /// Inversed list collection
    /// </summary>
    /// <typeparam name="T">
    /// Type of items
    /// </typeparam>
    public class ReverseCollection<T> : ICollection<T>, IList<T>, INotifyCollectionChanged 
    {
        // FIELDS
        Stack<T> baseCollection;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="ReverseCollection{T}"/>
        /// </summary>
        public ReverseCollection()
        {
            baseCollection = new Stack<T>();
        }
        /// <summary>
        /// Initializes a new instance of <see cref="ReverseCollection{T}"/>
        /// </summary>
        /// <param name="collection">
        /// The collection to copy elements from.
        /// </param>
        public ReverseCollection(IEnumerable<T> collection)
        {
            baseCollection = new Stack<T>(collection);
        }

        // PROPERTIES
        /// <summary>
        /// Gets the number of elements contained in the <see cref="ReverseCollection{T}"/>
        /// </summary>
        public int Count => baseCollection.Count;
        /// <summary>
        /// Determines if collection can be modified
        /// </summary>
        public bool IsReadOnly => false;

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // EVENTS
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        // METHODS
        /// <summary>
        /// Inserts an object to collection
        /// </summary>
        /// <param name="item">
        /// Inserted item
        /// </param>
        public void Add(T item)
        {
            baseCollection.Push(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Add, changedItem: item, index: 0));
        }
        
        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns an enumerator for <see cref="ReverseCollection{T}"/>
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IEnumerator{T}"/>
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return baseCollection.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseCollection.GetEnumerator();
        }

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CollectionChanged?.Invoke(this, notifyCollectionChangedEventArgs);
        }
    }
}
