﻿using System.Collections.Generic;
using Autofac;
using ESFA.DC.ILR.AmalgamationService.Services.Logger;
using ILogger = ESFA.DC.Logging.Interfaces.ILogger;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace ESFA.DC.ILR.Amalgamation.WPF.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            LogManager.Configuration = BuildLoggingConfiguration();

            containerBuilder.Register(c => (AmalgamatorLogger)LogManager.GetLogger("Amalgamator", typeof(AmalgamatorLogger))).As<ILogger>();
        }

        private LoggingConfiguration BuildLoggingConfiguration()
        {
            var loggingConfiguration = new LoggingConfiguration();

            var columns = new List<CsvColumn>()
            {
                new CsvColumn("DateTime", "${longdate}") { Quoting = CsvQuotingMode.Nothing },
                new CsvColumn("Level", "${level:upperCase=true}") { Quoting = CsvQuotingMode.Nothing },
                new CsvColumn("Message", "${message}") { Quoting = CsvQuotingMode.All },
                new CsvColumn("Exception", "${exception:format=ToString}") { Quoting = CsvQuotingMode.All },
                new CsvColumn("CallSite", "${callsite}") { Quoting = CsvQuotingMode.All },
                new CsvColumn("CallSiteLineNumber", "${callsite-linenumber}") { Quoting = CsvQuotingMode.Nothing },
                new CsvColumn("StackTrace", "${stacktrace:topFrames=5}") { Quoting = CsvQuotingMode.All },
            };

            var csvLayout = new CsvLayout();

            foreach (var column in columns)
            {
                csvLayout.Columns.Add(column);
            }

            var fileTarget = new FileTarget("FileTarget")
            {
                FileName = "${basedir}/Logs/${date:format=yyyy-MM-dd}.csv",
                Layout = csvLayout,
            };

            loggingConfiguration.AddTarget(fileTarget);

            loggingConfiguration.AddRuleForAllLevels(fileTarget);

            return loggingConfiguration;
        }
    }
}
