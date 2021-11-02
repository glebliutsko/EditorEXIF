using PhotoDateEditor.Image;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoDateEditor.ViewModels
{
    public interface IImagesViewModel
    {
        public ObservableCollection<ImageMetadataModel> Images { get; set; }
        public ImageMetadataModel SelectImage { get; set; }
        public ICommand AddImagesCommand { get; }
    }
}
