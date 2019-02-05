using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Galagram.Services;

namespace UnitTest.View.Services
{
    [TestClass]
    public class NavigationManagerTest
    {
        [TestMethod]
        public void Registrate()
        {
            // BEHAVIOR 
            // * passing wrong argument and getting exception
            // * last one takes regular argument
            // TAKE
            // * both null
            // * key is null
            // * type is null
            // * key white space
            // * key is empty
            // * abstract type
            // * interface type
            // * same key twice
            // * regular behaviour
            // RETURN
            // * exception should be throwm
            // * last one is regular


            // Arrange
            NavigationManager navigationManager = NavigationManager.Instance;
            PrivateObject privateObject = new PrivateObject(navigationManager);
            IDictionary<string, Type> factory = (privateObject.GetField("factory") as IDictionary<string, Type>);
            // clean up
            factory.Clear();


            string keyNull = null;
            string key = "key";
            string keySpace = "    ";
            string keyEmpty = String.Empty;
            string keyTwice = "this key is doubled";

            Type typeNull = null;
            Type type = typeof(int);
            Type typeInterface = typeof(IDisposable);
            Type typeAbstract = typeof(Array);

            int expectedRegisteredValueBeforeRegistration = 0;
            int expectedRegisteredValueAfterRegistration = 1;

            KeyValuePair<string, Type> registeredValue = new KeyValuePair<string, Type>(keyTwice, type);

            // Act
            int actualRegisteredValueBeforeRegistration = factory.Count;
            navigationManager.Registrate(keyTwice, type);
            int actualRegisteredValueAfterRegistration = factory.Count;

            // Assert

            // wrong ones
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.Registrate(keyNull, typeNull));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.Registrate(keyNull, type));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.Registrate(key, typeNull));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.Registrate(keySpace, type));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.Registrate(keyEmpty, type));
            Assert.ThrowsException<ArgumentException>(() => navigationManager.Registrate(key, typeInterface));
            Assert.ThrowsException<ArgumentException>(() => navigationManager.Registrate(key, typeAbstract));
            Assert.ThrowsException<InvalidOperationException>(() => navigationManager.Registrate(keyTwice, type));
            // regular one
            Assert.AreEqual(expectedRegisteredValueBeforeRegistration, actualRegisteredValueBeforeRegistration);
            Assert.AreEqual(expectedRegisteredValueAfterRegistration, actualRegisteredValueAfterRegistration);
            CollectionAssert.Contains(factory.Keys.ToArray(), registeredValue.Key);
            CollectionAssert.Contains(factory.Values.ToArray(), registeredValue.Value);

        }
        [TestMethod]
        public void UnRegistrate()
        {
            // BEHAVIOR 
            // * passing wrong argument and getting exception
            // * last one takes regular argument
            // TAKE
            // * key is null
            // * key is white space
            // * key is empty
            // * no shuch key
            // * regular behaviour
            // RETURN
            // * exception should be throwm
            // * last one is regular

            // Arrange
            NavigationManager navigationManager = NavigationManager.Instance;
            PrivateObject privateObject = new PrivateObject(navigationManager);
            IDictionary<string, Type> factory = (privateObject.GetField("factory") as IDictionary<string, Type>);
            // clean up
            factory.Clear();

            string keyNull = null;
            string keySpace = "    ";
            string keyEmpty = String.Empty;
            string keyMissing = "there is no such key";
            string keyPresent = nameof(System.Windows.Controls.Button);
            Type typePresent = typeof(System.Windows.Controls.Button);

            int expectedFactoryAmountBeforeUnregistrate = 1;
            int expectedFactoryAmountAfterUnregistrate = 0;
            

            KeyValuePair<string, Type> unRegisteredValue = new KeyValuePair<string, Type>(nameof(System.Windows.Controls.Button), typeof(System.Windows.Controls.Button));
            navigationManager.Registrate(keyPresent, typePresent);
           
            // Act
            int actualFactoryAmountBeforeUnregistrate = factory.Count;
            navigationManager.UnRegistrate(keyPresent);
            int actualFactoryAmouneAfterUnregistrate = factory.Count;

            // Assert
            // wrong ones
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.UnRegistrate(keyNull));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.UnRegistrate(keySpace));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.UnRegistrate(keyEmpty));
            Assert.ThrowsException<InvalidOperationException>(() => navigationManager.UnRegistrate(keyMissing));

            // regular one
            Assert.AreEqual(expectedFactoryAmountBeforeUnregistrate, actualFactoryAmountBeforeUnregistrate);
            Assert.AreEqual(expectedFactoryAmountAfterUnregistrate, actualFactoryAmouneAfterUnregistrate);
            CollectionAssert.DoesNotContain(factory.Keys.ToArray(), unRegisteredValue.Key);
            CollectionAssert.DoesNotContain(factory.Values.ToArray(), unRegisteredValue.Value);

            // clean up
            factory.Clear();
        }
        [TestMethod]
        public void MakeInstance()
        {
            // BEHAVIOR 
            // * passing wrong argument and getting exception
            // TAKE
            // * key is null
            // * key is whitespace
            // * key is empty
            // * key is missing
            // * no shuch key
            // RETURN
            // * exception should be throwm

            // Arrange
            NavigationManager navigationManager = NavigationManager.Instance;
            PrivateObject privateObject = new PrivateObject(navigationManager);
            IDictionary<string, Type> factory = (privateObject.GetField("factory") as IDictionary<string, Type>);
            // clean up
            factory.Clear();


            string keyNull = null;
            string keySpace = "    ";
            string keyEmpty = String.Empty;
            string keyMissing = "there is no such key";

            // Act       
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.MakeInstance(keyNull));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.MakeInstance(keySpace));
            Assert.ThrowsException<ArgumentNullException>(() => navigationManager.MakeInstance(keyEmpty));
            Assert.ThrowsException<InvalidOperationException>(() => navigationManager.MakeInstance(keyMissing));

        }
    }
}
