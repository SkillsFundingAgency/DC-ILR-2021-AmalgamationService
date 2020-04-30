using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LLDDandHealthProblemCollectionRule : IRule<List<MessageLearnerLLDDandHealthProblem>>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.LLDDandHealthProblem);

        public IRuleResult<List<MessageLearnerLLDDandHealthProblem>> Definition(IEnumerable<List<MessageLearnerLLDDandHealthProblem>> lLDDandHealthProblems)
        {
            var amalgamationValidationErrors = new List<AmalgamationValidationError>();

            var distinctLlddCat = lLDDandHealthProblems.SelectMany(v => v).GroupBy(g => g.LLDDCat).Where(x => x.Key != null).Select(s => s.First()).ToArray();

            if (distinctLlddCat.Length > 21)
            {
                amalgamationValidationErrors.AddRange(lLDDandHealthProblems.SelectMany(v => v).Where(x => x.LLDDCat != null).Select(c => GetAmalgamationValidationError(c, "LLDDCat", c.LLDDCat.ToString())));
            }

            var distinctPrimaryLldd = distinctLlddCat.GroupBy(x => new { x.LLDDCat, x.PrimaryLLDD }).Where(g => g.Key.PrimaryLLDD != null).Select(s => s.First());
            if (distinctPrimaryLldd.Count() > 1)
            {
                amalgamationValidationErrors.AddRange(lLDDandHealthProblems.SelectMany(v => v).Where(x => x.PrimaryLLDD != null).Select(c => GetAmalgamationValidationError(c, "PrimaryLLDD", c.PrimaryLLDD.ToString())));
            }

            if (amalgamationValidationErrors.Count == 0)
            {
                return new RuleResult<List<MessageLearnerLLDDandHealthProblem>>
                {
                    Success = true,
                    AmalgamatedValue = distinctLlddCat.ToList()
                };
            }
            else
            {
                return new RuleResult<List<MessageLearnerLLDDandHealthProblem>>
                {
                    AmalgamationValidationErrors = amalgamationValidationErrors
                };
            }
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
