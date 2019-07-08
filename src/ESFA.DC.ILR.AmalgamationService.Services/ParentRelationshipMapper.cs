using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
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
            return RecursiveMap<T>(input);
        }

        private T RecursiveMap<T>(T input)
        {
            var parent = input;

            var parentChildProperties = GetPropertyInfos<T>(input);

            foreach (var child in parentChildProperties)
            {
                var childValue = (IParentRelationshipSetter)child.GetValue(input);
                if (childValue == null)
                {
                    continue;
                }

                if (child.PropertyType.GetInterfaces().Any(i => i.IsInterface && i.UnderlyingSystemType == typeof(IEnumerable)))
                {
                    var collection = (IEnumerable<IParentRelationshipSetter>)child.GetValue(input, null);
                    foreach (var item in collection)
                    {
                        ApplyParentValue(parent, child, item);
                    }

                    continue;
                }

                //var newchildValue = Convert.ChangeType(childValue, child.PropertyType);
                ApplyParentValue(parent, child, childValue);
            }

            return parent;
        }

        private void ApplyParentValue<T, TChild>(T parent, PropertyInfo child, TChild childValue)
        {
            var propertyInfo = typeof(TChild).GetProperty("ParentSetter");
            propertyInfo.SetValue(childValue, parent);
            RecursiveMap(childValue);
        }

        private IEnumerable<PropertyInfo> GetPropertyInfos<T>(T input)
        {
            var properties = typeof(T).GetProperties();
            var assignable = properties.Where(
                p =>
                p.PropertyType.IsAssignableFrom(typeof(IParentRelationshipSetter)));

            //p.PropertyType.GetInterfaces().Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IParentRelationship<>))));
            //    || (i.IsInterface && i.UnderlyingSystemType == typeof(IEnumerable))));

            return assignable;
        }
    }
}