using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRule<in T>
        where T : class
    {
        string RuleName { get; }

        void Validate(T objectToValidate);
    }
}
