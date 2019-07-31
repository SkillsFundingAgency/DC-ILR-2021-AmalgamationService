using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Validation;
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
            var errorList = new List<IValidationError>();
            XmlSchemaValidationException exception = new XmlSchemaValidationException();

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            validationErrorHandlerMock.SetupGet(x => x.ValidationErrors).Returns(errorList);
            validationErrorHandlerMock.Setup(x => x.XmlValidationErrorHandler(It.IsAny<XmlSchemaValidationException>(), It.IsAny<XmlSeverityType>()))
                .Callback(() =>
                {
                    errorList.Add(new ValidationError(exception.Message, XmlSeverityType.Error | XmlSeverityType.Warning, exception.LineNumber, exception.LinePosition));
                });

            var sampleFile = @"SchemaValidation\WrongNameSpaceXml.xml";

            using (var testStream = GetSchemaSctream())
            {
                var res = ValidationService(validationErrorHandler: validationErrorHandlerMock.Object).ValidateSchema(sampleFile, testStream);

                res.Should().BeFalse();
                validationErrorHandlerMock.Object.ValidationErrors.Should().HaveCountGreaterOrEqualTo(1);
            }

            validationErrorHandlerMock.VerifyGet(x => x.ValidationErrors, Times.AtLeastOnce);
            validationErrorHandlerMock.Verify(x => x.XmlValidationErrorHandler(It.IsAny<XmlSchemaValidationException>(), It.IsAny<XmlSeverityType>()), Times.AtLeastOnce);
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
