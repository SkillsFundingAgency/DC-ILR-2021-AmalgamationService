using ESFA.DC.ILR.AmalgamationService.Services.Comparer;
using System.Linq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Comparer
{
    public class AddressComparerTest
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House5 Quinton Rd Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House. 5 'Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House,-- 5 Quinton Rd, Coventry CV12WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House,     5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House, 5 QUINTON Rd,'' Coventry CV1 2WT ,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData(" Cheylesmore House,,, 5 Quinton Rd, Coventry CV1 2WT", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        public void AlwaysTrue(string left, string right)
        {
            AddressComparer addressComparer = new AddressComparer();

            Assert.True(addressComparer.Equals(left, right));
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData("Cheylesmore House, 6 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House5  **Rd Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("?Cheylesmore House. 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData(">Cheylesmore House, 5 Quinton Rd, Coventry CV12WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmore House< 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData("Cheylesmorerouse, 5 Quinton Rd, Coventry CV1 2WT ,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        [InlineData(" Cheylesmore$$ House, 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,")]
        public void AlwaysFail(string left, string right)
        {
            AddressComparer addressComparer = new AddressComparer();

            Assert.False(addressComparer.Equals(left, right));
        }

        [Fact]
        public void Performance()
        {
            AddressComparer addressComparer = new AddressComparer();

            foreach (var i in Enumerable.Range(0, 1000000))
            {
                addressComparer.Equals("Cheylesmore House, 6 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,");
            }
        }
    }
}
