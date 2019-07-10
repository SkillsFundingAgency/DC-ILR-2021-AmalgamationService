using System;

namespace ESFA.DC.ILR.Amalgamation.WPF.Command.Interface
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
