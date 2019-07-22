using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerHEFinancialSupportAmalgamator : AbstractAmalgamator<MessageLearnerLearnerHELearnerHEFinancialSupport>, IAmalgamator<MessageLearnerLearnerHELearnerHEFinancialSupport>
    {
        private IRule<long?> _standardRuleLong;

        public LearnerHEFinancialSupportAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerHEFinancialSupport, (x) => x.FINTYPE.ToString(), amalgamationErrorHandler)
        {
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLearnerHELearnerHEFinancialSupport Amalgamate(IEnumerable<MessageLearnerLearnerHELearnerHEFinancialSupport> models)
        {
            var messageLearnerLearnerHELearnerHEFinancialSupport = new MessageLearnerLearnerHELearnerHEFinancialSupport();

            ApplyRule(s => s.FINTYPENullable, _standardRuleLong.Definition, models, messageLearnerLearnerHELearnerHEFinancialSupport);
            ApplyRule(s => s.FINAMOUNTNullable, _standardRuleLong.Definition, models, messageLearnerLearnerHELearnerHEFinancialSupport);

            return messageLearnerLearnerHELearnerHEFinancialSupport;
        }
    }
}
