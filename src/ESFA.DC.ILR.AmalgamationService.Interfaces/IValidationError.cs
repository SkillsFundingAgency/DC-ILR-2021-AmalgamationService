using XmlSeverityType = System.Xml.Schema.XmlSeverityType;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IValidationError
    {
        XmlSeverityType? Severity { get; }

        string Message { get; }

        int? LineNumber { get; }

        int? LinePosition { get; }
    }
}
