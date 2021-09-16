using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.App.Annotations;
using ITDimkClicker.App.Commands;
using ITDimkClicker.App.Services;
using ITDimkClicker.BL.Services;

namespace ITDimkClicker.App.ViewModels
{
    
    public class MainViewModel : IMainViewModel
    {
        private string _currentFileName;
        private string _state = "Idle";

        public NewFileCommand NewFile { get; }
        public OpenFileCommand OpenFile { get; }
        public SaveFileCommand SaveFile { get; }
        public RecordCommand Record { get; }
        public PlayCommand Play { get; }
        public ConcatCommand RunConcat { get; }

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

        public string State
        {
            get => _state;
            private set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IConsoleAppWrapper wrapper)
        {
            OpenFile = new OpenFileCommand(wrapper);
            OpenFile.FileSelected += (_, e) => CurrentFile = e;

            SaveFile = new SaveFileCommand(wrapper);
            SaveFile.FileSelected += (_, e) =>
            {
                File.Copy(CurrentFile, e, true);
                CurrentFile = e;
            };

            NewFile = new NewFileCommand(wrapper);
            NewFile.NewFileCreated += (_, e) => CurrentFile = e;
            NewFile.Execute(new object());

            RunConcat = new ConcatCommand(wrapper);
            Play = new PlayCommand(wrapper);
            Record = new RecordCommand(wrapper);

            wrapper.IsRunningChanged += (sender, args) 
                => State = wrapper.IsRunning ? "Working" : $"Idle (exit code: {wrapper.ExitCode})";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}