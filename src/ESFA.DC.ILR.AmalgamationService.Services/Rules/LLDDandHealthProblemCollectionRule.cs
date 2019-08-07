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
            var amalgamatedlLDDandHealthProblems = new List<MessageLearnerLLDDandHealthProblem>();

            AmalgamateLLDDAndHealthProblem(amalgamationValidationErrors, amalgamatedlLDDandHealthProblems, x => x.LLDDCat, "LLDDCat", lLDDandHealthProblems, 21);
            AmalgamateLLDDAndHealthProblem(amalgamationValidationErrors, amalgamatedlLDDandHealthProblems, x => x.PrimaryLLDD, "PrimaryLLDD", lLDDandHealthProblems, 1);

            return new RuleResult<MessageLearnerLLDDandHealthProblem[]>
            {
                AmalgamatedValue = amalgamatedlLDDandHealthProblems.ToArray(),
                AmalgamationValidationErrors = amalgamationValidationErrors
            };
        }

        private void AmalgamateLLDDAndHealthProblem(List<AmalgamationValidationError> amalgamationValidationErrors, List<MessageLearnerLLDDandHealthProblem> lldds, Expression<Func<MessageLearnerLLDDandHealthProblem, long>> selector, string keyPropertyName, IEnumerable<MessageLearnerLLDDandHealthProblem[]> originallLDDandHealthProblems, int maxOccurrence)
        {
            var selectorFunc = selector.Compile();

            var distinctlLDDandHealthProblems = originallLDDandHealthProblems.SelectMany(v => v).GroupBy(selectorFunc).Select(s => s.First()).ToArray();

            if (distinctlLDDandHealthProblems.Length > maxOccurrence)
            {
                var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;

                amalgamationValidationErrors.AddRange(originallLDDandHealthProblems.SelectMany(v => v).Select(c => new AmalgamationValidationError()
                {
                    File = c.SourceFileName,
                    LearnRefNumber = c.LearnRefNumber,
                    ErrorType = ErrorType.FieldValueConflict,
                    Entity = _entityName,
                    Key = $"{keyPropertyName} : {selectorFunc(c)}",
                    Value = prop.GetValue(c).ToString()
                }));

                return;
            }

            lldds.AddRange(distinctlLDDandHealthProblems);
        }
    }
}
