namespace Galagram.ViewModel.Commands.Admin.Subject.Single
{
    public class GoBackCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Services.NavigationManager.Instance.NavigateToPrevious(
                parent: Services.DataStorage.Instance.AdminWindowContentControl,
                viewModel: null);
        }
    }
}
