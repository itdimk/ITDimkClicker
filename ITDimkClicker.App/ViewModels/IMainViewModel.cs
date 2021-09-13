using System.ComponentModel;
using ITDimkClicker.App.Commands;

namespace ITDimkClicker.App.ViewModels
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        NewFileCommand NewFile { get; }
        OpenFileCommand OpenFile { get; }
        SaveFileCommand SaveFile { get; }
        RunRecordCommand RunRecord { get; }
        RunPlayCommand RunPlay { get; }
        string CurrentFile { get; }
    }
}