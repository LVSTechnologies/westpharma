using System;
using System.ComponentModel;

namespace YoutubePlayer.ViewModels
{
    public class NotifyingTypedDictionary : TypedDictionary, INotifyPropertyChanged
    {
        protected override bool Set<U>(U backing, U value, Action<U> onChanging, string name)
        {
            if (!base.Set(backing, value, onChanging, name))
                return false;

            OnPropertyChanging(name);
            return true;
        }

        protected override void Set(Action onChanged, string name)
        {
            base.Set(onChanged, name);

            OnPropertyChanged(name);
        }

        #region INotifyPropertyChanging implementation

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging == null)
                return;

            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void ClearReferencePropertiesWithNotify()
        {
            var keys = Backing.Keys;
            ClearReferenceProperties();
            foreach (var key in keys)
            {
                OnPropertyChanged(key);
            }

        }
    }
}
