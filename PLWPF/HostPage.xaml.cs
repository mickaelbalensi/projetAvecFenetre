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


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostPage.xaml
    /// </summary>
    public partial class HostPage : Window
    {
        public Host currentHost { get; set; }
        public HostPage(Host host)
        {
            InitializeComponent();
            currentHost = host;
        }

        private void butonAddUnit_Click(object sender, RoutedEventArgs e)
        {
            Window unitPage = new UnitPage(currentHost);
            unitPage.Show();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Window orderPage = new OrderPage();
            orderPage.Show();
        }

        private void butonDeleteUnit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
