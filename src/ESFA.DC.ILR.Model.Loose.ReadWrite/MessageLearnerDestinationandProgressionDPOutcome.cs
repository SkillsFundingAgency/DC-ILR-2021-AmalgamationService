using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerDestinationandProgressionDPOutcome : AbstractLooseReadWriteModel<ILooseLearnerDestinationAndProgression>, ILooseDPOutcome
    {
        [XmlIgnore]
        public long? OutCodeNullable
        {
            get => outCodeFieldSpecified ? outCodeField : default(long?);
            set
            {
                outCodeFieldSpecified = value.HasValue;
                outCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? OutStartDateNullable
        {
            get => outStartDateFieldSpecified ? outStartDateField : default(DateTime?);
            set
            {
                outStartDateFieldSpecified = value.HasValue;
                outStartDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? OutCollDateNullable
        {
            get => outCollDateFieldSpecified ? outCollDateField : default(DateTime?);
            set
            {
                outCollDateFieldSpecified = value.HasValue;
                outCollDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? OutEndDateNullable
        {
            get => outEndDateFieldSpecified ? outEndDateField : default(DateTime?);
            set
            {
                outEndDateFieldSpecified = value.HasValue;
                outEndDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
