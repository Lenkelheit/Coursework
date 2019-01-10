namespace Galagram.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Registration : System.Windows.Window
    {
        public Registration()
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var db = new DataAccess.Context.AppContext())
            {
                foreach(var item in db.Subjects)
                {
                    System.Console.WriteLine(item.Name);
                }
            }
        }
    }
}
