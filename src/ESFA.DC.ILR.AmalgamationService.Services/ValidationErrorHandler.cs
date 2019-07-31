﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
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

        public void XmlValidationErrorHandler(XmlSchemaValidationException xmlException, XmlSeverityType? xmlSeverity)
        {
            IValidationError objToAdd = new ValidationError(xmlException.Message, xmlSeverity, xmlException.LineNumber, xmlException.LinePosition);
            AddUniqueItems(objToAdd);
        }

        public void XmlValidationErrorHandler(XmlException xmlException, XmlSeverityType? xmlSeverity)
        {
            IValidationError objToAdd = new ValidationError(xmlException.Message, xmlSeverity, xmlException.LineNumber, xmlException.LinePosition);
            AddUniqueItems(objToAdd);
        }

        public void XsdValidationErrorHandler(object sender, ValidationEventArgs e)
        {
            if (sender is IXmlLineInfo xmlLineInfo)
            {
                IValidationError objToAdd = new ValidationError(e.Message, e.Severity, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
                AddUniqueItems(objToAdd);
            }
        }

        public void AddUniqueItems(IValidationError errorObj)
        {
            IValidationError objToAdd;
            var existing = _validationErrors.TryPeek(out objToAdd);

            if (!existing)
            {
                _validationErrors.Add(errorObj);
            }
        }
    }
}
