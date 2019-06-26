using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class Message : ILooseMessage
    {
        public ILooseHeader HeaderEntity
        {
            get => headerField;
            set => headerField = (MessageHeader) value;
        }

        public IReadOnlyCollection<ILooseSourceFile> SourceFilesCollection
        {
            get => sourceFilesField;
            set => sourceFilesField = (MessageSourceFile[]) value;
        }

        public ILooseLearningProvider LearningProviderEntity
        {
            get => learningProviderField;
            set => learningProviderField = (MessageLearningProvider) value;
        }

        public IReadOnlyCollection<ILooseLearner> Learners
        {
            get => learnerField;
            set => learnerField = (MessageLearner[]) value;
        }

        public IReadOnlyCollection<ILooseLearnerDestinationAndProgression> LearnerDestinationAndProgressions
        {
            get => learnerDestinationandProgressionField;
            set => learnerDestinationandProgressionField = (MessageLearnerDestinationandProgression[]) value;
        }
    }
}
