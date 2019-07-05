using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearningProvider : ILooseLearningProvider
    {
        public ILooseMessage Message { get; set; }

        public ILooseMessage Parent { get => Message; set => Message = value; }
        public string SourceFileName { get => Message.AmalgamationRoot.Filename; }
        public string LearnRefNumber => null;
    }
}
