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
                if (groupedDpOutcome.Select(v => new { v.OutCodeNullable, v.OutEndDateNullable }).Distinct().Count() > 1)
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

                amalgamatedDpOutcomes.Add(groupedDpOutcome.Select(v => v).First());
            }

            return new RuleResult<MessageLearnerDestinationandProgressionDPOutcome[]> { AmalgamatedValue = amalgamatedDpOutcomes.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }
    }
}
