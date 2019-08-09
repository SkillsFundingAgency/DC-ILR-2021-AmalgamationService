using System;
using System.Configuration;

namespace ESFA.DC.ILR.Amalgamation.WPF.Config
{
    public class DesktopServiceConfiguration : ConfigurationSection
    {
        private const string ReleaseDateKey = "ReleaseDate";

        public static DesktopServiceConfiguration Configuration => ConfigurationManager.GetSection("DesktopServiceConfiguration") as DesktopServiceConfiguration;

        [ConfigurationProperty(ReleaseDateKey, IsRequired = true)]
        public string ReleaseDate => FormattedDate(ReleaseDateKey);

        private string FormattedDate(string key)
        {
            if (DateTime.TryParse(this[key].ToString(), out var returnDate))
            {
                return returnDate.ToString("dd/MM/yyyy hh:mm:ss");
            }

            return string.Empty;
        }
    }
}
