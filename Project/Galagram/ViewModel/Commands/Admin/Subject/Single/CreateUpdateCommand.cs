namespace Galagram.ViewModel.Commands.Admin.Subject.Single
{
    /// <summary>
    /// Create or update an instance of <see cref="DataAccess.Entities.Subject"/>
    /// </summary>
    public class CreateUpdateCommand : CommandBase
    {
        // FIELDS
        ViewModel.Admin.Subject.SingleViewModel subjectSingleViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="CreateUpdateCommand"/>
        /// </summary>
        /// <param name="subjectSingleViewModel">
        /// An instance of <see cref="ViewModel.Admin.Subject.SingleViewModel"/>
        /// </param>
        public CreateUpdateCommand(ViewModel.Admin.Subject.SingleViewModel subjectSingleViewModel)
        {
            this.subjectSingleViewModel = subjectSingleViewModel;
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Execure {nameof(CreateUpdateCommand)}");

            return true;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// <para/>
        /// True if value should be created, false if updated
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execure {nameof(CreateUpdateCommand)}");

            // get new subject name
            string subjectName = subjectSingleViewModel.SubjectName;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"New subject name = {subjectName}");


            // check if right
            if (subjectName.Length > Core.Configuration.DBConfig.ADMIN_MESSAGE_SUBJECT_MAX_LENGTH ||
                subjectName.Length < Core.Configuration.DBConfig.ADMIN_MESSAGE_SUBJECT_MIN_LENGTH)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Subject name is wrong. Interrupt command executing");

                Services.WindowManager.Instance.ShowMessageWindow(Core.Messages.Info.Admin.ADMIN_WRONG_SUBJECT_LENGTH);
                return;
            }

            // create or update, depend on parameter
            bool isNew = System.Convert.ToBoolean(parameter);
            if (isNew)
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Create new subject");

                DataAccess.Context.UnitOfWork.Instance
                    .SubjectRepository
                        .Insert(
                            new DataAccess.Entities.Subject()
                            {
                                Name = subjectName
                            });

            }
            else
            {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update subject");

                subjectSingleViewModel.Subject.Name = subjectName;

                DataAccess.Context.UnitOfWork.Instance
                    .SubjectRepository
                        .Update(subjectSingleViewModel.Subject);
            }

            // save changes
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes");
            DataAccess.Context.UnitOfWork.Instance.Save();
            
            // go back to all items
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Go to previous content");
            Services.NavigationManager.Instance.NavigateToPrevious(Services.DataStorage.Instance.AdminWindowContentControl);
        }
    }
}
