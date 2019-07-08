using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerDestinationandProgressionDPOutcome : AbstractLooseReadWriteModel<ILooseLearnerDestinationAndProgression>, ILooseDPOutcome
    {
        public long? OutCodeNullable
        {
            get => outCodeFieldSpecified ? outCodeField : default(long?);
            set
            {
                outCodeFieldSpecified = value.HasValue;
                outCodeField = value.GetValueOrDefault();
            }
        }

        public DateTime? OutStartDateNullable
        {
            get => outStartDateFieldSpecified ? outStartDateField : default(DateTime?);
            set
            {
                outStartDateFieldSpecified = value.HasValue;
                outStartDateField = value.GetValueOrDefault();
            }
        }

        public DateTime? OutCollDateNullable
        {
            get => outCollDateFieldSpecified ? outCollDateField : default(DateTime?);
            set
            {
                outCollDateFieldSpecified = value.HasValue;
                outCollDateField = value.GetValueOrDefault();
            }
        }

        public DateTime? OutEndDateNullable
        {
            get => outEndDateFieldSpecified ? outEndDateField : default(DateTime?);
            set
            {
                outEndDateFieldSpecified = value.HasValue;
                outEndDateField = value.GetValueOrDefault();
            }
        }
        public string SourceFileName => Parent.Parent.AmalgamationRoot.Filename;
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
