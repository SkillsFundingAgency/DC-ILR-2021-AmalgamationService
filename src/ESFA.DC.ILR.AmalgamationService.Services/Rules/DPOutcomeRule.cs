using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class DPOutcomeRule : IRule<List<MessageLearnerDestinationandProgressionDPOutcome>>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.LearnerDestinationandProgressionDPOutcome);

        public IRuleResult<List<MessageLearnerDestinationandProgressionDPOutcome>> Definition(IEnumerable<List<MessageLearnerDestinationandProgressionDPOutcome>> dpOutcomes)
        {
            List<AmalgamationValidationError> amalgamationValidationErrors = new List<AmalgamationValidationError>();
            List<MessageLearnerDestinationandProgressionDPOutcome> amalgamatedDpOutcomes = new List<MessageLearnerDestinationandProgressionDPOutcome>();

            var groupedDpOutcomes = dpOutcomes.SelectMany(v => v).GroupBy(x => new { x.OutType, x.OutCode, x.OutStartDate });

            foreach (var groupedDpOutcome in groupedDpOutcomes)
            {
                if (groupedDpOutcome.Select(v => v.OutEndDate).Where(x => x != null).Distinct().Count() > 1 || groupedDpOutcome.Select(v => v.OutCollDate).Where(x => x != null).Distinct().Count() > 1)
                {
                    amalgamationValidationErrors.AddRange(groupedDpOutcome.Select(c => new AmalgamationValidationError()
                    {
                        File = c.SourceFileName,
                        LearnRefNumber = c.LearnRefNumber,
                        ErrorType = ErrorType.FieldValueConflict,
                        Entity = _entityName,
                        Key = $"OutType : {c.OutType}, OutCode : {c.OutCode}, OutStartDate : {c.OutStartDate}",
                        Value = $"OutEndDate : {c.OutEndDate}, OutCollDate : {c.OutCollDate}",
                        Severity = Severity.Error
                    }));

                    continue;
                }

                amalgamatedDpOutcomes.Add(new MessageLearnerDestinationandProgressionDPOutcome
                {
                    OutType = groupedDpOutcome.Key.OutType,
                    OutCode = groupedDpOutcome.Key.OutCode,
                    OutStartDate = groupedDpOutcome.Key.OutStartDate,
                    OutEndDate = groupedDpOutcome.Any(x => x.OutEndDate != null) ? groupedDpOutcome.First(x => x.OutEndDate != null).OutEndDate : null,
                    OutCollDate = groupedDpOutcome.Any(x => x.OutCollDate != null) ? groupedDpOutcome.First(x => x.OutCollDate != null).OutCollDate : null
                });
            }

            return new RuleResult<List<MessageLearnerDestinationandProgressionDPOutcome>>
            {
                AmalgamatedValue = amalgamatedDpOutcomes.ToList(),
                Success = amalgamationValidationErrors.Count < 1,
                AmalgamationValidationErrors = amalgamationValidationErrors
            };
        }
    }
}
