using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerProviderSpecLearnerMonitoring : ILooseProviderSpecLearnerMonitoring
    {
        public ILooseLearner Learner { get; set; }
        public ILooseLearner Parent { get => Learner; set => Learner = value; }
        public string SourceFileName => Learner.Message.AmalgamationRoot.Filename;
        public string LearnRefNumber => Learner.LearnRefNumber;
    }
}
