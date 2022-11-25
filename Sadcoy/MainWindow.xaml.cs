using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sadcoy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunSw_Click(object sender, RoutedEventArgs e)
        {
            Optimize.Optimizing();
            string TempPath = System.IO.Path.GetTempPath();
            var Dir = new DirectoryInfo(TempPath);

            if (!Dir.Exists)
            {
                MessageBox.Show("Can't Find Temp Folder");
                return;
            }

            foreach (DirectoryInfo dir in Dir.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch (Exception)
                {

                }
            }

            foreach (FileInfo filez in Dir.GetFiles())
            {
                try
                {
                    filez.Delete();
                }
                catch (Exception)
                {

                }
            }
            if (MessageBox.Show("You need to restart the computer to apply the effects, do you want to restart now?", "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Process.Start("shutdown.exe", "-r -t 00");
            }
            else
            {
                // nothing
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
            => Environment.Exit(0);

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
            => this.DragMove();

        private void SupportMe_Click(object sender, RoutedEventArgs e)
            => System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Jisll/Sadcoy",
                UseShellExecute = true
            });

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Optimize.ConvertToNormal();
            if (MessageBox.Show("You need to restart the computer to convert the effects, do you want to restart now?", "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Process.Start("shutdown.exe", "-r -t 00");
            }
            else
            {
                // nothing
            }
        }

        private void nameofcomputer_Initialized(object sender, EventArgs e)
            => nameofcomputer.Text = (System.Environment.MachineName);

        private void services_Click(object sender, RoutedEventArgs e)
        {
            Services sv = new Services();
            sv.Show();
        }
    }
}
