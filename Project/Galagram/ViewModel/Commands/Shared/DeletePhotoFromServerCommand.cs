namespace Galagram.ViewModel.Commands.Shared
{
    /// <summary>
    /// Deletes photo from server
    /// </summary>
    public class DeletePhotoFromServerCommand : CommandBase
    {
        // FIELDS
        readonly string photoPath;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="DeletePhotoFromServerCommand"/>
        /// </summary>
        /// <param name="photoPath">
        /// Path to photo
        /// </param>
        public DeletePhotoFromServerCommand(string photoPath)
        {
            this.photoPath = photoPath;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execute {nameof(DeletePhotoFromServerCommand)}");

            return true;
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter which command needs to be executed.
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(DeletePhotoFromServerCommand)}");

            System.IO.File.Delete(photoPath);
        }
    }
}
