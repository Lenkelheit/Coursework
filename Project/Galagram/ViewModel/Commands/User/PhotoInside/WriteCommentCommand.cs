namespace Galagram.ViewModel.Commands.User.PhotoInside
{
    /// <summary>
    /// Writes a comment to photo
    /// </summary>
    public class WriteCommentCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.PhotoInsideViewModel photoInsideViewModel;

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
        /// Initializes a new instance of <see cref="WriteCommentCommand"/>
        /// </summary>
        /// <param name="photoInsideViewModel">
        /// An instance of <see cref="ViewModel.User.PhotoInsideViewModel"/>
        /// </param>
        public WriteCommentCommand(ViewModel.User.PhotoInsideViewModel photoInsideViewModel)
        {
            this.photoInsideViewModel = photoInsideViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(WriteCommentCommand)}");

            // get comment text
            string commentText = photoInsideViewModel.CommentText;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Get {nameof(commentText)} with value = {commentText}. Length = {commentText.Length}");

            // validate
            bool canExecute = !string.IsNullOrWhiteSpace(commentText);
            canExecute &= Core.Configuration.DBConfig.COMMENT_TEXT_MIN_LENGTH < commentText.Length;
            canExecute &= Core.Configuration.DBConfig.COMMENT_TEXT_MAX_LENGTH > commentText.Length;

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"{nameof(canExecute)} value = {canExecute}");

            // return
            return canExecute;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(WriteCommentCommand)}");

            // gets comment text
            string commentText = photoInsideViewModel.CommentText;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Get {nameof(commentText)} with value = {commentText}. Length = {commentText.Length}");

            // create comment
            DataAccess.Entities.Comment comment = new DataAccess.Entities.Comment
            {
                Text = commentText,
                User = Services.DataStorage.Instance.LoggedUser.User,
                Photo = photoInsideViewModel.PhotoWrapper.Photo
            };
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Create comment");

            // update view
            photoInsideViewModel.Comments.Add(new DataAccess.Wrappers.CommentWrapper(comment));
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update view");

            // update database
            photoInsideViewModel.UnitOfWork.CommentRepository.Insert(comment);
            photoInsideViewModel.UnitOfWork.Save();
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update data base");

            // clear current comment text
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Clear comment");
            photoInsideViewModel.CommentText = string.Empty;
        }
    }
}
