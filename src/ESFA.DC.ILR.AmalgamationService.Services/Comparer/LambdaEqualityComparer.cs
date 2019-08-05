using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services.Comparer
{
    public class LambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equalsFunction;

        public LambdaEqualityComparer(Func<T, T, bool> equalsFunction)
        {
            _equalsFunction = equalsFunction;
        }

        public bool Equals(T x, T y)
        {
            return _equalsFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}