using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LearnerContactPreferenceCollectionRule : IRule<MessageLearnerContactPreference[]>
    {
        private static string _entityName = Enum.GetName(typeof(Entity), Entity.ContactPreference);

        private static string contPrefTypeRUI = "RUI";
        private static string contPrefTypePMC = "PMC";

        private readonly List<KeyValuePair<string, int>> contPrefTypes = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>(contPrefTypeRUI, 2),
            new KeyValuePair<string, int>(contPrefTypePMC, 3)
        };

        public IRuleResult<MessageLearnerContactPreference[]> Definition(IEnumerable<MessageLearnerContactPreference[]> contactPreferences)
        {
            List<AmalgamationValidationError> amalgamationValidationErrors = new List<AmalgamationValidationError>();
            List<MessageLearnerContactPreference> amalgamatedContactPreferences = new List<MessageLearnerContactPreference>();

            var contactPreferencesList = contactPreferences.SelectMany(v => v).ToList();

            foreach (var type in contPrefTypes)
            {
                var groupedContactPreferencesForType =
                    contactPreferencesList
                    .Where(cp => cp.ContPrefType.Equals(type.Key, StringComparison.OrdinalIgnoreCase))
                    .Distinct(new LambdaEqualityComparer<MessageLearnerContactPreference>((c1, c2) => c1.ContPrefType == c2.ContPrefType && c1.ContPrefCodeNullable == c2.ContPrefCodeNullable));

                var amlgamatedContactPreferencesForType = new List<MessageLearnerContactPreference>();

                if (groupedContactPreferencesForType.Count() > type.Value)
                {
                    amalgamationValidationErrors.AddRange(contactPreferencesList.Where(cp => cp.ContPrefType.Equals(type.Key, StringComparison.OrdinalIgnoreCase)).Select(c => new AmalgamationValidationError()
                    {
                        File = c.SourceFileName,
                        LearnRefNumber = c.LearnRefNumber,
                        ErrorType = ErrorType.MaxOccurrenceExceeded,
                        Entity = _entityName,
                        Key = $"ContPrefType : {c.ContPrefType}",
                        Value = c.ContPrefCode.ToString()
                    }));

                    continue;
                }

                if (groupedContactPreferencesForType.Any(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5))
                {
                    amlgamatedContactPreferencesForType.Add(groupedContactPreferencesForType.First(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5));
                }

                if (!(type.Key.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase) && amlgamatedContactPreferencesForType.Count() > 0))
                {
                    amlgamatedContactPreferencesForType.AddRange(groupedContactPreferencesForType.Where(x => x.ContPrefCodeNullable != 3 && x.ContPrefCodeNullable != 4 && x.ContPrefCodeNullable != 5).Take(type.Value - amlgamatedContactPreferencesForType.Count()));
                }

                amalgamatedContactPreferences.AddRange(amlgamatedContactPreferencesForType);
            }

            return new RuleResult<MessageLearnerContactPreference[]> { AmalgamatedValue = amalgamatedContactPreferences.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }
    }
}
