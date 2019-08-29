using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class StandardRuleFirstItemTest
    {
        [Fact]
        public void TrueForSameValues()
        {
            StandardRuleFirstItem<long> standardRule = new StandardRuleFirstItem<long>();
            List<long> items = new List<long>() { 123, 123 };
            var result = standardRule.Definition(items);
            RuleResult<long> ruleResultExpected = new RuleResult<long>() { Success = true, AmalgamatedValue = 123 };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void FirstItemForConflict()
        {
            StandardRuleFirstItem<long> standardRule = new StandardRuleFirstItem<long>();
            List<long> items = new List<long>() { 123, 1234 };
            var result = standardRule.Definition(items);
            RuleResult<long> ruleResultExpected = new RuleResult<long>() { Success = false, AmalgamatedValue = 123 };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }
    }
}