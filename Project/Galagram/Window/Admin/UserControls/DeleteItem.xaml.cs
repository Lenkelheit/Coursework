namespace Galagram.Window.Admin.UserControls
{
    /// <summary>
    /// Interaction logic for DeleteItem.xaml
    /// </summary>
    public partial class DeleteItem : System.Windows.Controls.UserControl
    {
        public DeleteItem()
        {
            InitializeComponent();
        }
        public string DeleteItemName
        {
            get
            {
                return DeleteItemLbl.Text;
            }
            set
            {
                DeleteItemLbl.Text = value;
            }
        }
    }
}
