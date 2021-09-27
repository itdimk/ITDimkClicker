using System.Windows;
using System.Windows.Forms;
using ITDimkClicker.App.ViewModels;
using ITDimkClicker.BL.Services;
using ITDImkClicker.ConsoleApp.Data;

namespace ITDimkClicker.App
{
    public partial class MainWindow
    {
        private readonly KeyboardHook _hook;
        private IMainViewModel ViewModel => (IMainViewModel) DataContext;

        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();

            _hook = new KeyboardHook();
            _hook.RegisterHotKey(ModifKeys.Alt, Keys.R);
            _hook.RegisterHotKey(ModifKeys.Alt, Keys.A);
            _hook.KeyPressed += OnHookKeyPressed;
        }

        private void OnHookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            var playCommand = ViewModel.Play;
            var recordCommand = ViewModel.Record;
            
            if (e.Key == Keys.A && playCommand.CanExecute(ViewModel.CurrentFile))
               playCommand.Execute(ViewModel.CurrentFileAccessor); 
            
            else if (e.Key == Keys.R && recordCommand.CanExecute(ViewModel.CurrentFile))
                recordCommand.Execute(ViewModel.CurrentFileAccessor);
        }
    }
}