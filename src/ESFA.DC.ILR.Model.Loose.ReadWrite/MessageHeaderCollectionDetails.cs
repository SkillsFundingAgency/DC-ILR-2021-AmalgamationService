using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Extension;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeaderCollectionDetails : AbstractLooseReadWriteModel<ILooseHeader>, ILooseCollectionDetails
    {
        public string CollectionString
        {
            get => collectionField.XmlEnumToString();
            set => collectionField = (MessageHeaderCollectionDetailsCollection)Enum.Parse(typeof(MessageHeaderCollectionDetailsCollection), value);
        }

        public string YearString
        {
            get => yearField.XmlEnumToString();
            set => yearField = (MessageHeaderCollectionDetailsYear)Enum.Parse(typeof(MessageHeaderCollectionDetailsYear), value);
        }
        public string SourceFileName => Parent.Parent.Parent.Filename;
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
