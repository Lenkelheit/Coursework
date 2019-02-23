using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Galagram.Collections;

namespace UnitTest.View.Collections
{
    [TestClass]
    public class ReverseCollectionTest
    {
        // FIELDS
        int arrayLength;
        int[] initialArray;
        int[] reversedArray;

        // CONSTRUCTORS
        public ReverseCollectionTest()
        {
            arrayLength = 5;

            initialArray = new int[] { 1, 2, 3, 4, 5 };
            reversedArray = new int[] { 5, 4, 3, 2, 1 };
        }

        // PROPERTIES
        public TestContext TestContext { get; set; }

        // TESTS
        [TestMethod]
        public void ReverseConstructor()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            // Act
            // Assert
            CollectionAssert.AreEqual(expected: reversedArray, actual: reversedCollection.ToArray());
        }

        // PROPERTIES
        #region PROPERTIES
        [TestMethod]
        public void Count()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>();
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = reversedCollection.Count;
            int expectedCountAfterAdding = 5;

            // Act
            for (int i = 0; i < expectedCountAfterAdding; ++i)
            {
                reversedCollection.Add(i);
            }
            int actualCountAfterAdding = reversedCollection.Count;

            // Assert
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(expectedCountAfterAdding, actualCountAfterAdding);
        }
        [TestMethod]
        public void IsEmpty()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>();
            bool expectedIsEmptyBeforeAdding = true;
            bool actualIsEmptyBeforeAdding = reversedCollection.IsEmpty;
            bool expectedIsEmptyAfterAdding = false;

            // Act
            reversedCollection.Add(1);
            bool actualIsEmptyAfterAdding = reversedCollection.IsEmpty;

            // Assert
            Assert.AreEqual(expectedIsEmptyBeforeAdding, actualIsEmptyBeforeAdding);
            Assert.AreEqual(expectedIsEmptyAfterAdding, actualIsEmptyAfterAdding);
        }
        #endregion
        // ADDING
        #region ADDING
        [TestMethod]
        public void Adding_WithForEach()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>();

            // Act
            foreach (int item in initialArray)
            {
                reversedCollection.Add(item);
            }

            // Assert
            CollectionAssert.AreEqual(reversedArray, reversedCollection.ToArray());
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\ThreeRandomItem.xml",
            tableName: "CollectionItems",
            dataAccessMethod: DataAccessMethod.Random)]
        public void Adding_ThreeRandom()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>();
            int[] baseCollection = new int[3]
            {
                Convert.ToInt32(TestContext.DataRow["First"]),
                Convert.ToInt32(TestContext.DataRow["Second"]),
                Convert.ToInt32(TestContext.DataRow["Third"])
            };

            int[] expectedCollection = baseCollection.Reverse().ToArray();

            // Act
            foreach (int item in baseCollection)
            {
                reversedCollection.Add(item);
            }
            int[] actualCollection = reversedCollection.ToArray();

            // Assert
            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }
        [TestMethod]
        public void Insert()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int insertedElement = 10;
            int insertedIndex = 2;

            int[] expectedCollectionBeforeInserting = reversedArray;
            int[] actualCollectionBeforeInserting = reversedCollection.ToArray();

            int[] expectedCollectionAfterInserting = new int[6] { 5, 4, insertedElement, 3, 2, 1 };

            // Act
            reversedCollection.Insert(insertedIndex, insertedElement);
            int[] actualCollectionAfterInserting = reversedCollection.ToArray();

            // Assert
            CollectionAssert.Contains(actualCollectionAfterInserting, insertedElement);
            CollectionAssert.AreEqual(expectedCollectionBeforeInserting, actualCollectionBeforeInserting);
            CollectionAssert.AreEqual(expectedCollectionAfterInserting, actualCollectionAfterInserting);
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\WrongIndex.xml",
            tableName: "WrongIndex",
            dataAccessMethod: DataAccessMethod.Random)]
        public void Insert_WrongIndex_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int valueToAssert = int.MaxValue;

            int wrongIndex = Convert.ToInt32(TestContext.DataRow["Index"]);

            // Act
            // Assert
            CollectionAssert.DoesNotContain(reversedCollection.ToArray(), valueToAssert);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => reversedCollection.Insert(wrongIndex, valueToAssert));
            CollectionAssert.DoesNotContain(reversedCollection.ToArray(), valueToAssert);
        }
        #endregion
        // REMOVING
        #region REMOVING
        [TestMethod]
        public void Clear()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>();
            int[] expectedCollectionAfterClearing = new int[0];

            // Act
            foreach (int item in initialArray)
            {
                reversedCollection.Add(item);
            }
            reversedCollection.Clear();

            // Assert
            Assert.IsTrue(reversedCollection.IsEmpty);
            CollectionAssert.AreEqual(expectedCollectionAfterClearing, reversedCollection.ToArray());
        }

        [TestMethod]
        public void RemoveAt()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int indexToRemove = 2;
            int itemToRemove = 3;
            int[] expectedCollection = new int[4] { 5, 4, 2, 1 };

            bool expectedHasItemBeforeRemoving = true;
            bool actualHasItemBeforeRemoving = reversedArray.Contains(itemToRemove);

            bool expectedHasItemAfterRemoving = false;

            // Act
            reversedCollection.RemoveAt(indexToRemove);

            bool actualHasItemAfterRemoving = reversedCollection.Contains(itemToRemove);
            int[] actualCollectionAfterRemoving = reversedCollection.ToArray();

            // Assert
            CollectionAssert.DoesNotContain(actualCollectionAfterRemoving, itemToRemove);
            Assert.AreEqual(expectedHasItemBeforeRemoving, actualHasItemBeforeRemoving);
            Assert.AreEqual(expectedHasItemAfterRemoving, actualHasItemAfterRemoving);
            CollectionAssert.AreEqual(expectedCollection, actualCollectionAfterRemoving);
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\WrongIndex.xml",
            tableName: "WrongIndex",
            dataAccessMethod: DataAccessMethod.Random)]
        public void RemoveAt_WrongIndex_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int wrongIndex = Convert.ToInt32(TestContext.DataRow["Index"]);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => reversedCollection.RemoveAt(wrongIndex));
        }

        [TestMethod]
        public void Remove_OneOccurrence()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int itemToRemove = 2;
            bool expectedRemoveResult = true;

            bool expectedContainItemBeforeRemoving = true;
            bool exoectedContainItemAfterRemoving = false;

            int[] expectedCollection = new int[4] { 5, 4, 3, 1 };

            // Act
            bool actualContainItemBeforeRemoving = reversedCollection.Contains(itemToRemove);
            bool actualRemoveResult = reversedCollection.Remove(itemToRemove);
            bool actualContainItemAfterRemoving = reversedCollection.Contains(itemToRemove);
            int[] actualCollection = reversedCollection.ToArray();

            // Assert
            Assert.AreEqual(expectedRemoveResult, actualRemoveResult);
            Assert.AreEqual(expectedContainItemBeforeRemoving, actualContainItemBeforeRemoving);
            Assert.AreEqual(exoectedContainItemAfterRemoving, actualContainItemAfterRemoving);
            CollectionAssert.AreEqual(expectedCollection, actualCollection);
            CollectionAssert.DoesNotContain(actualCollection, itemToRemove);
        }

        [TestMethod]
        public void Remove_ManyOccurrence()
        {
            // Arrange
            int[] startCollection = new int[] { 1, 2, 3, 3, 2, 1 };
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(startCollection);

            int itemToRemove = 2;
            bool expectedRemoveResult = true;

            bool expectedContainItemBeforeRemoving = true;
            bool exoectedContainItemAfterRemoving = true;

            int[] expectedCollection = new int[] { 1, 3, 3, 2, 1 };

            // Act
            bool actualContainItemBeforeRemoving = reversedCollection.Contains(itemToRemove);
            bool actualRemoveResult = reversedCollection.Remove(itemToRemove);
            bool actualContainItemAfterRemoving = reversedCollection.Contains(itemToRemove);

            // Assert
            Assert.AreEqual(expectedRemoveResult, actualRemoveResult);
            Assert.AreEqual(expectedContainItemBeforeRemoving, actualContainItemBeforeRemoving);
            Assert.AreEqual(exoectedContainItemAfterRemoving, actualContainItemAfterRemoving);
            CollectionAssert.Contains(reversedCollection.ToArray(), itemToRemove);
        }

        [TestMethod]
        public void Remove_WrongItem_False()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int itemToRemove = int.MaxValue;
            bool expectedRemoveResult = false;

            bool expectedContainItemBeforeRemoving = false;
            bool exoectedContainItemAfterRemoving = false;

            // Act
            bool actualContainItemBeforeRemoving = reversedCollection.Contains(itemToRemove);
            bool actualRemoveResult = reversedCollection.Remove(itemToRemove);
            bool actualContainItemAfterRemoving = reversedCollection.Contains(itemToRemove);

            // Assert
            Assert.AreEqual(expectedRemoveResult, actualRemoveResult);
            Assert.AreEqual(expectedContainItemBeforeRemoving, actualContainItemBeforeRemoving);
            Assert.AreEqual(exoectedContainItemAfterRemoving, actualContainItemAfterRemoving);
            CollectionAssert.DoesNotContain(reversedCollection.ToArray(), itemToRemove);
        }
        #endregion
        // SEARCHING
        #region SEARCHING
        [TestMethod]
        public void Contains()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>();

            int elementToSearch = 2;
            bool expectedContainsElementBeforeAdding = false;
            bool expectedContainsElementAfterAdding = true;

            // Act
            bool actualContainsElementBeforeAdding = reversedCollection.Contains(elementToSearch);
            foreach (int item in initialArray)
            {
                reversedCollection.Add(item);
            }
            bool actualContainsElementAfterAdding = reversedCollection.Contains(elementToSearch);

            // Assert
            Assert.AreEqual(expectedContainsElementBeforeAdding, actualContainsElementBeforeAdding);
            Assert.AreEqual(expectedContainsElementAfterAdding, actualContainsElementAfterAdding);
            CollectionAssert.Contains(reversedCollection.ToArray(), elementToSearch);
        }

        [TestMethod]
        public void IndexOf_ExistingItem()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int itemToSearch = 2;
            int expectedItemIndex = 3;

            // Act
            int actualItemIndex = reversedCollection.IndexOf(itemToSearch);

            // Assert
            Assert.AreEqual(expectedItemIndex, actualItemIndex);
        }
        [TestMethod]
        public void IndexOf_WrongItem()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int itemToSearch = int.MaxValue;
            int expectedItemIndex = -1;

            // Act
            int actualItemIndex = reversedCollection.IndexOf(itemToSearch);

            // Assert
            Assert.AreEqual(expectedItemIndex, actualItemIndex);
        }
        #endregion
        // COPY TO
        #region COPY TO
        [TestMethod]
        public void CopyTo_FromStart()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int startedIndex = 0;
            int arrayLength = 5;
            int[] copyArray = new int[arrayLength];

            // Act
            reversedCollection.CopyTo(copyArray, startedIndex);

            // Assert
            CollectionAssert.AreEqual(reversedArray, copyArray);
        }
        [TestMethod]
        public void CopyTo_ToMiddle()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int startedIndex = 2;
            int arrayLength = 7;
            int[] copyArray = new int[arrayLength];
            int[] expectedArray = new int[] { 0, 0, 5, 4, 3, 2, 1 };

            // Act
            reversedCollection.CopyTo(copyArray, startedIndex);

            // Assert
            CollectionAssert.AreEqual(expectedArray, copyArray);
        }
        [TestMethod]
        public void CopyTo_NullArray_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int[] copyArray = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => reversedCollection.CopyTo(copyArray, 0));
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\WrongIndex.xml",
            tableName: "WrongIndex",
            dataAccessMethod: DataAccessMethod.Random)]
        public void CopyTo_WrongIndex_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int[] copyArray = new int[5];
            int wrongIndex = Convert.ToInt32(TestContext.DataRow["Index"]);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => reversedCollection.CopyTo(copyArray, wrongIndex));
        }

        [TestMethod]
        public void CopyTo_SmallerArray_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int copyIndex = 0;
            int[] copyArray = new int[1];

            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => reversedCollection.CopyTo(copyArray, copyIndex));
        }
        [TestMethod]
        public void CopyTo_BiggerArray_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int copyIndex = 0;
            int[] copyArray = new int[10];
            int[] expectedArray = new int[10] { 5, 4, 3, 2, 1, 0, 0, 0, 0, 0 };

            // Act
            reversedCollection.CopyTo(copyArray, copyIndex);

            // Assert
            CollectionAssert.AreEquivalent(expectedArray, copyArray);
        }
        #endregion
        // INDEXERS
        #region INDEXERS
        [TestMethod]
        public void Indexer_Get()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            Random random = new Random();

            // Act
            // Assert
            CollectionAssert.AreEqual(reversedArray, reversedCollection.ToArray());

            // FOR is necessary, needs to check []
            // from start to end
            for (int i = 0; i < arrayLength; ++i)
            {
                Assert.AreEqual(reversedArray[i], reversedCollection[i]);
            }
            // from end to start
            for (int i = arrayLength - 1; i >= 0; --i)
            {
                Assert.AreEqual(reversedArray[i], reversedCollection[i]);
            }
            // random check
            for (int i = 0; i < arrayLength; ++i)
            {
                int index = random.Next(arrayLength);
                Assert.AreEqual(reversedArray[index], reversedCollection[index]);
            }
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\WrongIndex.xml",
            tableName: "WrongIndex",
            dataAccessMethod: DataAccessMethod.Random)]
        public void Indexer_Get_WrongIndex_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int wrongIndex = Convert.ToInt32(TestContext.DataRow["Index"]);

            // Act
            // Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => reversedCollection[wrongIndex]);
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\IndexerValueSet.xml",
            tableName: "SetValue",
            dataAccessMethod: DataAccessMethod.Random)]
        public void Indexer_Set()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int oldItem = Convert.ToInt32(TestContext.DataRow["OldValue"]);
            int newItem = Convert.ToInt32(TestContext.DataRow["NewValue"]);
            int indexToSet = Convert.ToInt32(TestContext.DataRow["Index"]);

            // Act
            reversedCollection[indexToSet] = newItem;
            int[] actualCollection = reversedCollection.ToArray();

            // Assert
            CollectionAssert.Contains(actualCollection, newItem);
            CollectionAssert.DoesNotContain(actualCollection, oldItem);
        }

        [TestMethod]
        [DataSource(
            providerInvariantName: "Microsoft.VisualStudio.TestTools.DataSource.XML",
            connectionString: @"..\..\Resources\View\Collections\ReverseCollection\WrongIndex.xml",
            tableName: "WrongIndex",
            dataAccessMethod: DataAccessMethod.Random)]
        public void Indexer_Set_WrongIndex_Exception()
        {
            // Arrange
            ReverseCollection<int> reversedCollection = new ReverseCollection<int>(initialArray);

            int wrongIndex = Convert.ToInt32(TestContext.DataRow["Index"]);
            int valueToSet = 10;

            // Act
            // Assert
            CollectionAssert.DoesNotContain(reversedCollection.ToArray(), valueToSet);
            Assert.ThrowsException<IndexOutOfRangeException>(() => reversedCollection[wrongIndex] = valueToSet);
            CollectionAssert.DoesNotContain(reversedCollection.ToArray(), valueToSet);
        }
        #endregion
    }
}