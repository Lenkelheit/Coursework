namespace Galagram.Window.User
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : System.Windows.Window
    {
        public Setting()
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
