using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring : AbstractLooseReadWriteModel<MessageLearnerLearnerEmploymentStatus>, IParentRelationship<MessageLearnerLearnerEmploymentStatus>, IAmalgamationModel
    {
        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.Parent.Parent.LearnRefNumber;
    }
}
