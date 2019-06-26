using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement : ILooseLearningDeliveryWorkPlacement
    {
        public DateTime? WorkPlaceStartDateNullable
        {
            get => workPlaceStartDateFieldSpecified ? workPlaceStartDateField : default(DateTime?);
            set
            {
                workPlaceStartDateFieldSpecified = value.HasValue;
                workPlaceStartDateField = value.GetValueOrDefault();
            }
        }

        public long? WorkPlaceHoursNullable
        {
            get => workPlaceHoursFieldSpecified ? workPlaceHoursField : default(long?);
            set
            {
                workPlaceHoursFieldSpecified = value.HasValue;
                workPlaceHoursField = value.GetValueOrDefault();
            }
        }

        public long? WorkPlaceModeNullable
        {
            get => workPlaceModeFieldSpecified ? workPlaceModeField : default(long?);
            set
            {
                workPlaceModeFieldSpecified = value.HasValue;
                workPlaceModeField = value.GetValueOrDefault();
            }
        }

        public long? WorkPlaceEmpIdNullable
        {
            get => workPlaceEmpIdFieldSpecified ? workPlaceEmpIdField : default(long?);
            set
            {
                workPlaceEmpIdFieldSpecified = value.HasValue;
                workPlaceEmpIdField = value.GetValueOrDefault();
            }
        }

        public DateTime? WorkPlaceEndDateNullable
        {
            get => workPlaceEndDateFieldSpecified ? workPlaceEndDateField : default(DateTime?);
            set
            {
                workPlaceEndDateFieldSpecified = value.HasValue;
                workPlaceEndDateField = value.GetValueOrDefault();
            }
        }
    }
}