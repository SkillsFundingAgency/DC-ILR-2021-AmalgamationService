using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using XmlSeverityType = System.Xml.Schema.XmlSeverityType;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class ValidationError : IValidationError
    {
        public ValidationError()
        {
        }

        public ValidationError(string message, XmlSeverityType? severity, int? lineNumber, int? linePosition)
        {
            Message = message;
            Severity = severity;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        public XmlSeverityType? Severity { get; set; }

        public string Message { get; set; }

        public int? LineNumber { get; set; }

        public int? LinePosition { get; set; }
    }
}
