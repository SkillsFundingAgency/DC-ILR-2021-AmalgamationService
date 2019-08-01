using XmlSeverityType = System.Xml.Schema.XmlSeverityType;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IValidationError
    {
        XmlSeverityType? XmlSeverity { get; }

        string Message { get; }

        int? LineNumber { get; }

        int? LinePosition { get; }

        string XMLFileName { get; }
    }
}
