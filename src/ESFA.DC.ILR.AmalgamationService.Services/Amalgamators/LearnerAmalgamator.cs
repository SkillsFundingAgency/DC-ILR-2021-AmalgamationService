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
    public class LearnerAmalgamator : AbstractAmalgamator<MessageLearner>, IAmalgamator<MessageLearner>
    {
        private readonly IAmalgamator<MessageLearnerLearnerEmploymentStatus> _learnerEmploymentStatusAmalgamator;
        private readonly IAmalgamator<MessageLearnerLearnerHE> _learnerHEAmalgamator;
        private readonly IAmalgamator<MessageLearnerLearningDelivery> _learningDeliveryAmalgamator;
        private readonly IAmalgamator<MessageLearnerLLDDandHealthProblem> _lLDDandHealthProblemAmalgamator;
        private readonly IAmalgamator<MessageLearnerProviderSpecLearnerMonitoring> _providerSpecLearnerMonitoringAmalgamator;
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;
        private IRule<DateTime?> _standardRuleDateTime;
        private IRule<string> _addressRule;
        private IRule<long?> _ulnRule;
        private IRule<long?> _alsCostrule;
        private IRule<string> _postCodeRule;
        private IRule<MessageLearnerContactPreference[]> _learnerContactPreferenceCollectionRule;
        private IRule<MessageLearnerLLDDandHealthProblem[]> _lLDDandHealthProblemCollectionRule;
        private IRule<MessageLearnerLearnerFAM[]> _learnerFAMAmalgamationRule;
        private IRule<MessageLearnerLearningDelivery[]> _learningDeliveryRule;

        public LearnerAmalgamator(
            IAmalgamator<MessageLearnerLLDDandHealthProblem> lLDDandHealthProblemAmalgamator,
            IAmalgamator<MessageLearnerProviderSpecLearnerMonitoring> providerSpecLearnerMonitoringAmalgamator,
            IAmalgamator<MessageLearnerLearnerEmploymentStatus> learnerEmploymentStatusAmalgamator,
            IAmalgamator<MessageLearnerLearnerHE> learnerHEAmalgamator,
            IAmalgamator<MessageLearnerLearningDelivery> learningDeliveryAmalgamator,
            IRuleProvider ruleProvider,
            IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.Learner, (x) => x.LearnRefNumber, amalgamationErrorHandler)
        {
            _lLDDandHealthProblemAmalgamator = lLDDandHealthProblemAmalgamator;
            _providerSpecLearnerMonitoringAmalgamator = providerSpecLearnerMonitoringAmalgamator;
            _learnerEmploymentStatusAmalgamator = learnerEmploymentStatusAmalgamator;
            _learnerHEAmalgamator = learnerHEAmalgamator;
            _learningDeliveryAmalgamator = learningDeliveryAmalgamator;

            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime?>();
            _addressRule = ruleProvider.BuildAddressRule();
            _ulnRule = ruleProvider.BuildUlnRule();
            _alsCostrule = ruleProvider.BuildAlsCostRule();
            _postCodeRule = ruleProvider.BuildPostCodeRule();
            _learnerContactPreferenceCollectionRule = ruleProvider.BuildLearnerContactPreferenceCollectionRule();
            _lLDDandHealthProblemCollectionRule = ruleProvider.BuildLLDDandHealthProblemCollectionRule();
            _learnerFAMAmalgamationRule = ruleProvider.BuildLearnerFAMAmalgamationRule();
            _learningDeliveryRule = ruleProvider.BuildLearningDeliveryRule();
        }

        public MessageLearner Amalgamate(IEnumerable<MessageLearner> models)
        {
            var messageLearner = new MessageLearner();

            ApplyRule(s => s.LearnRefNumber, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.PrevLearnRefNumber, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.PrevUKPRNNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.PMUKPRNNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.CampId, _standardRuleString.Definition, models, messageLearner);

            ApplyRule(s => s.ULNNullable, _ulnRule.Definition, models, messageLearner);
            ApplyRule(s => s.FamilyName, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.GivenNames, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.DateOfBirthNullable, _standardRuleDateTime.Definition, models, messageLearner);

            ApplyRule(s => s.EthnicityNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.Sex, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.LLDDHealthProbNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.NINumber, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.PriorAttainNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.AccomNullable, _standardRuleLong.Definition, models, messageLearner);

            ApplyRule(s => s.ALSCostNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.PlanLearnHoursNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.PlanEEPHoursNullable, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.MathGrade, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.EngGrade, _standardRuleString.Definition, models, messageLearner);

            ApplyRule(s => s.PostcodePrior, _postCodeRule.Definition, models, messageLearner);
            ApplyRule(s => s.Postcode, _postCodeRule.Definition, models, messageLearner);
            ApplyRule(s => s.AddLine1, _addressRule.Definition, models, messageLearner, Severity.Warning);
            ApplyRule(s => s.AddLine2, _addressRule.Definition, models, messageLearner, Severity.Warning);
            ApplyRule(s => s.AddLine3, _addressRule.Definition, models, messageLearner, Severity.Warning);
            ApplyRule(s => s.AddLine4, _addressRule.Definition, models, messageLearner, Severity.Warning);
            ApplyRule(s => s.TelNo, _standardRuleString.Definition, models, messageLearner);

            ApplyRule(s => s.Email, _standardRuleString.Definition, models, messageLearner, Severity.Warning);

            ApplyGroupedCollectionRule(s => s.ContactPreference, _learnerContactPreferenceCollectionRule.Definition, models, messageLearner);
            ApplyGroupedCollectionRule(s => s.LLDDandHealthProblem, _lLDDandHealthProblemCollectionRule.Definition, models, messageLearner);
            ApplyGroupedCollectionRule(s => s.LearnerFAM, _learnerFAMAmalgamationRule.Definition, models, messageLearner);

            ApplyGroupedChildCollectionRule(s => s.ProviderSpecLearnerMonitoring, g => g.ProvSpecLearnMonOccur, _providerSpecLearnerMonitoringAmalgamator, models, messageLearner);
            ApplyGroupedChildCollectionRule(s => s.LearnerEmploymentStatus, g => g.DateEmpStatApp, _learnerEmploymentStatusAmalgamator, models, messageLearner);
            ApplyGroupedChildCollectionRule(s => s.LearnerHE, g => g.LearnRefNumber, _learnerHEAmalgamator, models, messageLearner);

            ApplyGroupedCollectionRule(s => s.LearningDelivery, _learningDeliveryRule.Definition, models, messageLearner);

            return messageLearner;
        }
    }
}
