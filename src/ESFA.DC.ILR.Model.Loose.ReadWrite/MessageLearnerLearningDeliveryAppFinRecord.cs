using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryAppFinRecord : AbstractLooseReadWriteModel<MessageLearnerLearningDelivery>, IParentRelationship<MessageLearnerLearningDelivery>, IAmalgamationModel
    {
        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
