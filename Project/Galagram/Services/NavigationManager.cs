using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Galagram.Services
{
    /// <summary>
    /// Provides manager to navigate through user controls.
    /// <para />
    /// Implements a Factory pattern.
    /// <para />
    /// Implements a Singleton pattern
    /// </summary>
    public class NavigationManager : Core.Interfaces.IFactory<string, Type, UserControl>
    {
        // FIELDS
        IDictionary<string, Type> factory;
        Stack<Type> history;
        static NavigationManager instance;

        // CONSTRUCTORS
        private NavigationManager()
        {
            factory = new Dictionary<string, Type>();
            history = new Stack<Type>();
        }
        static NavigationManager()
        {
            // initialize singleton value
            instance = new NavigationManager();
        }

        // PROPERTIES
        /// <summary>
        /// Gets an instance of <see cref="NavigationManager"/>
        /// </summary>
        public static NavigationManager Instance => instance;

        // METHODS
        // factory interface implementation
        #region factory implementation
        /// <summary>
        /// Returns a new instance of a user control.
        /// <para/>
        /// Current instance has been saved in history
        /// </summary>
        /// <param name="key">
        /// A key by which user control was registered.
        /// </param>
        /// <returns>
        /// An instance of <see cref="UserControl"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public UserControl MakeInstance(string key)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (!factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.NAVIGATION_MANAGER_NO_SUCH_KEY_FORMAT, nameof(key)));

            // save instance type in history
            history.Push(factory[key]);

            // return control instance created by current type extracted by key
            return (UserControl)Activator.CreateInstance(factory[key]);
        }
        /// <summary>
        /// Registrates a type of the user control by current key.
        /// </summary>
        /// <param name="key">
        /// A key by which type is registered.
        /// </param>
        /// <param name="value">
        /// A type of the window
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> or <paramref name="value"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when the value with key already has been registered
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws when <paramref name="value"/> can not be registered.
        /// </exception>
        public void Registrate(string key, Type value)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.NAVIGATION_MANAGER_REGISTRATE_BY_THE_SAME_KEY_FORMAT, nameof(key)));
            // value
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.IsInterface || value.IsAbstract) throw new ArgumentException(nameof(value));

            // registrate type
            factory.Add(key, value);
        }
        /// <summary>
        /// Unregistrates type by current key
        /// </summary>
        /// <param name="key">
        /// A key by which Type was registered
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when factory does not contain current key
        /// </exception>
        public void UnRegistrate(string key)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (!factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.NAVIGATION_MANAGER_NO_SUCH_KEY_FORMAT, nameof(key)));

            // unregistrate
            factory.Remove(key);
        }
        #endregion
        // history
        #region manage history
        /// <summary>
        /// Remove last instance from the history
        /// </summary>
        public void PopHistory()
        {
            if (history.Count > 0) history.Pop();
        }
        /// <summary>
        /// Clear the history compleatly
        /// </summary>
        public void ClearHistory()
        {
            history.Clear();
        }
        /// <summary>
        /// Gets previous instance of user control that was created
        /// </summary>
        /// <returns>
        /// User control is instance was created, otherwise — null
        /// </returns>
        public UserControl PreviousInstance()
        {
            if (history.Count <= 0) return null;

            // gets value
            return (UserControl)Activator.CreateInstance(history.Peek());
        }
        #endregion
        // navigation
        #region navigation
        /// <summary>
        /// Shown in parent control a registered user control
        /// </summary>
        /// <param name="parent">
        /// A parent control in which current user control should be shown
        /// </param>
        /// <param name="key">
        /// A key by which user control was registered.
        /// </param>
        /// <param name="viewModel">
        /// A DataContext for user control
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public void NavigateTo(ContentControl parent, string key, object viewModel)
        {
            // sets to parent control navigate control value
            parent.Content = NavigateTo(key, viewModel);
        }
        /// <summary>
        /// Returns user control with setted DataContext
        /// </summary>
        /// <param name="key">
        /// A key by which user control was registered.
        /// </param>
        /// <param name="viewModel">
        /// A DataContext for user control
        /// </param>
        /// <returns>
        /// An instance of <see cref="UserControl"/> with setted view model
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public UserControl NavigateTo(string key, object viewModel)
        {
            // create child control
            UserControl userControl = MakeInstance(key);
            // sets viewModel to it
            userControl.DataContext = viewModel;

            return userControl;
        }
        /// <summary>
        /// Shown in parent control a previous registered user control
        /// </summary>
        /// <param name="parent">
        /// A parent control in which current user control should be shown
        /// </param>
        /// <param name="viewModel">
        /// A DataContext for user control
        /// </param>
        public void NavigateToPrevious(ContentControl parent, object viewModel)
        {
            // sets to current control previous value 
            parent.Content = NavigateToPrevious(viewModel); // null or user control
        }
        /// <summary>
        /// Returns previous user control with setted DataContext
        /// </summary>
        /// <param name="viewModel">
        /// A DataContext for user control
        /// </param>
        /// <returns>
        /// An instance of previous <see cref="UserControl"/> with setted view model
        /// </returns>
        public UserControl NavigateToPrevious(object viewModel)
        {
            // get previous control
            UserControl userControl = PreviousInstance();
            // sets viewModel to it
            if (userControl != null) userControl.DataContext = viewModel;

            // remove it from history
            PopHistory();

            return userControl;
        }
        #endregion
    }
}
