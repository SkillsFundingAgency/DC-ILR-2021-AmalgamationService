using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseLearningDeliveryWorkPlacement
    {
        [XmlIgnore]
        public DateTime? WorkPlaceStartDateNullable
        {
            get => workPlaceStartDateFieldSpecified ? workPlaceStartDateField : default(DateTime?);
            set
            {
                workPlaceStartDateFieldSpecified = value.HasValue;
                workPlaceStartDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? WorkPlaceHoursNullable
        {
            get => workPlaceHoursFieldSpecified ? workPlaceHoursField : default(long?);
            set
            {
                workPlaceHoursFieldSpecified = value.HasValue;
                workPlaceHoursField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? WorkPlaceModeNullable
        {
            get => workPlaceModeFieldSpecified ? workPlaceModeField : default(long?);
            set
            {
                workPlaceModeFieldSpecified = value.HasValue;
                workPlaceModeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? WorkPlaceEmpIdNullable
        {
            get => workPlaceEmpIdFieldSpecified ? workPlaceEmpIdField : default(long?);
            set
            {
                workPlaceEmpIdFieldSpecified = value.HasValue;
                workPlaceEmpIdField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? WorkPlaceEndDateNullable
        {
            get => workPlaceEndDateFieldSpecified ? workPlaceEndDateField : default(DateTime?);
            set
            {
                workPlaceEndDateFieldSpecified = value.HasValue;
                workPlaceEndDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}