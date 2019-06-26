using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLLDDandHealthProblem : ILooseLLDDAndHealthProblem
    {
        public long? LLDDCatNullable
        {
            get => lLDDCatFieldSpecified ? lLDDCatField : default(long?);
            set
            {
                lLDDCatFieldSpecified = value.HasValue;
                lLDDCatField = value.GetValueOrDefault();
            }
        }

        public long? PrimaryLLDDNullable
        {
            get => primaryLLDDFieldSpecified ? primaryLLDDField : default(long?);
            set
            {
                primaryLLDDFieldSpecified = value.HasValue;
                primaryLLDDField = value.GetValueOrDefault();
            }
        }
    }
}
