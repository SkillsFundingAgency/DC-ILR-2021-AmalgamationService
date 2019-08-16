using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.CrossValidation;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.CrossValidation
{
    public class CrossValidationServiceTest
    {
        [Fact]
        public void RemoveDuplicateNumbers_Pass()
        {
            var filename = "xyz.xml";
            MessageLearner[] learners = new[]
            {
                new MessageLearner() { LearnRefNumber = "00100308", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "00100308", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "abc", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "ABC", Parent = GetParent(filename) }
            };

            ILooseMessage message = new Message()
            {
                Learners = learners
            };

            var errorList = new List<IValidationError>();

            var mockValidationErrorHandler = new Mock<IValidationErrorHandler>();
            mockValidationErrorHandler.SetupGet(x => x.ValidationErrors).Returns(errorList);
            mockValidationErrorHandler.Setup(x => x.CrossRecordValidationErrorHandler(It.IsAny<string>(), It.IsAny<string>()))
                 .Callback(() =>
                 {
                     errorList.Add(new ValidationError(It.IsAny<string>(), System.Xml.Schema.XmlSeverityType.Error, 0, 0));
                 });

            var duplicateResult = CrossValidationService(mockValidationErrorHandler.Object).GetLearnerDuplicateLearnRefNumbers(learners);
            duplicateResult.Should().HaveCount(2);

            mockValidationErrorHandler.Object.ValidationErrors.Should().HaveCountGreaterOrEqualTo(2);
            mockValidationErrorHandler.Verify(x => x.CrossRecordValidationErrorHandler(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
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

            var result = CrossValidationService().GetLearnerDuplicateLearnRefNumbers(learners);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void RemoveDuplicateNumbers_DestinationProgression_Pass()
        {
            var filename = "xyz.xml";
            MessageLearnerDestinationandProgression[] progressionList = new[]
            {
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "00100308", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "00100308", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abc", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "ABC", Parent = GetParent(filename) }
            };

            var errorList = new List<IValidationError>();

            var mockValidationErrorHandler = new Mock<IValidationErrorHandler>();
            mockValidationErrorHandler.SetupGet(x => x.ValidationErrors).Returns(errorList);
            mockValidationErrorHandler.Setup(x => x.CrossRecordValidationErrorHandler(It.IsAny<string>(), It.IsAny<string>()))
                 .Callback(() =>
                 {
                     errorList.Add(new ValidationError(It.IsAny<string>(), System.Xml.Schema.XmlSeverityType.Error, 0, 0));
                 });

            var duplicateResult = CrossValidationService(mockValidationErrorHandler.Object).GetDPduplicateLearnRefNumbers(progressionList);
            duplicateResult.Should().HaveCount(2);

            mockValidationErrorHandler.Object.ValidationErrors.Should().HaveCountGreaterOrEqualTo(2);
            mockValidationErrorHandler.Verify(x => x.CrossRecordValidationErrorHandler(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void DestinationProgressionReferenceNumbers_NoDuplicateFound()
        {
            var filename = "xyz.xml";
            MessageLearnerDestinationandProgression[] progressionList = new[]
             {
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "123", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "456", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abc", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abcd", Parent = GetParent(filename) }
            };

            ILooseMessage message = new Message()
            {
                LearnerDestinationAndProgressions = progressionList
            };

            var result = CrossValidationService().GetDPduplicateLearnRefNumbers(progressionList: progressionList);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void NotSameObjectResulted_Pass()
        {
            string filename = "xyz.xml";
            MessageLearner[] learners = new[]
            {
                new MessageLearner() { LearnRefNumber = "00100308", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "00100308", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "abc", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "456", Parent = GetParent(filename) },
                new MessageLearner() { LearnRefNumber = "ABC", Parent = GetParent(filename) }
            };

            MessageLearnerDestinationandProgression[] progressionList = new[]
            {
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "123", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "ABC", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "xy0011365", Parent = GetParent(filename) },
                new MessageLearnerDestinationandProgression() { LearnRefNumber = "abc", Parent = GetParent(filename) }
            };

            ILooseMessage message = new Message()
            {
                Learners = learners,
                Parent = new AmalgamationRoot()
                {
                    Filename = "xyz.xml",
                    Message = new Message()
                },
                LearnerDestinationAndProgressions = progressionList
            };

            var errorList = new List<IValidationError>();

            var mockValidationErrorHandler = new Mock<IValidationErrorHandler>();
            mockValidationErrorHandler.SetupGet(x => x.ValidationErrors).Returns(errorList);
            mockValidationErrorHandler.Setup(x => x.CrossRecordValidationErrorHandler(It.IsAny<string>(), It.IsAny<string>()))
                 .Callback(() =>
                 {
                     errorList.Add(new ValidationError(It.IsAny<string>(), System.Xml.Schema.XmlSeverityType.Error, 0, 0));
                 });

            var resultMessage = CrossValidationService(mockValidationErrorHandler.Object).CrossValidateLearners(message);

            resultMessage.Learners.Should().HaveCount(1);
            resultMessage.Learners.Should().NotBeSameAs(learners);

            resultMessage.LearnerDestinationAndProgressions.Should().HaveCount(2);
            resultMessage.LearnerDestinationAndProgressions.Should().NotBeSameAs(progressionList);

            mockValidationErrorHandler.Object.ValidationErrors.Should().HaveCountGreaterOrEqualTo(3);
            mockValidationErrorHandler.Verify(x => x.CrossRecordValidationErrorHandler(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        public CrossValidationService CrossValidationService(IValidationErrorHandler validationErrorHandler = null)
        {
            return new CrossValidationService(validationErrorHandler);
        }

        public Message GetParent(string filename) => new Message()
            {
                Parent = new AmalgamationRoot()
                {
                    Filename = filename,
                    Message = new Message()
                }
        };
    }
}
