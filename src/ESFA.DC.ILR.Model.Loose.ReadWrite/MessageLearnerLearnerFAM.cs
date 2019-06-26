using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerFAM : ILooseLearnerFAM
    {
        public long? LearnFAMCodeNullable
        {
            get => learnFAMCodeFieldSpecified ? learnFAMCodeField : default(long?);
            set
            {
                learnFAMCodeFieldSpecified = value.HasValue;
                learnFAMCodeField = value.GetValueOrDefault();
            }
        }
    }
}
