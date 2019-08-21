using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class DPOutcomeRule : IRule<MessageLearnerDestinationandProgressionDPOutcome[]>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.LearnerDestinationandProgressionDPOutcome);

        public IRuleResult<MessageLearnerDestinationandProgressionDPOutcome[]> Definition(IEnumerable<MessageLearnerDestinationandProgressionDPOutcome[]> dpOutcomes)
        {
            List<AmalgamationValidationError> amalgamationValidationErrors = new List<AmalgamationValidationError>();
            List<MessageLearnerDestinationandProgressionDPOutcome> amalgamatedDpOutcomes = new List<MessageLearnerDestinationandProgressionDPOutcome>();

            var groupedDpOutcomes = dpOutcomes.SelectMany(v => v).GroupBy(x => new { x.OutType, x.OutCodeNullable, x.OutStartDateNullable });

            foreach (var groupedDpOutcome in groupedDpOutcomes)
            {
                if (groupedDpOutcome.Select(v => v.OutEndDateNullable).Where(x => x != null).Distinct().Count() > 1 || groupedDpOutcome.Select(v => v.OutCollDateNullable).Where(x => x != null).Distinct().Count() > 1)
                {
                    amalgamationValidationErrors.AddRange(groupedDpOutcome.Select(c => new AmalgamationValidationError()
                    {
                        File = c.SourceFileName,
                        LearnRefNumber = c.LearnRefNumber,
                        ErrorType = ErrorType.FieldValueConflict,
                        Entity = _entityName,
                        Key = $"OutType : {c.OutType}, OutCode : {c.OutCodeNullable}, OutStartDate : {c.OutStartDateNullable}",
                        Value = $"OutEndDate : {c.OutEndDateNullable}, OutCollDate : {c.OutCollDateNullable}",
                        Severity = Severity.Error
                    }));

                    continue;
                }

                amalgamatedDpOutcomes.Add(new MessageLearnerDestinationandProgressionDPOutcome
                {
                    OutType = groupedDpOutcome.Key.OutType,
                    OutCodeNullable = groupedDpOutcome.Key.OutCodeNullable,
                    OutStartDateNullable = groupedDpOutcome.Key.OutStartDateNullable,
                    OutEndDateNullable = groupedDpOutcome.Any(x => x.OutEndDateNullable != null) ? groupedDpOutcome.First(x => x.OutEndDateNullable != null).OutEndDateNullable : null,
                    OutCollDateNullable = groupedDpOutcome.Any(x => x.OutCollDateNullable != null) ? groupedDpOutcome.First(x => x.OutCollDateNullable != null).OutCollDateNullable : null
                });
            }

            return new RuleResult<MessageLearnerDestinationandProgressionDPOutcome[]> { AmalgamatedValue = amalgamatedDpOutcomes.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }
    }
}
