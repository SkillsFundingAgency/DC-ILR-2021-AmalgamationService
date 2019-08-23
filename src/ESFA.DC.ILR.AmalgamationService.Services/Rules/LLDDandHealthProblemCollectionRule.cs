using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LLDDandHealthProblemCollectionRule : IRule<MessageLearnerLLDDandHealthProblem[]>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.LLDDandHealthProblem);

        public IRuleResult<MessageLearnerLLDDandHealthProblem[]> Definition(IEnumerable<MessageLearnerLLDDandHealthProblem[]> lLDDandHealthProblems)
        {
            var amalgamationValidationErrors = new List<AmalgamationValidationError>();

            var distinctLlddCat = lLDDandHealthProblems.SelectMany(v => v).GroupBy(g => g.LLDDCatNullable).Where(x => x.Key != null).Select(s => s.First()).ToArray();

            if (distinctLlddCat.Length > 21)
            {
                amalgamationValidationErrors.AddRange(lLDDandHealthProblems.SelectMany(v => v).Where(x => x.LLDDCatNullable != null).Select(c => GetAmalgamationValidationError(c, "LLDDCat", c.LLDDCatNullable.ToString())));
            }

            var distinctPrimaryLldd = distinctLlddCat.GroupBy(x => new { x.LLDDCatNullable, x.PrimaryLLDDNullable }).Where(g => g.Key.PrimaryLLDDNullable != null).Select(s => s.First());
            if (distinctPrimaryLldd.Count() > 1)
            {
                amalgamationValidationErrors.AddRange(lLDDandHealthProblems.SelectMany(v => v).Where(x => x.PrimaryLLDDNullable != null).Select(c => GetAmalgamationValidationError(c, "PrimaryLLDD", c.PrimaryLLDDNullable.ToString())));
            }

            return new RuleResult<MessageLearnerLLDDandHealthProblem[]>
            {
                AmalgamatedValue = distinctLlddCat.ToArray(),
                AmalgamationValidationErrors = amalgamationValidationErrors
            };
        }

        private AmalgamationValidationError GetAmalgamationValidationError(MessageLearnerLLDDandHealthProblem lLDDandHealthProblem, string key, string value)
        {
            return new AmalgamationValidationError()
            {
                File = lLDDandHealthProblem.SourceFileName,
                LearnRefNumber = lLDDandHealthProblem.LearnRefNumber,
                ErrorType = ErrorType.FieldValueConflict,
                Entity = _entityName,
                Key = key,
                Value = value,
                Severity = Severity.Error
            };
        }
    }
}
