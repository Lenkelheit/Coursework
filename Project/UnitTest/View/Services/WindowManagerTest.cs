using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Galagram.Services;

namespace UnitTest.View.Services
{
    [TestClass]
    public class WindowManagerTest
    {
        // singleton, so each test require his own clean up block
        // unfortunatelly ordered test does not work in MSTest V2
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
            WindowManager windowManager = WindowManager.Instance;
            PrivateObject privateObject = new PrivateObject(windowManager);
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
            windowManager.Registrate(keyTwice, type);
            int actualRegisteredValueAfterRegistration = factory.Count;

            // Assert

            // wrong ones
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.Registrate(keyNull, typeNull));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.Registrate(keyNull, type));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.Registrate(key, typeNull));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.Registrate(keySpace, type));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.Registrate(keyEmpty, type));
            Assert.ThrowsException<ArgumentException>(() => windowManager.Registrate(key, typeInterface));
            Assert.ThrowsException<ArgumentException>(() => windowManager.Registrate(key, typeAbstract));
            Assert.ThrowsException<InvalidOperationException>(() => windowManager.Registrate(keyTwice, type));
            // regular one
            Assert.AreEqual(expectedRegisteredValueBeforeRegistration, actualRegisteredValueBeforeRegistration);
            Assert.AreEqual(expectedRegisteredValueAfterRegistration, actualRegisteredValueAfterRegistration);
            CollectionAssert.Contains(factory.Keys.ToArray(), registeredValue.Key);
            CollectionAssert.Contains(factory.Values.ToArray(), registeredValue.Value);

            // clean up
            factory.Remove(keyTwice);
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
            WindowManager windowManager = WindowManager.Instance;
            PrivateObject privateObject = new PrivateObject(windowManager);
            IDictionary<string, Type> factory = (privateObject.GetField("factory") as IDictionary<string, Type>);
            // clean up
            factory.Clear();


            string keyNull = null;
            string keySpace = "    ";
            string keyEmpty = String.Empty;
            string keyMissing = "there is no such key";
            string keyPresent = nameof(Galagram.Window.User.MainWindow);
            factory.Add(keyPresent, typeof(int));

            int expectedFactoryAmountBeforeUnregistrate = 1;
            int expectedFactoryAmountAfterUnregistrate = 0;

            KeyValuePair<string, Type> unRegisteredValue = new KeyValuePair<string, Type>(nameof(Galagram.Window.User.MainWindow), typeof(Galagram.Window.User.MainWindow));
            // Act
            int actualFactoryAmountBeforeUnregistrate = factory.Count;
            windowManager.UnRegistrate(keyPresent);
            int actualFactoryAmouneAfterUnregistrate = factory.Count;

            // Assert
            // wrong ones
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.UnRegistrate(keyNull));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.UnRegistrate(keySpace));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.UnRegistrate(keyEmpty));
            Assert.ThrowsException<InvalidOperationException>(() => windowManager.UnRegistrate(keyMissing));

            // regular one
            Assert.AreEqual(expectedFactoryAmountBeforeUnregistrate, actualFactoryAmountBeforeUnregistrate);
            Assert.AreEqual(expectedFactoryAmountAfterUnregistrate, actualFactoryAmouneAfterUnregistrate);
            CollectionAssert.DoesNotContain(factory.Keys.ToArray(), unRegisteredValue.Key);
            CollectionAssert.DoesNotContain(factory.Values.ToArray(), unRegisteredValue.Value);
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
            WindowManager windowManager = WindowManager.Instance;
            PrivateObject privateObject = new PrivateObject(windowManager);
            IDictionary<string, Type> factory = (privateObject.GetField("factory") as IDictionary<string, Type>);
            // clean up
            factory.Clear();


            string keyNull = null;
            string keySpace = "    ";
            string keyEmpty = String.Empty;
            string keyMissing = "there is no such key";

            // Act       
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.MakeInstance(keyNull));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.MakeInstance(keySpace));
            Assert.ThrowsException<ArgumentNullException>(() => windowManager.MakeInstance(keyEmpty));
            Assert.ThrowsException<InvalidOperationException>(() => windowManager.MakeInstance(keyMissing));         

        }
    }
}
