namespace Galagram.ViewModel.Commands.Admin.Subject
{
    /// <summary>
    /// An logic class to create new subject
    /// </summary>
    public class CreateCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.Subject.AllSubjectViewModel subjectViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new new instance of <see cref="ViewModel.Admin.Subject.AllSubjectViewModel"/>
        /// </summary>
        /// <param name="subjectViewModel">
        /// An instance of <see cref="ViewModel.Admin.Subject.AllSubjectViewModel"/>
        /// </param>
        public CreateCommand(ViewModel.Admin.Subject.AllSubjectViewModel subjectViewModel)
        {
            this.subjectViewModel = subjectViewModel;
        }

        // METHODS
        /// <summary>
        /// Check if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(CreateCommand)}");

            return true;
        }
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(CreateCommand)}");

            Services.NavigationManager.Instance.NavigateTo(
                parent: Services.DataStorage.Instance.AdminWindowContentControl,
                key: nameof(Window.Admin.UserControls.Subjects.Single),
                viewModel: new ViewModel.Admin.Subject.SingleViewModel());

        }
    }
}
