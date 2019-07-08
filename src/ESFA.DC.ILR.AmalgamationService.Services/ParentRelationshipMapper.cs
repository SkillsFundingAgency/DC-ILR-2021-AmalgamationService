using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

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
            var childEntities = GetChildEntities<T>(parent);

            foreach (var child in childEntities.Where(c => c != null))
            {
                child.ParentSetter = parent;
                RecursiveMap(child);
            }

            return parent;
        }

        private IEnumerable<IParentRelationshipSetter> GetChildEntities<T>(T input)
        {
            var childEntities = new List<IParentRelationshipSetter>();

            var properties = input.GetType().GetProperties();

            var assignableProperties = properties.Where(
                p =>
                p.PropertyType.IsInterface &&
                p.Name != "Parent" &&
                typeof(IParentRelationshipSetter).IsAssignableFrom(p.PropertyType));

            childEntities.AddRange(assignableProperties.Select(p => (IParentRelationshipSetter)p.GetValue(input)));

            var assignableCollections = properties.Where(
                p =>
                typeof(IEnumerable<IParentRelationshipSetter>).IsAssignableFrom(p.PropertyType) && p.CustomAttributes.Any(x => x.AttributeType == typeof(XmlArrayItemAttribute) || x.AttributeType == typeof(XmlElementAttribute)));

            foreach (var listItem in assignableCollections)
            {
                var listItemCollections = (IEnumerable<IParentRelationshipSetter>)listItem.GetValue(input, null);
                if (listItemCollections != null)
                {
                    childEntities.AddRange(listItemCollections);
                }
            }

            return childEntities;
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