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
        LinkedList<T> baseCollection;

        bool isIndexReseted;
        int currentIndex;
        LinkedListNode<T> currentNode;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="ReverseCollection{T}"/>
        /// </summary>
        public ReverseCollection()
        {
            baseCollection = new LinkedList<T>();

            isIndexReseted = true;
            currentIndex = Core.Configuration.Constants.WRONG_INDEX;
            currentNode = null;
        }
        /// <summary>
        /// Initializes a new instance of <see cref="ReverseCollection{T}"/>
        /// </summary>
        /// <param name="collection">
        /// The collection to copy elements from.
        /// </param>
        public ReverseCollection(IEnumerable<T> collection)
        {
            baseCollection = new LinkedList<T>(System.Linq.Enumerable.Reverse(collection));

            isIndexReseted = true;
            currentIndex = Core.Configuration.Constants.WRONG_INDEX;
            currentNode = null;
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
        /// <summary>
        /// Determines whenever collection is empty
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        // INDEXIERS        
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the element to get or set.
        /// </param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="ReverseCollection{T}"/>
        /// </exception>
        public T this[int index]
        {
            get
            {
                // check index
                if (IsIndexWrong(index)) throw new IndexOutOfRangeException();

                // move to node
                MoveToIndex(index);

                // get value
                return currentNode.Value;
            }
            set
            {
                // check index
                if (IsIndexWrong(index)) throw new IndexOutOfRangeException();

                // move to node
                MoveToIndex(index);

                // notify
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Replace,
                                                                         oldItem: currentNode.Value,
                                                                         newItem: value,
                                                                         index: index));
                // change item
                currentNode.Value = value;
            }
        }
        private void MoveToIndex(int index)
        {
            // if index is reseted, start from beginning
            FirstStep();

            // lift behaviour
            // stays at current node
            // if index is higher, move up
            // if index is lowwer, move down
            while (currentIndex < index) NextIndex();
            while (currentIndex > index) PreviousIndex();
        }
        private void MoveToItemFromStart(T item)
        {
            while (currentNode != null && !currentNode.Value.Equals(item))
            {
                NextIndex();
            }
        }
        private void MoveToItemFromEnd(T item)
        {
            while (currentNode != null && !currentNode.Value.Equals(item))
            {
                PreviousIndex();
            }
        }
        private void NextIndex()
        {
            currentNode = currentNode.Next;
            ++currentIndex;
        }
        private void PreviousIndex()
        {
            currentNode = currentNode.Previous;
            --currentIndex;
        }
        private void ResetIndex()
        {
            isIndexReseted = true;
            currentNode = null;
            currentIndex = Core.Configuration.Constants.WRONG_INDEX;
        }
        private void FirstStep()
        {
            if (this.IsEmpty) throw new InvalidOperationException();

            if (!isIndexReseted) return;

            isIndexReseted = false;
            currentNode = baseCollection.First;
            currentIndex = 0;
        }
        private bool IsIndexWrong(int index)
        {
            return index < 0 || index >= baseCollection.Count;
        }

        // EVENTS
        /// <summary>
        /// Occurs when the collection changes
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        // METHODS
        // ADDING
        #region ADDING
        /// <summary>
        /// Inserts an object to collection
        /// </summary>
        /// <param name="item">
        /// Inserted item
        /// </param>
        public void Add(T item)
        {
            baseCollection.AddFirst(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Add, changedItem: item, index: 0));
        }
        /// <summary>
        /// Inserts an item to the <see cref="ReverseCollection{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which item should be inserted.
        /// </param>
        /// <param name="item">
        /// The object to insert into the <see cref="ReverseCollection{T}"/>
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero or greater than amount of item in <see cref="ReverseCollection{T}"/>
        /// </exception>
        public void Insert(int index, T item)
        {
            // check index
            if (IsIndexWrong(index)) throw new ArgumentOutOfRangeException(nameof(index));

            // search for index
            MoveToIndex(index);

            // insert
            baseCollection.AddBefore(currentNode, item);

            // notify
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Add, changedItem: item, index: index));
        }
        #endregion
        // SEARCHING
        #region SEARCHING
        /// <summary>
        /// Determines whether a value is in the <see cref="ReverseCollection{T}"/>
        /// </summary>
        /// <param name="item">
        /// The value to locate in the <see cref="ReverseCollection{T}"/>. The value can be null for reference types.
        /// </param>
        /// <returns>
        /// True if value is found in the <see cref="ReverseCollection{T}"/>; otherwise — false.
        /// </returns>
        public bool Contains(T item)
        {
            return baseCollection.Contains(item);
        }
        /// <summary>
        /// Determines the index of a specific item in the <see cref="ReverseCollection{T}"/>
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="ReverseCollection{T}"/>
        /// </param>
        /// <returns>
        /// The index of item if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            // start searching from first item
            ResetIndex();
            FirstStep();

            // search item
            MoveToItemFromStart(item);

            // return result : has item or not
            return currentNode != null ? currentIndex : Core.Configuration.Constants.WRONG_INDEX;
        }
        #endregion
        // REMOVING
        #region REMOVING
        /// <summary>
        /// Removes the <see cref="ReverseCollection{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the item to remove.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="ReverseCollection{T}"/>
        /// </exception>
        public void RemoveAt(int index)
        {
            // check index
            if (IsIndexWrong(index)) throw new ArgumentOutOfRangeException(nameof(index));

            // find item
            MoveToIndex(index);

            // delete item
            baseCollection.Remove(currentNode);

            // notify
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Remove,
                                                                     changedItem: currentNode.Value,
                                                                     index: index));
        }
        /// <summary>
        /// Removes the first occurrence of <paramref name="item"/> from the <see cref="ReverseCollection{T}"/>
        /// </summary>
        /// <param name="item">
        /// The value to remove from the <see cref="ReverseCollection{T}"/>.
        /// </param>
        /// <returns>
        /// True if the element containing value is successfully removed; otherwise — false.
        /// <para/>
        /// This method also returns false if value was not found in the original <see cref="ReverseCollection{T}"/>.
        /// </returns>
        public bool Remove(T item)
        {
            // start searching from first item
            ResetIndex();
            FirstStep();

            // search item
            MoveToItemFromStart(item);

            // remove item, if can
            if (currentNode != null)
            {
                baseCollection.Remove(currentNode);

                // notify
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Remove,
                                                                         changedItem: currentNode.Value,
                                                                         index: currentIndex));
                // removing result
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Removes all items from the <see cref="ReverseCollection{T}"/>
        /// </summary>
        public void Clear()
        {
            baseCollection.Clear();

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Reset));
        }
        #endregion
        /// <summary>
        /// Copies the entire <see cref="ReverseCollection{T}"/> to a compatible one-dimensional <see cref="Array"/>, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ReverseCollection{T}"/>. 
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in array at which copying begins
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array"/> is null
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than zero or greater than amount of item in <see cref="ReverseCollection{T}"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of elements in the source <see cref="ReverseCollection{T}"/> is greater than the available space from index to the end of the destination array.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            baseCollection.CopyTo(array, arrayIndex);
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
        /// <summary>
        /// Raises <see cref="CollectionChanged"/>
        /// </summary>
        /// <param name="notifyCollectionChangedEventArgs">
        /// Provides data for <see cref="CollectionChanged"/> event
        /// </param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CollectionChanged?.Invoke(this, notifyCollectionChangedEventArgs);
        }
    }
}