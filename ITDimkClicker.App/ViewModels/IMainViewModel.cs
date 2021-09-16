using System.ComponentModel;
using ITDimkClicker.App.Commands;

namespace ITDimkClicker.App.ViewModels
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        NewFileCommand NewFile { get; }
        OpenFileCommand OpenFile { get; }
        SaveFileCommand SaveFile { get; }
        RecordCommand Record { get; }
        PlayCommand Play { get; }
        ConcatCommand RunConcat { get; }
        string CurrentFile { get; }
        string State { get; }
    }
}