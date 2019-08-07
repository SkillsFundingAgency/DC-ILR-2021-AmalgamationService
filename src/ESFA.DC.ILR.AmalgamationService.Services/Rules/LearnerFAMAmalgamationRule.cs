using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LearnerFAMAmalgamationRule : IRule<MessageLearnerLearnerFAM[]>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.LearnerFAM);

        private readonly Dictionary<string, int> famTypes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
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

        public IRuleResult<MessageLearnerLearnerFAM[]> Definition(IEnumerable<MessageLearnerLearnerFAM[]> fams)
        {
            var amalgamationValidationErrors = new List<AmalgamationValidationError>();
            var amalgamatedFams = new List<MessageLearnerLearnerFAM>();

            var groupedFams = fams.SelectMany(v => v).GroupBy(g => g.LearnFAMType);
            foreach (var fam in groupedFams)
            {
                var famtype = famTypes.First(x => fam.Key.Equals(x.Key, StringComparison.OrdinalIgnoreCase));

                AmalgamateFAMs(amalgamationValidationErrors, amalgamatedFams, fam, famtype.Value);
            }

            return new RuleResult<MessageLearnerLearnerFAM[]> { AmalgamatedValue = amalgamatedFams.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }

        private void AmalgamateFAMs(List<AmalgamationValidationError> validationErrors, List<MessageLearnerLearnerFAM> learnerFams, IEnumerable<MessageLearnerLearnerFAM> originalFams, int maxOccurrence)
        {
            var distinctFams = originalFams.GroupBy(g => g.LearnFAMType).Select(s => s.First());

            if (distinctFams.Count() > maxOccurrence)
            {
                validationErrors.AddRange(originalFams.Select(c => new AmalgamationValidationError()
                {
                    File = c.SourceFileName,
                    LearnRefNumber = c.LearnRefNumber,
                    ErrorType = maxOccurrence > 1 ? ErrorType.MaxOccurrenceExceeded : ErrorType.FieldValueConflict,
                    Entity = _entityName,
                    Key = $"LearnFAMType : {c.LearnFAMType}",
                    Value = c.LearnFAMType.ToString()
                }));

                return;
            }

            learnerFams.AddRange(distinctFams);
        }
    }
}
