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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Window
    {
        public Host host;
        public OrderPage()
        {
            InitializeComponent();

        }

        private void UpdateRequest_Click(object sender, RoutedEventArgs e)
        {
            Window updateOrder = new UpdateOrder(host);
            updateOrder.Show();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            Window addOrder = new AddOrderWindow();
            addOrder.Show();
        }

    }
}
