using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class AddressRule : IRule<MessageLearner>
    {
        private readonly HashSet<Expression<Func<MessageLearner, string>>> selectors = new HashSet<Expression<Func<MessageLearner, string>>>()
        {
            s => s.AddLine1,
            s => s.AddLine2,
            s => s.AddLine3,
            s => s.AddLine4
        };

        public IRuleResult<MessageLearner> Definition(IEnumerable<MessageLearner> messageLearners)
        {
            MessageLearner amalgamatedMessageLearner = new MessageLearner();
            List<AmalgamationValidationError> amalgamationValidationErrors = new List<AmalgamationValidationError>();
            MessageLearner messageLearnerFromFirstFile = null;

            foreach (var selector in selectors)
            {
                var selectorFunc = selector.Compile();
                var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;

                var addressLines = messageLearners.Select(e => selectorFunc.Invoke(e));
                var distinctAddressLines = addressLines.Distinct(new AddressComparer()).ToList();
                var distinctAddress = distinctAddressLines.Count() <= 1 ? distinctAddressLines.First() : messageLearnerFromFirstFile != null ? prop.GetValue(messageLearnerFromFirstFile) : addressLines.FirstOrDefault(x => !string.IsNullOrEmpty(x));

                if (messageLearnerFromFirstFile == null)
                {
                    messageLearnerFromFirstFile = messageLearners.FirstOrDefault(x => Equals(x.AddLine1, distinctAddress));
                }

                prop.SetValue(amalgamatedMessageLearner, distinctAddress);

                if (distinctAddressLines.Count() > 1)
                {
                    amalgamationValidationErrors.AddRange(messageLearners.Select(c => new AmalgamationValidationError()
                    {
                        File = c.SourceFileName,
                        LearnRefNumber = c.LearnRefNumber,
                        ErrorType = ErrorType.FieldValueConflict,
                        Entity = Enum.GetName(typeof(Entity), Entity.Learner),
                        Key = $"LearnRefNumber : {c.LearnRefNumber}",
                        Value = prop.GetValue(c) == null ? string.Empty : prop.GetValue(c).ToString(),
                        ConflictingAttribute = prop.Name,
                        Severity = Severity.Warning
                    }));
                }
            }

            return new RuleResult<MessageLearner> { AmalgamatedValue = amalgamatedMessageLearner, AmalgamationValidationErrors = amalgamationValidationErrors };
        }
    }
}