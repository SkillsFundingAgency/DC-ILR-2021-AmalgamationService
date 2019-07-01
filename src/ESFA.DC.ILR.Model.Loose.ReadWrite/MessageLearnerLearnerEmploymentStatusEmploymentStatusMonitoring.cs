using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring : ILooseEmploymentStatusMonitoring
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

        public ILooseLearner Parent { get => Learner; set => Learner = value; }

        public string SourceFileName => Learner.Message.AmalgamationRoot.Filename;

        public string LearnRefNumber => Learner.LearnRefNumber;

        public ILooseLearner Learner { get; set; }
    }
}
