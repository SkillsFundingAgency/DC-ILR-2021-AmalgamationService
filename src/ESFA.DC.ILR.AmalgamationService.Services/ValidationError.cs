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

        public ValidationError(string message, XmlSeverityType? xmlSeverity, int? lineNumber, int? linePosition, string fileName = null)
        {
            Message = message;
            XmlSeverity = xmlSeverity;
            LineNumber = lineNumber;
            LinePosition = linePosition;
            XMLFileName = fileName;
        }

        public XmlSeverityType? XmlSeverity { get; set; }

        public string Message { get; set; }

        public int? LineNumber { get; set; }

        public int? LinePosition { get; set; }

        public string XMLFileName { get; set; }
    }
}
