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
        private readonly IValidationErrorHandler _validationErrorHandler;

        public CrossValidationService(IValidationErrorHandler validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        public ILooseMessage CrossValidateLearners(ILooseMessage message)
        {
            HashSet<string> duplicateLearners = new HashSet<string>(GetLearnerDuplicateLearnRefNumbers(message.Learners), StringComparer.OrdinalIgnoreCase);

            if (duplicateLearners.Count() > 0)
            {
                var msgLearners = message.Learners as MessageLearner[];
                var validLearners = msgLearners.Where(dp => !duplicateLearners.Contains(dp.LearnRefNumber)).ToArray();
                message.Learners = validLearners;
            }

            HashSet<string> duplicateProgressionList = new HashSet<string>(GetDPduplicateLearnRefNumbers(message.LearnerDestinationAndProgressions), StringComparer.OrdinalIgnoreCase);

            if (duplicateProgressionList.Count() > 0)
            {
                var msgProgressionList = message.LearnerDestinationAndProgressions as MessageLearnerDestinationandProgression[];
                var validProgressionList = msgProgressionList.Where(dp => !duplicateProgressionList.Contains(dp.LearnRefNumber)).ToArray();
                message.LearnerDestinationAndProgressions = validProgressionList;
            }

            return message;
        }

        public IEnumerable<string> GetLearnerDuplicateLearnRefNumbers(IEnumerable<ILooseLearner> learners)
        {
            var duplicates = learners.GroupBy(x => x.LearnRefNumber, StringComparer.OrdinalIgnoreCase)
                           .Where(g => g.Count() > 1)
                           .Select(g => g.Key);

            foreach (var refNumber in duplicates)
            {
                _validationErrorHandler.CrossRecordValidationErrorHandler($"Duplicate LearnRefNumber:{refNumber} found for Learners");
            }

            return duplicates;
        }

        /// <summary>
        ///     Destination & Progression duplicate list
        /// </summary>
        /// <param name="progressionList">list to check for duplicates</param>
        /// <returns>duplicate items</returns>
        public IEnumerable<string> GetDPduplicateLearnRefNumbers(IEnumerable<ILooseLearnerDestinationAndProgression> progressionList)
        {
            var duplicates = progressionList.GroupBy(x => x.LearnRefNumber, StringComparer.OrdinalIgnoreCase)
                               .Where(g => g.Count() > 1)
                               .Select(g => g.Key);

            foreach (var refNumber in duplicates)
            {
                _validationErrorHandler.CrossRecordValidationErrorHandler($"Duplicate LearnRefNumber:{refNumber} found for DestinationAndProgression");
            }

            return duplicates;
        }
    }
}