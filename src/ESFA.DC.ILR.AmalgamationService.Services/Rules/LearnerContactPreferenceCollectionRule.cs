using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LearnerContactPreferenceCollectionRule : IRule<MessageLearnerContactPreference[]>
    {
        private readonly string contPrefTypeRUI = "RUI";
        private readonly string contPrefTypePMC = "PMC";

        public IRuleResult<MessageLearnerContactPreference[]> Definition(IEnumerable<MessageLearnerContactPreference[]> contactPreferences)
        {
            if (contactPreferences.SelectMany(v => v).Any(x => x.ContPrefType.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase)))
            {
                return GetContractPreferences(contPrefTypeRUI, contactPreferences, 2);
            }
            else
            {
                return GetContractPreferences(contPrefTypePMC, contactPreferences, 3);
            }
        }

        private RuleResult<MessageLearnerContactPreference[]> GetContractPreferences(string contPrefType, IEnumerable<IEnumerable<MessageLearnerContactPreference>> originalContactPreferences, int maxOccurrence)
        {
            var amlgamatedContactPreferences = new List<MessageLearnerContactPreference>();

            var distinctContactPreferences = originalContactPreferences.SelectMany(v => v).Where(c => c.ContPrefType.Equals(contPrefType, StringComparison.OrdinalIgnoreCase)).GroupBy(g => g.ContPrefCodeNullable).Select(s => s.First());

            if (distinctContactPreferences.Count() > maxOccurrence)
            {
                return new RuleResult<MessageLearnerContactPreference[]>() { Success = false };
            }

            if (distinctContactPreferences.Any(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5))
            {
                amlgamatedContactPreferences.Add(distinctContactPreferences.First(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5));
            }

            if (!(contPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase) && amlgamatedContactPreferences.Count() > 0))
            {
                amlgamatedContactPreferences.AddRange(distinctContactPreferences.Where(x => x.ContPrefCodeNullable != 3 && x.ContPrefCodeNullable != 4 && x.ContPrefCodeNullable != 5).Take(maxOccurrence - amlgamatedContactPreferences.Count()));
            }

            return new RuleResult<MessageLearnerContactPreference[]>() { Success = true, AmalgamatedValue = amlgamatedContactPreferences.ToArray<MessageLearnerContactPreference>() };
        }
    }
}
