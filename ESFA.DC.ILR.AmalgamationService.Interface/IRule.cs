using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Interface
{
    public interface IRule<in T>
        where T : class
    {
        string RuleName { get; }

        void Validate(T objectToValidate);
    }
}