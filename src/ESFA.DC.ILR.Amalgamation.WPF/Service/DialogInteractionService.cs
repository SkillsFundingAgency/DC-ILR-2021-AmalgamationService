using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;
using Microsoft.Win32;
using DialogResult = System.Windows.Forms.DialogResult;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class DialogInteractionService : IDialogInteractionService
    {
        private const string ChooseOutputDirectoryDescription = @"Choose Output Directory";

        public string[] GetFileNamesFromOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog() { Multiselect = true, Filter = "ILR Files (ILR-????????-2021-????????-??????-??.xml)|ILR-????????-2021-????????-??????-??.xml" };

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileNames;
            }

            return null;
        }

        public string GetFolderNameFromFolderBrowserDialog(string outputDirectoryPath, string outputDirectoryDescription)
        {
            string selectedOutputDirectory = string.Empty;

            using (var folderBrowserDlg = new FolderBrowserDialog())
            {
                // Configure browser dialog box
                folderBrowserDlg.Description = outputDirectoryDescription;
                folderBrowserDlg.ShowNewFolderButton = true;
                folderBrowserDlg.SelectedPath = outputDirectoryPath;

                // Show the dialog
                var result = folderBrowserDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Retrieve the selected path
                    selectedOutputDirectory = folderBrowserDlg.SelectedPath;
                }
            }

            return selectedOutputDirectory;
        }
    }
}
