using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class LearnerEmploymentStatusAmalgamatorTest : AbstractAmalgamatorTest
    {
        private IRuleProvider _ruleProvider = new RuleProvider();

        private LearnerEmploymentStatusAmalgamator _learnerEmploymentStatusAmalgamator;
        private AmalgamationErrorHandler _amalgamationErrorHandler;

        [Fact]
        public void AmalgamateEmploymentStatus_Success()
        {
            InitializeAmalgamator();
            MessageLearner messageLearner = new MessageLearner() { Parent = new Message() { Parent = new AmalgamationRoot() { Filename = "xyz.xml", Message = new Message() } } };

            MessageLearnerLearnerEmploymentStatus[] messageLearnerLearnerEmploymentStatuses =
            {
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, Parent = messageLearner },
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, Parent = messageLearner },
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, AgreeId = "4", Parent = messageLearner }
            };

            var expectedResult = new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, AgreeId = "4" };

            var amalgamated = _learnerEmploymentStatusAmalgamator.Amalgamate(messageLearnerLearnerEmploymentStatuses);
            Assert.Equal(amalgamated.DateEmpStatApp, expectedResult.DateEmpStatApp);
            Assert.Equal(amalgamated.EmpStat, expectedResult.EmpStat);
            Assert.Equal(amalgamated.EmpId, expectedResult.EmpId);
            Assert.Equal(amalgamated.AgreeId, expectedResult.AgreeId);
        }

        [Fact]
        public void AmalgamateEmploymentStatus_Fail()
        {
            InitializeAmalgamator();
            MessageLearner messageLearner = new MessageLearner() { LearnRefNumber = "123", Parent = new Message() { Parent = new AmalgamationRoot() { Filename = "xyz.xml", Message = new Message() } } };

            MessageLearnerLearnerEmploymentStatus[] messageLearnerLearnerEmploymentStatuses =
            {
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 1, EmpId = 3, Parent = messageLearner },
            new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, Parent = messageLearner },
            };

            var expectedResult = new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = new DateTime(2019, 06, 01), EmpStat = 2, EmpId = 3, AgreeId = "4" };

            var amalgamated = _learnerEmploymentStatusAmalgamator.Amalgamate(messageLearnerLearnerEmploymentStatuses);
            var validationError = _amalgamationErrorHandler.Errors;

            validationError.Count.Equals(2);
        }

        private void InitializeAmalgamator()
        {
            _amalgamationErrorHandler = new AmalgamationErrorHandler();
            _learnerEmploymentStatusAmalgamator = new LearnerEmploymentStatusAmalgamator(_ruleProvider, _amalgamationErrorHandler);
        }
    }
}