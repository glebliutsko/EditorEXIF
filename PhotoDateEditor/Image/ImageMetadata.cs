using ExifLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoDateEditor.Image
{
    public class ImageMetadata
    {
        private ImageFile _fileMetadata;
        public string PathToFile { get; }

        public DateTime? CreateDateTime { get; set; }
        public DateTime? ModifyImageDateTime { get; set; }
        public DateTime? ModifyFileDateTime { get; set; }

        public ImageMetadata(string pathToFile)
        {
            PathToFile = pathToFile;
            _fileMetadata = ImageFile.FromFile(PathToFile);

            ReadMetadata();
        }

        private void ReadMetadata()
        {
            CreateDateTime = _fileMetadata.Properties.Get<ExifDateTime>(ExifTag.DateTimeOriginal)?.Value;
            ModifyImageDateTime = _fileMetadata.Properties.Get<ExifDateTime>(ExifTag.DateTimeDigitized)?.Value;
            ModifyFileDateTime = _fileMetadata.Properties.Get<ExifDateTime>(ExifTag.DateTime)?.Value;
        }

        private void SaveProperty(ExifTag tag, DateTime? value)
        {
            if (value == null)
                _fileMetadata.Properties.Remove(tag);
            else
                _fileMetadata.Properties.Set(tag, (DateTime)value);
        }

        public void SaveFile()
        {
            SaveProperty(ExifTag.DateTimeOriginal, CreateDateTime);
            SaveProperty(ExifTag.DateTimeDigitized, ModifyImageDateTime);
            SaveProperty(ExifTag.DateTime, ModifyFileDateTime);

            _fileMetadata.Save(PathToFile);
        }
    }
}