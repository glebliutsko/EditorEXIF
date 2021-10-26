using PhotoDateEditor.Image;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PhotoDateEditor.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ImageMetadata> Images { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Images = new();
            PhotosListBox.ItemsSource = Images;
        }

        private void PhotosListBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetFormats().Contains(DataFormats.FileDrop))
                return;

            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

            var images = fileNames.Select(x => new ImageMetadata(x)).ToArray();
            foreach (var i in images)
                Images.Add(i);
        }
    }
}
