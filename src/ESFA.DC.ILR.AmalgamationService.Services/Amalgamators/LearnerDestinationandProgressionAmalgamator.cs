using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerDestinationandProgressionAmalgamator : AbstractAmalgamator<MessageLearnerDestinationandProgression>, IAmalgamator<MessageLearnerDestinationandProgression>
    {
        private IRule<long?> _standardRuleLong;

        public LearnerDestinationandProgressionAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerDestinationandProgression, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerDestinationandProgression Amalgamate(IEnumerable<MessageLearnerDestinationandProgression> models)
        {
            var messageLearnerDestinationandProgression = new MessageLearnerDestinationandProgression();

            ApplyRule(s => s.ULNNullable, _standardRuleLong.Definition, models, messageLearnerDestinationandProgression);

            return messageLearnerDestinationandProgression;
        }
    }
}
