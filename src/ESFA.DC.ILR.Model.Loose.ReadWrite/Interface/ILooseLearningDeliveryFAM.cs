using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearningDeliveryFAM : IParentRelationship<ILooseLearningDelivery>, IAmalgamationModel
    {
        string LearnDelFAMType { get;  set; }

        string LearnDelFAMCode { get;  set; }

        DateTime? LearnDelFAMDateFromNullable { get;  set; }

        DateTime? LearnDelFAMDateToNullable { get;  set; }
    }
}
