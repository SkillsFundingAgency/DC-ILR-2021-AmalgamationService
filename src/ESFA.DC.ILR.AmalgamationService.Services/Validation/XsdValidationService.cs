namespace ESFA.DC.ILR.AmalgamationService.Services.Validation
{
    using ESFA.DC.ILR.AmalgamationService.Interfaces;
    using ESFA.DC.ILR.Model.Loose.ReadWrite.Schema.Interface;
    using ESFA.DC.Logging.Interfaces;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Schema;

    public class XsdValidationService : IXsdValidationService
    {
        private readonly ISchemaProvider _schemaProvider;
        private readonly IValidationErrorHandler _validationErrorHandler;
        private readonly ILogger _logger;
        private bool _isSchemaValid;
        private string _fileName;

        public XsdValidationService(ISchemaProvider schemaProvider, IValidationErrorHandler validationErrorHandler, ILogger logger)
        {
            _schemaProvider = schemaProvider;
            _validationErrorHandler = validationErrorHandler;
            _logger = logger;
        }

        public bool ValidateSchema(string xmlFileName)
        {
            _isSchemaValid = true;
            _fileName = Path.GetFileName(xmlFileName);

            XmlSchema schema = _schemaProvider.Provide();
            var readerSettings = GetReaderSettings(schema);

            using (var reader = XmlReader.Create(xmlFileName, readerSettings))
            {
                ValidateNameSpace(reader);

                if (!_isSchemaValid)
                {
                    return _isSchemaValid;
                }
                else
                {
                    try
                    {
                        while (reader.Read())
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        _isSchemaValid = false;

                        var xmlException = ex as XmlException;
                        if (xmlException != null)
                        {
                            _validationErrorHandler.XmlValidationErrorHandler(xmlException, XmlSeverityType.Error, _fileName);
                        }
                    }
                }
            }

            return _isSchemaValid;
        }

        public XmlReaderSettings GetReaderSettings(XmlSchema schema)
        {
            var settings = new XmlReaderSettings
            {
                CloseInput = false,
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.ReportValidationWarnings
            };
            settings.ValidationEventHandler += Settings_ValidationEventHandler;
            settings.Schemas.Add(schema);
            return settings;
        }

        public void ValidateNameSpace(XmlReader reader)
        {
            var xmlSchema = _schemaProvider.Provide();
            var rootElement = xmlSchema.Items.OfType<XmlSchemaElement>().FirstOrDefault()?.Name;
            reader.ReadToFollowing(rootElement);
        }

        private void Settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            _isSchemaValid = false;

            XmlSchemaValidationException exType = e.Exception as XmlSchemaValidationException;

            if (exType != null)
            {
                _validationErrorHandler.XmlValidationErrorHandler(exType, e.Severity, _fileName);
            }
        }
    }
}
