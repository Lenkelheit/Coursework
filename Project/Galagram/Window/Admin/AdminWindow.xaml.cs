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
            Services.DataStorage.Instance.AdminWindowContentControl = ContentControl;
        }

        // METHODS
        private void MovingWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
