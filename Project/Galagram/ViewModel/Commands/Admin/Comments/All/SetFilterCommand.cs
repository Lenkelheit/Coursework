using static DataAccess.Filters.CommentFilter;

namespace Galagram.ViewModel.Commands.Admin.Comments.All
{
    /// <summary>
    /// Filter <see cref="DataAccess.Entities.Comment"/> on view
    /// </summary>
    public class SetFilterCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.Comments.AllViewModel allCommentsViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SetFilterCommand"/>
        /// </summary>
        /// <param name="allCommentsViewModel">
        /// An instance of <see cref="ViewModel.Admin.Comments.AllViewModel"/>
        /// </param>
        public SetFilterCommand(ViewModel.Admin.Comments.AllViewModel allCommentsViewModel)
        {
            this.allCommentsViewModel = allCommentsViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(SetFilterCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(SetFilterCommand)}");

            // sets filter
            allCommentsViewModel.Filter = CommentFilter;
        }

        private bool CommentFilter(object comment)
        {
            DataAccess.Entities.Comment commentToFilter = (DataAccess.Entities.Comment)comment;
            bool isShown = true;

            // checks text
            string textSubstring = allCommentsViewModel.Text;
            if (textSubstring != null)
            {
                isShown &= Has(comment: commentToFilter, textSubstring: textSubstring);
            }

            // checks date
            isShown &= Where(commentToFilter, allCommentsViewModel.From, allCommentsViewModel.To);

            // checks user nickname
            string nicknameSubstring = allCommentsViewModel.UserNickname;
            if (nicknameSubstring != null)
            {
                isShown &= Where(commentToFilter, nicknameSubstring);
            }

            return isShown;
        }
    }
}
