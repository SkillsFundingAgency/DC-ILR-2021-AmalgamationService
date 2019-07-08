using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerContactPreference : AbstractLooseReadWriteModel<ILooseLearner>,  ILooseContactPreference
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
        public string SourceFileName => Parent.Parent.Parent.Filename;
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
