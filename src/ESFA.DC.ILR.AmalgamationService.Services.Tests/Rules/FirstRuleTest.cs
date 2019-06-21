using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using System;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class FirstRuleTest
    {
        [Fact]
        public void AlwaysTrue()
        {
            FirstRule<DateTime> firstRule = new FirstRule<DateTime>();
            List<DateTime> items = new List<DateTime>() { new DateTime(2019, 06, 22), new DateTime(2019, 06, 22) };
            var result = firstRule.Definition(items);
            Assert.Equal(items[0], result);
        }

        [Fact]
        public void AlwaysFail()
        {
            FirstRule<DateTime> firstRule = new FirstRule<DateTime>();
            List<DateTime> items = new List<DateTime>() { new DateTime(2019, 06, 22), new DateTime(2019, 06, 21) };

            var result = firstRule.Definition(items);
            Assert.NotEqual(items[1], result);
        }
    }
}
