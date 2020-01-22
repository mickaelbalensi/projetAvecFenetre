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
using BE1;
using BL1;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for PersonnalAccount.xaml
    /// </summary>
    public partial class PersonnalAccount : Window
    {
        public Host currentHost { get; set; }
        IBL bl;
        public PersonnalAccount()
        {
            InitializeComponent();
        }

        private void buttonPersonnal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Host host=bl.getHost()
                Window hostPage = new HostPage();
                hostPage.Show();
                this.Close();
            }
            catch ()
            {

            }
        }
    }
}
