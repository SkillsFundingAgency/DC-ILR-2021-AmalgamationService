using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IValidationErrorHandler
    {
        IEnumerable<IValidationError> ValidationErrors { get; }

        void XmlValidationErrorHandler(XmlSchemaValidationException xmlException, XmlSeverityType? severity);

        void XmlValidationErrorHandler(XmlException xmlException, XmlSeverityType? severity);

        void XsdValidationErrorHandler(object sender, ValidationEventArgs e);
    }
}
