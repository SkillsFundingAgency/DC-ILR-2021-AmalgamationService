using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Validation;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Schema.Interface;
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
            var sampleFile = @"SchemaValidation\SampleXml.xml";

            var mockSchemaProvider = new Mock<ISchemaProvider>();
            mockSchemaProvider.Setup(x => x.Provide()).Returns(GetSchema());

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            var service = ValidationService(schemaProvider: mockSchemaProvider.Object, validationErrorHandler: validationErrorHandlerMock.Object);

            var res = service.ValidateSchema(sampleFile);
            res.Should().BeTrue();

            mockSchemaProvider.Verify(x => x.Provide(), Times.AtLeastOnce);
        }

        [Fact]
        public void ValidSchema_Fails_WrongNameSpace()
        {
            var sampleFile = @"SchemaValidation\WrongNameSpaceXml.xml";

            var mockSchemaProvider = new Mock<ISchemaProvider>();
            mockSchemaProvider.Setup(x => x.Provide()).Returns(GetSchema());

            var errorList = new List<IValidationError>();
            XmlSchemaValidationException exception = new XmlSchemaValidationException();

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            validationErrorHandlerMock.SetupGet(x => x.ValidationErrors).Returns(errorList);
            validationErrorHandlerMock.Setup(x => x.XmlValidationErrorHandler(It.IsAny<XmlSchemaValidationException>(), It.IsAny<XmlSeverityType>()))
                .Callback(() =>
                {
                    errorList.Add(new ValidationError(exception.Message, XmlSeverityType.Error | XmlSeverityType.Warning, exception.LineNumber, exception.LinePosition));
                });

            var service = ValidationService(schemaProvider: mockSchemaProvider.Object, validationErrorHandler: validationErrorHandlerMock.Object);

            var res = service.ValidateSchema(sampleFile);

            res.Should().BeFalse();
            validationErrorHandlerMock.Object.ValidationErrors.Should().HaveCountGreaterOrEqualTo(1);

            mockSchemaProvider.Verify(x => x.Provide(), Times.AtLeastOnce);
            validationErrorHandlerMock.VerifyGet(x => x.ValidationErrors, Times.AtLeastOnce);
            validationErrorHandlerMock.Verify(x => x.XmlValidationErrorHandler(It.IsAny<XmlSchemaValidationException>(), It.IsAny<XmlSeverityType>()), Times.AtLeastOnce);
        }

        [Fact]
        public void TestBuildReaderSettings()
        {
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            var schema = GetSchema();

            var xmlReaderSettings = ValidationService(validationErrorHandler: validationErrorHandlerMock.Object).GetSettingsWithSchema(schema);

            xmlReaderSettings.CloseInput.Should().BeFalse();
            xmlReaderSettings.ValidationType.Should().Be(ValidationType.Schema);
            xmlReaderSettings.ValidationFlags.Should().Be(XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.ProcessInlineSchema);

            xmlReaderSettings.Schemas.Count.Should().Be(1);
        }

        public XsdValidationService ValidationService(ISchemaProvider schemaProvider = null, IValidationErrorHandler validationErrorHandler = null)
        {
            return new XsdValidationService(schemaProvider, validationErrorHandler);
        }

        public XmlSchema GetSchema()
        {
            XmlTextReader reader = new XmlTextReader(@"SchemaValidation\SampleXml.xsd");
            XmlSchema xmlSchema = XmlSchema.Read(reader, null);
            return xmlSchema;
        }
    }
}
