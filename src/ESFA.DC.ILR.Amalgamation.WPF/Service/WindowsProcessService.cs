using System;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class WindowsProcessService : IWindowsProcessService
    {
        private readonly ILogger _logger;

        public WindowsProcessService(ILogger logger)
        {
            _logger = logger;
        }

        public void ProcessStart(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                _logger.LogError("Windows Process Failed to Start", ex);
            }
        }
    }
}
