using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class AddressRuleTest
    {
        [Fact]
        public void AlwaysTrue()
        {
            AddressRule addressRule = new AddressRule();
            List<string> adresses = new List<string>() { "address 1", "address 1" };
            var result = addressRule.Definition(adresses);
            Assert.Equal(adresses[0], result);
        }

        [Fact]
        public void AlwaysFail()
        {
            AddressRule addressRule = new AddressRule();
            List<string> adresses = new List<string>() { "address 1", "address 2" };

            // TODO expect error once error handling is done
            Assert.Throws<Exception>(() => addressRule.Definition(adresses));
        }
    }
}
