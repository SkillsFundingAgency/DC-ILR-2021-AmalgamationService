using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearnerDestinationAndProgression
    {
        string LearnRefNumber { get;  set; }

        long? ULNNullable { get;  set; }

        IReadOnlyCollection<ILooseDPOutcome> DPOutcomes { get;  set; }
    }
}
