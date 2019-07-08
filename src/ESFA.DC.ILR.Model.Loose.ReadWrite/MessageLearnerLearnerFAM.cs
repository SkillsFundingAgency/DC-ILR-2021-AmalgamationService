using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerFAM : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearnerFAM
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

        public string SourceFileName => Parent.Parent.AmalgamationRoot.Filename;
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
