using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerHE : ILooseLearnerHE
    {
        public long? TTACCOMNullable
        {
            get => tTACCOMFieldSpecified ? tTACCOMField : default(long?);
            set
            {
                tTACCOMFieldSpecified = value.HasValue;
                tTACCOMField = value.GetValueOrDefault();
            }
        }

        public IReadOnlyCollection<ILooseLearnerHEFinancialSupport> LearnerHEFinancialSupports
        {
            get => learnerHEFinancialSupportField;
            set => learnerHEFinancialSupportField = (MessageLearnerLearnerHELearnerHEFinancialSupport[]) value;
        }
    }
}
