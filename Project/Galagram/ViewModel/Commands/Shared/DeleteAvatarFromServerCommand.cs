namespace Galagram.ViewModel.Commands.Shared
{
    /// <summary>
    /// Deletes avatar from server
    /// <para/>
    /// Implements State pattern
    /// </summary>
    public class DeleteAvatarFromServerCommand : CommandBase
    {
        // INNER CLASSSES
        private class DeleteByArguments : CommandBase
        {
            readonly string avatarPath;
            public DeleteByArguments(string avatarPath)
            {
                this.avatarPath = avatarPath;
            }
            public override bool CanExecute(object parameter)
            {
                return true;
            }

            public override void Execute(object parameter)
            {
                if (!string.IsNullOrEmpty(avatarPath))
                {
                    System.IO.File.Delete(avatarPath);
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

                if (!string.IsNullOrEmpty(user.MainPhotoPath))
                {
                    System.IO.File.Delete(user.MainPhotoPath);
                }
            }
        }

        // FIELDS
        readonly CommandBase baseCommand;
        
        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteAvatarFromServerCommand"/>
        /// </summary>
        /// <param name="avatarPath">
        /// A path to avatar that should be deleted
        /// </param>
        public DeleteAvatarFromServerCommand(string avatarPath = null)
        {
            if (!string.IsNullOrEmpty(avatarPath)) baseCommand = new DeleteByArguments(avatarPath);
            else                                   baseCommand = new DeleteByParameters();
        }

        // METHODS
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(DeleteAvatarFromServerCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(DeleteAvatarFromServerCommand)}");

            baseCommand.Execute(parameter);
        }
    }
}
