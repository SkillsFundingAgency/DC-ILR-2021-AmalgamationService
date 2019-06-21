﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract;
using ESFA.DC.ILR.Model.Loose;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class LearnerAmalgamatorTest : AbstractAmalgamatorTest
    {
        private string _testString1 = "string1";
        private long _testLong1 = 1111111;
        private DateTime _testDateTime1 = new DateTime(2019, 6, 1);

        private string _testString2 = "string2";
        private long _testLong2 = 2222222;
        private DateTime _testDateTime2 = new DateTime(2017, 6, 1);

        private IRuleProvider _ruleProvider = new RuleProvider();
        private LearnerAmalgamator _learnerAmalgamator;

        public LearnerAmalgamatorTest()
        {
            LearnerEmploymentStatusAmalgamator learnerEmploymentStatusAmalgamator = new LearnerEmploymentStatusAmalgamator(_ruleProvider);
            _learnerAmalgamator = BuildAmalgamator(learnerEmploymentStatusAmalgamator, _ruleProvider);
        }

        [Fact]
        public void Amalgamate_Success()
        {
            MessageLearner messageLearner1 = new MessageLearner() { LearnRefNumber = _testString1, PrevLearnRefNumber = _testString1, PMUKPRN = _testLong1, PrevUKPRN = _testLong1, DateOfBirth = _testDateTime1, FamilyName = _testString1, GivenNames = _testString1 };
            MessageLearner messageLearner2 = new MessageLearner() { LearnRefNumber = _testString1, PMUKPRN = _testLong1, PrevUKPRN = _testLong1, CampId = _testString1, DateOfBirth = _testDateTime1 };
            List<MessageLearner> messageLearners = new List<MessageLearner>() { messageLearner1, messageLearner2 };

            MessageLearner messageLearnerExpected = new MessageLearner() { LearnRefNumber = _testString1, PrevLearnRefNumber = _testString1, PrevUKPRN = _testLong1, FamilyName = _testString1, GivenNames = _testString1, PMUKPRN = _testLong1, CampId = _testString1, DateOfBirth = _testDateTime1 };

            var amalgamated = _learnerAmalgamator.Amalgamate(messageLearners);

            amalgamated.Should().BeEquivalentTo(messageLearnerExpected);
        }

        public LearnerAmalgamator BuildAmalgamator(
            IAmalgamator<MessageLearnerLearnerEmploymentStatus> learnerEmploymentStatusAmalgamator = null,
            IRuleProvider ruleProvider = null)
        {
            return new LearnerAmalgamator(
                learnerEmploymentStatusAmalgamator ?? Mock.Of<IAmalgamator<MessageLearnerLearnerEmploymentStatus>>(),
                ruleProvider ?? Mock.Of<IRuleProvider>());
        }
    }
}