using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class PostCodeRuleTest
    {
        [Theory]
        [InlineData("CV1 2WT", "CV1 2WT", "CV1 2WT", "CV1 2WT")]
        [InlineData("CV1 2WT", null, null, "CV1 2WT")]
        [InlineData("CV1 2WT", "ZZ99 9ZZ", null, "CV1 2WT")]
        [InlineData(null, null, null, "")]
        public void PostCodeRuleTest_True(string firstItem, string secondItem, string thirdItem, string expectedResult)
        {
            var postCodeRule = new PostCodeRule();
            List<string> items = new List<string>() { firstItem, secondItem, thirdItem };
            var result = postCodeRule.Definition(items);
            result.Success.Should().BeTrue();
            result.AmalgamatedValue.Equals(expectedResult);
        }

        [Theory]
        [InlineData("CV1 2WT", "CV2 1WT", "CV1 2WT")]
        [InlineData("CV1 2WT", null, "CV2 1WT")]
        [InlineData("CV1 2WT", "CV1 3WT", "CV1 4WT")]
        [InlineData("CV1 2WT", "ZZ99 9ZZ", "CV1 4WT")]
        public void PostCodeRuleTest_False(string firstItem, string secondItem, string thirdItem)
        {
            var postCodeRule = new PostCodeRule();
            List<string> items = new List<string>() { firstItem, secondItem, thirdItem };
            var result = postCodeRule.Definition(items);
            result.Success.Should().BeFalse();
            result.AmalgamatedValue.Should().BeNull();
        }
    }
}