using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class AlsCostRuleTest
    {
        [InlineData(123, 123, 123, true, 123)]
        [InlineData(0, 123, 123, true, 123)]
        [InlineData(0, 0, null, true, 0)]
        [InlineData(null, null, null, true, null)]
        [InlineData(123, 123, 13423, false, null)]
        [InlineData(null, 123, 1234, false, null)]
        [Theory]
        public void FirstNonNullItem(long? firstItem, long? secondItem, long? thirdItem, bool sucess, long? expectedResult)
        {
            AlsCostRule alsCostRule = new AlsCostRule();
            List<long?> items = new List<long?>() { firstItem, secondItem, thirdItem };
            var result = alsCostRule.Definition(items);
            RuleResult<long?> ruleResultExpected = new RuleResult<long?>() { Success = sucess, AmalgamatedValue = expectedResult };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }
    }
}