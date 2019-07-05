using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerContactPreference : ILooseContactPreference
    {
        public long? ContPrefCodeNullable
        {
            get => contPrefCodeFieldSpecified ? contPrefCodeField : default(long?);
            set
            {
                contPrefCodeFieldSpecified = value.HasValue;
                contPrefCodeField = value.GetValueOrDefault();
            }
        }

        public ILooseLearner Learner { get; set; }
        public ILooseLearner Parent { get => Learner; set => Learner = value; }
        public string SourceFileName => Learner.Message.AmalgamationRoot.Filename;
        public string LearnRefNumber => Learner.LearnRefNumber;
    }
}
