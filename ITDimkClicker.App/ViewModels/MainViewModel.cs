using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public int Value { get; set; }

        public MainViewModel(IMacroFileManager fileManager)
        {
            Value = 5;
        }
    }
}