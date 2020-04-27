using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerHEAmalgamator : AbstractAmalgamator<MessageLearnerLearnerHE>, IAmalgamator<MessageLearnerLearnerHE>
    {
        private IAmalgamator<MessageLearnerLearnerHELearnerHEFinancialSupport> _learnerHEFinancialSupportAmalgamator;
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public LearnerHEAmalgamator(IAmalgamator<MessageLearnerLearnerHELearnerHEFinancialSupport> learnerHEFinancialSupportAmalgamator, IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerHE, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _learnerHEFinancialSupportAmalgamator = learnerHEFinancialSupportAmalgamator;
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLearnerHE Amalgamate(IEnumerable<MessageLearnerLearnerHE> models)
        {
            var messageLearnerLearnerHE = new MessageLearnerLearnerHE();

            ApplyRule(s => s.UCASPERID, _standardRuleString.Definition, models, messageLearnerLearnerHE);
            ApplyRule(s => s.TTACCOMNullable, _standardRuleLong.Definition, models, messageLearnerLearnerHE);

            ApplyGroupedChildCollectionRule(s => s.LearnerHEFinancialSupport, g => g.FINTYPE, _learnerHEFinancialSupportAmalgamator, models, messageLearnerLearnerHE);

            return messageLearnerLearnerHE;
        }
    }
}
