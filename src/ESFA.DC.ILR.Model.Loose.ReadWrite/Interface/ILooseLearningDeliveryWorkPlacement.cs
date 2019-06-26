using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearningDeliveryWorkPlacement
    {
        DateTime? WorkPlaceStartDateNullable { get;  set; }

        DateTime? WorkPlaceEndDateNullable { get;  set; }

        long? WorkPlaceHoursNullable { get;  set; }

        long? WorkPlaceModeNullable { get;  set; }

        long? WorkPlaceEmpIdNullable { get;  set; }
    }
}
