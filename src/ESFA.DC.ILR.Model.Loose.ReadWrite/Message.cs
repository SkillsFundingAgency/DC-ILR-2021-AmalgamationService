using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class Message : AbstractLooseReadWriteModel<IAmalgamationRoot>, ILooseMessage
    {
        [XmlIgnore]
        public ILooseHeader HeaderEntity
        {
            get => headerField;
            set => headerField = (MessageHeader) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseSourceFile> SourceFilesCollection
        {
            get => sourceFilesField;
            set => sourceFilesField = (MessageSourceFile[]) value;
        }

        [XmlIgnore]
        public ILooseLearningProvider LearningProviderEntity
        {
            get => learningProviderField;
            set => learningProviderField = (MessageLearningProvider) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearner> Learners
        {
            get => learnerField;
            set => learnerField = (MessageLearner[]) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearnerDestinationAndProgression> LearnerDestinationAndProgressions
        {
            get => learnerDestinationandProgressionField;
            set => learnerDestinationandProgressionField = (MessageLearnerDestinationandProgression[]) value;
        }
        [XmlIgnore]
        public IAmalgamationRoot AmalgamationRoot { get; set; }

        [XmlIgnore]
        public string SourceFileName => AmalgamationRoot.Filename;

        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
