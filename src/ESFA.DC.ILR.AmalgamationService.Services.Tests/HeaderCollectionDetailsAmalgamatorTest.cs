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
            var collection = "ILR";
            var year = "1920";
            var filePrepDate = new DateTime(2018, 08, 19);

            List<MessageHeaderCollectionDetails> msgHeaderCollection = new List<MessageHeaderCollectionDetails>()
            {
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    YearString = year,
                    FilePreparationDate = filePrepDate,
                    Parent = GetParent()
                },
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    YearString = year,
                    FilePreparationDate = filePrepDate,
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(_ruleProvider, _amalgamationErrorHandler).Amalgamate(msgHeaderCollection);
            amalgamate.CollectionString.Should().Be(collection);
            amalgamate.YearString.Should().Be(year);
            amalgamate.FilePreparationDate.Should().Be(filePrepDate);
        }

        [Fact]
        public void Amalgamate_Errors_DueTOUnMatchDate()
        {
            var collection = "ILR";
            var year = "1920";
            var filePrepDate = new DateTime(2018, 08, 19);

            List<MessageHeaderCollectionDetails> msgHeaderCollection = new List<MessageHeaderCollectionDetails>()
            {
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    YearString = year,
                    FilePreparationDate = filePrepDate,
                    Parent = GetParent()
                },
                new MessageHeaderCollectionDetails()
                {
                    CollectionString = collection,
                    YearString = year,
                    FilePreparationDate = new DateTime(2018, 08, 01),
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(_ruleProvider, _amalgamationErrorHandler).Amalgamate(msgHeaderCollection);
            var validationError = _amalgamationErrorHandler.Errors;

            validationError.Should().HaveCount(2);
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
                        IRuleProvider ruleProvider = null,
                        IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return new HeaderCollectionDetailsAmalgamator(
                                         ruleProvider ?? Mock.Of<IRuleProvider>(),
                                         amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }
    }
}
