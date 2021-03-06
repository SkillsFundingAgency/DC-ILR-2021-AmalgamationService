﻿using System;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using GalaSoft.MvvmLight.Messaging;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class MessengerService : Messenger, IMessengerService
    {
        public void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            base.Register(recipient, action);
        }
    }
}
