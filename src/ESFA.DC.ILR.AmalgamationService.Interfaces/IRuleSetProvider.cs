using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleSetProvider<in T>
        where T : class
    {
        IEnumerable<IRule<T>> ProvideAsync();
    }
}
