using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class LLDDandHealthProblemCollectionRuleTest
    {
        [Fact]
        public void LlddCat20Pass()
        {
            LLDDandHealthProblemCollectionRule lLDDandHealthProblemCollectionRule = new LLDDandHealthProblemCollectionRule();
            IEnumerable<MessageLearnerLLDDandHealthProblem> firstList = GetMessageLearnerLLDDandHealthProblems(1, "first.xml", 10);
            IEnumerable<MessageLearnerLLDDandHealthProblem> secondList = GetMessageLearnerLLDDandHealthProblems(11, "second.xml", 10);
            List<MessageLearnerLLDDandHealthProblem[]> items = new List<MessageLearnerLLDDandHealthProblem[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = lLDDandHealthProblemCollectionRule.Definition(items);

            List<MessageLearnerLLDDandHealthProblem> expectedResult = new List<MessageLearnerLLDDandHealthProblem>();
            expectedResult.AddRange(firstList);
            expectedResult.AddRange(secondList);

            RuleResult<MessageLearnerLLDDandHealthProblem[]> ruleResultExpected = new RuleResult<MessageLearnerLLDDandHealthProblem[]>()
            {
                Success = true,
                AmalgamatedValue = expectedResult.ToArray(),
            };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void LlddCat23Fail()
        {
            LLDDandHealthProblemCollectionRule lLDDandHealthProblemCollectionRule = new LLDDandHealthProblemCollectionRule();
            IEnumerable<MessageLearnerLLDDandHealthProblem> firstList = GetMessageLearnerLLDDandHealthProblems(1, "first.xml", 11);
            IEnumerable<MessageLearnerLLDDandHealthProblem> secondList = GetMessageLearnerLLDDandHealthProblems(12, "second.xml", 11);
            List<MessageLearnerLLDDandHealthProblem[]> items = new List<MessageLearnerLLDDandHealthProblem[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = lLDDandHealthProblemCollectionRule.Definition(items);

            result.Success.Should().Equals(false);
            result.AmalgamationValidationErrors.Count().Should().Equals(22);
        }

        [Fact]
        public void OnePrimaryLlddPass()
        {
            LLDDandHealthProblemCollectionRule lLDDandHealthProblemCollectionRule = new LLDDandHealthProblemCollectionRule();
            List<MessageLearnerLLDDandHealthProblem> firstList = GetMessageLearnerLLDDandHealthProblems(1, "first.xml", 10).ToList();
            firstList.First().PrimaryLLDDNullable = 1;
            List<MessageLearnerLLDDandHealthProblem> secondList = GetMessageLearnerLLDDandHealthProblems(12, "second.xml", 10).ToList();
            List<MessageLearnerLLDDandHealthProblem[]> items = new List<MessageLearnerLLDDandHealthProblem[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = lLDDandHealthProblemCollectionRule.Definition(items);

            List<MessageLearnerLLDDandHealthProblem> expectedResult = new List<MessageLearnerLLDDandHealthProblem>();
            expectedResult.AddRange(firstList);
            expectedResult.AddRange(secondList);

            RuleResult<MessageLearnerLLDDandHealthProblem[]> ruleResultExpected = new RuleResult<MessageLearnerLLDDandHealthProblem[]>()
            {
                Success = true,
                AmalgamatedValue = expectedResult.ToArray(),
            };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void MoreThanOnePrimaryLlddFail()
        {
            LLDDandHealthProblemCollectionRule lLDDandHealthProblemCollectionRule = new LLDDandHealthProblemCollectionRule();
            List<MessageLearnerLLDDandHealthProblem> firstList = GetMessageLearnerLLDDandHealthProblems(1, "first.xml", 10).ToList();
            firstList[0].PrimaryLLDDNullable = 1;
            List<MessageLearnerLLDDandHealthProblem> secondList = GetMessageLearnerLLDDandHealthProblems(12, "second.xml", 10).ToList();
            secondList.First().PrimaryLLDDNullable = 1;
            List<MessageLearnerLLDDandHealthProblem[]> items = new List<MessageLearnerLLDDandHealthProblem[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = lLDDandHealthProblemCollectionRule.Definition(items);

            result.Success.Should().Equals(false);
            result.AmalgamationValidationErrors.Count().Should().Equals(2);
        }

        private IEnumerable<MessageLearnerLLDDandHealthProblem> GetMessageLearnerLLDDandHealthProblems(int seedCat, string filename, int count)
        {
            for (int index = 0; index < count; index++)
            {
                yield return GetMessageLearnerLLDDandHealthProblem(seedCat + index, filename);
            }
        }

        private MessageLearnerLLDDandHealthProblem GetMessageLearnerLLDDandHealthProblem(long lLDDCat, string filename)
        {
            return new MessageLearnerLLDDandHealthProblem() { LLDDCatNullable = lLDDCat, Parent = GetMessageLearner(filename) };
        }

        private MessageLearner GetMessageLearner(string filename)
        {
            return new MessageLearner() { Parent = new Message() { Parent = new AmalgamationRoot() { Filename = filename } } };
        }
    }
}