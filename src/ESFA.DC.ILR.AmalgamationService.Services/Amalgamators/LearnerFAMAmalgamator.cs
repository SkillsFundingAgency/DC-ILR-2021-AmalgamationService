using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerFAMAmalgamator : AbstractAmalgamator<MessageLearnerLearnerFAM>, IAmalgamator<MessageLearnerLearnerFAM>
    {
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public LearnerFAMAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerFAM, (x) => x.LearnFAMType.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLearnerFAM Amalgamate(IEnumerable<MessageLearnerLearnerFAM> models)
        {
            var messageLearnerLearnerFAM = new MessageLearnerLearnerFAM();

            // TODO : apply rules
            throw new NotImplementedException();
        }
    }
}
