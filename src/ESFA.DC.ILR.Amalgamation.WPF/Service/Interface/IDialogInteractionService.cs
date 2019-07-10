namespace ESFA.DC.ILR.Amalgamation.WPF.Service.Interface
{
    public interface IDialogInteractionService
    {
        string GetFileNameFromOpenFileDialog();

        string GetFolderNameFromFolderBrowserDialog(string outputDirectoryPath, string outputDirectoryDescription);
    }
}
