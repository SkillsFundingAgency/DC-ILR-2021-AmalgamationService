using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class HeaderCollectionDetailsAmalgamator : AbstractAmalgamator<MessageHeaderCollectionDetails>, IAmalgamator<MessageHeaderCollectionDetails>
    {
        public HeaderCollectionDetailsAmalgamator(IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.CollectionDetails, (x) => null, amalgamationErrorHandler)
        {
        }

        public MessageHeaderCollectionDetails Amalgamate(IEnumerable<MessageHeaderCollectionDetails> models)
        {
            return new MessageHeaderCollectionDetails() { FilePreparationDate = models.Max(x => x.FilePreparationDate) };
        }
    }
}
