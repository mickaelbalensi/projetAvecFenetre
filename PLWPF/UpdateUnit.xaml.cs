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
    /// Interaction logic for UpdateUnit.xaml
    /// </summary>
    public partial class UpdateUnit : Window
    {
        public HostingUnit currentUnit;
        IBL bl;
        public UpdateUnit(Host host)
        {
            InitializeComponent();
            if (currentUnit == null)
                currentUnit = new HostingUnit();
                {
                    //guestRequestKey = Configuration.guestRequestCount + 1
                };
            InitializeComponent();
            this.DataContext = currentUnit;
            //currentUnit.owner.hostKey = host.hostKey;
            //  InitializeComponent();
            // currentRequest = new GuestRequest();
            bl = FactoryBL.getBL();
            UnitsDetails.ItemsSource = bl.getAllHostingUnit(x => x.owner.hostKey == host.hostKey);
            UnitsDetails.Visibility = Visibility.Visible;
        }

        private void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {



        }

        private void UpdateUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateHostingUnit(currentUnit);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
