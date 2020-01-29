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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        public Order currentOrder { get; set; }
        public HostingUnit currentUnit;
        public List<Order> ordersLists;
        public Host currentHost = new Host();
        IBL bl;
        public UpdateOrder(Host host)
        {
            InitializeComponent();
            bl = FactoryBL.getBL();
            ordersLists = new List<Order>();
            if (currentOrder == null)
                currentOrder = new Order();

            DataContext = currentOrder;
            foreach (HostingUnit unit in bl.getAllHostingUnit((x => x.owner.hostKey == host.hostKey)))
            {
                foreach (Order orders in bl.getAllOrder(x => x.hostingUnitKey == unit.hostingUnitKey))
                {
                    ordersLists.Add(orders);
                }
            }           
            OrderDetails.ItemsSource = bl.getAllOrder();
            currentHost = host;
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
           // try
            {
                int currentKey =int.Parse(unitKeyBox.Text);
                currentOrder = bl.getOrder(currentKey);
                bl.updateOrder(currentOrder,currentHost);
                MessageBox.Show("Your order has been updated you have know a new client in your unit");
                this.Close();
            }
            //catch(Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        //    .ItemsSource = App.bl.getAllOrder(x => x != null);
        //PersonDetails.Visibility = Visibility.Visible;
        //ContractDetails.Visibility = Visibility.Hidden;
        //ContractDetails.SelectedItem = null;
    }
}
