using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
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
            var input2 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(2, contPrefTypeRUI), GetMessageLearnerContactPreference(2, contPrefTypeRUI), GetMessageLearnerContactPreference(1, contPrefTypeRUI), GetMessageLearnerContactPreference(11, contPrefTypePMC) };

            IEnumerable<List<MessageLearnerContactPreference>> contactPreferences = new List<List<MessageLearnerContactPreference>>()
            {
                input1.ToList(), input2.ToList()
            };

            var result = learnerContactPreferenceCollectionRule.Definition(contactPreferences);
            result.AmalgamatedValue.Count().Equals(5);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase)).Count().Equals(2);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase)).Count().Equals(3);
        }

        [Fact]
        public void Rui3ExistsNoPMC()
        {
            var learnerContactPreferenceCollectionRule = new LearnerContactPreferenceCollectionRule();

            var input1 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(1, contPrefTypeRUI), GetMessageLearnerContactPreference(2, contPrefTypePMC), GetMessageLearnerContactPreference(5, contPrefTypePMC) };
            var input2 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(2, contPrefTypeRUI), GetMessageLearnerContactPreference(3, contPrefTypePMC), GetMessageLearnerContactPreference(3, contPrefTypeRUI), GetMessageLearnerContactPreference(4, contPrefTypePMC) };

            IEnumerable<List<MessageLearnerContactPreference>> contactPreferences = new List<List<MessageLearnerContactPreference>>()
            {
                input1.ToList(), input2.ToList()
            };

            var result = learnerContactPreferenceCollectionRule.Definition(contactPreferences);
            result.AmalgamatedValue.Count().Equals(1);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase)).Count().Equals(1);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase)).Count().Equals(0);
        }

        [Fact]
        public void RuiManyPMCMany()
        {
            var learnerContactPreferenceCollectionRule = new LearnerContactPreferenceCollectionRule();

            var input1 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(1, contPrefTypeRUI), GetMessageLearnerContactPreference(2, contPrefTypePMC), GetMessageLearnerContactPreference(5, contPrefTypePMC) };
            var input2 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(2, contPrefTypeRUI), GetMessageLearnerContactPreference(5, contPrefTypePMC), GetMessageLearnerContactPreference(1, contPrefTypeRUI), GetMessageLearnerContactPreference(4, contPrefTypePMC) };

            IEnumerable<List<MessageLearnerContactPreference>> contactPreferences = new List<List<MessageLearnerContactPreference>>()
            {
                input1.ToList(), input2.ToList()
            };

            var result = learnerContactPreferenceCollectionRule.Definition(contactPreferences);
            result.AmalgamatedValue.Count().Equals(1);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase)).Count().Equals(2);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase)).Count().Equals(3);
        }

        [Fact]
        public void Rui2PmcMaxError()
        {
            var learnerContactPreferenceCollectionRule = new LearnerContactPreferenceCollectionRule();

            var file1 = GetMessageLearner("ILR-99999999-2021-20200704-092701-05.xml");
            var file2 = GetMessageLearner("ILR-99999999-2021-20200704-092701-06.xml");

            var input1 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(1, contPrefTypeRUI, file1), GetMessageLearnerContactPreference(7, contPrefTypePMC, file1), GetMessageLearnerContactPreference(6, contPrefTypeRUI, file1), GetMessageLearnerContactPreference(6, contPrefTypePMC, file1) };
            var input2 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(2, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(8, contPrefTypePMC, file2), GetMessageLearnerContactPreference(7, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(9, contPrefTypePMC, file2) };

            IEnumerable<List<MessageLearnerContactPreference>> contactPreferences = new List<List<MessageLearnerContactPreference>>()
            {
                input1.ToList(), input2.ToList()
            };

            var result = learnerContactPreferenceCollectionRule.Definition(contactPreferences);
            result.AmalgamatedValue.Count().Equals(5);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase)).Count().Equals(1);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase)).Count().Equals(0);
            result.AmalgamationValidationErrors.Count().Equals(4);
        }

        [Fact]
        public void FailRUIPassPMC()
        {
            var learnerContactPreferenceCollectionRule = new LearnerContactPreferenceCollectionRule();

            var file1 = GetMessageLearner("ILR-99999999-2021-20200704-092701-05.xml");
            var file2 = GetMessageLearner("ILR-99999999-2021-20200704-092701-06.xml");

            var input1 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(1, contPrefTypeRUI, file1), GetMessageLearnerContactPreference(2, contPrefTypePMC, file1), GetMessageLearnerContactPreference(1, contPrefTypePMC, file1) };
            var input2 = new MessageLearnerContactPreference[] { GetMessageLearnerContactPreference(2, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(2, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(12, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(3, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(12, contPrefTypeRUI, file2), GetMessageLearnerContactPreference(11, contPrefTypePMC, file2) };

            IEnumerable<List<MessageLearnerContactPreference>> contactPreferences = new List<List<MessageLearnerContactPreference>>()
            {
                input1.ToList(), input2.ToList()
            };

            var result = learnerContactPreferenceCollectionRule.Definition(contactPreferences);

            result.AmalgamationValidationErrors.Count().Equals(5);
            result.AmalgamatedValue.Count().Equals(2);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypeRUI, StringComparison.OrdinalIgnoreCase)).Count().Equals(0);
            result.AmalgamatedValue.Where(x => x.ContPrefType.Equals(contPrefTypePMC, StringComparison.OrdinalIgnoreCase)).Count().Equals(2);
        }

        private MessageLearnerContactPreference GetMessageLearnerContactPreference(long contPrefCode, string contPrefType, MessageLearner parent = null)
        {
            MessageLearnerContactPreference messageLearnerContactPreference = new MessageLearnerContactPreference() { ContPrefCode = contPrefCode, ContPrefType = contPrefType, Parent = parent };

            return messageLearnerContactPreference;
        }

        private MessageLearner GetMessageLearner(string filename)
        {
            return new MessageLearner() { Parent = new Message() { Parent = new AmalgamationRoot() { Filename = filename } } };
        }
    }
}