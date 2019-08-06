using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class LearnerFAMAmalgamationRuleTest
    {
        private readonly List<KeyValuePair<string, int>> famTypes = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("HNS", 1),
            new KeyValuePair<string, int>("EHC", 1),
            new KeyValuePair<string, int>("DLA", 1),
            new KeyValuePair<string, int>("LSR", 4),
            new KeyValuePair<string, int>("SEN", 1),
            new KeyValuePair<string, int>("NLM", 2),
            new KeyValuePair<string, int>("EDF", 2),
            new KeyValuePair<string, int>("MCF", 1),
            new KeyValuePair<string, int>("ECF", 1),
            new KeyValuePair<string, int>("FME", 1),
            new KeyValuePair<string, int>("PPE", 2),
        };

        [Fact]
        public void AlwaysTrue()
        {
            var learnerFAMAmalgamationRule = new LearnerFAMAmalgamationRule();

            IEnumerable<MessageLearnerLearnerFAM[]> fams = new List<MessageLearnerLearnerFAM[]>()
            {
                GetAllFams("ILR-99999999-1920-20190704-092701-05.xml", 10), GetAllFams("ILR-99999999-1920-20190704-092701-06.xml", 10)
            };

            var result = learnerFAMAmalgamationRule.Definition(fams);
            fams.Count().Equals(22);
            result.AmalgamatedValue.Count().Equals(11);
            foreach (var famType in famTypes)
            {
                result.AmalgamatedValue.Where(x => x.LearnFAMType.Equals(famType.Key, StringComparison.OrdinalIgnoreCase)).Count().Equals(1);
            }
        }

        [Fact]
        public void FailDuetoCount()
        {
            var learnerFAMAmalgamationRule = new LearnerFAMAmalgamationRule();

            IEnumerable<MessageLearnerLearnerFAM[]> fams = new List<MessageLearnerLearnerFAM[]>()
            {
                GetAllFams("ILR-99999999-1920-20190704-092701-05.xml", 10), GetAllFams("ILR-99999999-1920-20190704-092701-06.xml", 11)
            };

            var result = learnerFAMAmalgamationRule.Definition(fams);
            fams.Count().Equals(20);
            result.AmalgamationValidationErrors.Count().Equals(14);
            result.AmalgamatedValue.Count().Equals(8);
            foreach (var famType in famTypes.Where(x => x.Value > 1))
            {
                result.AmalgamatedValue.Where(x => x.LearnFAMType.Equals(famType.Key, StringComparison.OrdinalIgnoreCase)).Count().Equals(2);
            }
        }

        private MessageLearnerLearnerFAM[] GetAllFams(string filename, long famcode)
        {
            List<MessageLearnerLearnerFAM> fams = new List<MessageLearnerLearnerFAM>();

            var parent = GetMessageLearner(filename);

            foreach (var famType in famTypes)
            {
                fams.Add(GetMessageLearnerLearnerFAM(famType.Key, famcode, parent));
            }

            return fams.ToArray();
        }

        private MessageLearnerLearnerFAM GetMessageLearnerLearnerFAM(string learnFAMType, long famCode, MessageLearner parent = null)
        {
            return new MessageLearnerLearnerFAM() { LearnFAMType = learnFAMType, LearnFAMCodeNullable = famCode, Parent = parent };
        }

        private MessageLearner GetMessageLearner(string filename)
        {
            return new MessageLearner() { Parent = new Message() { Parent = new AmalgamationRoot() { Filename = filename } } };
        }
    }
}