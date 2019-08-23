using CsvHelper.Configuration;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.AmalgamationService.Services.Mapper
{
    public sealed class AmalgamationValidationErrorMapper : ClassMap<IAmalgamationValidationError>
    {
        public AmalgamationValidationErrorMapper()
        {
            Map(m => m.LearnRefNumber).Name("LearnRefNumber");
            Map(m => m.Entity).Name("Entity");
            Map(m => m.Key).Name("Key");
            Map(m => m.ConflictingAttribute).Name("ConflictingAttribute");
            Map(m => m.File).Name("File");
            Map(m => m.Value).Name("Value");
            Map(m => m.ErrorType).Name("ErrorType");
            Map(m => m.Severity).Name("Severity");
        }
    }
}
