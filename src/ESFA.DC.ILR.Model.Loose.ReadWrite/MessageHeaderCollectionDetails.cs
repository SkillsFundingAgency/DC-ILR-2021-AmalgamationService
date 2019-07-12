using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Extension;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeaderCollectionDetails : AbstractLooseReadWriteModel<ILooseHeader>, ILooseCollectionDetails
    {
        [XmlIgnore]
        public string CollectionString
        {
            get => collectionField.XmlEnumToString();
            set => collectionField = (MessageHeaderCollectionDetailsCollection)Enum.Parse(typeof(MessageHeaderCollectionDetailsCollection), value);
        }

        [XmlIgnore]
        public string YearString
        {
            get => yearField.XmlEnumToString();
            set => yearField = (MessageHeaderCollectionDetailsYear)Enum.Parse(typeof(MessageHeaderCollectionDetailsYear), value);
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
