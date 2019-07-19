using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ESFA.DC.ILR.AmalgamationService.Services.Comparer
{
    public class PostCodeComparer : EqualityComparer<string>
    {
        public override bool Equals(string x, string y)
        {
            return x == y || (x != null && GetStrippedPostcode(x).Equals(GetStrippedPostcode(y), StringComparison.OrdinalIgnoreCase));
        }

        public override int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }

        private string GetStrippedPostcode(string str)
        {
            return Regex.Replace(str, "[\\s*\\-*\\'*\\,*\\.*]", string.Empty);
        }
    }
}
