using System;

namespace Galagram.ViewModel.ViewModel.Admin.Services
{
    /// <summary>
    /// Saves view model type by menu items
    /// </summary>
    public class MenuItemViewModelFactory : Core.Interfaces.IFactory<string, Type, ViewModelBase>
    {
        // FIELDS
        System.Collections.Generic.IDictionary<string, Type> factory;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="MenuItemViewModelFactory"/>
        /// </summary>
        public MenuItemViewModelFactory()
        {
            factory = new System.Collections.Generic.Dictionary<string, Type>();
        }

        // METHODS
        /// <summary>
        /// Returns a new instance of a view model.
        /// </summary>
        /// <param name="key">
        /// A key by which view model was registered.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ViewModelBase"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws when key was not registered before.
        /// </exception>
        public ViewModelBase MakeInstance(string key)
        {
            // checks
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (!factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.Admin.FACTORY_NO_SUCH_KEY_FORMAT, key));

            // make instance
            return Activator.CreateInstance(factory[key]) as ViewModelBase;
        }
        /// <summary>
        /// Registrates a type of the view model by current key.
        /// </summary>
        /// <param name="key">
        /// A key by which type is registered.
        /// </param>
        /// <param name="value">
        /// A type of the view model
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
            if (factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.Admin.FACTORY_REGISTRATE_BY_THE_SAME_KEY_FORMAT, key));
            // value
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.IsInterface || value.IsAbstract) throw new ArgumentException(nameof(value));
            
            // adding
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
            if (!factory.ContainsKey(key)) throw new InvalidOperationException(string.Format(Core.Messages.Error.View.NAVIGATION_MANAGER_NO_SUCH_KEY_FORMAT, key));
            
            // removing 
            factory.Remove(key);
        }
    }
}
