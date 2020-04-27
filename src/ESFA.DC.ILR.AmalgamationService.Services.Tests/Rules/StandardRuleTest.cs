using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class StandardRuleTest
    {
        [Fact]
        public void AlwaysTrue()
        {
            StandardRule<long> standardRule = new StandardRule<long>();
            List<long> items = new List<long>() { 123, 123 };
            var result = standardRule.Definition(items);
            RuleResult<long> ruleResultExpected = new RuleResult<long>() { Success = true, AmalgamatedValue = 123 };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void AlwaysFailNull()
        {
            StandardRule<long?> standardRule = new StandardRule<long?>();
            var result = standardRule.Definition(null);
            RuleResult<long?> ruleResultExpected = new RuleResult<long?>();
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void AlwaysFailEmptyList()
        {
            StandardRule<long?> standardRule = new StandardRule<long?>();
            var result = standardRule.Definition(new List<long?>());
            RuleResult<long?> ruleResultExpected = new RuleResult<long?>();
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void AlwaysFailListOfNulls()
        {
            StandardRule<string> standardRule = new StandardRule<string>();
            var result = standardRule.Definition(new List<string>() { null, null });
            RuleResult<string> ruleResultExpected = new RuleResult<string>();
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void AlwaysFail()
        {
            StandardRule<long> standardRule = new StandardRule<long>();
            List<long> items = new List<long>() { 123, 124 };

            var result = standardRule.Definition(items);
            RuleResult<long> ruleResultExpected = new RuleResult<long>() { Success = false };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }
    }
}
