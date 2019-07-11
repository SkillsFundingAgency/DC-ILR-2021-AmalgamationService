using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseProviderSpecDeliveryMonitoring
    {
        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;
        
        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
