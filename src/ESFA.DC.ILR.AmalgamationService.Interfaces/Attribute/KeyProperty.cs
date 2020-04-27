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
