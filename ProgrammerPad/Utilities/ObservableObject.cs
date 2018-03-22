using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProgrammerPad.Utilities
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
