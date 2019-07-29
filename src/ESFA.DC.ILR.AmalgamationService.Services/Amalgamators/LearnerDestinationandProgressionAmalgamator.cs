using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerDestinationandProgressionAmalgamator : AbstractAmalgamator<MessageLearnerDestinationandProgression>, IAmalgamator<MessageLearnerDestinationandProgression>
    {
        private IRule<long?> _ulnRule;

        public LearnerDestinationandProgressionAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerDestinationandProgression, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _ulnRule = ruleProvider.BuildUlnRule();
        }

        public MessageLearnerDestinationandProgression Amalgamate(IEnumerable<MessageLearnerDestinationandProgression> models)
        {
            var messageLearnerDestinationandProgression = new MessageLearnerDestinationandProgression();

            ApplyRule(s => s.ULNNullable, _ulnRule.Definition, models, messageLearnerDestinationandProgression);

            ApplyGroupedCollectionRule(s => s.DPOutcome, new DPOutcomeRule().Definition, models, messageLearnerDestinationandProgression);

            return messageLearnerDestinationandProgression;
        }
    }
}
