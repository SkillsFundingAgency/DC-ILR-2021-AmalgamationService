using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring : AbstractLooseReadWriteModel<ILooseLearner>, ILooseEmploymentStatusMonitoring
    {
        public long? ESMCodeNullable
        {
            get => eSMCodeFieldSpecified ? eSMCodeField : default(long?);
            set
            {
                eSMCodeFieldSpecified = value.HasValue;
                eSMCodeField = value.GetValueOrDefault();
            }
        }

        public string SourceFileName => Parent.Parent.AmalgamationRoot.Filename;

        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
