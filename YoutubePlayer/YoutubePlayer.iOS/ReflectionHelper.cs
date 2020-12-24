using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using YoutubePlayer.Common;

[assembly: Dependency(typeof(YoutubePlayer.iOS.ReflectionHelper))]
namespace YoutubePlayer.iOS
{
    [ExcludeFromCodeCoverage]
    public class ReflectionHelper : IReflection
    {
        private Dictionary<Type, IList<PropertyInfo>> _propertyInfo = new Dictionary<Type, IList<PropertyInfo>>();
        private Dictionary<Type, IList<Type>> _assignableTypes = new Dictionary<Type, IList<Type>>();
        private Assembly[] _types;

        public Assembly[] Assemblies()
        {
            if (_types == null)
                _types = AppDomain.CurrentDomain
                    .GetAssemblies();
            return _types;
        }

        public IList<Type> AssignableTypes<T>()
        {
            return AssignableTypes(typeof(T));
        }
        public IList<Type> AssignableTypes(Type type)
        {
            if (!_assignableTypes.TryGetValue(type, out IList<Type> types))
            {
                types = Assemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.IsClass && !t.IsAbstract && type.IsAssignableFrom(t)).ToList();
                _assignableTypes[type] = types;
            }
            return types;
        }

        public Type GetBaseType(Type t)
        {
            return t.BaseType;
        }

        public IEnumerable<ITypeWithAttribute<T>> AssignableTypeswithAtrribute<T, U>() where T : Attribute
        {
            return AssignableTypes<U>().Select(x => new TypeWithAttribute<T> { ModelType = x, Attribute = x.GetCustomAttribute<T>() }).Where(p => p.Attribute != null);
        }
        public IList<PropertyInfo> Properties(Type type)
        {
            if (!_propertyInfo.TryGetValue(type, out IList<PropertyInfo> propertyInfo))
            {
                propertyInfo = type.GetRuntimeProperties().ToArray();
                _propertyInfo[type] = propertyInfo;
            }
            return propertyInfo;
        }

        public IEnumerable<PropertyInfo> PropertiesWithAttribute<T>(Type type)
            where T : Attribute
        {
            return Properties(type)
                .Where(p => p.GetCustomAttribute<T>() != null);
        }
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public class TypeWithAttribute<T> : ITypeWithAttribute<T>
        where T : Attribute
        {
            public Type ModelType { get; set; }
            public T Attribute { get; set; }
        }
    }
}
