using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerHE : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearnerHE
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

        public string SourceFileName => Parent.Parent.AmalgamationRoot.Filename;
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
