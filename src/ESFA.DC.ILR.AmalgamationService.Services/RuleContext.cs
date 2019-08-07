using ESFA.DC.ILR.AmalgamationService.Interfaces;

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
