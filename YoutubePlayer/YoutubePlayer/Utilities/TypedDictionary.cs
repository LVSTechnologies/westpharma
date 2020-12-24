using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YoutubePlayer.ViewModels
{
    public abstract class TypedDictionary
    {
        protected virtual IDictionary<string, object> Backing { get; set; } = new Dictionary<string, object>();

        protected T Get<T>([CallerMemberName] string name = null)
        {

            object value;
            if (Backing.TryGetValue(name, out value))
                return (T)value;
            return default(T);
        }

        protected void SetReference<U>(U value,
            Action onChanged = null,
            Action<U> onChanging = null,
            [CallerMemberName] string name = null)
        {
            U backing = Get<U>(name);
            if (!Set(backing, value, onChanging, name))
                return;

            Backing[name] = value;

            Set(onChanged, name);
        }

        protected void SetValue<U>(
            ref U backingStore, U value,
            Action onChanged = null,
            Action<U> onChanging = null,
            [CallerMemberName] string name = null)
        {
            if (!Set(backingStore, value, onChanging, name))
                return;

            backingStore = value;

            Set(onChanged, name);
        }

        protected virtual bool Set<U>(U backing, U value, Action<U> onChanging, string name)
        {
            if (EqualityComparer<U>.Default.Equals(backing, value))
                return false;

            onChanging?.Invoke(value);

            return true;
        }

        protected virtual void Set(Action onChanged, string name)
        {
            onChanged?.Invoke();
        }

        protected virtual void ClearReferenceProperties()
        {
            Backing.Clear();
        }
    }
}
