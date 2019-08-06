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

            List<MessageHeaderCollectionDetails> msgHeaderCollection = new List<MessageHeaderCollectionDetails>()
            {
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    FilePreparationDate = new DateTime(2019, 08, 02),
                    Parent = GetParent()
                },
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    FilePreparationDate = new DateTime(2019, 08, 10),
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(_amalgamationErrorHandler).Amalgamate(msgHeaderCollection);

            amalgamate.CollectionString.Should().Be(collection);
            amalgamate.YearString.Should().Be(year);
            amalgamate.FilePreparationDate.Should().Be(new DateTime(2019, 08, 10));
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
