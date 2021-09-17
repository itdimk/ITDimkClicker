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
        private string _state = "Idle";

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

        public MainViewModel(IConsoleAppWrapper wrapper, IMacroIO io)
        {
            CurrentFileAccessor = new CurrentFileAccessor((f) => CurrentFile = f, () => CurrentFile);
            
            FileCreate = new FileCreateCommand(wrapper, io);
            FileOpen = new FileOpenCommand(wrapper);
            FileSave = new FileSaveCommand(wrapper);
            Record = new RecordCommand(wrapper);
            Merge = new MergeCommand(wrapper);
            Play = new PlayCommand(wrapper);

            wrapper.IsRunningChanged += (sender, args) => State = wrapper.IsRunning ? "Working" : "Idle";
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