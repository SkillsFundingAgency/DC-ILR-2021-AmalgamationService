using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory
{
    public class RuleProvider : IRuleProvider
    {
        public IRule<T> BuildStandardRule<T>()
        {
            return new StandardRule<T>();
        }

        public IRule<string> BuildAddressRule()
        {
            return new AddressRule();
        }
    }
}
