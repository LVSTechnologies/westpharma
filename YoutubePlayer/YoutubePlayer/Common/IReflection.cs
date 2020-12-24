using System;
using System.Collections.Generic;
using System.Reflection;

namespace YoutubePlayer.Common
{
    public interface IReflection
    {
        Assembly[] Assemblies();

        IList<Type> AssignableTypes<T>();
        IList<Type> AssignableTypes(Type type);
        IEnumerable<ITypeWithAttribute<T>> AssignableTypeswithAtrribute<T, U>() where T : Attribute;
        IList<PropertyInfo> Properties(Type type);
        Type GetBaseType(Type t);
        IEnumerable<PropertyInfo> PropertiesWithAttribute<T>(Type type)
            where T : Attribute;
    }

    public interface ITypeWithAttribute<T>
        where T : Attribute
    {
        Type ModelType { get; }
        T Attribute { get; }
    }
}
