using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationValidationError
    {
        Severity Severity { get; }

        ErrorType? ErrorType { get; }

        string LearnRefNumber { get; }

        string Entity { get; }

        string Key { get; }

        string ConflictingAttribute { get; }

        string File { get; }

        string Value { get; }

        string ReportDescription { get; }
    }
}
