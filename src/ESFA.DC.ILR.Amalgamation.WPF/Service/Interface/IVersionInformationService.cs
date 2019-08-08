using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service.Interface
{
    public interface IVersionInformationService
    {
        string Date { get; }

        string VersionNumber { get; }
    }
}
