using PhotoDateEditor.Utils;
using System;
using System.IO;

namespace PhotoDateEditor.Image
{
    public class ImageMetadataModel : AbstractPropertyChangedClass
    {
        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set
            {
                _isModified = value;
                OnPropertyChanged(nameof(IsModified));
                OnPropertyChanged(nameof(ViewFileName));
            }
        }

        private ImageMetadata _metadata;

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
                OnPropertyChanged(nameof(CreateDateTime));
            }
        }
        public DateTime? ModifyImageDateTime
        {
            get => _metadata.ModifyImageDateTime;
            set
            {
                _metadata.ModifyImageDateTime = value;
                IsModified = true;
                OnPropertyChanged(nameof(ModifyImageDateTime));
            }
        }
        public DateTime? ModifyFileDateTime
        {
            get => _metadata.ModifyFileDateTime;
            set
            {
                _metadata.ModifyFileDateTime = value;
                IsModified = true;
                OnPropertyChanged(nameof(ModifyFileDateTime));
            }
        }

        public ImageMetadataModel(ImageMetadata metadata)
        {
            _metadata = metadata;
        }

        public ImageMetadataModel(string filename)
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
