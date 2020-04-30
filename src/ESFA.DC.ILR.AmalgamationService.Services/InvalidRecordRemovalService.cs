using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class InvalidRecordRemovalService : IInvalidRecordRemovalService
    {
        public IAmalgamationResult RemoveInvalidLearners(IAmalgamationResult amalgamationResult)
        {
            var invalidLearners = new HashSet<string>(amalgamationResult.ValidationErrors.GroupBy(x => x.LearnRefNumber).Select(s => s.First()).Where(t => t.Severity == Severity.Error).Select(m => m.LearnRefNumber), StringComparer.OrdinalIgnoreCase);

            if (invalidLearners.Count() > 0)
            {
                amalgamationResult.Message.Learner.RemoveAll(l => invalidLearners.Contains(l.LearnRefNumber));
                //var amalgamatedLearners = amalgamationResult.Message.Learner;
                //var validLearners = amalgamatedLearners.Where(l => !invalidLearners.Contains(l.LearnRefNumber)).ToArray();
                //amalgamationResult.Message.Learner = validLearners;
            }

            return amalgamationResult;
        }
    }
}
