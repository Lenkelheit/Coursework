namespace Galagram.Window.Admin.UserControls.Messages
{
    /// <summary>
    /// Interaction logic for MessageInside.xaml
    /// </summary>
    public partial class MessageInside : System.Windows.Controls.UserControl
    {
        public MessageInside()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.Parent as System.Windows.Controls.Border).Child = new UserControls.DeleteItem()
            {
                Width = (Parent as System.Windows.Controls.Border).Width,
                Height = (Parent as System.Windows.Controls.Border).Height,
                DeleteItemName = "#"
            };
        }
    }
}
