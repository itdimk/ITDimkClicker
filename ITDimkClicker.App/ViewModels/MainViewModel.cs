using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.App.Annotations;
using ITDimkClicker.App.Commands;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.App.Services;
using ITDimkClicker.BL.Services;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        private string _currentFileName;
        private string _state = "ALT + R to record\nALT + A to play";
        private float _speed = 1f;

        public FileCreateCommand FileCreate { get; }
        public FileOpenCommand FileOpen { get; }
        public FileSaveCommand FileSave { get; }
        public RecordCommand Record { get; }
        public PlayCommand Play { get; }
        public MergeCommand Merge { get; }
        public CurrentFileAccessor CurrentFileAccessor { get; }

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

        public float Speed
        {
            get => _speed;
            set
            {
                if (value.Equals(_speed)) return;
                _speed = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IConsoleAppRunner runner, IMacroIO io)
        {
            CurrentFileAccessor =
                new CurrentFileAccessor((f) => CurrentFile = f, () => CurrentFile, (s) => Speed = s, () => Speed);

            FileCreate = new FileCreateCommand(runner, io);
            FileOpen = new FileOpenCommand(runner);
            FileSave = new FileSaveCommand(runner);
            Record = new RecordCommand(runner);
            Merge = new MergeCommand(runner);
            Play = new PlayCommand(runner);

            runner.IsRunningChanged += (sender, args) => State = runner.IsRunning ? "Working\nALT + S to stop" : "ALT + R to record\nALT + A to play";
            FileCreate.Execute(CurrentFileAccessor);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}