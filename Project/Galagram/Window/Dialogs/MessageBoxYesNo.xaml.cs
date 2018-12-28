namespace Galagram.Window.Dialogs
{
    /// <summary>
    /// Interaction logic for MessageBoxYesNo.xaml
    /// </summary>
    public partial class MessageBoxYesNo : System.Windows.Window, Interfaces.IMessageBox
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="MessageBoxYesNo"/>
        /// </summary>
        public MessageBoxYesNo()
        {
            InitializeComponent();
        }
        // PROPERTIES
        /// <summary>
        /// Set up title for <see cref="MessageBoxYesNo"/>
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
        /// Set up message text for <see cref="MessageBoxYesNo"/>
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
        private void Yes(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        private void No(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
