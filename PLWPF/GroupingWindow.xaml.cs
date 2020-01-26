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
    /// Logique d'interaction pour GroupingWindow.xaml
    /// </summary>
    public partial class GroupingWindow : Window
    {
        public HostingUnit currentUnit=new HostingUnit();
        public Host currentHost=new Host();
        public GuestRequest currentRequest=new GuestRequest();
        BL_imp bl;
        public GroupingWindow()
        {
            InitializeComponent();
            bl =new BL_imp();
        }


        private void requestBynumbers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                requestDetails.ItemsSource = bl.getAllGuestRequest(x => x.adults + x.children < int.Parse(personBox.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void requestByArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                requestDetails.ItemsSource = bl.getAllGuestRequest(x => x.adults + x.children < int.Parse(personBox.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hostByUnits_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                requestDetails.ItemsSource = bl.getAllGuestRequest(x => x.adults + x.children < int.Parse(personBox.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UnitByArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                requestDetails.ItemsSource = bl.getAllGuestRequest(x => x.adults + x.children < int.Parse(personBox.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}