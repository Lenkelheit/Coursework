namespace Galagram.Window.Dialogs
{
    /// <summary>
    /// Interaction logic for MessageBoxOk.xaml
    /// </summary>
    public partial class MessageBoxOk : System.Windows.Window, Interfaces.IMessageBox
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="MessageBoxOk"/>
        /// </summary>
        public MessageBoxOk()
        {
            InitializeComponent();
        }
        // PROPERTIES
        /// <summary>
        /// Set up title for <see cref="MessageBoxOk"/>
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
        /// <summary>
        /// Set up message text for <see cref="MessageBoxOk"/>
        /// </summary>
        public string Text
        {
            get
            {
                return ContentTb.Text;
            }
            set
            {
                ContentTb.Text = value;
            }
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
