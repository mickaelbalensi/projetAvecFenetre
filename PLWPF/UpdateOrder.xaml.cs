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
        IBL bl;
        public UpdateOrder(Host host)
        {
            InitializeComponent();
            foreach( HostingUnit unit in  bl.getAllHostingUnit((x=> x.owner.hostKey == host.hostKey)))
            {
                foreach( Order orders in bl.getAllOrder(x => x.hostingUnitKey == unit.hostingUnitKey))
                {
                    ordersLists.Add(orders);
                }
            }
            OrderDetails.ItemsSource = ordersLists;


          //OrderDetails.ItemsSource=bl.getAllOrder(currentOrder.hostingUnitKey==)
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {         
                
                bl.updateOrder(currentOrder);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //    .ItemsSource = App.bl.getAllOrder(x => x != null);
        //PersonDetails.Visibility = Visibility.Visible;
        //ContractDetails.Visibility = Visibility.Hidden;
        //ContractDetails.SelectedItem = null;
    }
}
