using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring : AbstractLooseReadWriteModel<ILooseLearner>, ILooseEmploymentStatusMonitoring
    {
        [XmlIgnore]
        public long? ESMCodeNullable
        {
            get => eSMCodeFieldSpecified ? eSMCodeField : default(long?);
            set
            {
                eSMCodeFieldSpecified = value.HasValue;
                eSMCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
