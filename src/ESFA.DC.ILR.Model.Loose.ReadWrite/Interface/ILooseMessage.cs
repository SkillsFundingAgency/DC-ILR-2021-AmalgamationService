using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseMessage : IParentRelationship<IAmalgamationRoot>, IAmalgamationModel
    {
        ILooseHeader HeaderEntity { get;  set; }

        IReadOnlyCollection<ILooseSourceFile> SourceFilesCollection { get;  set; }

        ILooseLearningProvider LearningProviderEntity { get;  set; }

        IReadOnlyCollection<ILooseLearner> Learners { get;  set; }

        IReadOnlyCollection<ILooseLearnerDestinationAndProgression> LearnerDestinationAndProgressions { get;  set; }

        IAmalgamationRoot AmalgamationRoot { get; set; }
    }
}
