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
            InitializeComponent();
            this.DataContext = currentUnit;
            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));
            currentUnit.owner = host;
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
                currentUnit.typeArea =
                    AreaComboBox.SelectedIndex == 0 ? TypeAreaOfTheCountry.all :
                    AreaComboBox.SelectedIndex == 1 ? TypeAreaOfTheCountry.north :
                    AreaComboBox.SelectedIndex == 2 ? TypeAreaOfTheCountry.south :
                    AreaComboBox.SelectedIndex == 3 ? TypeAreaOfTheCountry.center :
                    TypeAreaOfTheCountry.jerusalem;
                currentUnit.typeOfUnit =
                    UnitComboBox.SelectedIndex == 0 ? TypeOfHostingUnit.all :
                    UnitComboBox.SelectedIndex == 1 ? TypeOfHostingUnit.zimmer :
                    UnitComboBox.SelectedIndex == 2 ? TypeOfHostingUnit.apartment :
                    UnitComboBox.SelectedIndex == 3 ? TypeOfHostingUnit.roomOfHotel :
                    TypeOfHostingUnit.tent;
                bl.updateHostingUnit(currentUnit);
                MessageBox.Show("your unit has been sucessfully updated");
                this.Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
    }
}
