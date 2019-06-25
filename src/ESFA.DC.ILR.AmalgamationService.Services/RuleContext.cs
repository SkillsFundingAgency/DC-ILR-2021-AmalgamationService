using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class RuleContext : IRuleContext
    {
        public string FileName { get; set; }

        public string LearnRefNumber { get; set; }

        public string Entity { get; set; }

        public string Key { get; set; }
    }
}
