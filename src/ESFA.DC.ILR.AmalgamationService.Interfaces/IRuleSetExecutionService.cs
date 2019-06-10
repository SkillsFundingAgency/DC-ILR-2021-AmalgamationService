using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleSetExecutionService<T>
         where T : class
    {
        void Execute(IEnumerable<IRule<T>> ruleSet, T objectToValidate);
    }
}
