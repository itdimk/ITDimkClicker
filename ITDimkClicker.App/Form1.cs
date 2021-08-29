using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITDimkClicker.RecorderApp.Utility;

namespace ITDimkClicker.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var hook = new KeyboardHook();
            hook.RegisterHotKey(ModifKeys.Alt, Keys.R);
            hook.RegisterHotKey(ModifKeys.Alt, Keys.A);
            hook.KeyPressed += HookOnKeyPressed;
        }

        private void HookOnKeyPressed(object? sender, KeyPressedEventArgs e)
        {
            if (e.Key == Keys.R)
                RunRecord();

            if (e.Key == Keys.A)
                RunPlayer();
        }

        private void RunRecord()
        {
            var startInfo = new ProcessStartInfo("ConsoleApp\\ITDImkClicker.ConsoleApp.exe ")
            {
                Arguments = "record -b S -bm Alt -o macros.bin",
                UseShellExecute = true,
                CreateNoWindow =  true,
                
                WindowStyle =  ProcessWindowStyle.Hidden
            };

            Process.Start(startInfo);
        }

        private void RunPlayer()
        {
            var startInfo = new ProcessStartInfo("ConsoleApp\\ITDImkClicker.ConsoleApp.exe ")
            {
                Arguments = "play -b S -bm Alt -i macros.bin",
                UseShellExecute = true,
                CreateNoWindow =  true,
                WindowStyle =  ProcessWindowStyle.Hidden
            };

            Process.Start(startInfo);
        }
    }
}