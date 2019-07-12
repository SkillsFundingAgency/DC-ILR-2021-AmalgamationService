using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerFAM : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearnerFAM
    {
        [XmlIgnore]
        public long? LearnFAMCodeNullable
        {
            get => learnFAMCodeFieldSpecified ? learnFAMCodeField : default(long?);
            set
            {
                learnFAMCodeFieldSpecified = value.HasValue;
                learnFAMCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
