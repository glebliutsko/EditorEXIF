using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhotoDateEditor.Utils
{
    public abstract class AbstractPropertyChangedClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
