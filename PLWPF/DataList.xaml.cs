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
    /// Interaction logic for DataList.xaml
    /// </summary>
    public partial class DataList : Window
    {
        IBL bl;
        public DataList()
        {
            InitializeComponent();
            bl = FactoryBL.getBL();
        }

        private void RequestList_Click(object sender, RoutedEventArgs e)
        {
            requestDetails.ItemsSource = bl.getAllGuestRequest();
            requestDetails.Visibility = Visibility.Visible;
            unitDetails.Visibility = Visibility.Hidden;
            orderDetails.Visibility = Visibility.Hidden;
        }

        private void UnitList_Click(object sender, RoutedEventArgs e)
        {
            unitDetails.ItemsSource = bl.getAllHostingUnit();
            unitDetails.Visibility = Visibility.Visible;
            orderDetails.Visibility = Visibility.Hidden;
            requestDetails.Visibility = Visibility.Hidden;
        }

        private void OrdersList_Click(object sender, RoutedEventArgs e)
        {
            orderDetails.ItemsSource = bl.getAllOrder();
            orderDetails.Visibility = Visibility.Visible;
            unitDetails.Visibility = Visibility.Hidden;
            requestDetails.Visibility = Visibility.Hidden;
        }

        private void MoreOptions_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
