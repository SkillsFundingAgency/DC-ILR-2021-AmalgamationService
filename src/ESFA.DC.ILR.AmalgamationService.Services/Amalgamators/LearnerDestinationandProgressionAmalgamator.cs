using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerDestinationandProgressionAmalgamator : AbstractAmalgamator<MessageLearnerDestinationandProgression>, IAmalgamator<MessageLearnerDestinationandProgression>
    {
        private IRuleProvider _ruleProvider;

        public LearnerDestinationandProgressionAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerDestinationandProgression, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _ruleProvider = ruleProvider;
        }

        public MessageLearnerDestinationandProgression Amalgamate(IEnumerable<MessageLearnerDestinationandProgression> models)
        {
            var messageLearnerDestinationandProgression = new MessageLearnerDestinationandProgression();

            ApplyRule(s => s.LearnRefNumber, _ruleProvider.BuildStandardRule<string>().Definition, models, messageLearnerDestinationandProgression);
            ApplyRule(s => s.ULN, _ruleProvider.BuildUlnRule().Definition, models, messageLearnerDestinationandProgression);
            ApplyGroupedCollectionRule(s => s.DPOutcome, _ruleProvider.BuildDPOutcomeRule().Definition, models, messageLearnerDestinationandProgression);

            return messageLearnerDestinationandProgression;
        }
    }
}
