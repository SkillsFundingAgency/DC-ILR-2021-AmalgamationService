using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract;
using ESFA.DC.ILR.Model.Loose;
using FluentAssertions;
using Moq;
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

        [Fact]
        public void AmalgamatePrevLearnRefNumber_Success()
        {
            var inputModels = GetInputModels(l => l.PrevLearnRefNumber, _testString1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).PrevLearnRefNumber.Should().Be(_testString1);
        }

        [Fact]
        public void AmalgamatePrevLearnRefNumber_Failure()
        {
            var inputModels = GetInputModels(l => l.PrevLearnRefNumber, _testString1, () => new MessageLearner());
            inputModels[0].PrevLearnRefNumber = _testString2;
            var amalgamatedMessageLearner = Assert.Throws<Exception>(() => BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels));
        }

        [Fact]
        public void AmalgamatePrevUKPRN_Success()
        {
            var inputModels = GetInputModels(l => l.PrevUKPRN, _testLong1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).PrevUKPRN.Should().Be(_testLong1);
        }

        [Fact]
        public void AmalgamatePMUKPRN_Success()
        {
            var inputModels = GetInputModels(l => l.PMUKPRN, _testLong1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).PMUKPRN.Should().Be(_testLong1);
        }

        [Fact]
        public void AmalgamateCampId_Success()
        {
            var inputModels = GetInputModels(l => l.CampId, _testString1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).CampId.Should().Be(_testString1);
        }

        [Fact]
        public void AmalgamateFamilyName_Success()
        {
            var inputModels = GetInputModels(l => l.FamilyName, _testString1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).FamilyName.Should().Be(_testString1);
        }

        [Fact]
        public void AmalgamateGivenNames_Success()
        {
            var inputModels = GetInputModels(l => l.GivenNames, _testString1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).GivenNames.Should().Be(_testString1);
        }

        [Fact]
        public void AmalgamateDateOfBirth_Success()
        {
            var inputModels = GetInputModels(l => l.DateOfBirth, _testDateTime1, () => new MessageLearner());
            BuildAmalgamator(null, _ruleProvider).Amalgamate(inputModels).DateOfBirth.Should().Be(_testDateTime1);
        }

        public LearnerAmalgamator BuildAmalgamator(
            IAmalgamator<MessageLearnerLLDDandHealthProblem> llddAndHealthProblemAmalgamator = null,
            IRuleProvider ruleProvider = null)
        {
            return new LearnerAmalgamator(
                llddAndHealthProblemAmalgamator ?? Mock.Of<IAmalgamator<MessageLearnerLLDDandHealthProblem>>(),
                ruleProvider ?? Mock.Of<IRuleProvider>());
        }
    }
}