namespace Galagram.Window.User
{
    /// <summary>
    /// Interaction logic for Follow.xaml
    /// </summary>
    public partial class Follow : System.Windows.Window
    {
        public Follow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Set up title for <see cref="Follow"/>
        /// </summary>
        public string Header
        {
            get
            {
                return HeaderLbl.Content.ToString();
            }
            set
            {
                HeaderLbl.Content = value;
            }
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
