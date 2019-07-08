using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    LearningDeliveries = new[]
                    {
                        new MessageLearnerLearningDelivery()
                        {
                            ConRefNumber = "conRef1",
                            ProviderSpecDeliveryMonitorings = new[]
                            {
                                new MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring()
                                {
                                    ProvSpecDelMon = "ProvSpecDelMon"
                                }
                            }
                        }
                    }
                },
                new MessageLearner() { LearnRefNumber = "1231" }
            };

            ILooseMessage message = new Message()
            {
                HeaderEntity = new MessageHeader()
                {
                    CollectionDetails = new MessageHeaderCollectionDetails()
                },
                Learners = learners
            };

            var result = NewMapper().MapChildren<ILooseMessage>(message);
        }

        private ParentRelationshipMapper NewMapper()
        {
            return new ParentRelationshipMapper();
        }
    }
}
