namespace ESFA.DC.ILR.Amalgamation.WPF.Service.Interface
{
    public interface IDialogInteractionService
    {
        string[] GetFileNamesFromOpenFileDialog();

        string GetFolderNameFromFolderBrowserDialog(string outputDirectoryPath, string outputDirectoryDescription);
    }
}
