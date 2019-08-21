﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
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
                if (!contactPreferencesList.Any(x => x.ContPrefType.Equals(type.Key, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                var groupedContactPreferencesForType = contactPreferencesList.Where(cp => cp.ContPrefType.Equals(type.Key, StringComparison.OrdinalIgnoreCase)).GroupBy(x => new { x.ContPrefType, x.ContPrefCodeNullable }).Select(c => c.First()).ToArray();

                var amlgamatedContactPreferencesForType = new List<MessageLearnerContactPreference>();

                if (type.Key.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase))
                {
                    // RUI3 takes precedence over all other RUI records. Only one record of RUI3, 4 or 5 should be stored. Other RUI records discarded if RUI3, 4 or 5 is present.
                    if (groupedContactPreferencesForType.Any(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5))
                    {
                        amlgamatedContactPreferencesForType.Add(groupedContactPreferencesForType.First(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5));
                    }
                    else
                    {
                        if (groupedContactPreferencesForType.Count() > type.Value)
                        {
                            amalgamationValidationErrors.AddRange(contactPreferencesList.Where(cp => cp.ContPrefType.Equals(type.Key, StringComparison.OrdinalIgnoreCase)).Select(GetAmalgamationValidationError));
                            continue;
                        }

                        // Append unique records of RUI until max occurrence reached, unless there is RUI 3,4,5 exists.
                        amlgamatedContactPreferencesForType.AddRange(groupedContactPreferencesForType.Take(type.Value));
                    }
                }

                if (type.Key.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase))
                {
                    // Append all PMC 3, 4 or 5 records
                    if (groupedContactPreferencesForType.Any(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5))
                    {
                        amlgamatedContactPreferencesForType.AddRange(groupedContactPreferencesForType.Where(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5));
                    }
                    else
                    {
                        if (groupedContactPreferencesForType.Count() > type.Value)
                        {
                            amalgamationValidationErrors.AddRange(contactPreferencesList.Where(cp => cp.ContPrefType.Equals(type.Key, StringComparison.OrdinalIgnoreCase)).Select(GetAmalgamationValidationError));
                            continue;
                        }

                        // Append unique records of PMC until max occurrence reached, unless there is PMC 3,4,5 exists.
                        amlgamatedContactPreferencesForType.AddRange(groupedContactPreferencesForType.Take(type.Value));
                    }
                }

                amalgamatedContactPreferences.AddRange(amlgamatedContactPreferencesForType);
            }

            return new RuleResult<MessageLearnerContactPreference[]> { AmalgamatedValue = amalgamatedContactPreferences.ToArray(), AmalgamationValidationErrors = amalgamationValidationErrors };
        }

        private AmalgamationValidationError GetAmalgamationValidationError(MessageLearnerContactPreference c)
        {
            return new AmalgamationValidationError()
            {
                File = c.SourceFileName,
                LearnRefNumber = c.LearnRefNumber,
                ErrorType = ErrorType.MaxOccurrenceExceeded,
                Entity = _entityName,
                Key = $"ContPrefType : {c.ContPrefType}",
                Value = c.ContPrefCode.ToString(),
                Severity = Severity.Error
            };
        }
    }
}
