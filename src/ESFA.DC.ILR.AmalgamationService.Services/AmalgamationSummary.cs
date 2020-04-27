using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationSummary
    {
        public IEnumerable<KeyValuePair<string, int>> FileLearnerCount { get; set; }

        public int LearnersInAllFiles { get; set; }

        public int RejectedLearners { get; set; }

        public int LearnersInAmalgamatedFile { get; set; }

        public int AmalgamationErrors { get; set; }

        public int AmalgamationWarnings { get; set; }
    }
}
