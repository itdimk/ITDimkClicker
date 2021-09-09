using System.IO;
using System.Windows.Input;
using ITDimkClicker.App.Commands;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        private readonly IMacroFileManager _fileManager;

        private Macro _currentMacro;

        public OpenFileCommand OpenFile { get; }
        public SaveFileCommand SaveFile { get; }

        public MainViewModel(IMacroFileManager fileManager)
        {
            _fileManager = fileManager;

            OpenFile = new OpenFileCommand();
            OpenFile.FileSelected += OpenFileOnFileSelected;

            SaveFile = new SaveFileCommand();
            SaveFile.FileSelected += SaveFileOnFileSelected;
        }

        private void SaveFileOnFileSelected(object? sender, string e)
        {
            _fileManager.Write(_currentMacro, File.OpenWrite(e));
        }

        private void OpenFileOnFileSelected(object? sender, string e)
        {
            _currentMacro = _fileManager.Read(File.OpenRead(e));
        }
    }
}