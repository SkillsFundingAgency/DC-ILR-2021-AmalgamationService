﻿using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Amalgamation.WPF.Command.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Command.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
