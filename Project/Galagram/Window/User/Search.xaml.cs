namespace Galagram.Window.User
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : System.Windows.Window
    {
        public Search()
        {
            InitializeComponent();
        }
        private void MovingWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Exit(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
