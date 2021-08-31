using System.Windows;
using ITDimkClicker.App.ViewModels;

namespace ITDimkClicker.App
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
            
        }
    }
}