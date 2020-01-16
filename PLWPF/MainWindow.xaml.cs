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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Window addRequestWindow = new AddRequestWindow();
            addRequestWindow.Show();
        }



        private void addUnitButton_Click(object sender, RoutedEventArgs e)
        {
            Window addUnitWindow = new UnitPage();
            addUnitWindow.Show();              
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Window  orderPage = new OrderPage();
            orderPage.Show();
        }

        private void ConsultData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
