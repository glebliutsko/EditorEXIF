using PhotoDateEditor.Commands;
using PhotoDateEditor.Image;
using PhotoDateEditor.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PhotoDateEditor.ViewModels
{
    class MainWindowViewModel : AbstractPropertyChangedClass, IImagesViewModel
    {
        private ObservableCollection<ImageMetadataModel> _images = new();
        public ObservableCollection<ImageMetadataModel> Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        private ImageMetadataModel _selectImage;
        public ImageMetadataModel SelectImage
        {
            get => _selectImage;
            set
            {
                _selectImage = value;
                OnPropertyChanged(nameof(SelectImage));
            }
        }

        private ICommand _addImagesCommand;
        public ICommand AddImagesCommand
        {
            get
            {
                return _addImagesCommand ??
                    (_addImagesCommand = new RealyCommand(obj =>
                    {
                        string[] fileNames = (string[])obj;

                        ImageMetadataModel[] images = fileNames.Select(x => new ImageMetadataModel(x)).ToArray();
                        foreach (var i in images)
                            Images.Add(i);
                    }));
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RealyCommand(obj =>
                    {
                        SelectImage.SaveFile();
                    }, obj => SelectImage != null));
            }
        }
    }
}
