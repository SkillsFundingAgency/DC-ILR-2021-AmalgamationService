using System;
using System.Collections.Generic;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class HeaderCollectionDetailsAmalgamatorTest : BaseAmalgamatorTest
    {
        private readonly IRuleProvider _ruleProvider = new RuleProvider();
        private readonly IAmalgamationErrorHandler _amalgamationErrorHandler = new AmalgamationErrorHandler();

        [Fact]
        public void Amalgamate_Pass()
        {
            var collection = "ILR";
            var year = "1920";

            var nowDateTime = DateTime.UtcNow;
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(d => d.GetNowUtc()).Returns(nowDateTime);

            List<MessageHeaderCollectionDetails> msgHeaderCollection = new List<MessageHeaderCollectionDetails>()
            {
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    Parent = GetParent()
                },
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(mockDateTimeProvider.Object, _ruleProvider, _amalgamationErrorHandler).Amalgamate(msgHeaderCollection);

            amalgamate.CollectionString.Should().Be(collection);
            amalgamate.YearString.Should().Be(year);
            amalgamate.FilePreparationDate.Should().Be(nowDateTime);

            mockDateTimeProvider.Verify(d => d.GetNowUtc(), Times.AtLeastOnce);
        }

        public MessageHeader GetParent() => new MessageHeader()
        {
            Parent = new Message()
            {
                Parent = new AmalgamationRoot()
                {
                    Filename = "xyz.xml",
                    Message = new Message()
                }
            }
        };

        public HeaderCollectionDetailsAmalgamator NewAmalgamator(
                        IDateTimeProvider dateTimeProvider = null,
                        IRuleProvider ruleProvider = null,
                        IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return new HeaderCollectionDetailsAmalgamator(
                                         dateTimeProvider ?? Mock.Of<IDateTimeProvider>(),
                                         ruleProvider ?? Mock.Of<IRuleProvider>(),
                                         amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }
    }
}
