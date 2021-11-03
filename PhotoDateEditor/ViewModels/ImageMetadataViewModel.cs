using System;
using System.IO;
using PhotoDateEditor.Image;
using PhotoDateEditor.Utils;

namespace PhotoDateEditor.ViewModels
{
    public class ImageMetadataViewModel : AbstractPropertyChangedClass
    {
        private readonly ImageMetadata _metadata;

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set
            {
                _isModified = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ViewFileName));
            }
        }

        public string PathToFile { get => _metadata.PathToFile; }
        public string FileName { get => Path.GetFileName(PathToFile); }
        public string ViewFileName
        {
            get
            {
                if (IsModified)
                    return $"*{FileName}";
                else
                    return FileName;
            }
        }

        public DateTime? CreateDateTime
        {
            get => _metadata.CreateDateTime;
            set
            {
                _metadata.CreateDateTime = value;
                IsModified = true;
                OnPropertyChanged();
            }
        }
        public DateTime? ModifyImageDateTime
        {
            get => _metadata.ModifyImageDateTime;
            set
            {
                _metadata.ModifyImageDateTime = value;
                IsModified = true;
                OnPropertyChanged();
            }
        }
        public DateTime? ModifyFileDateTime
        {
            get => _metadata.ModifyFileDateTime;
            set
            {
                _metadata.ModifyFileDateTime = value;
                IsModified = true;
                OnPropertyChanged();
            }
        }

        public ImageMetadataViewModel(ImageMetadata metadata)
        {
            _metadata = metadata;
        }

        public ImageMetadataViewModel(string filename)
        {
            _metadata = new ImageMetadata(filename);
        }

        public void SaveFile()
        {
            IsModified = false;
            _metadata.SaveFile();
        }
    }
}
