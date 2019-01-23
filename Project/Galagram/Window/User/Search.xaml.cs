namespace Galagram.Window.User
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : System.Windows.Window
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="Search"/>
        /// </summary>
        public Search()
        {
            InitializeComponent();
        }

        // METHODS
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
