namespace Galagram.Window.Admin.UserControls
{
    /// <summary>
    /// Interaction logic for DeleteItem.xaml
    /// </summary>
    public partial class DeleteItem : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Initialize a new instance of <see cref="DeleteItem"/>
        /// </summary>
        public DeleteItem()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets or sets delete item name
        /// </summary>
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
