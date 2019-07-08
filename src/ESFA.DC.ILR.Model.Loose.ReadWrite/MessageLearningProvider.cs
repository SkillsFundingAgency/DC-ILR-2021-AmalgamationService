using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearningProvider : AbstractLooseReadWriteModel<ILooseMessage>, ILooseLearningProvider
    {        
        public string SourceFileName { get => Parent.AmalgamationRoot.Filename; }
        public string LearnRefNumber => null;
    }
}
