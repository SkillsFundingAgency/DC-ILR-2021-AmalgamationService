using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerDestinationandProgression : ILooseLearnerDestinationAndProgression
    {
        public long? ULNNullable
        {
            get => uLNFieldSpecified ? uLNField : default(long?);
            set
            {
                uLNFieldSpecified = value.HasValue;
                uLNField = value.GetValueOrDefault();
            }
        }

        public IReadOnlyCollection<ILooseDPOutcome> DPOutcomes
        {
            get => dPOutcomeField;
            set => dPOutcomeField = (MessageLearnerDestinationandProgressionDPOutcome[]) value;
        }

        public ILooseMessage Message { get; set; }

        public ILooseMessage Parent { get => Message; set => Message = value; }
        public string SourceFileName { get => Message.AmalgamationRoot.Filename; }
    }
}
