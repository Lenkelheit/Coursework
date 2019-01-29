using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Galagram.Window.Dialogs
{
    /// <summary>
    /// Interaction logic for DropWindow.xaml
    /// </summary>
    public partial class DropWindow : System.Windows.Window, Interfaces.IFileDialog
    {
        // CONST
        private static readonly string FILTER_REGEX_PATTERN = @"^[a-z]+(,[a-z]+)*$";

        // FIELDS
        Style dropPanelRegular;
        Style dropPanelOnHover;

        bool multiselect;
        string[] fileNames;
        string filter;
        HashSet<string> allowedExtension;

        ObservableCollection<string> uploadedFiles;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="DropWindow"/>
        /// </summary>
        public DropWindow()
        {
            InitializeComponent();

            dropPanelRegular = Application.Current.FindResource("DropPanelRegular") as Style;
            dropPanelOnHover = Application.Current.FindResource("DropPanelOnHover") as Style;

            multiselect = false;
            fileNames = null;
            filter = null;
            allowedExtension = new HashSet<string>();

            uploadedFiles = new ObservableCollection<string>();

            DataContext = this;
        }

        // PROPERTIES
        /// <summary>
        /// Gets uploaded files observeable collection.
        /// <para/>
        /// Has been used to bind on the window
        /// </summary>
        public ObservableCollection<string> UploadedFiles => uploadedFiles;
        /// <summary>
        /// Gets or sets an option indicating whether dialog allows users to select multiple files.
        /// </summary>
        public bool Multiselect
        {
            get
            {
                return multiselect;
            }
            set
            {
                multiselect = value;
            }
        }
        /// <summary>
        /// Gets an array that contains one file name for each selected file.
        /// </summary>
        public string[] FileNames => fileNames;
        /// <summary>
        /// Gets or sets the filter string that determines what types of files are allowed
        /// </summary>
        /// <example>
        /// This is an example of filter string.
        /// <para/>
        /// Without spaces in lowercase and separated with commas
        /// <code>
        /// jpg,gif,png
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentException">
        /// The filter string is invalid.
        /// </exception>
        public string Filter
        {
            get 
            {
                return filter;
            }
            set
            {
                if (!Regex.IsMatch(input: value, pattern: FILTER_REGEX_PATTERN))
                {
                    throw new System.ArgumentException(Core.Messages.Error.View.DROP_WINDOW_WRONG_FILTER_STRING);
                }

                filter = value;

                // reset extension allowed
                allowedExtension.Clear();
                foreach (string extension in filter.Split(',').Select(ext => '.' + ext))
                {
                    allowedExtension.Add(extension);
                }
            }
        }

        // METHODS
        // window
        #region window option
        private void MovingWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Exit(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        #endregion

        // drag and drop
        #region drag and drop
        private void CornerControl_DragEnter(object sender, DragEventArgs e)
        {
            dropPanel.Style = dropPanelOnHover;
        }

        private void CornerControl_DragLeave(object sender, DragEventArgs e)
        {
            dropPanel.Style = dropPanelRegular;
        }

        private void CornerControl_DragOver(object sender, DragEventArgs e)
        {
            // checks if files with current extension are allowed
            bool dropEnabled = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                foreach (string filePath in e.Data.GetData(DataFormats.FileDrop, true) as string[])
                {
                    if (!allowedExtension.Contains(System.IO.Path.GetExtension(filePath).ToLowerInvariant()))
                    {
                        dropEnabled = false;
                        break;
                    }

                }
            }
            else
            {
                dropEnabled = false;
            }

            if (!dropEnabled)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }
        private void CornerControl_Drop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop, true) as string[];

            if (multiselect == true) // add multiple files
            {
                foreach (string filePath in files)
                {
                    uploadedFiles.Add(filePath);
                }
            }
            else // add single last file
            {
                uploadedFiles.Clear();
                uploadedFiles.Add(files.Last());
            }

            // reset styles
            dropPanel.Style = dropPanelRegular;
        }
        private void Upload(object sender, RoutedEventArgs e)
        {
            fileNames = uploadedFiles.Where(filePath => System.IO.File.Exists(filePath)).ToArray();

            DialogResult = true;
            Close();
        }
        #endregion
    }
}
