using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            MessageBox.Show("Optimizing started!");
            Optimize.Optimizing();
            string tempPath = System.IO.Path.GetTempPath();
            var dir = new DirectoryInfo(tempPath);

            if (!dir.Exists)
            {
                MessageBox.Show("Can't Find Temp Folder");
                return;
            }

            ClearTempFolder(dir);

            if (MessageBox.Show("You need to restart the computer to apply the effects, do you want to restart now?", "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Process.Start("shutdown.exe", "-r -t 00");
            }
        }

        private void ClearTempFolder(DirectoryInfo dir)
        {
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                try
                {
                    subDir.Delete(true);
                }
                catch (Exception)
                {
                    // Ignore
                }
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SupportMe_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Jisll/Sadcoy");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Optimize.ConvertToNormal();
            if (MessageBox.Show("You need to restart the computer to convert the effects, do you want to restart now?", "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Process.Start("shutdown.exe", "-r -t 00");
            }
        }

        private void services_Click(object sender, RoutedEventArgs e)
        {
            Services sv = new Services();
            sv.Show();
        }
    }
}
