using System;
using System.Collections.Generic;

using Galagram.Window.Dialogs;
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
        IDictionary<string, Type> factory; // a factory has string as a key and WindowType as a value
        IDictionary<string, System.Windows.Window> modalWindows; // all currently opened modal windows
        IDictionary<object, System.Windows.Window> presentationWindow; // not modal window

        // CONSTRUCTORS
        private WindowManager()
        {
            // initialize all fields
            factory = new Dictionary<string, Type>();
            presentationWindow = new Dictionary<object, System.Windows.Window>();
            modalWindows = new Dictionary<string, System.Windows.Window>();

            // registrate all windows
            // registrate main window
            Registrate(nameof(Window.Registration), typeof(Window.Registration));
            // registrate dialogs
            Registrate(nameof(MessageBox), typeof(MessageBox));
            // registrate user windows
            Registrate(nameof(AskQuestion), typeof(AskQuestion));
            Registrate(nameof(Follow), typeof(Follow));
            Registrate(nameof(MainWindow), typeof(MainWindow));
            Registrate(nameof(PhotoInside), typeof(PhotoInside));
            Registrate(nameof(Search), typeof(Search));
            Registrate(nameof(Setting), typeof(Setting));
        }
        static WindowManager()
        {
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

            // save modal window to dictionary
            modalWindows.Add(key, window);

            // show window
            return window.ShowDialog();
        }
        /// <summary>
        /// Closes a window opened as modal
        /// </summary>
        /// <param name="key">
        /// A key by which window was opened.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when window is not opened.
        /// </exception>
        public void CloseModalWindow(string key)
        {
            // checking
            if (string.IsNullOrWhiteSpace(key)) throw new System.ArgumentNullException(key);

            // try get window
            System.Windows.Window openedModalWindow;
            if (modalWindows.TryGetValue(key, out openedModalWindow) == false)
            {
                // window is not opened
                // or is not opened as modal
                throw new InvalidOperationException(Core.Messages.Error.View.WINDOW_MANAGER_MODAL_WINDOW_IS_NOT_OPENED);
            }

            // close window or do nothing
            openedModalWindow.Close();

            // remove window from dictionary
            modalWindows.Remove(key);
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
        public bool? ShowMessageWindow(string text, string header, MessageBoxButton buttonType)
        {
            // show window and return result
            return new MessageBox()
            {
                // set up all values
                Text = text,
                Header = header
            }.ShowDialog(buttonType);
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

                // clear all modal window list
                modalWindows.Clear();
                // clear presentation window list
                presentationWindow.Clear();
            }
            else
            {
                // close only current main window
                oldMainWindow.Close();
            }
            newMainWindow.ShowDialog();
        }

        // PRESENTATION        
        /// <summary>
        /// Shows window as presentation
        /// </summary>
        /// <param name="key">
        /// A key by which window was registered
        /// </param>
        /// <param name="viewModel">
        /// A DataContext for window
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> or <paramref name="viewModel"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public void ShowPresentation(string key, object viewModel)
        {
            // check, key is checked in MakeInstance
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            // create window
            System.Windows.Window window = MakeInstance(key);
            // set view model
            window.DataContext = viewModel;

            // add it to window
            presentationWindow.Add(viewModel, window);

            // show it
            window.Show();
        }
        /// <summary>
        /// Close opened as presentation window 
        /// </summary>
        /// <param name="viewModel">
        /// A DataContext for window
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="viewModel"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when window with current DataContext is not shown
        /// </exception>
        public void ClosePresentation(object viewModel)
        {
            // check
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            // try get opened window. throws exception if window is not shwon
            System.Windows.Window openedWindow;
            if (!presentationWindow.TryGetValue(viewModel, out openedWindow))
            {
                throw new InvalidOperationException(Core.Messages.Error.View.WINDOW_MANAGER_WINDOW_IS_NOT_OPENED);
            }
            // remove opened window from list
            presentationWindow.Remove(viewModel);
            // close this window
            openedWindow.Close();
        }
    }
}