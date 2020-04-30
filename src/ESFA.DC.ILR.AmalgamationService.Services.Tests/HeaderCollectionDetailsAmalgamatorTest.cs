using System;
using System.Collections.Generic;
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
            var collection = MessageHeaderCollectionDetailsCollection.ILR;
            var year = MessageHeaderCollectionDetailsYear.Item2021;

            List<MessageHeaderCollectionDetails> msgHeaderCollection = new List<MessageHeaderCollectionDetails>()
            {
                new MessageHeaderCollectionDetails()
                {
                    Collection = MessageHeaderCollectionDetailsCollection.ILR,
                    FilePreparationDate = new DateTime(2020, 08, 02),
                    Parent = GetParent()
                },
                new MessageHeaderCollectionDetails()
                {
                    Collection = MessageHeaderCollectionDetailsCollection.ILR,
                    FilePreparationDate = new DateTime(2020, 08, 10),
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(_amalgamationErrorHandler).Amalgamate(msgHeaderCollection);

            amalgamate.Collection.Should().Be(collection);
            amalgamate.Year.Should().Be(year);
            amalgamate.FilePreparationDate.Should().Be(new DateTime(2020, 08, 10));
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
                        IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return new HeaderCollectionDetailsAmalgamator(
                                         amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }
    }
}
