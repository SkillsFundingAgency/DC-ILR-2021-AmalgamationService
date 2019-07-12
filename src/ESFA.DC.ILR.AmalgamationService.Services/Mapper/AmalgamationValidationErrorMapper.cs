using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.AmalgamationService.Services.Mapper
{
    public sealed class AmalgamationValidationErrorMapper : ClassMap<IAmalgamationValidationError>
    {
        public AmalgamationValidationErrorMapper()
        {
            Map(m => m.Entity).Name("Entity");
            Map(m => m.LearnRefNumber).Name("LearnRefNumber");
            Map(m => m.Key).Name("Key");
            Map(m => m.RuleName).Name("RuleName");
            Map(m => m.ConflictingAttribute).Name("ConflictingAttribute");
            Map(m => m.Description).Name("Description");
            Map(m => m.Value).Name("Value");
        }
    }
}
