using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeader : AbstractLooseReadWriteModel<ILooseMessage>, ILooseHeader
    {
        [XmlIgnore]
        public ILooseCollectionDetails CollectionDetailsEntity
        {
            get => collectionDetailsField;
            set => collectionDetailsField = (MessageHeaderCollectionDetails) value;
        }

        [XmlIgnore]
        public ILooseSource SourceEntity
        {
            get => sourceField;
            set => sourceField = (MessageHeaderSource) value;
        }

        [XmlIgnore]
        public string SourceFileName { get => Parent.Parent.Filename; }

        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
