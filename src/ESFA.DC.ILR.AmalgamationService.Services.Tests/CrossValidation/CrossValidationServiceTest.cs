using ESFA.DC.ILR.AmalgamationService.Services.CrossValidation;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.CrossValidation
{
    public class CrossValidationServiceTest
    {
        [Fact]
        public void RemoveDuplicateNumbers_Pass()
        {
            MessageLearner[] learners = new[]
            {
                new MessageLearner() { LearnRefNumber = "00100308" },
                new MessageLearner() { LearnRefNumber = "00100308" },
                new MessageLearner() { LearnRefNumber = "abc" },
                new MessageLearner() { LearnRefNumber = "ABC" }
            };

            ILooseMessage message = new Message()
            {
                Learners = learners
            };

            var result = CrossValidationService().GetDuplicateLearnRefNumbers(learners);
            result.Should().HaveCount(2);
        }

        [Fact]
        public void LearnerReferenceNumbers_NoDuplicateFound()
        {
            MessageLearner[] learners = new[]
             {
                new MessageLearner() { LearnRefNumber = "123" },
                new MessageLearner() { LearnRefNumber = "456" },
                new MessageLearner() { LearnRefNumber = "abc" }
            };

            ILooseMessage message = new Message()
            {
                Learners = learners
            };

            var result = CrossValidationService().GetDuplicateLearnRefNumbers(learners);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void RemoveDuplicateNumbers_DestinationProgression_Pass()
        {
            MessageLearnerDestinationandProgression[] progressionList = new[]
            {
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "00100308" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "00100308" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abc" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "ABC" }
            };

            ILooseMessage message = new Message()
            {
                LearnerDestinationAndProgressions = progressionList
            };

            var result = CrossValidationService().GetDuplicateLearnRefNumbers(progressions: progressionList);
            result.Should().HaveCount(2);
        }

        [Fact]
        public void DestinationProgressionReferenceNumbers_NoDuplicateFound()
        {
            MessageLearnerDestinationandProgression[] progressionList = new[]
             {
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "123" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "456" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abc" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abcd" }
            };

            ILooseMessage message = new Message()
            {
                LearnerDestinationAndProgressions = progressionList
            };

            var result = CrossValidationService().GetDuplicateLearnRefNumbers(progressions: progressionList);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void NotSameObjectResulted_Pass()
        {
            MessageLearner[] learners = new[]
            {
                new MessageLearner() { LearnRefNumber = "00100308" },
                new MessageLearner() { LearnRefNumber = "00100308" },
                new MessageLearner() { LearnRefNumber = "abc" },
                new MessageLearner() { LearnRefNumber = "456" },
                new MessageLearner() { LearnRefNumber = "ABC" }
            };

            MessageLearnerDestinationandProgression[] progressionList = new[]
            {
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "123" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "ABC" },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abc" }
            };

            ILooseMessage message = new Message()
            {
                Learners = learners,
                LearnerDestinationAndProgressions = progressionList
            };

            var resultMessage = CrossValidationService().CrossValidateLearners(message);

            resultMessage.Learners.Should().HaveCount(3);
            resultMessage.Learners.Should().NotBeSameAs(learners);

            resultMessage.LearnerDestinationAndProgressions.Should().HaveCount(2);
            resultMessage.LearnerDestinationAndProgressions.Should().NotBeSameAs(progressionList);
        }

        public CrossValidationService CrossValidationService()
        {
            return new CrossValidationService();
        }
    }
}
