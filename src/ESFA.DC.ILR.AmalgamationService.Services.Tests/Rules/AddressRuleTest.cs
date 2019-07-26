using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class AddressRuleTest
    {
        [Fact]
        public void AlwaysTrue()
        {
            string strTestAddress = "address 1";

            AddressRule addressRule = new AddressRule();
            List<string> adresses = new List<string>() { strTestAddress, strTestAddress };
            var result = addressRule.Definition(adresses);
            RuleResult<string> ruleResultExpected = new RuleResult<string>() { Success = true, AmalgamatedValue = strTestAddress };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }

        [Fact]
        public void AlwaysFail()
        {
            AddressRule addressRule = new AddressRule();
            List<string> adresses = new List<string>() { "address 1", "address 2" };
            var result = addressRule.Definition(adresses);
            RuleResult<string> ruleResultExpected = new RuleResult<string>() { Success = false };
            result.Should().BeEquivalentTo(ruleResultExpected);
        }
    }
}
