using static DataAccess.Filters.MessageFilter;

namespace Galagram.ViewModel.Commands.Admin.Message.All
{
    /// <summary>
    /// Filter items on view
    /// </summary>
    public class FilterCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.Message.AllViewModel allMessgesViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="FilterCommand"/>
        /// </summary>
        /// <param name="allMessgesViewModel">
        /// An instance of <see cref="ViewModel.Admin.Message.AllViewModel"/>
        /// </param>
        public FilterCommand(ViewModel.Admin.Message.AllViewModel allMessgesViewModel)
        {
            this.allMessgesViewModel = allMessgesViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(FilterCommand)}");

            return true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(FilterCommand)}");

            // sets filter
            allMessgesViewModel.Filter = MessageFilter;
        }

        private bool MessageFilter(object message)
        {
            DataAccess.Entities.Message messageToFilter = (DataAccess.Entities.Message)message;
            bool isShown = true;
            
            // checks subject
            if (allMessgesViewModel.SubjectIndex != Core.Configuration.Constants.WRONG_INDEX)
            {
                isShown &= Where(messageToFilter, allMessgesViewModel.Subjects[allMessgesViewModel.SubjectIndex]);
            }
            // checks date
            isShown &= Where(messageToFilter, allMessgesViewModel.From, allMessgesViewModel.To);

            return isShown;
        }
    }
}
