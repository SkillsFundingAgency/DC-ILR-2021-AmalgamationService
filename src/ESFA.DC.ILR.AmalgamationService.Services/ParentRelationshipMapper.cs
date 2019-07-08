using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class ParentRelationshipMapper
    {
        public T MapChildren<T>(T input)
        {
            return RecursiveMap(input);
        }

        private T RecursiveMap<T>(T input)
        {
            var parent = input;

            var parentChildProperties = GetPropertyInfos(input);

            foreach (var child in parentChildProperties)
            {
                var obj = child.GetValue(input);
                if (obj == null)
                {
                    continue;
                }

                if (child.PropertyType.GetInterfaces().Any(i => i.IsInterface && i.UnderlyingSystemType == typeof(IEnumerable)))
                {
                    var collection = (IEnumerable)child.GetValue(input, null);
                    foreach (var item in collection)
                    {
                        ApplyParentValue(input, child, item);
                    }

                    continue;
                }

                ApplyParentValue(input, child, obj);
            }

            return input;
        }

        private void ApplyParentValue<T>(T input, PropertyInfo child, object obj)
        {
            var propertyInfo = obj.GetType().GetProperty("Parent");
            propertyInfo.SetValue(obj, input);
            RecursiveMap(obj);
        }

        private IEnumerable<PropertyInfo> GetPropertyInfos<T>(T input)
        {
            var properties = typeof(T).GetProperties();
            var assignable = properties.Where(
                p =>
                p.PropertyType.GetInterfaces().Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IParentRelationship<>))
             || (i.IsInterface && i.UnderlyingSystemType == typeof(IEnumerable))));

            return assignable;
        }
    }
}