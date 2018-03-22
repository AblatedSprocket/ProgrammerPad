using Microsoft.Win32;
using ProgrammerPad.Editor;
using ProgrammerPad.Utilities;
using System.IO;

namespace ProgrammerPad.FileMenu
{
    class FileMenuModel
    {
        public Document Document { get; set; }
        public RelayCommand NewCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }

        public FileMenuModel(Document document)
        {
            Document = document;
            NewCommand = new RelayCommand(NewFile);
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
        }
        public void NewFile()
        {
            Document.FileName = string.Empty;
            Document.FilePath = string.Empty;
            Document.Text = string.Empty;
        }
        private void SaveFile()
        {
            if (!string.IsNullOrEmpty(Document.FilePath))
            {
                File.WriteAllText(Document.FilePath, Document.Text);
            }
            else
            {
                SaveFileAs();
            }
        }
        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if (Document.FileName != null)
            {
                saveFileDialog.FileName = Document.FileName;
            }
            if (saveFileDialog.ShowDialog() == true)
            {
                DockFile(saveFileDialog);
                File.WriteAllText(saveFileDialog.FileName, Document.Text);
            }
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DockFile(openFileDialog);
                Document.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }
        private void DockFile<T>(T dialog) where T : FileDialog
        {
            Document.FilePath = dialog.FileName;
            Document.FileName = dialog.SafeFileName;
        }
    }
}
