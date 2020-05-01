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

        public Message CrossValidateLearners(Message message)
        {
            if (message.Learner != null && message.Learner.Count > 1)
            {
                HashSet<string> duplicateLearners = new HashSet<string>(GetLearnerDuplicateLearnRefNumbers(message.Learner), StringComparer.OrdinalIgnoreCase);

                if (duplicateLearners.Count() > 0)
                {
                    message.Learner.RemoveAll(l => duplicateLearners.Contains(l.LearnRefNumber));
                }
            }

            if (message.LearnerDestinationandProgression != null && message.LearnerDestinationandProgression.Count > 1)
            {
                HashSet<string> duplicateProgressionList = new HashSet<string>(GetDPduplicateLearnRefNumbers(message.LearnerDestinationandProgression), StringComparer.OrdinalIgnoreCase);

                if (duplicateProgressionList.Count() > 0)
                {
                    message.LearnerDestinationandProgression.RemoveAll(ldp => duplicateProgressionList.Contains(ldp.LearnRefNumber));
                }
            }

            return message;
        }

        public IEnumerable<string> GetLearnerDuplicateLearnRefNumbers(List<MessageLearner> learners)
        {
            var duplicates = learners.GroupBy(x => x.LearnRefNumber, StringComparer.OrdinalIgnoreCase)
                           .Where(g => g.Count() > 1)
                           .Select(g => g.Key);

            foreach (var refNumber in duplicates)
            {
                _validationErrorHandler.CrossRecordValidationErrorHandler($"Duplicate LearnRefNumber:{refNumber} found for Learners", learners.FirstOrDefault().SourceFileName);
            }

            return duplicates;
        }

        /// <summary>
        ///     Destination & Progression duplicate list
        /// </summary>
        /// <param name="progressionList">list to check for duplicates</param>
        /// <returns>duplicate items</returns>
        public IEnumerable<string> GetDPduplicateLearnRefNumbers(IList<MessageLearnerDestinationandProgression> progressionList)
        {
            var duplicates = progressionList.GroupBy(x => x.LearnRefNumber, StringComparer.OrdinalIgnoreCase)
                               .Where(g => g.Count() > 1)
                               .Select(g => g.Key);

            foreach (var refNumber in duplicates)
            {
                _validationErrorHandler.CrossRecordValidationErrorHandler($"Duplicate LearnRefNumber:{refNumber} found for DestinationAndProgression", progressionList.FirstOrDefault().SourceFileName);
            }

            return duplicates;
        }
    }
}