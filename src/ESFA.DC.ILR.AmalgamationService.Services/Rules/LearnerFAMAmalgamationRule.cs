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

        private readonly List<KeyValuePair<string, int>> famTypes = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("HNS", 1),
            new KeyValuePair<string, int>("EHC", 1),
            new KeyValuePair<string, int>("DLA", 1),
            new KeyValuePair<string, int>("LSR", 4),
            new KeyValuePair<string, int>("SEN", 1),
            new KeyValuePair<string, int>("NLM", 2),
            new KeyValuePair<string, int>("EDF", 2),
            new KeyValuePair<string, int>("MCF", 1),
            new KeyValuePair<string, int>("ECF", 1),
            new KeyValuePair<string, int>("FME", 1),
            new KeyValuePair<string, int>("PPE", 2),
        };

        private List<AmalgamationValidationError> amalgamationValidationErrors = new List<AmalgamationValidationError>();
        private List<MessageLearnerLearnerFAM> amalgamatedFams = new List<MessageLearnerLearnerFAM>();

        public IRuleResult<MessageLearnerLearnerFAM[]> Definition(IEnumerable<MessageLearnerLearnerFAM[]> fams)
        {
            RuleResult<MessageLearnerLearnerFAM[]> ruleResult = new RuleResult<MessageLearnerLearnerFAM[]>();

            var groupedFams = fams.SelectMany(v => v).GroupBy(g => g.LearnFAMType);
            foreach (var fam in groupedFams)
            {
                var famtype = famTypes.First(x => fam.Key.Equals(x.Key, StringComparison.OrdinalIgnoreCase));
                AmalgamateFams(famtype.Key, fam, famtype.Value);
            }

            return new RuleResult<MessageLearnerLearnerFAM[]> { AmalgamatedValue = amalgamatedFams.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }

        private void AmalgamateFams(string contPrefType, IEnumerable<MessageLearnerLearnerFAM> originalFams, int maxOccurrence)
        {
            var distinctFams = originalFams.GroupBy(g => new { g.LearnFAMType, g.LearnFAMCodeNullable }).Select(s => s.First());

            if (distinctFams.Count() > maxOccurrence)
            {
                amalgamationValidationErrors.AddRange(originalFams.Select(c => new AmalgamationValidationError()
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

            amalgamatedFams.AddRange(distinctFams);
        }
    }
}
