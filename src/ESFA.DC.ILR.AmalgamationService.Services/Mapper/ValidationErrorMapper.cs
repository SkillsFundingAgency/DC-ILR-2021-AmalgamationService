using CsvHelper.Configuration;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.AmalgamationService.Services.Mapper
{
    public sealed class ValidationErrorMapper : ClassMap<IValidationError>
    {
        public ValidationErrorMapper()
        {
            Map(m => m.XmlSeverity).Name("Severity");
            Map(m => m.Message).Name("Message");
            Map(m => m.LineNumber).Name("LineNumber");
            Map(m => m.LinePosition).Name("LinePosition");
            Map(m => m.XMLFileName).Name("FileName");
        }
    }
}
