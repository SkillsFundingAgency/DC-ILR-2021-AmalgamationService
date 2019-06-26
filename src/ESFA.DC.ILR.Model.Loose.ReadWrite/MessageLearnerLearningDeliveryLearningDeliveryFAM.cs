using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryLearningDeliveryFAM : ILooseLearningDeliveryFAM
    {
        public DateTime? LearnDelFAMDateFromNullable
        {
            get => LearnDelFAMDateFromSpecified ? LearnDelFAMDateFrom : default(DateTime?);
            set
            {
                LearnDelFAMDateFromSpecified = value.HasValue;
                LearnDelFAMDateFrom = value.GetValueOrDefault();
            }
        }

        public DateTime? LearnDelFAMDateToNullable
        {
            get => LearnDelFAMDateToSpecified ? LearnDelFAMDateTo : default(DateTime?);
            set
            {
                LearnDelFAMDateToSpecified = value.HasValue;
                LearnDelFAMDateTo = value.GetValueOrDefault();
            }
        }
    }
}
