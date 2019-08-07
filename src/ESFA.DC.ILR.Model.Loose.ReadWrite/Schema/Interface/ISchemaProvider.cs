using System.Xml.Schema;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Schema.Interface
{
    public interface ISchemaProvider
    {
        XmlSchema Provide();
    }
}
