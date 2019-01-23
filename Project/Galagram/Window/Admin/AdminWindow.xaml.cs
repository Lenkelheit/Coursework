namespace Galagram.Window.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : System.Windows.Window
    {
        /// <summary>
        /// Initialize a new instance of <see cref="AdminWindow"/>
        /// </summary>
        public AdminWindow()
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
        // currently just test method
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainContent.Child = new UserControls.Messages.MessageInside()
            {
                Width = MainContent.Width,
                Height = MainContent.Height,
            };
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            MainContent.Child = new UserControls.DeleteItem()
            {
                Width = MainContent.Width,
                Height = MainContent.Height,
                DeleteItemName = "test"
            };
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            MainContent.Child = null;
        }
    }
}
