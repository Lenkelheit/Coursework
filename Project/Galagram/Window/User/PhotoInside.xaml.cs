namespace Galagram.Window.User
{
    /// <summary>
    /// Interaction logic for PhotoInside.xaml
    /// </summary>
    public partial class PhotoInside : System.Windows.Window
    {
        public PhotoInside()
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
