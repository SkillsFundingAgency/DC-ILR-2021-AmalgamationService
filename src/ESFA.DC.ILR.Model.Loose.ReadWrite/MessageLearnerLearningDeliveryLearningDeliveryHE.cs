﻿using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryLearningDeliveryHE : AbstractLooseReadWriteModel<MessageLearnerLearningDelivery>, IParentRelationship<MessageLearnerLearningDelivery>, IAmalgamationModel
    {
        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
