using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class LearnerContactPreferenceCollectionRuleTest
    {
        private readonly string contPrefTypeRUI = "RUI";
        private readonly string contPrefTypePMC = "PMC";

        [Fact]
        public void AlwaysTrue()
        {
            var learnerContactPreferenceCollectionRule = new LearnerContactPreferenceCollectionRule();

            var input1 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(1, contPrefTypeRUI), GetMessageLearnerContactPreference(2, contPrefTypePMC), GetMessageLearnerContactPreference(1, contPrefTypePMC) };
            var input2 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(12, contPrefTypeRUI), GetMessageLearnerContactPreference(11, contPrefTypePMC) };

            IEnumerable<MessageLearnerContactPreference[]> contactPreferences = new List<MessageLearnerContactPreference[]>()
            {
                input1, input2
            };

            var result = learnerContactPreferenceCollectionRule.Definition(contactPreferences);
        }

        private MessageLearnerContactPreference GetMessageLearnerContactPreference(long contPrefCode, string contPrefType)
        {
            MessageLearnerContactPreference messageLearnerContactPreference = new MessageLearnerContactPreference() { ContPrefCodeNullable = contPrefCode, ContPrefType = contPrefType };

            return messageLearnerContactPreference;
        }
    }
}