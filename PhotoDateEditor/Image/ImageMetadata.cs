using ExifLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoDateEditor.Image
{
    class ImageMetadata
    {
        public ImageFile Metadata { get; private set; }
        public string PathToFile { get; private set; }

        public string FileName
        {
            get
            {
                return Path.GetFileName(PathToFile);
            }
        }

        public DateTime CreateDateTime
        {
            get
            {
                return Metadata.Properties.Get<ExifDateTime>(ExifTag.DateTimeOriginal);
            }
        }


        public ImageMetadata(string pathToFile)
        {
            Metadata = ImageFile.FromFile(pathToFile);
            PathToFile = pathToFile;
        }

        
    }
}
