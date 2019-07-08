using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerHELearnerHEFinancialSupport : AbstractLooseReadWriteModel<ILooseLearnerHE>, ILooseLearnerHEFinancialSupport
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
        
        public string SourceFileName => Parent.Parent.Parent.AmalgamationRoot.Filename;

        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
