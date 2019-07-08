using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseProviderSpecDeliveryMonitoring
    {
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
