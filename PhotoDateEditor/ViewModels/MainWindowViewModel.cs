using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PhotoDateEditor.Commands;
using PhotoDateEditor.Utils;

namespace PhotoDateEditor.ViewModels
{
    public class MainWindowViewModel : AbstractPropertyChangedClass
    {
        #region Property
        private ObservableCollection<ImageMetadataViewModel> _images = new();
        public ObservableCollection<ImageMetadataViewModel> Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        private ImageMetadataViewModel _selectImage;
        public ImageMetadataViewModel SelectImage
        {
            get => _selectImage;
            set
            {
                _selectImage = value;

                _displayedCreateDateTime = value.CreateDateTime;
                OnPropertyChanged(nameof(DisplayedCreateDateTimeView));

                _displayedModifyImageDateTime = value.ModifyImageDateTime;
                OnPropertyChanged(nameof(DisplayedModifyImageDateTime));

                _displayedModifyFileDateTime = value.ModifyFileDateTime;
                OnPropertyChanged(nameof(DisplayedModifyFileDateTime));

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedImage));
                OnPropertyChanged(nameof(IsCanEditAllDate));
            }
        }

        public bool IsSelectedImage { get => SelectImage != null; }

        #region DateProperty
        private DateTime? _displayedCreateDateTime;
        public DateTime? DisplayedCreateDateTimeView
        {
            get => _displayedCreateDateTime;
            set
            {
                _displayedCreateDateTime = value;
                SelectImage.CreateDateTime = value;
                
                if (IsSameDateForAll)
                {
                    DisplayedModifyImageDateTime = value;
                    DisplayedModifyFileDateTime = value;
                }

                OnPropertyChanged();
            }
        }

        private DateTime? _displayedModifyImageDateTime;
        public DateTime? DisplayedModifyImageDateTime
        {
            get => _displayedModifyImageDateTime;
            set
            {
                _displayedModifyImageDateTime = value;
                SelectImage.ModifyImageDateTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _displayedModifyFileDateTime;
        public DateTime? DisplayedModifyFileDateTime
        {
            get => _displayedModifyFileDateTime;
            set
            {
                _displayedModifyFileDateTime = value;
                SelectImage.ModifyFileDateTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsCanEditAllDate { get => !IsSameDateForAll && IsSelectedImage; }
        #endregion

        private bool _isSameDateForAll;
        public bool IsSameDateForAll
        {
            get => _isSameDateForAll;
            set
            {
                _isSameDateForAll = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCanEditAllDate));
            }
        }
        #endregion

        #region Command
        private ICommand _addImagesCommand;
        public ICommand AddImagesCommand
        {
            get
            {
                return _addImagesCommand ??
                    (_addImagesCommand = new RealyCommand(obj =>
                    {
                        string[] fileNames = (string[])obj;

                        ImageMetadataViewModel[] images = fileNames.Select(x => new ImageMetadataViewModel(x)).ToArray();
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
                    }, obj => IsSelectedImage));
            }
        }
        #endregion
    }
}
