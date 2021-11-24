using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PhotoDateEditor.Commands;
using PhotoDateEditor.Utils;
using PhotoDateEditor.Windows;

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
            }
        }

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
        #endregion

        private bool _isSameDateForAll;
        public bool IsSameDateForAll
        {
            get => _isSameDateForAll;
            set
            {
                _isSameDateForAll = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        private ICommand _openImagesDialogCommand;
        public ICommand OpenImagesDialogCommand
        {
            get
            {
                return _openImagesDialogCommand ??
                    (_openImagesDialogCommand = new RealyCommand(obj =>
                    {
                        var dialog = new DialogService();
                        if (dialog.OpenFile())
                        {
                            AddFilesList(dialog.FilesPath);
                        }
                    }));
            }
        }

        private ICommand _openImagesCommand;
        public ICommand OpenImagesCommand
        {
            get
            {
                return _openImagesCommand ??
                    (_openImagesCommand = new RealyCommand(obj =>
                    {
                        string[] fileNames = (string[])obj;
                        AddFilesList(fileNames);
                    }));
            }
        }

        private ICommand _saveSelectImageCommand;
        public ICommand SaveSelectImageCommand
        {
            get
            {
                return _saveSelectImageCommand ??
                    (_saveSelectImageCommand = new RealyCommand(obj =>
                    {
                        try
                        {
                            SelectImage.SaveFile();
                            MessageBox.Show("Файл сохранен", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }, obj => SelectImage != null));
            }
        }

        private ICommand _closeImageCommand;
        public ICommand CloseImageCommand
        {
            get
            {
                return _closeImageCommand ??
                    (_closeImageCommand = new RealyCommand(obj =>
                    {
                        var closeImage = (ImageMetadataViewModel)obj;
                        Images.Remove(closeImage);
                    }));
            }
        }

        private ICommand _openAboutCommand;
        public ICommand OpenAboutCommand
        {
            get
            {
                return _openAboutCommand ??
                    (_openAboutCommand = new RealyCommand(obj =>
                    {
                        new AboutWindow().ShowDialog();
                    }));
            }
        }
        #endregion

        private void AddFilesList(string[] fileNames)
        {
            fileNames
                .Select(x =>
                {
                    try
                    {
                        return new ImageMetadataViewModel(x);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось открыть файл {x}\n{ex.Message}", "Ошибка чтения", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                })
                .ToList()
                .ForEach(x => { if (x != null) Images.Add(x); });
        }
    }
}
