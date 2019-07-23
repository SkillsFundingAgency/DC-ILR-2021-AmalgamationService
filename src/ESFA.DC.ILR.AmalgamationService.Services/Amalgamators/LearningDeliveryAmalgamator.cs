using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearningDeliveryAmalgamator : AbstractAmalgamator<MessageLearnerLearningDelivery>, IAmalgamator<MessageLearnerLearningDelivery>
    {
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public LearningDeliveryAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearningDelivery, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLearningDelivery Amalgamate(IEnumerable<MessageLearnerLearningDelivery> models)
        {
            var messageLearnerLearningDelivery = new MessageLearnerLearningDelivery();

            // TODO : apply rules
            return messageLearnerLearningDelivery;
        }
    }
}
