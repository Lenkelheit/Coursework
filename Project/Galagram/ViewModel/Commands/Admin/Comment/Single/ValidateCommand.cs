namespace Galagram.ViewModel.Commands.Admin.Comment.Single
{
    /// <summary>
    /// Validates an instance of <see cref="DataAccess.Entities.Comment"/>
    /// </summary>
    public class ValidateCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.Comments.SingleViewModel commentSingleViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="ValidateCommand"/>
        /// </summary>
        /// <param name="commentSingleViewModel">
        /// An instance of <see cref="ViewModel.Admin.Comments.SingleViewModel"/>
        /// </param>
        public ValidateCommand(ViewModel.Admin.Comments.SingleViewModel commentSingleViewModel)
        {
            this.commentSingleViewModel = commentSingleViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// <para/>
        /// Can not be executed if comment text is empty
        /// <para/>
        /// Or if comment text has wrong length
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(ValidateCommand)}");

            // get comment text
            string commentText = ((DataAccess.Entities.Comment)commentSingleViewModel.ShownEntity).Text;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Get {nameof(commentText)} with value = {commentText}. Length = {commentText.Length}");

            // validate
            bool canExecute = !string.IsNullOrWhiteSpace(commentText);
            canExecute &= Core.Configuration.DBConfig.COMMENT_TEXT_MIN_LENGTH <= commentText.Length;
            canExecute &= Core.Configuration.DBConfig.COMMENT_TEXT_MAX_LENGTH >= commentText.Length;

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"{nameof(canExecute)} value = {canExecute}");

            // return
            return canExecute;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(ValidateCommand)}");
        }
    }
}
