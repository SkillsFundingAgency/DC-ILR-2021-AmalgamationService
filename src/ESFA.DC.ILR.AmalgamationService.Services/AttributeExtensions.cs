using System;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this System.Enum value)
            where T : Attribute
        {
            var enumType = value.GetType();
            var name = System.Enum.GetName(enumType, value);
            var attribute = enumType.GetField(name)
                .GetCustomAttribute<T>();

            return attribute;
        }
    }
}
