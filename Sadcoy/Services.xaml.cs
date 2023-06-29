using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sadcoy
{
    /// <summary>
    /// Interaktionslogik für Services.xaml
    /// </summary>
    public partial class Services : Window
    {
        private readonly List<string> _serviceNames = new List<string>
        {
            "Fax",
            "DPS",
            "Spooler",
            "WPDBusEnum",
            "iphlpsvc",
            "PcaSvc",
            "RemoteRegistry",
            "seclogon",
            "lmhosts",
            "TrkWks",
            "WerSvc",
            "W32Time",
            "DiagTrack",
            "TermService",
            "lfsvc",
            "EntAppSvc",
            "RetailDemo",
            "SecurityHealthService",
            "wisvc",
            "AJRouter",
            "WpcMonSvc"
        };

        public Services()
        {
            InitializeComponent();
        }

        private void ExitMethod(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Date_Initialized(object sender, EventArgs e)
        {
            Date.Text = DateTime.Now.ToString();
        }

        private void NameOfComputer_Initialized(object sender, EventArgs e)
        {
            NameOfComputer.Text = Environment.MachineName;
        }

        private void Os_Initialized(object sender, EventArgs e)
        {
            Os.Text = Environment.OSVersion.ToString();
        }

        private void Processors_Initialized(object sender, EventArgs e)
        {
            string processorCount = Environment.ProcessorCount.ToString();
            string otherMessage = "Processors";
            Processors.Text = $"{processorCount} {otherMessage}";
        }

        private void Version_Initialized(object sender, EventArgs e)
        {
            string versionNumber = "1.0.7";
            Version1.Text = versionNumber;
        }

        private void DisableServices_Click(object sender, RoutedEventArgs e)
        {
            string message = "\n" + "\n" + string.Join("\n", _serviceNames.Select(name => $"- {name}")) + "\n";
            if (MessageBox.Show("Sadcoy will stop the following services." + message, "Sadcoy",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                foreach (string serviceName in _serviceNames)
                {
                    Utilities.StopService(serviceName);
                }
            }
        }

        private void EnableServices_Click(object sender, RoutedEventArgs e)
        {
            string message = "\n" + "\n" + string.Join("\n", _serviceNames.Select(name => $"- {name}")) + "\n";
            if (MessageBox.Show("Sadcoy will start the following services." + message, "Sadcoy",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                foreach (string serviceName in _serviceNames)
                {
                    Utilities.StartService(serviceName);
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            long before = GC.GetTotalMemory(false);
            ClearCache();
            long after = GC.GetTotalMemory(true);
            long freed = before - after;
            string message = "";
            if (freed > 1073741824)
            {
                double freedGB = (double)freed / 1073741824;
                message = string.Format("Cache from RAM has been freed up by {0:0.00} GB.", freedGB);
            }
            else if (freed > 1048576)
            {
                double freedMB = (double)freed / 1048576;
                message = string.Format("Cache from RAM has been freed up by {0:0.00} MB.", freedMB);
            }
            else
            {
                message = string.Format("Cache from RAM has been freed up by {0} Bytes.", freed);
            }

            MessageBox.Show(message, "Cache Cleared", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static void ClearCache()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }

    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        internal static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);
    }
}
