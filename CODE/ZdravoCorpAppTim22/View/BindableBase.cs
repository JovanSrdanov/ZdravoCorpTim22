using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM1
{
    //Remembering to raise the event every time you change a property’s value can get very tedious.
    //Because this pattern is so common, many MVVM frameworks provide a base class
    //for your view model classes similar to the following
    public class BindableBase : INotifyPropertyChanged
    {

        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
