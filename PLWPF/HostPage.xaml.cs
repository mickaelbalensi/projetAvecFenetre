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
            AddUnit.Visibility = Visibility.Visible;
            UpdateUnit.Visibility = Visibility.Visible;
            DeleteUnit.Visibility = Visibility.Visible;
            butonAddUnit.Visibility = Visibility.Hidden;

            //Window unitPage = new UnitPage(currentHost);
            //unitPage.Show();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            AddOrder.Visibility = Visibility.Visible;
            UpdateOrder.Visibility = Visibility.Visible;
            Order.Visibility = Visibility.Hidden;
            //Window orderPage = new OrderPage();
            //orderPage.Show();
        }


        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            Window addUnitWindow = new AddUnitWindow(currentHost);

            addUnitWindow.Show();
        }

        private void UpdateUnit_Click(object sender, RoutedEventArgs e)
        {
            Window updateUnit = new UpdateUnit(currentHost);
            updateUnit.Show();

        }

        private void DeleteUnit_Click(object sender, RoutedEventArgs e)
        {
            Window deleteUnit = new DeleteUnit();
            deleteUnit.Show();

        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            Window addOrder = new AddOrderWindow();
            addOrder.Show();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();

        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            Window updateOrder = new UpdateOrder(currentHost);
            updateOrder.Show();
        }
    }
}
