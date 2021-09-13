using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.App.Annotations;
using ITDimkClicker.App.Commands;
using ITDimkClicker.BL.Services;

namespace ITDimkClicker.App.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        private string _currentFileName;

        public NewFileCommand NewFile { get; }
        public OpenFileCommand OpenFile { get; }
        public SaveFileCommand SaveFile { get; }
        public RunRecordCommand RunRecord { get; }
        public RunPlayCommand RunPlay { get; }

        public string CurrentFile
        {
            get => _currentFileName;
            set
            {
                if (value == _currentFileName) return;
                _currentFileName = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            OpenFile = new OpenFileCommand();
            OpenFile.FileSelected += (_, e) => CurrentFile = e;

            SaveFile = new SaveFileCommand();
            SaveFile.FileSelected += (_, e) =>
            {
                File.Copy(CurrentFile, e, true);
                CurrentFile = e;
            };

            NewFile = new NewFileCommand();
            NewFile.NewFileCreated += (_, e) => CurrentFile = e;

            RunPlay = new RunPlayCommand("ITDimkClicker.ConsoleApp.lnk", "ITDimkClicker.ConsoleApp");
            RunRecord = new RunRecordCommand("ITDimkClicker.ConsoleApp.lnk", "ITDimkClicker.ConsoleApp");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}