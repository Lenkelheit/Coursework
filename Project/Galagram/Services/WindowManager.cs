namespace Galagram.Services
{
    public class WindowManager : Core.Interfaces.IFactory<string, System.Type, System.Windows.Window>
    {
        public System.Windows.Window MakeInstance(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Registrate(string key, System.Type value)
        {
            throw new System.NotImplementedException();
        }

        public void UnRegistrate(string key)
        {
            throw new System.NotImplementedException();
        }
        // WINDOW
        public void ShowWindow(string key)
        {
            throw new System.NotImplementedException();
        }
        public void ShowWindow(string key, object viewModel)
        {
            throw new System.NotImplementedException();
        }
        // MESSAGE BOX
        public void ShowMessageWindow(string text)
        {
            throw new System.NotImplementedException();
        }
        public void ShowMessageWindow(string text, string header)
        {
            throw new System.NotImplementedException();
        }
        public void ShowMessageWindow(string text, string header, MessageBoxButton buttonType)
        {
            throw new System.NotImplementedException();
        }
    }
}
