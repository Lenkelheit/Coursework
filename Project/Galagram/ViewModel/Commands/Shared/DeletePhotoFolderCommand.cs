using static Core.Configuration.AppConfig;

namespace Galagram.ViewModel.Commands.Shared
{
    /// <summary>
    /// Deletes folder with photos for current user
    /// <para/>
    /// Implements State pattern
    /// </summary>
    public class DeletePhotoFolderCommand : CommandBase
    {
        // INNER CLASSES
        private class DeleteByArguments : CommandBase
        {
            readonly string userId;
            public DeleteByArguments(string userId)
            {
                this.userId = userId;
            }

            public override bool CanExecute(object parameter)
            {
                return true;
            }

            public override void Execute(object parameter)
            {
                string directoryToDelete = string.Join(DIRECTORY_SEPARATOR_STR, PHOTOS_SAVE_FOLDER, userId);

                if (System.IO.Directory.Exists(directoryToDelete))
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Delete {directoryToDelete}");

                    System.IO.Directory.Delete(path: directoryToDelete, recursive: true);
                }
            }
        }
        private class DeleteByParameters : CommandBase
        {
            public override bool CanExecute(object parameter)
            {
                return true;
            }
            public override void Execute(object parameter)
            {
                DataAccess.Entities.User user = (DataAccess.Entities.User)parameter;

                string directoryToDelete = string.Join(DIRECTORY_SEPARATOR_STR, PHOTOS_SAVE_FOLDER, user.Id);

                if (System.IO.Directory.Exists(directoryToDelete))
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug | Core.LogMode.Info, $"Delete {directoryToDelete}");

                    System.IO.Directory.Delete(path: directoryToDelete, recursive: true);
                }
            }
        }

        // FIELDS
        CommandBase baseCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="DeletePhotoFolderCommand"/>
        /// </summary>
        /// <param name="userId">
        /// User id
        /// </param>
        public DeletePhotoFolderCommand(string userId = null)
        {
            if (!string.IsNullOrEmpty(userId)) baseCommand = new DeleteByArguments(userId);
            else                               baseCommand = new DeleteByParameters();
        }
        /// <summary>
        /// Checks if command can be executed.
        /// </summary>
        /// <param name="parameter">
        /// The parameter that defines if command can be executed.
        /// </param>
        /// <returns>
        /// True — if command can be executed, otherwise — false.
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(DeletePhotoFolderCommand)}");

            return baseCommand.CanExecute(parameter);
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter which command needs to be executed.
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(DeletePhotoFolderCommand)}");

            baseCommand.Execute(parameter);
        }
    }
}
