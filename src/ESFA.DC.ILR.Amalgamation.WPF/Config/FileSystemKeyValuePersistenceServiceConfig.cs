using ESFA.DC.IO.FileSystem.Config.Interfaces;

namespace ESFA.DC.ILR.Amalgamation.WPF.Config
{
    public class FileSystemKeyValuePersistenceServiceConfig : IFileSystemKeyValuePersistenceServiceConfig
    {
        public string Directory { get; set; }
    }
}
