using System.Windows.Input;

namespace Galagram.ViewModel.ViewModel.Admin.Subject
{
    /// <summary>
    /// An logic class for <see cref="Window.Admin.UserControls.Subjects.Single"/>
    /// </summary>
    public class SingleViewModel : SingleItemViewModelBase
    {
        // FIELDS
        bool isNew;

        ICommand insertUpdateCommand;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="SingleViewModel"/>
        /// </summary>
        /// <param name="subject">
        /// An instance of <see cref="DataAccess.Entities.Subject"/>
        /// </param>
        /// <param name="isNew">
        /// Determines is this entities is new
        /// </param>
        /// <param name="isEditingEnabled">
        /// Determines is editing allowed
        /// </param>
        public SingleViewModel(DataAccess.Entities.Subject subject, bool isNew, bool isEditingEnabled= true)
            : base(shownEntity: subject, isWritingEnabled: isEditingEnabled)
        {
            this.isNew = isNew;

            insertUpdateCommand = new Commands.Admin.Subject.Single.CreateUpdateCommand(this);

            Logger.LogAsync(Core.LogMode.Debug, $"{nameof(SingleViewModel)} created");
        }

        // PROPERTIES
        /// <summary>
        /// Gets value that determines if current entities is new
        /// </summary>
        public bool IsNew
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Info, $"Gets {nameof(IsNew)} with value = {isNew}");

                return isNew;
            }
        }
        /// <summary>
        /// Gets operation button's name
        /// </summary>
        public override string CrudOperationName
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperationName)}");

                return isNew ? CreateText: EditText;
            }
        }

        // COMMANDS
        /// <summary>
        /// Gets command to insert or update value
        /// </summary>
        public override ICommand CrudOperation
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CrudOperation)}");

                return insertUpdateCommand;
            }
        }
    }
}
