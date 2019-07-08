using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
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
            ILooseMessage message = new Message()
            {
                HeaderEntity = new MessageHeader()
                {
                    CollectionDetails = new MessageHeaderCollectionDetails()
                }
            };

            var result = NewMapper().MapChildren<ILooseMessage>(message);
        }

        private ParentRelationshipMapper NewMapper()
        {
            return new ParentRelationshipMapper();
        }
    }
}
