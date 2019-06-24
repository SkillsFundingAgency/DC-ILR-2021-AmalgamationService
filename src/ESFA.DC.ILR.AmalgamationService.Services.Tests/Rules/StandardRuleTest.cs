using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
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
            Assert.Equal(items[0], result);
        }

        [Fact]
        public void AlwaysFailNull()
        {
            StandardRule<long?> standardRule = new StandardRule<long?>();
            Assert.Null(standardRule.Definition(null));
        }

        [Fact]
        public void AlwaysFailEmptyList()
        {
            StandardRule<long?> standardRule = new StandardRule<long?>();
            Assert.Null(standardRule.Definition(new List<long?>()));
        }

        [Fact]
        public void AlwaysFailListOfNulls()
        {
            StandardRule<string> standardRule = new StandardRule<string>();
            Assert.Null(standardRule.Definition(new List<string>() { null, null }));
        }

        [Fact]
        public void AlwaysFail()
        {
            StandardRule<long> standardRule = new StandardRule<long>();
            List<long> items = new List<long>() { 123, 124 };

            // TODO expect error once error handling is done
            Assert.Throws<Exception>(() => standardRule.Definition(items));
        }
    }
}
