using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class LLDDandHealthProblemAmalgamatorTest : BaseAmalgamatorTest
    {
        private readonly IRuleProvider _ruleProvider = new RuleProvider();
        private readonly IAmalgamationErrorHandler _amalgamationErrorHandler = new AmalgamationErrorHandler();

        [Fact]
        public void Amalgamate_Pass()
        {
            long? lLDDCat = 12;
            long? primaryLLDD = 15;

            MessageLearner messageLearner = new MessageLearner()
            {
                LearnRefNumber = "123",
                Parent = new Message() { Parent = new AmalgamationRoot() { Filename = "xyz.xml", Message = new Message() } }
            };

            List<MessageLearnerLLDDandHealthProblem> msgLearnersLLDDHealthProblem = new List<MessageLearnerLLDDandHealthProblem>()
            {
                new MessageLearnerLLDDandHealthProblem()
                {
                    LLDDCat = lLDDCat,
                    PrimaryLLDD = primaryLLDD,
                    Parent = messageLearner
                },
                 new MessageLearnerLLDDandHealthProblem()
                {
                    LLDDCat = lLDDCat,
                    PrimaryLLDD = primaryLLDD,
                    Parent = messageLearner
                }
            };

            var amalgamate = NewAmalgamator(_ruleProvider, _amalgamationErrorHandler).Amalgamate(msgLearnersLLDDHealthProblem);

            amalgamate.LLDDCat.Should().Be(lLDDCat);
            amalgamate.PrimaryLLDD.Should().Be(primaryLLDD);
        }

        [Fact]
        public void Amalgamate_Errors()
        {
            long? lLDDCat = 12;
            long? primaryLLDD = 15;

            MessageLearner messageLearner = new MessageLearner()
            {
                LearnRefNumber = "123",
                Parent = new Message() { Parent = new AmalgamationRoot() { Filename = "xyz.xml", Message = new Message() } }
            };

            List<MessageLearnerLLDDandHealthProblem> msgLearnersLLDDHealthProblem = new List<MessageLearnerLLDDandHealthProblem>()
            {
                new MessageLearnerLLDDandHealthProblem()
                {
                    LLDDCat = lLDDCat,
                    PrimaryLLDD = 20,
                    Parent = messageLearner
                },
                 new MessageLearnerLLDDandHealthProblem()
                {
                    LLDDCat = lLDDCat,
                    PrimaryLLDD = primaryLLDD,
                    Parent = messageLearner
                }
            };

            var amalgamate = NewAmalgamator(_ruleProvider, _amalgamationErrorHandler).Amalgamate(msgLearnersLLDDHealthProblem);
            var validationError = _amalgamationErrorHandler.Errors;

            validationError.Should().HaveCount(2);
        }

        public LLDDandHealthProblemAmalgamator NewAmalgamator(
                                        IRuleProvider ruleProvider = null,
                                        IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return new LLDDandHealthProblemAmalgamator(
                                         ruleProvider ?? Mock.Of<IRuleProvider>(),
                                         amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }
    }
}
