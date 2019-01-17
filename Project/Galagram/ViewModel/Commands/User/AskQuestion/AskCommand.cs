namespace Galagram.ViewModel.Commands.User.AskQuestion
{
    /// <summary>
    /// Sends a message to an admin
    /// <para/>
    /// If message fields are empty, users will get MessageBox
    /// <para/>
    /// If message is send, message text clean up, subject do not
    /// <para/>
    /// User will be notified about succesful message sending
    /// </summary>
    public class AskCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.AskQuestionViewModel askQuestionViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="AskCommand"/>
        /// </summary>
        /// <param name="askQuestionViewModel">
        /// An instance of <see cref="ViewModel.User.AskQuestionViewModel"/>
        /// </param>
        public AskCommand(ViewModel.User.AskQuestionViewModel askQuestionViewModel)
        {
            this.askQuestionViewModel = askQuestionViewModel;
        }
        // MEHTODS
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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(AskCommand)}");

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
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(AskCommand)}");

            // check if fields are empty
            if (!askQuestionViewModel.IsDataValid()) return;
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"All validation has been passed succesfuly");


            // send message 
            DataAccess.Entities.Message message = new DataAccess.Entities.Message
            {
                Subject = askQuestionViewModel.Subjects[askQuestionViewModel.SelectedSubjectIndex],
                Text = askQuestionViewModel.Message,
                User = askQuestionViewModel.User,
            };
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Info, $"Create new message with: Subject: {message.Subject.Name}, Message Length: {message.Text.Length}, Messge: {message.Text}, User NickName: {message.User.NickName}, Date: {message.Date}");

            askQuestionViewModel.UnitOfWork.MessageRepository.Insert(message);
            askQuestionViewModel.UnitOfWork.Save();
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save message to DataBase");

            // reset message text, subject stay the same
            askQuestionViewModel.ResetFields();

            // notified user about succesful message sending
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Notify user about succesful message sending");
            askQuestionViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.User.AskQuestion.Ask.MESSAGE_SENT);
        }
    }
}
