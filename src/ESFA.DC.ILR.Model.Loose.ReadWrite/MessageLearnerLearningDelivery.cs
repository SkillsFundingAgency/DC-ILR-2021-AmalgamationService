using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDelivery : AbstractLooseReadWriteModel<MessageLearner>, IParentRelationship<MessageLearner>, IAmalgamationModel
    {
        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
