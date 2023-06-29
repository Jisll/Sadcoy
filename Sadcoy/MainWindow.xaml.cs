using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace Sadcoy
{
    public partial class MainWindow : Window
    {
        private string logFilePath = "Log.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RunSw_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to optimize your system now?", "Sadcoy", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Optimizing started!");

                string tempPath = Path.GetTempPath();
                var dir = new DirectoryInfo(tempPath);

                if (!dir.Exists)
                {
                    MessageBox.Show("Can't find Temp folder");
                    return;
                }

                ProgressBar.Visibility = Visibility.Visible;
                ProgressBar.Value = 0;
                ProgressTextBlock.Text = "Optimizing system...";
                await Task.Run(() => OptimizeSystem());

                ProgressTextBlock.Dispatcher.Invoke(() =>
                {
                    ProgressTextBlock.Text = "Clearing temporary folder...";
                });

                await Task.Run(() => ClearTempFolder(dir));

                if (MessageBox.Show("You need to restart the computer to apply the effects. Do you want to restart now?", "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    ProgressTextBlock.Dispatcher.Invoke(() =>
                    {
                        ProgressTextBlock.Text = "Restarting computer...";
                    });
                    await Task.Run(() => RestartComputer());
                }
                else
                {
                    MessageBox.Show("Restart the computer later to apply the effects!");
                }

                ProgressBar.Dispatcher.Invoke(() =>
                {
                    ProgressBar.Visibility = Visibility.Hidden;
                });
                ProgressTextBlock.Dispatcher.Invoke(() =>
                {
                    ProgressTextBlock.Text = "";
                });
                CreateLogFile();
            }
        }
        private void CreateLogFile()
        {
            string logContent = $"Optimization completed: {DateTime.Now}\n";
            logContent += $"- Cleared temporary folder\n";
            logContent += $"- System optimized\n";
            logContent += $"- Restarted computer\n";

            try
            {
                File.WriteAllText(logFilePath, logContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating log file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearTempFolder(DirectoryInfo dir)
        {
            double totalItems = dir.GetDirectories().Length + dir.GetFiles().Length;
            double progress = 0;

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                try
                {
                    subDir.Delete(true);
                }
                catch (Exception ex)
                {
                    LogError("Error deleting subdirectory: " + ex.Message);
                }

                progress++;
                UpdateProgressBar(progress / totalItems, $"Deleting subdirectories... {progress}/{totalItems}");
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception ex)
                {
                    LogError("Error deleting file: " + ex.Message);
                }

                progress++;
                UpdateProgressBar(progress / totalItems, $"Deleting files... {progress}/{totalItems}");
            }
        }

        private void OptimizeSystem()
        {
            try
            {
                Optimize.Optimizing();
            }
            catch (Exception ex)
            {
                LogError("Error optimizing system: " + ex.Message);
            }

            UpdateProgressBar(1, "Optimization complete!");
        }

        private void RestartComputer()
        {
            try
            {
                Process.Start("shutdown.exe", "-r -t 00");
            }
            catch (Exception ex)
            {
                LogError("Error restarting computer: " + ex.Message);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SupportMe_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Jisll/Sadcoy");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Optimize.ConvertToNormal();
            if (MessageBox.Show("You need to restart the computer to convert the effects. Do you want to restart now?", "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                RestartComputer();
            }
        }

        private void services_Click(object sender, RoutedEventArgs e)
        {
            Services sv = new Services();
            sv.Show();
        }

        private void LogError(string errorMessage)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("ErrorLog.txt", true))
                {
                    writer.WriteLine(errorMessage + " " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to error log: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateProgressBar(double progress, string progressText)
        {
            progress = Math.Max(0, Math.Min(progress * 100, 100));
            ProgressBar.Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = progress;
            });
            ProgressTextBlock.Dispatcher.Invoke(() =>
            {
                ProgressTextBlock.Text = progressText;
            });
        }
    }
}
