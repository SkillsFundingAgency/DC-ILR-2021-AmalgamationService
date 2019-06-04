using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class DummyTest
    {
        [Fact]
        public void AlwaysTrue()
        {
            Assert.Equal(1, 1);
        }
    }
}
