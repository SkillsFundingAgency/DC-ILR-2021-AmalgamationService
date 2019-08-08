using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.CrossValidation
{
    public class CrossValidationService : ICrossValidationService
    {
        public ILooseMessage CrossValidateLearners(ILooseMessage message)
        {
            var learnerDuplicates = GetDuplicateLearnRefNumbers(message.Learners);

            if (learnerDuplicates.Count() > 0)
            {
                var msgLearners = message.Learners as MessageLearner[];
                var distinctLearners = msgLearners.GroupBy(i => i.LearnRefNumber, StringComparer.OrdinalIgnoreCase).Select(g => g.First()).ToArray();
                message.Learners = distinctLearners;
            }

            var destinationProgressionDuplicates = GetDuplicateLearnRefNumbers(null, message.LearnerDestinationAndProgressions);
            if (destinationProgressionDuplicates.Count() > 0)
            {
                var destinationProgression = message.LearnerDestinationAndProgressions as MessageLearnerDestinationandProgression[];
                var distinctProgressionList = destinationProgression.GroupBy(i => i.LearnRefNumber, StringComparer.OrdinalIgnoreCase).Select(g => g.First()).ToArray();
                message.LearnerDestinationAndProgressions = distinctProgressionList;
            }

            return message;
        }

        public IEnumerable<string> GetDuplicateLearnRefNumbers(IEnumerable<ILooseLearner> learners = null, IEnumerable<ILooseLearnerDestinationAndProgression> progressions = null)
        {
            if (learners != null)
            {
                return learners.GroupBy(x => x.LearnRefNumber, StringComparer.OrdinalIgnoreCase)
                               .Where(g => g.Count() > 1)
                               .Select(g => g.Key);
            }
            else
            {
                return progressions.GroupBy(x => x.LearnRefNumber, StringComparer.OrdinalIgnoreCase)
                                   .Where(g => g.Count() > 1)
                                   .Select(g => g.Key);
            }
        }
    }
}