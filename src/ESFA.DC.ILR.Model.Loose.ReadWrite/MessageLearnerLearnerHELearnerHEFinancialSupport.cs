using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerHELearnerHEFinancialSupport : ILooseLearnerHEFinancialSupport
    {
        public long? FINTYPENullable
        {
            get => fINTYPEFieldSpecified ? fINTYPEField : default(long?);
            set
            {
                fINTYPEFieldSpecified = value.HasValue;
                fINTYPEField = value.GetValueOrDefault();
            }
        }

        public long? FINAMOUNTNullable
        {
            get => fINAMOUNTFieldSpecified ? fINAMOUNTField : default(long?);
            set
            {
                fINAMOUNTFieldSpecified = value.HasValue;
                fINAMOUNTField = value.GetValueOrDefault();
            }
        }

        public ILooseLearnerHE LearnerHE { get; set; }
        public ILooseLearnerHE Parent { get => LearnerHE; set => LearnerHE = value; }

        public string SourceFileName => LearnerHE.SourceFileName;

        public string LearnRefNumber => LearnerHE.LearnRefNumber;
    }
}
