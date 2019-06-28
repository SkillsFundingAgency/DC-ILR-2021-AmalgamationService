using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeader : ILooseHeader
    {
        public ILooseCollectionDetails CollectionDetailsEntity
        {
            get => collectionDetailsField;
            set => collectionDetailsField = (MessageHeaderCollectionDetails) value;
        }

        public ILooseSource SourceEntity
        {
            get => sourceField;
            set => sourceField = (MessageHeaderSource) value;
        }
        public ILooseMessage Message { get;  set; }

        public ILooseMessage Parent { get => Message; set => Message = value; }
    }
}
