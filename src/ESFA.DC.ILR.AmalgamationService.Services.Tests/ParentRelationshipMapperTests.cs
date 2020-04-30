using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Linq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class ParentRelationshipMapperTests
    {
        [Fact]
        public void Map()
        {
            MessageLearner[] learners = new[]
            {
                new MessageLearner()
                {
                    LearnRefNumber = "123",
                },
                new MessageLearner() { LearnRefNumber = "1231" }
            };

            var ldps = new MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring()
            {
                ProvSpecDelMon = "ProvSpecDelMon"
            };

            var ld = new MessageLearnerLearningDelivery()
            {
                ConRefNumber = "conref1"
            };

            ld.ProviderSpecDeliveryMonitoring.Add(ldps);

            learners[0].LearningDelivery.Add(ld);

            Message message = new Message()
            {
                Header = new MessageHeader()
                {
                    CollectionDetails = new MessageHeaderCollectionDetails()
                }
            };

            message.Learner.AddRange(learners);

            var result = NewMapper().MapChildren<Message>(message);
        }

        private ParentRelationshipMapper NewMapper()
        {
            return new ParentRelationshipMapper();
        }
    }
}
