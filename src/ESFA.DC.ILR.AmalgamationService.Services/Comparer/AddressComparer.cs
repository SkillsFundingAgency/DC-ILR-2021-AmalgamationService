using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ESFA.DC.ILR.AmalgamationService.Services.Comparer
{
    public class AddressComparer : EqualityComparer<string>
    {
        private readonly IEnumerable<char> _removeCharacters = new HashSet<char>
        {
            ' ',
            '-',
            '\'',
            ',',
            '.'
        };

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
            return string.IsNullOrEmpty(str) ? str : new string(str.Where(c => !_removeCharacters.Contains(c)).ToArray());
        }
    }
}
