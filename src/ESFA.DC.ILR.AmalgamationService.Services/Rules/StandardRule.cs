using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class StandardRule<T> : IRule<T>
    {
        public IRuleResult<T> Definition(IEnumerable<T> values)
        {
            if (values == null || !values.Any() || values.All(x => x == null))
            {
                return new RuleResult<T>();
            }

            IEnumerable<T> distinctValues;

            if (typeof(T) == typeof(string))
            {
                distinctValues = (IEnumerable<T>)((IEnumerable<string>)values).Distinct(StringComparer.CurrentCultureIgnoreCase).Where(x => x != null).ToList();
            }
            else
            {
                distinctValues = values.Distinct().Where(x => x != null).ToList();
            }

            if (distinctValues.Count() == 1)
            {
                return new RuleResult<T>()
                {
                    Success = true,
                    AmalgamatedValue = distinctValues.First()
                };
            }
            else
            {
                return new RuleResult<T>()
                {
                    Success = false
                };
            }
        }
    }
}
