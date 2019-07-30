namespace ESFA.DC.ILR.AmalgamationService.Services
{
    using ESFA.DC.ILR.AmalgamationService.Interfaces;
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;

    public class XsdValidationService : IXsdValidationService
    {
        private readonly IValidationErrorHandler _validationErrorHandler;
        private bool _isSchemaValid = true;

        public XsdValidationService(IValidationErrorHandler validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        public bool ValidateSchema(string xmlFileName, Stream stream)
        {
            XmlSchema schema;
            try
            {
                schema = XmlSchema.Read(stream, null);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }

            var readerSettings = BuildReaderSettings(schema);

            using (var reader = XmlReader.Create(xmlFileName, readerSettings))
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
                        _validationErrorHandler.XmlValidationErrorHandler(xmlException, XmlSeverityType.Error);
                    }
                }
            }

            return _isSchemaValid;
        }

        public XmlReaderSettings BuildReaderSettings(XmlSchema schema)
        {
            var readerSettings = GetSettingsWithSchema(schema);
            readerSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            return readerSettings;
        }

        public XmlReaderSettings GetSettingsWithSchema(XmlSchema schema)
        {
            var settings = new XmlReaderSettings
            {
                CloseInput = false,
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints
            };
            settings.ValidationEventHandler += Settings_ValidationEventHandler;
            settings.Schemas.Add(schema);

            return settings;
        }

        private void Settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            _isSchemaValid = false;

            XmlSchemaValidationException exType = e.Exception as XmlSchemaValidationException;

            if (exType != null)
            {
                _validationErrorHandler.XmlValidationErrorHandler(exType, e.Severity);
            }
        }
    }
}
