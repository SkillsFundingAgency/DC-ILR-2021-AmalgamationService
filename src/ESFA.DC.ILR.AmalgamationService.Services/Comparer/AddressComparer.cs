using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ESFA.DC.ILR.AmalgamationService.Services.Comparer
{
    public class AddressComparer : EqualityComparer<string>
    {
        public override bool Equals(string x, string y)
        {
            return x == y || (x != null && GetStrippedAddress(x).Equals(GetStrippedAddress(y), StringComparison.OrdinalIgnoreCase));
        }

        public override int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }

        private string GetStrippedAddress(string str)
        {
            return string.IsNullOrEmpty(str) ? str : Regex.Replace(str, "[\\s*\\-*\\'*\\,*\\.*]", string.Empty);
        }
    }
}
