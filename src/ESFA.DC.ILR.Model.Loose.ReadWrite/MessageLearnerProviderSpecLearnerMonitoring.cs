using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerProviderSpecLearnerMonitoring : AbstractLooseReadWriteModel<ILooseLearner>, ILooseProviderSpecLearnerMonitoring
    {
        public string SourceFileName => Parent.Parent.Parent.Filename;
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
