using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Extension;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeaderCollectionDetails : ILooseCollectionDetails
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
    }
}
