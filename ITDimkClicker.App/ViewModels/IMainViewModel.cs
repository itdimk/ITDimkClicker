using System.ComponentModel;
using ITDimkClicker.App.Commands;
using ITDimkClicker.App.Commands.Parameters;

namespace ITDimkClicker.App.ViewModels
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        FileCreateCommand FileCreate { get; }
        FileOpenCommand FileOpen { get; }
        FileSaveCommand FileSave { get; }
        RecordCommand Record { get; }
        PlayCommand Play { get; }
        MergeCommand Merge { get; }
        CurrentFileAccessor CurrentFileAccessor { get; }
        string CurrentFile { get; }
        string State { get; }
    }
}