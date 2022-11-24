using System;
using System.Collections.Generic;
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
            string othermessage = "Version 1.0.3";

            version.Text = othermessage;
        }
    }
}
