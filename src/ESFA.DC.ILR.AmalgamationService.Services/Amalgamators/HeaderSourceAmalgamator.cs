using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class HeaderSourceAmalgamator : AbstractAmalgamator<MessageHeaderSource>, IAmalgamator<MessageHeaderSource>
    {
        private IDateTimeProvider _dateTimeProvider;

        public HeaderSourceAmalgamator(IDateTimeProvider dateTimeProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.Source, (x) => null, amalgamationErrorHandler)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public MessageHeaderSource Amalgamate(IEnumerable<MessageHeaderSource> models)
        {
            var source = new MessageHeaderSource()
            {
                DateTime = _dateTimeProvider.GetNowUtc(),
                ProtectiveMarking = MessageHeaderSourceProtectiveMarking.OFFICIALSENSITIVEPersonal,
                UKPRN = models.Select(x => x.UKPRN).First(),
                SoftwareSupplier = "ESFA",
                SoftwarePackage = "FileMerge",
                Release = "01",
                SerialNo = "00"
            };

            return source;
        }
    }
}
