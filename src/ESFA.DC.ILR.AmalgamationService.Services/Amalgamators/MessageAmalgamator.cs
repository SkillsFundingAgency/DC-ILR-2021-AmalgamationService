using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class MessageAmalgamator : AbstractAmalgamator<Message>, IAmalgamator<Message>
    {
        private readonly IAmalgamator<MessageHeader> _headerAmalgamator;
        private readonly IAmalgamator<MessageLearner> _learnerAmalgamator;
        private readonly IAmalgamator<MessageLearnerDestinationandProgression> _learnerDestinationandProgressionAmalgamator;

        public MessageAmalgamator(IAmalgamator<MessageHeader> headerAmalgamator, IAmalgamator<MessageLearner> learnerAmalgamator, IAmalgamator<MessageLearnerDestinationandProgression> learnerDestinationandProgressionAmalgamator, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.Message, (x) => null, amalgamationErrorHandler)
        {
            _headerAmalgamator = headerAmalgamator;
            _learnerAmalgamator = learnerAmalgamator;
            _learnerDestinationandProgressionAmalgamator = learnerDestinationandProgressionAmalgamator;
        }

        public IAmalgamationErrorHandler AmalgamationErrorHandler { get; }

        public Message Amalgamate(IEnumerable<Message> models)
        {
            var message = new Message();

            ApplyChildRule(m => m.Header, _headerAmalgamator, models, message);
            message.SourceFiles = GetSourceFiles(models);
            message.LearningProvider = GetLearningProvider(models);
            ApplyGroupedChildCollectionRule(m => m.Learner, g => g.LearnRefNumber, _learnerAmalgamator, models, message);
            ApplyGroupedChildCollectionRule(m => m.LearnerDestinationandProgression, g => g.LearnRefNumber, _learnerDestinationandProgressionAmalgamator, models, message);

            return message;
        }

        private MessageSourceFile[] GetSourceFiles(IEnumerable<Message> models)
        {
            return models.Select(x => new MessageSourceFile()
            {
                SourceFileName = x.Parent.Filename,
                FilePreparationDate = x.Header.CollectionDetails.FilePreparationDate,
                SoftwareSupplier = x.Header.Source.SoftwareSupplier,
                SoftwarePackage = x.Header.Source.SoftwarePackage,
                Release = x.Header.Source.Release,
                SerialNo = x.Header.Source.SerialNo,
                DateTime = x.Header.Source.DateTime
            }).ToArray();
        }

        private MessageLearningProvider GetLearningProvider(IEnumerable<Message> models)
        {
            return models.Where(x => x.LearningProvider != null && x.LearningProvider.UKPRN != 0).Select(s => s.LearningProvider).First();
        }
    }
}
