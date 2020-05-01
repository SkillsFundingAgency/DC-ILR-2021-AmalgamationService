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
    public class HeaderSourceAmalgamatorTest : BaseAmalgamatorTest
    {
        private readonly IRuleProvider _ruleProvider = new RuleProvider();
        private readonly IAmalgamationErrorHandler _amalgamationErrorHandler = new AmalgamationErrorHandler();

        [Fact]
        public void ProtectiveMarkingString_Property_NotNull()
        {
            var valToTest = MessageHeaderSourceProtectiveMarking.OFFICIAL_SENSITIVE_Personal;

            var msgHeaderSource = new MessageHeaderSource
            {
                ProtectiveMarking = MessageHeaderSourceProtectiveMarking.OFFICIAL_SENSITIVE_Personal
            };

            msgHeaderSource.ProtectiveMarking.Should().Be(valToTest);
        }

        [Fact]
        public void Amalgamate_Pass1()
        {
            DateTime dateTimeNow = new DateTime(2020, 08, 16);

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(d => d.GetNowUtc()).Returns(dateTimeNow);

            List<MessageHeaderSource> headerSourcesList = new List<MessageHeaderSource>()
            {
                new MessageHeaderSource()
                {
                    UKPRN = 10000001,
                    Parent = GetParent()
                },
                 new MessageHeaderSource()
                {
                    UKPRN = 10000001,
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(mockDateTimeProvider.Object, _amalgamationErrorHandler).Amalgamate(headerSourcesList);

            amalgamate.DateTime.Should().Be(dateTimeNow);
        }

        [Fact]
        public void Amalgamate_Pass2()
        {
            DateTime date1 = new DateTime(2020, 07, 26);
            DateTime date2 = new DateTime(2018, 07, 26);

            DateTime dateTimeNow = new DateTime(2020, 08, 16);

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(d => d.GetNowUtc()).Returns(dateTimeNow);

            var objToCompare = new MessageHeaderSource()
            {
                DateTime = date1,
                UKPRN = 10000001,
                ProtectiveMarking = MessageHeaderSourceProtectiveMarking.OFFICIAL_SENSITIVE_Personal,
                SoftwareSupplier = "ESFA",
                SoftwarePackage = "FileMerge",
                Release = "01",
                SerialNo = "00"
            };

            List<MessageHeaderSource> headerSourcesList = new List<MessageHeaderSource>()
            {
                new MessageHeaderSource()
                {
                    DateTime = date1,
                    ProtectiveMarking = MessageHeaderSourceProtectiveMarking.OFFICIAL_SENSITIVE_Personal,
                    UKPRN = 10000001,
                    SoftwareSupplier = "Skills Funding Agency",
                    SoftwarePackage = "ILR Learner Entry",
                    Release = "2021.1.0.14278",
                    SerialNo = "01",
                    ReferenceData = "ReferenceData",
                    ComponentSetVersion = "ComponentSetVersion",
                    Parent = GetParent()
                },
                 new MessageHeaderSource()
                {
                    DateTime = date2,
                    ProtectiveMarking = MessageHeaderSourceProtectiveMarking.OFFICIAL_SENSITIVE_Personal,
                    UKPRN = 10000001,
                    SoftwareSupplier = "Skills Funding Agency",
                    SoftwarePackage = "ILR Learner Entry",
                    Release = "2021.1.0.14278",
                    SerialNo = "01",
                    ReferenceData = "ReferenceData",
                    ComponentSetVersion = "ComponentSetVersion",
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(mockDateTimeProvider.Object, _amalgamationErrorHandler).Amalgamate(headerSourcesList);

            amalgamate.DateTime.Should().Be(dateTimeNow);
            amalgamate.ProtectiveMarking.Should().Be(objToCompare.ProtectiveMarking);
            amalgamate.UKPRN.Should().Be(objToCompare.UKPRN);
            amalgamate.SoftwareSupplier.Should().Be(objToCompare.SoftwareSupplier);
            amalgamate.SoftwarePackage.Should().Be(objToCompare.SoftwarePackage);
            amalgamate.Release.Should().Be(objToCompare.Release);
            amalgamate.SerialNo.Should().Be(objToCompare.SerialNo);
            amalgamate.ReferenceData.Should().Be(objToCompare.ReferenceData);
            amalgamate.ComponentSetVersion.Should().Be(objToCompare.ComponentSetVersion);
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

        public HeaderSourceAmalgamator NewAmalgamator(IDateTimeProvider dateTimeProvider = null, IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return new HeaderSourceAmalgamator(
                dateTimeProvider ?? Mock.Of<IDateTimeProvider>(),
                amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }
    }
}
