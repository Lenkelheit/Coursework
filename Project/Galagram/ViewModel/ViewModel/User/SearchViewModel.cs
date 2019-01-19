using System.Windows.Input;
using System.ComponentModel;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="Galagram.Window.User.Search"/> window
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
        // FIELDS
        internal DataAccess.Entities.User loggedUser;
        string searchText;
        int selectedUserIndex;
        DataAccess.Entities.User[] foundedUsers;

        ICommand searchCommand;
        ICommand openProfileCommand;
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SearchViewModel"/>
        /// </summary>
        /// <param name="user">
        /// An logged user
        /// </param>
        public SearchViewModel(DataAccess.Entities.User user)
        {
            this.loggedUser = user;
            this.searchText = string.Empty;
            this.selectedUserIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.foundedUsers = null;

            this.searchCommand = new Commands.User.Search.SearchCommand(this);
            this.openProfileCommand = new Commands.User.Search.OpenProfileCommand(this);
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
        /// <summary>
        /// Gets or sets selected user index
        /// </summary>
        public int SelectedUserIndex
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(SelectedUserIndex)} with value {selectedUserIndex}");
                return selectedUserIndex;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedUserIndex)}, old value = {selectedUserIndex}, new value = {value}");
                selectedUserIndex = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets founded user
        /// </summary>
        public DataAccess.Entities.User[] Users
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, "Gets founded user");
                return foundedUsers;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets founded user. Amount {value.Length}");
                foundedUsers = value;
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
        /// <summary>
        /// A logic to open profile with current user id
        /// </summary>
        public ICommand OpenProfileCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(OpenProfileCommand)}");
                return openProfileCommand;
            }
        }
    }
}
