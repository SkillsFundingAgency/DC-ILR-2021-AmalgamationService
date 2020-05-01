using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LearnerFAMAmalgamationRule : IRule<List<MessageLearnerLearnerFAM>>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.LearnerFAM);

        private readonly IDictionary<string, int> famTypesMaxOccurenceDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["HNS"] = 1,
            ["EHC"] = 1,
            ["DLA"] = 1,
            ["LSR"] = 4,
            ["SEN"] = 1,
            ["NLM"] = 2,
            ["EDF"] = 2,
            ["MCF"] = 1,
            ["ECF"] = 1,
            ["FME"] = 1,
            ["PPE"] = 2,
        };

        public IRuleResult<List<MessageLearnerLearnerFAM>> Definition(IEnumerable<List<MessageLearnerLearnerFAM>> fams)
        {
            var amalgamationValidationErrors = new List<AmalgamationValidationError>();
            var amalgamatedFams = new List<MessageLearnerLearnerFAM>();

            var groupedFams = fams.SelectMany(v => v).GroupBy(g => g.LearnFAMType);

            foreach (var famGroup in groupedFams)
            {
                if (famTypesMaxOccurenceDictionary.TryGetValue(famGroup.Key, out var maxOccurences))
                {
                    AmalgamateFAMs(amalgamationValidationErrors, amalgamatedFams, famGroup, maxOccurences);
                }
            }

            return new RuleResult<List<MessageLearnerLearnerFAM>>
            {
                AmalgamatedValue = amalgamatedFams.ToList(),
                AmalgamationValidationErrors = amalgamationValidationErrors
            };
        }

        private void AmalgamateFAMs(List<AmalgamationValidationError> validationErrors, List<MessageLearnerLearnerFAM> learnerFams, IEnumerable<MessageLearnerLearnerFAM> originalFams, int maxOccurrence)
        {
            var distinctFamCodes = originalFams.GroupBy(g => g.LearnFAMCode).Select(s => s.FirstOrDefault());

            if (distinctFamCodes?.Count() > maxOccurrence)
            {
                validationErrors.AddRange(originalFams.Select(c => new AmalgamationValidationError()
                {
                    File = c.SourceFileName,
                    LearnRefNumber = c.LearnRefNumber,
                    ErrorType = maxOccurrence > 1 ? ErrorType.MaxOccurrenceExceeded : ErrorType.FieldValueConflict,
                    Entity = _entityName,
                    Key = $"LearnFAMType : {c.LearnFAMType}",
                    Value = c.LearnFAMType.ToString(),
                    Severity = Severity.Error
                }));

                return;
            }

            learnerFams.AddRange(distinctFamCodes);
        }
    }
}
