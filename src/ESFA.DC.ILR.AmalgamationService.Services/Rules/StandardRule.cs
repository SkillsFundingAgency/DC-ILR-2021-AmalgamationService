using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class StandardRule<T> : IRule<T>
    {
        public T Definition(IEnumerable<T> values)
        {
            if (values == null || values.Count() < 1 || values.All(x => x == null))
            {
                return default(T);
            }

            var distinctValues = values.Distinct().Where(x => x != null).ToList();

            if (distinctValues.Count <= 1)
            {
                return distinctValues.FirstOrDefault();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
