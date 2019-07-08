using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring : ILooseProviderSpecDeliveryMonitoring
    {
        public ILooseLearningDelivery LearningDelivery { get; set; }
        public ILooseLearningDelivery Parent { get => LearningDelivery; set => LearningDelivery = value; }

        public string SourceFileName => LearningDelivery.SourceFileName;

        public string LearnRefNumber => LearningDelivery.LearnRefNumber;
    }
}
