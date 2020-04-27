using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearnerDestinationAndProgression : IParentRelationship<ILooseMessage>, IAmalgamationModel
    {
        string LearnRefNumber { get;  set; }

        long? ULNNullable { get;  set; }

        IReadOnlyCollection<ILooseDPOutcome> DPOutcomes { get;  set; }
    }
}
