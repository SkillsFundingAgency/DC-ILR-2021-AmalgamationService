﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerAmalgamator : AbstractAmalgamator, IAmalgamator<MessageLearner>
    {
        private readonly IAmalgamator<MessageLearnerLearnerEmploymentStatus> _learnerEmploymentStatusAmalgamator;

        private IRule<string> _standardRuleString;
        private IRule<long> _standardRuleLong;
        private IRule<DateTime> _standardRuleDateTime;
        private IRule<string> _addressRule;

        public LearnerAmalgamator(
            IAmalgamator<MessageLearnerLearnerEmploymentStatus> learnerEmploymentStatusAmalgamator,
           IRuleProvider ruleProvider)
        {
            _learnerEmploymentStatusAmalgamator = learnerEmploymentStatusAmalgamator;

            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
            _addressRule = ruleProvider.BuildAddressRule();
        }

        public MessageLearner Amalgamate(IEnumerable<MessageLearner> models)
        {
            var messageLearner = new MessageLearner() { LearnRefNumber = models.FirstOrDefault().LearnRefNumber };

            ApplyRule(s => s.PrevLearnRefNumber, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.PrevUKPRN, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.PMUKPRN, _standardRuleLong.Definition, models, messageLearner);
            ApplyRule(s => s.CampId, _standardRuleString.Definition, models, messageLearner);

            ApplyRule(s => s.FamilyName, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.GivenNames, _standardRuleString.Definition, models, messageLearner);
            ApplyRule(s => s.DateOfBirth, _standardRuleDateTime.Definition, models, messageLearner);

            ApplyGroupedChildCollectionRule(s => s.LearnerEmploymentStatus, g => g.DateEmpStatApp, _learnerEmploymentStatusAmalgamator, models, messageLearner);

            return messageLearner;
        }
    }
}
