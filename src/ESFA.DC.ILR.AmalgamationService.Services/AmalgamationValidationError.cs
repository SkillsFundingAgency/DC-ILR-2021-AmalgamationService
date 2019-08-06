using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using System;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationValidationError : IAmalgamationValidationError
    {
        public Severity? Severity { get; set; }

        public ErrorType? ErrorType { get; set; }

        public string LearnRefNumber { get; set; }

        public string Entity { get; set; }

        public string Key { get; set; }

        public string ConflictingAttribute { get; set; }

        public string File { get; set; }

        public string Value { get; set; }

        public string ReportDescription => ErrorType != null ? Enum.GetName(typeof(ErrorType), ErrorType) : string.Empty;
    }
}
