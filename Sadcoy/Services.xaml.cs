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
            => this.Close();

        private void Date_Initialized(object sender, EventArgs e)
            => Date.Text = DateTime.Now.ToString();

        private void NameOfComputer_Initialized(object sender, EventArgs e)
            => NameOfComputer.Text = System.Environment.MachineName;

        private void Os_Initialized(object sender, EventArgs e)
            => Os.Text = System.Environment.OSVersion.ToString();

        private void Processors_Initialized(object sender, EventArgs e)
        {
            string processorCount = System.Environment.ProcessorCount.ToString();
            string otherMessage = "Processors";

            Processors.Text = $"{processorCount} {otherMessage}";
        }

        private void Version_Initialized(object sender, EventArgs e)
        {
            string versionNumber = "1.0.6";

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
            if (freed > 1073741824) // 1 GB = 1073741824 Bytes
            {
                double freedGB = (double)freed / 1073741824;
                message = string.Format("Cache vom RAM wurde um {0:0.00} GB freigegeben.", freedGB);
            }
            else if (freed > 1048576) // 1 MB = 1048576 Bytes
            {
                double freedMB = (double)freed / 1048576;
                message = string.Format("Cache vom RAM wurde um {0:0.00} MB freigegeben.", freedMB);
            }
            else
            {
                message = string.Format("Cache vom RAM wurde um {0} Bytes freigegeben.", freed);
            }

            MessageBox.Show(message, "Cache vom RAM freigegeben", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        static void ClearCache()
        {
            GC.Collect();
        }
    }
}
