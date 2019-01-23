using System.Linq;

namespace Galagram.ViewModel.Commands.User.Search
{
    /// <summary>
    /// Search user by passed text, shown all founded values
    /// </summary>
    public class SearchCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.SearchViewModel searchViewModel;

        // EVENT
        /// <summary>
        /// Occurs when state of the command has been changed
        /// </summary>
        public override event System.EventHandler CanExecuteChanged
        {
            add
            {
                System.Windows.Input.CommandManager.RequerySuggested += value;
            }
            remove
            {
                System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="SearchCommand"/>
        /// </summary>
        /// <param name="searchViewModel">
        /// An instance of <see cref="ViewModel.User.SearchViewModel"/>
        /// </param>
        public SearchCommand(ViewModel.User.SearchViewModel searchViewModel)
        {
            this.searchViewModel = searchViewModel;
        }

        // METHODS
        /// <summary>
        /// Check if command can be executed
        /// <para/>
        /// Can not be executed if search text is empty
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            bool canExecute = !string.IsNullOrWhiteSpace(searchViewModel.Text);
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(SearchCommand)} value = {canExecute}");
            return canExecute;
        }
        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(SearchCommand)}");

            // gets all founded user ordered by nickname, and set them to array
            searchViewModel.Users = searchViewModel.UnitOfWork
                    .UserRepository
                        .Get(filter: user => user.NickName.Contains(searchViewModel.Text), 
                             orderBy: o => o.OrderBy(user => user.NickName))
                                .ToArray();
        }
    }
}
