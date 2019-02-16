using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="Galagram.Window.User.Search"/> window
    /// </summary>
    public class SearchViewModel : OpenProfileViewModelBase
    {
        // FIELDS
        string searchText;

        ICommand searchCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SearchViewModel"/>
        /// </summary>
        public SearchViewModel() : base()
        {
            this.searchText = string.Empty;

            this.searchCommand = new Commands.User.Search.SearchCommand(this);
        }
        // PROPERTIES
        /// <summary>
        /// Gets or sets search text
        /// </summary>
        public string Text
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets search text with value {searchText}");
                return searchText;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets new search text. Old value = {searchText}, new value = {value}");
                searchText = value;
                OnPropertyChanged();
            }
        }
        // COMMANDS
        /// <summary>
        /// A logic to show user by passed pattern
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SearchCommand)}");
                return searchCommand;
            }
        }
    }
}
