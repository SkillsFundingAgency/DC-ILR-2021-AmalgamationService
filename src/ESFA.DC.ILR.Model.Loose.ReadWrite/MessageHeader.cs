using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeader : AbstractLooseReadWriteModel<Message>, IParentRelationship<Message>, IAmalgamationModel
    {
        [XmlIgnore]
        public string SourceFileName { get => Parent.Parent.Filename; }

        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
