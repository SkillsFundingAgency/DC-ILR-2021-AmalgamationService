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

        private T RecursiveMap<T>(T parent)
        {
            var parentChildProperties = GetPropertyInfos<T>(parent);

            foreach (var child in parentChildProperties)
            {
                var childValue = (IParentRelationshipSetter)child.GetValue(parent);
                if (childValue == null)
                {
                    continue;
                }

                if (typeof(IEnumerable<IParentRelationshipSetter>).IsAssignableFrom(child.PropertyType))
                {
                    var collection = (IEnumerable<IParentRelationshipSetter>)child.GetValue(parent, null);
                    foreach (var item in collection)
                    {
                        ApplyParentValue(parent, child, item);
                    }

                    continue;
                }

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
                typeof(IParentRelationshipSetter).IsAssignableFrom(p.PropertyType)
                || typeof(IEnumerable<IParentRelationshipSetter>).IsAssignableFrom(p.PropertyType));

            return assignable;
        }
    }
}