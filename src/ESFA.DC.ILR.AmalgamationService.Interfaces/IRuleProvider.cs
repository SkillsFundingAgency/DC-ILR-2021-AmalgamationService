using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleProvider
    {
        IRule<T> BuildStandardRule<T>();

        IRule<string> BuildAddressRule();

        IRule<long?> BuildUlnRule();

        IRule<long?> BuildAlsCostRule();

        IRule<string> BuildPostCodeRule();

        IRule<MessageLearnerContactPreference[]> BuildLearnerContactPreferenceCollectionRule();
    }
}
