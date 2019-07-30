using ESFA.DC.ILR.AmalgamationService.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.SchemaValidation
{
    public class XsdValidationServiceTest
    {
        [Fact]
        public void ValidSchema_True()
        {
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            var sampleFile = @"SchemaValidation\SampleXml.xml";

            using (var testStream = GetSchemaSctream())
            {
                var res = ValidationService(validationErrorHandler: validationErrorHandlerMock.Object).ValidateSchema(sampleFile, testStream);

                res.Should().BeTrue();
            }
        }

        [Fact]
        public void ValidSchema_Fails_WrongNameSpace()
        {
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            validationErrorHandlerMock.SetupGet(x => x.ValidationErrors).Returns(new List<IValidationError>());

            var sampleFile = @"SchemaValidation\WrongNameSpaceXml.xml";

            using (var testStream = GetSchemaSctream())
            {
                var res = ValidationService(validationErrorHandler: validationErrorHandlerMock.Object).ValidateSchema(sampleFile, testStream);

                res.Should().BeFalse();
            }
        }

        [Fact]
        public void TestBuildReaderSettings()
        {
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            var schema = new XmlSchema();
            var xmlReaderSettings = ValidationService(validationErrorHandler: validationErrorHandlerMock.Object).GetSettingsWithSchema(schema);

            xmlReaderSettings.CloseInput.Should().BeFalse();
            xmlReaderSettings.ValidationType.Should().Be(ValidationType.Schema);
            xmlReaderSettings.ValidationFlags.Should().Be(XmlSchemaValidationFlags.ProcessIdentityConstraints);
        }

        public XsdValidationService ValidationService(IValidationErrorHandler validationErrorHandler = null)
        {
            return new XsdValidationService(validationErrorHandler);
        }

        public Stream GetSchemaSctream()
        {
            return Assembly
                    .Load("ESFA.DC.ILR.AmalgamationService.Services.Tests")
                    .GetManifestResourceStream("ESFA.DC.ILR.AmalgamationService.Services.Tests.SchemaValidation.SampleXml.xsd");
        }
    }
}
