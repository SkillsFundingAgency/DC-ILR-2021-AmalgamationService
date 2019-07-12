using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Amalgamation.WPF.Interface
{
    public interface IMessengerService
    {
        void Register<TMessage>(object recipient, Action<TMessage> action);

        void Send<TMessage>(TMessage message);
    }
}
