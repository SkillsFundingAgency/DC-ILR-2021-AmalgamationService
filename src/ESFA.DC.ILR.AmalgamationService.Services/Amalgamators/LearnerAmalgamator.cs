using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerAmalgamator : AbstractAmalgamator, IAmalgamator<MessageLearner>
    {
        private readonly IAmalgamator<MessageLearnerLLDDandHealthProblem> _lLDDandHealthProblemAmalgamator;

        private IRule<string> _standardRuleString;
        private IRule<long> _standardRuleLong;
        private IRule<DateTime> _standardRuleDateTime;
        private IRule<string> _addressRule;

        public LearnerAmalgamator(
            IAmalgamator<MessageLearnerLLDDandHealthProblem> lLDDandHealthProblemAmalgamator,
           IRuleProvider ruleProvider)
        {
            _lLDDandHealthProblemAmalgamator = lLDDandHealthProblemAmalgamator;

            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
            _addressRule = ruleProvider.BuildAddressRule();
        }

        public MessageLearner Amalgamate(IEnumerable<MessageLearner> models)
        {
            var messageLearner = new MessageLearner();

            ApplyRule(s => s.PrevLearnRefNumber, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.PrevUKPRN, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.PMUKPRN, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.CampId, _standardRuleString.Definition, models, messageLearner);

            //TODO
            //ApplyRule(s => s.ULN, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.FamilyName, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.GivenNames, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.DateOfBirth, _standardRuleDateTime.Definition, models, messageLearner);
            //ApplyRule(s => s.Ethnicity, _standardRuleLong.Definition, models, messageLearner);
            //ApplyRule(s => s.Sex, _standardRuleString.Definition, models, messageLearner);
            //ApplyRule(s => s.LLDDHealthProb, _standardRuleLong.Definition, models, messageLearner);
            //ApplyRule(s => s.NINumber, _standardRuleString.Definition, models, messageLearner);
            //ApplyRule(s => s.PriorAttain, _standardRuleLong.Definition, models, messageLearner);
            //ApplyRule(s => s.Accom, _standardRuleLong.Definition, models, messageLearner);

            ////TODO
            ////ApplyRule(s => s.ALSCost, _standardRuleLong.Definition, models, messageLearner);
            //ApplyRule(s => s.PlanLearnHours, _standardRuleLong.Definition, models, messageLearner);
            //ApplyRule(s => s.PlanEEPHours, _standardRuleLong.Definition, models, messageLearner);
            //ApplyRule(s => s.MathGrade, _standardRuleString.Definition, models, messageLearner);
            //ApplyRule(s => s.EngGrade, _standardRuleString.Definition, models, messageLearner);
            //ApplyRule(s => s.PostcodePrior, _standardRuleString.Definition, models, messageLearner);
            //ApplyRule(s => s.Postcode, _standardRuleString.Definition, models, messageLearner);
            //ApplyRule(s => s.AddLine1, _addressRule.Definition, models, messageLearner);
            //ApplyRule(s => s.AddLine2, _addressRule.Definition, models, messageLearner);
            //ApplyRule(s => s.AddLine3, _addressRule.Definition, models, messageLearner);
            //ApplyRule(s => s.AddLine4, _addressRule.Definition, models, messageLearner);
            //ApplyRule(s => s.TelNo, _standardRuleString.Definition, models, messageLearner);
            ////ApplyRule(s => s.Email, _standardRuleString.Definition, models, messageLearner);

            ////ApplyGroupedChildCollectionRule(s => s.LLDDandHealthProblem, g => g.LLDDCat, _lLDDandHealthProblemAmalgamator, models, messageLearner);

            return messageLearner;
        }
    }
}
