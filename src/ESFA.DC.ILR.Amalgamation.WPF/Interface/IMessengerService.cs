using System;

namespace ESFA.DC.ILR.Amalgamation.WPF.Interface
{
    public interface IMessengerService
    {
        void Register<TMessage>(object recipient, Action<TMessage> action);

        void Send<TMessage>(TMessage message);
    }
}
