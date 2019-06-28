using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class ParentRelationshipMapperTests
    {
        [Fact]
        public void Map()
        {
            var message = new Message()
            {
                HeaderEntity = new MessageHeader()
            };

            var result = NewMapper().MapChildren(message);
        }

        private ParentRelationshipMapper NewMapper()
        {
            return new ParentRelationshipMapper();
        }
    }
}
