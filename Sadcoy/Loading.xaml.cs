using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Sadcoy
{
    /// <summary>
    /// Interaktionslogik für Services.xaml
    /// </summary>
    public partial class Services : Window
    {
        public Services()
        {
            InitializeComponent();
        }

        private void ExitMethod(object sender, EventArgs e)
            => this.Close();

        private void date_Initialized(object sender, EventArgs e)
            => date.Text = DateTime.Now.ToString();

        private void nameofcomputer_Initialized(object sender, EventArgs e)
            => nameofcomputer.Text = System.Environment.MachineName;

        private void os_Initialized(object sender, EventArgs e)
            => os.Text = System.Environment.OSVersion.ToString();
        private void processors_Initialized(object sender, EventArgs e)
        {
            string processors1 = System.Environment.ProcessorCount.ToString();
            string othermessage = "Processors";

            processors.Text = processors1 + " " + othermessage;
        }

        private void version_Initialized(object sender, EventArgs e)
        {
            string othermessage = "Version 1.0.4";

            version.Text = othermessage;
        }

        private void disableservices_Click(object sender, RoutedEventArgs e)
        {
            string message = "\n" + "\n" + "- Fax" + "\n" +
                "- DPS" + "\n" +
                "- Spooler" + "\n" +
                "- WPDBusEnum" + "\n" +
                "- iphlpsvc" + "\n" +
                "- PcaSvc" + "\n" +
                "- RemoteRegistry" + "\n" +
                "- seclogon" + "\n" +
                "- lmhosts" + "\n" +
                "- TrkWks" + "\n" +
                "- WerSvc" + "\n" +
                "- W32Time" + "\n" +
                "- DiagTrack" + "\n" +
                "- TermService" + "\n" +
                "- lfsvc" + "\n" +
                "- EntAppSvc" + "\n" +
                "- RetailDemo" + "\n" +
                "- SecurityHealthService" + "\n" +
                "- wisvc" + "\n" +
                "- AJRouter" + "\n" +
                "- WpcMonSvc";

            if (MessageBox.Show("Sadcoy will stop the following services." +
                message, "Sadcoy", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Utilities.StopService("Fax");
                Utilities.StopService("DPS");
                Utilities.StopService("Spooler");
                Utilities.StopService("WPDBusEnum");
                Utilities.StopService("iphlpsvc");
                Utilities.StopService("PcaSvc");
                Utilities.StopService("RemoteRegistry");
                Utilities.StopService("seclogon");
                Utilities.StopService("lmhosts");
                Utilities.StopService("TrkWks");
                Utilities.StopService("WerSvc");
                Utilities.StopService("W32Time");
                Utilities.StopService("DiagTrack");
                Utilities.StopService("TermService");
                Utilities.StopService("lfsvc");
                Utilities.StopService("EntAppSvc");
                Utilities.StopService("RetailDemo");
                Utilities.StopService("SecurityHealthService");
                Utilities.StopService("wisvc");
                Utilities.StopService("AJRouter");
                Utilities.StopService("WpcMonSvc");
            }
            else
            {
                // nothing
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Utilities.StartService("Fax");
            Utilities.StartService("DPS");
            Utilities.StartService("Spooler");
            Utilities.StartService("WPDBusEnum");
            Utilities.StartService("iphlpsvc");
            Utilities.StartService("PcaSvc");
            Utilities.StartService("RemoteRegistry");
            Utilities.StartService("seclogon");
            Utilities.StartService("lmhosts");
            Utilities.StartService("TrkWks");
            Utilities.StartService("WerSvc");
            Utilities.StartService("W32Time");
            Utilities.StartService("DiagTrack");
            Utilities.StartService("TermService");
            Utilities.StartService("lfsvc");
            Utilities.StartService("EntAppSvc");
            Utilities.StartService("RetailDemo");
            Utilities.StartService("SecurityHealthService");
            Utilities.StartService("wisvc");
            Utilities.StartService("AJRouter");
            Utilities.StartService("WpcMonSvc");
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
