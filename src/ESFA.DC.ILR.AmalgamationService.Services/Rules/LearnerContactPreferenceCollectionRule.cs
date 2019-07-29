using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LearnerContactPreferenceCollectionRule : IRule<MessageLearnerContactPreference[]>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.ContactPreference);

        private readonly string contPrefTypeRUI = "RUI";
        private readonly string contPrefTypePMC = "PMC";

        private List<AmalgamationValidationError> amalgamationValidationErrors = new List<AmalgamationValidationError>();
        private List<MessageLearnerContactPreference> amalgamatedContactPreferences = new List<MessageLearnerContactPreference>();

        public IRuleResult<MessageLearnerContactPreference[]> Definition(IEnumerable<MessageLearnerContactPreference[]> contactPreferences)
        {
            RuleResult<MessageLearnerContactPreference[]> ruleResult = new RuleResult<MessageLearnerContactPreference[]>();

            var groupedConactPreferences = contactPreferences.SelectMany(v => v).GroupBy(g => g.ContPrefType);
            foreach (var contactPreference in groupedConactPreferences)
            {
                if (contactPreference.Key.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase))
                {
                    AmalgamateContactPreference(contPrefTypeRUI, contactPreference, 2);
                }

                if (contactPreference.Key.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase))
                {
                    AmalgamateContactPreference(contPrefTypePMC, contactPreference, 3);
                }
            }

            return new RuleResult<MessageLearnerContactPreference[]> { AmalgamatedValue = amalgamatedContactPreferences.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }

        private void AmalgamateContactPreference(string contPrefType, IEnumerable<MessageLearnerContactPreference> originalContactPreferences, int maxOccurrence)
        {
            var amlgamatedContactPreferencesForType = new List<MessageLearnerContactPreference>();

            var distinctContactPreferences = originalContactPreferences.GroupBy(g => g.ContPrefCodeNullable).Select(s => s.First());

            if (distinctContactPreferences.Count() > maxOccurrence)
            {
                amalgamationValidationErrors.AddRange(originalContactPreferences.Select(c => new AmalgamationValidationError()
                {
                    File = c.SourceFileName,
                    LearnRefNumber = c.LearnRefNumber,
                    ErrorType = ErrorType.MaxOccurrenceExceeded,
                    Entity = _entityName,
                    Key = string.Format("ContPrefType : {0}", c.ContPrefType),
                    Value = c.ContPrefCode.ToString()
                }));

                return;
            }

            if (distinctContactPreferences.Any(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5))
            {
                amlgamatedContactPreferencesForType.Add(distinctContactPreferences.First(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5));
            }

            if (!(contPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase) && amlgamatedContactPreferencesForType.Count() > 0))
            {
                amlgamatedContactPreferencesForType.AddRange(distinctContactPreferences.Where(x => x.ContPrefCodeNullable != 3 && x.ContPrefCodeNullable != 4 && x.ContPrefCodeNullable != 5).Take(maxOccurrence - amlgamatedContactPreferencesForType.Count()));
            }

            amalgamatedContactPreferences.AddRange(amlgamatedContactPreferencesForType);
        }
    }
}
