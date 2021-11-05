using Microsoft.Win32;

namespace PhotoDateEditor.Utils
{
    public class DialogService
    {
        public string[] FilesPath { get; private set; }
        public bool OpenFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilesPath = openFileDialog.FileNames;
                return true;
            }

            return false;
        }
    }
}
