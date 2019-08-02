using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class ValidationErrorHandler : IValidationErrorHandler
    {
        private readonly ConcurrentBag<IValidationError> _validationErrors = new ConcurrentBag<IValidationError>();

        public IEnumerable<IValidationError> ValidationErrors => _validationErrors;

        public void XmlValidationErrorHandler(XmlSchemaValidationException xmlException, XmlSeverityType? xmlSeverity, string fileName = null)
        {
            _validationErrors.Add(new ValidationError(xmlException.Message, xmlSeverity, xmlException.LineNumber, xmlException.LinePosition, fileName));
        }

        public void XmlValidationErrorHandler(XmlException xmlException, XmlSeverityType? xmlSeverity, string fileName = null)
        {
            _validationErrors.Add(new ValidationError(xmlException.Message, xmlSeverity, xmlException.LineNumber, xmlException.LinePosition, fileName));
        }

        public void XsdValidationErrorHandler(object sender, ValidationEventArgs e, string fileName = null)
        {
            if (sender is IXmlLineInfo xmlLineInfo)
            {
                _validationErrors.Add(new ValidationError(e.Message, e.Severity, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition, fileName));
            }
        }
    }
}
