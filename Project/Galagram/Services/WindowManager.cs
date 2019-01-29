using System;
using System.Collections.Generic;

using Galagram.Window.Interfaces;
using Galagram.Window.Dialogs;
using Galagram.Window.Enums;
using Galagram.Window.User;

namespace Galagram.Services
{
    /// <summary>
    /// Provides algorithms with window.
    /// <para />
    /// Implements a Factory pattern.
    /// <para />
    /// Implements a Singleton pattern
    /// </summary>
    public class WindowManager : Core.Interfaces.IFactory<string, Type, System.Windows.Window>
    {
        // FIELDS
        static WindowManager instance; // singleton
        static WindowManagerInitializers.WindowManagerInitializerBase initializerBase;

        IDictionary<string, Type> factory; // a factory has string as a key and WindowType as a value

        // CONSTRUCTORS
        private WindowManager()
        {
            // initialize all fields
            factory = new Dictionary<string, Type>();

            // registrate default window
            initializerBase?.Initialize(this);
        }
        static WindowManager()
        {
            // initialize window initializer
            initializerBase = new WindowManagerInitializers.DefaultWindowInitializers();

            // initialize singleton value
            instance = new WindowManager();
        }

        // PROPERTIES
        /// <summary>
        /// Gets an instance of <see cref=" WindowManager"/>.
        /// </summary>
        public static WindowManager Instance => instance;

        // METHODS
        /// <summary>
        /// Returns a new instance of a window.
        /// </summary>
        /// <param name="key">
        /// A key by which window was registered.
        /// </param>
        /// <returns>
        /// An instance of <see cref="System.Windows.Window"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public System.Windows.Window MakeInstance(string key)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (!factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.WINDOW_MANAGER_NO_SUCH_KEY_FORMAT, nameof(key)));

            // return window instance created by current type extracted by key
            return (System.Windows.Window)Activator.CreateInstance(factory[key]);
        }
        /// <summary>
        /// Registrates a type of the window by current key.
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
        /// Throws when the key already has been registered
        /// </exception>
        /// <exception cref="ArithmeticException">
        /// Throws when <paramref name="value"/> can not be registered.
        /// </exception>
        public void Registrate(string key, Type value)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.WINDOW_MANAGER_REGISTRATE_BY_THE_SAME_KEY_FORMAT, nameof(key)));
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
            if (!factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.WINDOW_MANAGER_NO_SUCH_KEY_FORMAT, nameof(key)));

            // unregistrate
            factory.Remove(key);
        }
        // WINDOW
        /// <summary>
        /// Opens a window and returns only when a newly opened window is closed.
        /// </summary>
        /// <param name="key">
        /// A key by which window was registered.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>    
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public bool? ShowWindowDialog(string key)
        {
            return ShowWindowDialog(key, null);
        }
        /// <summary>
        /// Opens a window and returns only when a newly opened window is closed.
        /// </summary>
        /// <param name="key">
        /// A key by which window was registered.
        /// </param>
        /// <param name="viewModel">
        /// A DataContext for window.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>    
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public bool? ShowWindowDialog(string key, object viewModel)
        {
            // create window
            System.Windows.Window window = MakeInstance(key);
            // set view model
            window.DataContext = viewModel;
            // show window
            return window.ShowDialog();            
        }
        // MESSAGE BOX
        /// <summary>
        /// Open a message box window and returns only when a newly opened window is closed.
        /// </summary>
        /// <param name="text">
        /// Specify the text of the window.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>    
        /// <exception cref="InvalidOperationException">
        /// Throws when message box is not registered.
        /// </exception>
        public bool? ShowMessageWindow(string text)
        {
            return ShowMessageWindow(text, String.Empty, MessageBoxButton.Ok);
        }
        /// <summary>
        /// Open a message box window and returns only when a newly opened window is closed.
        /// </summary>
        /// <param name="text">
        /// Specify the text of the window.
        /// </param>
        /// <param name="header">
        /// Specify the header text of the window.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>    
        /// <exception cref="InvalidOperationException">
        /// Throws when message box is not registered.
        /// </exception>
        public bool? ShowMessageWindow(string text, string header)
        {
            return ShowMessageWindow(text, header, MessageBoxButton.Ok);
        }
        /// <summary>
        /// Open a message box window and returns only when a newly opened window is closed.
        /// </summary>
        /// <param name="text">
        /// Specify the text of the window.
        /// </param>
        /// <param name="header">
        /// Specify the header text of the window.
        /// </param>
        /// <param name="buttonType">
        /// Specify what buttons will be on the window.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>    
        /// <exception cref="InvalidOperationException">
        /// Throws when message box is not registered.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Throws when registered dialog does not inherit default interface
        /// </exception>
        public bool? ShowMessageWindow(string text, string header, MessageBoxButton buttonType)
        {
            // make default instance             
            IMessageBox messageBox = MakeInstance(nameof(MessageBox)) as IMessageBox;            
            // throw exception if not the message box
            if (messageBox == null) throw new InvalidCastException(string.Concat(Core.Messages.Error.View.WINDOW_MANAGER_DIALOG_DOES_NOT_INHERIT_DEFAULT_INTERFACE_FORMAT, nameof(IMessageBox)));
            
            // set up all values
            messageBox.Header = header;
            messageBox.Text = text;

            // show window and return result
            return messageBox.ShowDialog(buttonType);             
        }
        /// <summary>
        /// Switch current main window to passed one.
        /// </summary>
        /// <param name="key">
        /// A key by which window was registered.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public void SwitchMainWindow(string key)
        {
            SwitchMainWindow(key, null);
        }
        /// <summary>
        /// Switch current main window to passed one.
        /// </summary>
        /// <param name="key">
        /// A key by which window was registered.
        /// </param>
        /// <param name="viewModel">
        /// A DataContext for window.
        /// </param>
        /// <param name="doCloseAllWindow">
        /// Determines if need to close all windows
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public void SwitchMainWindow(string key, object viewModel, bool doCloseAllWindow = false)
        {
            // get current main window
            System.Windows.Window oldMainWindow = App.Current.MainWindow;

            // get new window by key
            System.Windows.Window newMainWindow = MakeInstance(key);
            newMainWindow.DataContext = viewModel;

            // set it as a new main window
            App.Current.MainWindow = newMainWindow;

            // switch windows
            if (doCloseAllWindow)
            {
                // close all opened window
                foreach (System.Windows.Window window in App.Current.Windows)
                {
                    if (window != newMainWindow) window.Close();
                }
            }
            else
            {
                // close only current main window
                oldMainWindow.Close();
            }
            newMainWindow.ShowDialog();
        }
    }
}
