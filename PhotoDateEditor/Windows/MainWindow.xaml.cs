using PhotoDateEditor.Image;
using PhotoDateEditor.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            DataContext = new MainWindowViewModel();
        }

        private void PhotosListBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetFormats().Contains(DataFormats.FileDrop))
                return;

            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

            var dataContext = (IImagesViewModel)DataContext;
            if (dataContext.AddImagesCommand.CanExecute(fileNames))
                dataContext.AddImagesCommand.Execute(fileNames);
        }
    }
}
