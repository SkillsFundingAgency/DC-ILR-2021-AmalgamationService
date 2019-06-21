using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract;
using ESFA.DC.ILR.Model.Loose;
using FluentAssertions;
using System;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class LearnerEmploymentStatusAmalgamatorTest : AbstractAmalgamatorTest
    {
        private IRuleProvider _ruleProvider = new RuleProvider();

        private LearnerEmploymentStatusAmalgamator _learnerEmploymentStatusAmalgamator;

        public LearnerEmploymentStatusAmalgamatorTest()
        {
            _learnerEmploymentStatusAmalgamator = new LearnerEmploymentStatusAmalgamator(_ruleProvider);
        }

        [Fact]
        public void AmalgamateEmploymentStatus_Success()
        {
            MessageLearnerLearnerEmploymentStatus[] messageLearnerLearnerEmploymentStatuses =
            {
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3 },
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3 },
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, AgreeId = "4" }
            };

            var expectedResult = new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, AgreeId = "4" };

            var amalgamated = _learnerEmploymentStatusAmalgamator.Amalgamate(messageLearnerLearnerEmploymentStatuses);
            amalgamated.Should().BeEquivalentTo(expectedResult);
        }
    }
}