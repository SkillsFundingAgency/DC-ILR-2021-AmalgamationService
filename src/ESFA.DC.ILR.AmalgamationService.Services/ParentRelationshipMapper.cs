using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
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
                var propertyInfo = obj.GetType().GetProperty("Parent");
                propertyInfo.SetValue(obj, input);
                RecursiveMap(child);
            }

            return input;
        }

        private IEnumerable<PropertyInfo> GetPropertyInfos<T>(T input)
        {
            var properties = typeof(T).GetProperties();
            var assignable = properties.Where(
                p =>
                p.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IParentRelationship<>)));

            return assignable;
        }
    }
}