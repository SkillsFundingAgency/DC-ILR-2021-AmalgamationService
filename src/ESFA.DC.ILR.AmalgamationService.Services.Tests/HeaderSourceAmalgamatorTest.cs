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
    public class HeaderSourceAmalgamatorTest : BaseAmalgamatorTest
    {
        private readonly IRuleProvider _ruleProvider = new RuleProvider();
        private readonly IAmalgamationErrorHandler _amalgamationErrorHandler = new AmalgamationErrorHandler();

        [Fact]
        public void ProtectiveMarkingString_Property_NotNull()
        {
            var msgHeaderSource = new MessageHeaderSource
            {
                ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal"
            };

            msgHeaderSource.ProtectiveMarkingString.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ProtectiveMarkingString_Property_IsNull()
        {
            var msgHeaderSource = new MessageHeaderSource
            {
                ProtectiveMarkingString = "123"
            };

            msgHeaderSource.ProtectiveMarkingString.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Amalgamate_Pass()
        {
            var date = new DateTime(2018, 08, 19);

            var objToCompare = new MessageHeaderSource()
            {
                DateTime = date,
                ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal",
                UKPRN = 10000001,
                SoftwareSupplier = "Skills Funding Agency",
                SoftwarePackage = "ILR Learner Entry",
                Release = "1920.1.0.14278",
                SerialNo = "01",
                ReferenceData = "ReferenceData",
                ComponentSetVersion = "ComponentSetVersion"
            };

            List<MessageHeaderSource> headerSourcesList = new List<MessageHeaderSource>()
            {
                new MessageHeaderSource()
                {
                    DateTime = date,
                    ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal",
                    UKPRN = 10000001,
                    SoftwareSupplier = "Skills Funding Agency",
                    SoftwarePackage = "ILR Learner Entry",
                    Release = "1920.1.0.14278",
                    SerialNo = "01",
                    ReferenceData = "ReferenceData",
                    ComponentSetVersion = "ComponentSetVersion",
                    Parent = GetParent()
                },
                 new MessageHeaderSource()
                {
                    DateTime = date,
                    ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal",
                    UKPRN = 10000001,
                    SoftwareSupplier = "Skills Funding Agency",
                    SoftwarePackage = "ILR Learner Entry",
                    Release = "1920.1.0.14278",
                    SerialNo = "01",
                    ReferenceData = "ReferenceData",
                    ComponentSetVersion = "ComponentSetVersion",
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(_ruleProvider, _amalgamationErrorHandler).Amalgamate(headerSourcesList);

            amalgamate.DateTime.Should().Be(objToCompare.DateTime);
            amalgamate.ProtectiveMarkingString.Should().Be(objToCompare.ProtectiveMarkingString);
            amalgamate.UKPRN.Should().Be(objToCompare.UKPRN);
            amalgamate.SoftwareSupplier.Should().Be(objToCompare.SoftwareSupplier);
            amalgamate.SoftwarePackage.Should().Be(objToCompare.SoftwarePackage);
            amalgamate.Release.Should().Be(objToCompare.Release);
            amalgamate.SerialNo.Should().Be(objToCompare.SerialNo);
            amalgamate.ReferenceData.Should().Be(objToCompare.ReferenceData);
            amalgamate.ComponentSetVersion.Should().Be(objToCompare.ComponentSetVersion);
        }

        [Fact]
        public void Amalgamate_Errors()
        {
            var date = new DateTime(2018, 08, 19);

            var objToCompare = new MessageHeaderSource()
            {
                DateTime = date,
                ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal",
                UKPRN = 10000001,
                SoftwareSupplier = "Skills Funding Agency",
                SoftwarePackage = "ILR Learner Entry",
                Release = "1920.1.0.14278",
                SerialNo = "01",
                ReferenceData = "ReferenceData",
                ComponentSetVersion = "ComponentSetVersion"
            };

            List<MessageHeaderSource> headerSourcesList = new List<MessageHeaderSource>()
            {
                new MessageHeaderSource()
                {
                    DateTime = date,
                    ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal",
                    UKPRN = 10000001,
                    SoftwareSupplier = "Skills Funding Agency",
                    SoftwarePackage = "ILR Learner Entry",
                    Release = "1920.1.0.14278",
                    SerialNo = "01",
                    ReferenceData = "ReferenceData",
                    ComponentSetVersion = "ComponentSetVersion",
                    Parent = GetParent()
                },
                 new MessageHeaderSource()
                {
                    DateTime = date,
                    ProtectiveMarkingString = "OFFICIAL-SENSITIVE-Personal",
                    UKPRN = 10000001,
                    SoftwareSupplier = "Skills Funding Agency",
                    SoftwarePackage = "ILR Learner Entry",
                    Release = "1920.1.0.14278",
                    SerialNo = "2",
                    ReferenceData = "ReferenceData",
                    ComponentSetVersion = "ComponentSetVersion",
                    Parent = GetParent()
                }
            };

            var amalgamate = NewAmalgamator(_ruleProvider, _amalgamationErrorHandler).Amalgamate(headerSourcesList);
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

        public HeaderSourceAmalgamator NewAmalgamator(
                                       IRuleProvider ruleProvider = null,
                                       IAmalgamationErrorHandler amalgamationErrorHandler = null)
        {
            return new HeaderSourceAmalgamator(
                                         ruleProvider ?? Mock.Of<IRuleProvider>(),
                                         amalgamationErrorHandler ?? Mock.Of<IAmalgamationErrorHandler>());
        }
    }
}
