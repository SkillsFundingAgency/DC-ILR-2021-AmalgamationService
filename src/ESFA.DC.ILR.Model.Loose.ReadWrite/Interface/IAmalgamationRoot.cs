using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface IAmalgamationRoot
    {
        [XmlIgnore]
        string Filename { get; set; }

        [XmlIgnore]
        ILooseMessage Message { get; set; }
    }
}
