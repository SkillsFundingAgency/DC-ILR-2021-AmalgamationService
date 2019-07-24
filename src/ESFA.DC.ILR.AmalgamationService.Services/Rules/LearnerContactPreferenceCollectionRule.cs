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
        public IRuleResult<MessageLearnerContactPreference[]> Definition(IEnumerable<MessageLearnerContactPreference[]> contactPreferences)
        {
            var amlgamatedContactPreferences = new List<MessageLearnerContactPreference>();

            GetContractPreferences("RUI", contactPreferences, 2, amlgamatedContactPreferences);
            GetContractPreferences("PMC", contactPreferences, 3, amlgamatedContactPreferences);

            return new RuleResult<MessageLearnerContactPreference[]>() { Success = true, Result = amlgamatedContactPreferences.ToArray<MessageLearnerContactPreference>() };
        }

        private static void GetContractPreferences(string contPrefType, IEnumerable<IEnumerable<MessageLearnerContactPreference>> originalContactPreferences, int maxOccurrence, List<MessageLearnerContactPreference> amlgamatedContactPreferences)
        {
            var distinctContactPreferences = originalContactPreferences.SelectMany(v => v).Where(c => c.ContPrefType.ToUpper().Equals(contPrefType)).GroupBy(g => g.ContPrefCodeNullable).Select(s => s.First());
            if (distinctContactPreferences.Any(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5))
            {
                amlgamatedContactPreferences.Add(distinctContactPreferences.First(x => x.ContPrefCodeNullable == 3 || x.ContPrefCodeNullable == 4 || x.ContPrefCodeNullable == 5));
            }
            else
            {
                amlgamatedContactPreferences.AddRange(distinctContactPreferences.Take(maxOccurrence));
            }
        }
    }
}
