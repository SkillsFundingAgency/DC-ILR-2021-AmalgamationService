using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using System;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces.Attribute
{
    public class KeyProperty : System.Attribute
    {
        internal KeyProperty(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
