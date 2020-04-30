using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LLDDandHealthProblemAmalgamator : AbstractAmalgamator<MessageLearnerLLDDandHealthProblem>, IAmalgamator<MessageLearnerLLDDandHealthProblem>
    {
        private IRule<long?> _standardRuleLong;

        public LLDDandHealthProblemAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LLDDAndHealthProblems, (x) => x.LLDDCat.ToString(), amalgamationErrorHandler)
        {
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLLDDandHealthProblem Amalgamate(IEnumerable<MessageLearnerLLDDandHealthProblem> models)
        {
            var messageLearnerLLDDandHealthProblem = new MessageLearnerLLDDandHealthProblem();

            ApplyRule(s => s.LLDDCat, _standardRuleLong.Definition, models, messageLearnerLLDDandHealthProblem);
            ApplyRule(s => s.PrimaryLLDD, _standardRuleLong.Definition, models, messageLearnerLLDDandHealthProblem);

            return messageLearnerLLDDandHealthProblem;
        }
    }
}
