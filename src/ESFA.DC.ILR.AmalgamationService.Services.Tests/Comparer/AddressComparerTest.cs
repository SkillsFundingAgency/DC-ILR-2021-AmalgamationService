using ESFA.DC.ILR.AmalgamationService.Services.Comparer;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Comparer
{
    public class AddressComparerTest
    {
        [Fact]
        public void AlwaysTrue()
        {
            AddressComparer addressComparer = new AddressComparer();
            Assert.True(addressComparer.Equals(null, null));
            Assert.True(addressComparer.Equals(string.Empty, string.Empty));
            Assert.True(addressComparer.Equals("Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.True(addressComparer.Equals("Cheylesmore House5 Quinton Rd Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.True(addressComparer.Equals("Cheylesmore House. 5 'Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.True(addressComparer.Equals("Cheylesmore House,-- 5 Quinton Rd, Coventry CV12WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.True(addressComparer.Equals("Cheylesmore House,     5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.True(addressComparer.Equals("Cheylesmore House, 5 QUINTON Rd,'' Coventry CV1 2WT ,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.True(addressComparer.Equals(" Cheylesmore House,,, 5 Quinton Rd, Coventry CV1 2WT", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
        }

        [Fact]
        public void AlwaysFail()
        {
            AddressComparer addressComparer = new AddressComparer();
            Assert.False(addressComparer.Equals(null, string.Empty));
            Assert.False(addressComparer.Equals(string.Empty, null));
            Assert.False(addressComparer.Equals("Cheylesmore House, 6 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.False(addressComparer.Equals("Cheylesmore House5  **Rd Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.False(addressComparer.Equals("?Cheylesmore House. 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.False(addressComparer.Equals(">Cheylesmore House, 5 Quinton Rd, Coventry CV12WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.False(addressComparer.Equals("Cheylesmore House< 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.False(addressComparer.Equals("Cheylesmorerouse, 5 Quinton Rd, Coventry CV1 2WT ,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
            Assert.False(addressComparer.Equals(" Cheylesmore$$ House, 5 Quinton Rd, Coventry CV1 2WT,", "Cheylesmore House, 5 Quinton Rd, Coventry CV1 2WT,"));
        }
    }
}
