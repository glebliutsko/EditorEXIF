using System.Collections.ObjectModel;
using System.Windows.Input;
using PhotoDateEditor.Image;

namespace PhotoDateEditor.ViewModels
{
    public interface IImagesViewModel
    {
        public ObservableCollection<ImageMetadataModel> Images { get; set; }
        public ImageMetadataModel SelectImage { get; set; }
        public ICommand AddImagesCommand { get; }
    }
}
