using System.Collections.Generic;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerDestinationandProgression : AbstractLooseReadWriteModel<ILooseMessage> ,ILooseLearnerDestinationAndProgression
    {

        [XmlIgnore]
        public long? ULNNullable
        {
            get => uLNFieldSpecified ? uLNField : default(long?);
            set
            {
                uLNFieldSpecified = value.HasValue;
                uLNField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseDPOutcome> DPOutcomes
        {
            get => dPOutcomeField;
            set => dPOutcomeField = (MessageLearnerDestinationandProgressionDPOutcome[]) value;
        }

        [XmlIgnore]
        public string SourceFileName { get => Parent.Parent.Filename; }
    }
}
