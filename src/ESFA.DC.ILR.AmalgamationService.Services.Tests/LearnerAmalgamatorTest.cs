﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class LearnerAmalgamatorTest : BaseAmalgamatorTest
    {
        private string _testString1 = "string1";
        private long _testLong1 = 1111111;
        private DateTime _testDateTime1 = new DateTime(2020, 6, 1);

        private string _testString2 = "string2";
        private long _testLong2 = 2222222;
        private DateTime _testDateTime2 = new DateTime(2017, 6, 1);

        private IRuleProvider _ruleProvider = new RuleProvider();
        private LearnerAmalgamator _learnerAmalgamator;
        private AmalgamationErrorHandler _amalgamationErrorHandler;

        [Fact]
        public void Amalgamate_Success()
        {
            InitializeAmalgamator();
            MessageLearner messageLearner1 = new MessageLearner() { LearnRefNumber = _testString1, PrevLearnRefNumber = _testString1, PMUKPRN = _testLong1, PrevUKPRN = _testLong1, DateOfBirth = _testDateTime1, FamilyName = _testString1, GivenNames = _testString1 };
            MessageLearner messageLearner2 = new MessageLearner() { LearnRefNumber = _testString1, PMUKPRN = _testLong1, PrevUKPRN = _testLong1, CampId = _testString1, DateOfBirth = _testDateTime1 };
            List<MessageLearner> messageLearners = new List<MessageLearner>() { messageLearner1, messageLearner2 };

            MessageLearner expectedResult = new MessageLearner() { LearnRefNumber = _testString1, PrevLearnRefNumber = _testString1, PrevUKPRN = _testLong1, FamilyName = _testString1, GivenNames = _testString1, PMUKPRN = _testLong1, CampId = _testString1, DateOfBirth = _testDateTime1 };

            //var amalgamated = _learnerAmalgamator.Amalgamate(messageLearners);

            // TODO : Need to revisit test strategy for entities.
            //Assert.Equal(amalgamated.LearnRefNumber, expectedResult.LearnRefNumber);
            //Assert.Equal(amalgamated.PrevLearnRefNumber, expectedResult.PrevLearnRefNumber);
            //Assert.Equal(amalgamated.PrevUKPRN, expectedResult.PrevUKPRN);
            //Assert.Equal(amalgamated.DateOfBirth, expectedResult.DateOfBirth);
            //Assert.Equal(amalgamated.FamilyName, expectedResult.FamilyName);
            //Assert.Equal(amalgamated.GivenNames, expectedResult.GivenNames);
            //Assert.Equal(amalgamated.CampId, expectedResult.CampId);
        }

        public LearnerAmalgamator BuildAmalgamator(
            IAmalgamator<MessageLearnerLearnerEmploymentStatus> learnerEmploymentStatusAmalgamator = null,
            IAmalgamator<MessageLearnerLearnerHE> learnerHEAmalgamator = null,
            IRuleProvider ruleProvider = null,
            IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return null;
            //new LearnerAmalgamator(
            //learnerEmploymentStatusAmalgamator ?? Mock.Of<IAmalgamator<MessageLearnerLearnerEmploymentStatus>>(),
            //learnerHEAmalgamator ?? Mock.Of<IAmalgamator<MessageLearnerLearnerHE>>(),
            //ruleProvider ?? Mock.Of<IRuleProvider>(),
            //amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }

        private void InitializeAmalgamator()
        {
            _amalgamationErrorHandler = new AmalgamationErrorHandler();
            LearnerEmploymentStatusMonitoringAmalgamator learnerEmploymentStatusMonitoringAmalgamator = new LearnerEmploymentStatusMonitoringAmalgamator(_ruleProvider, _amalgamationErrorHandler);
            LearnerEmploymentStatusAmalgamator learnerEmploymentStatusAmalgamator = new LearnerEmploymentStatusAmalgamator(learnerEmploymentStatusMonitoringAmalgamator, _ruleProvider, _amalgamationErrorHandler);
            LearnerHEFinancialSupportAmalgamator learnerHEFinancialSupportAmalgamator = new LearnerHEFinancialSupportAmalgamator(_ruleProvider, _amalgamationErrorHandler);
            LearnerHEAmalgamator learnerHEAmalgamator = new LearnerHEAmalgamator(learnerHEFinancialSupportAmalgamator, _ruleProvider, _amalgamationErrorHandler);

            _learnerAmalgamator = BuildAmalgamator(learnerEmploymentStatusAmalgamator, learnerHEAmalgamator, _ruleProvider, _amalgamationErrorHandler);
        }
    }
}