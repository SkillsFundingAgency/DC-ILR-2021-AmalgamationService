using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IValidationErrorHandler
    {
        IEnumerable<IValidationError> ValidationErrors { get; }

        void XmlValidationErrorHandler(XmlSchemaValidationException xmlException, XmlSeverityType? severity, string fileName = null);

        void XmlValidationErrorHandler(XmlException xmlException, XmlSeverityType? severity, string fileName = null);

        void XsdValidationErrorHandler(object sender, ValidationEventArgs e, string fileName = null);

        void CrossRecordValidationErrorHandler(string message, string fileName = null);
    }
}
