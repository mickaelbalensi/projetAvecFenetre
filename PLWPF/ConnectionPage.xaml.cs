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
    /// Logique d'interaction pour ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : Window
    {
        public Host currentHost { get; set; }
        public ConnectionPage()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //il faut verifier que le password et le mailaddress existent
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            Window personnalAccount = new PersonnalAccount();
            personnalAccount.Show();
            this.Close();
        }

        private void SignUP_Click(object sender, RoutedEventArgs e)
        {
            Window subscriptionAsHost = new SubscriptionAsHost();
            subscriptionAsHost.Show();
            this.Close();
        }
    }
}
