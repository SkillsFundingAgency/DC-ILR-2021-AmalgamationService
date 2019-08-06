using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationValidationError
    {
        Severity? Severity { get; }

        ErrorType? ErrorType { get; set; }

        string LearnRefNumber { get; set; }

        string Entity { get; set; }

        string Key { get; set; }

        string ConflictingAttribute { get; set; }

        string File { get; set; }

        string Value { get; set; }

        string ReportDescription { get; }
    }
}
