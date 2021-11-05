using System;
using System.Linq;
using System.Windows;
using PhotoDateEditor.ViewModels;

namespace PhotoDateEditor.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PhotosListBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetFormats().Contains(DataFormats.FileDrop))
                return;

            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

            var dataContext = (MainWindowViewModel)DataContext;
            if (dataContext.OpenImagesCommand.CanExecute(fileNames))
                dataContext.OpenImagesCommand.Execute(fileNames);
        }

        private void CloseWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
