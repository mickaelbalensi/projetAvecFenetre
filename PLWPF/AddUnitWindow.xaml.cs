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
    /// Logique d'interaction pour AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {
        public HostingUnit currentUnit;
        IBL bl;

        public AddUnitWindow()
        {
            
            currentUnit = new HostingUnit();
            InitializeComponent();
            this.DataContext = currentUnit;
           
            bl = FactoryBL.getBL();
            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));

        }

        private void butoun_addUnit(object sender, RoutedEventArgs e)

        {
            try
            {
                //checkExceptions();
                //currentUnit.jacuzzi =  JacuzziCheckBox.IsChecked == true ? true : false;
                //currentUnit.pool =  PoolCheckBox.IsChecked == true ? true : false;
                //currentUnit.garden =  GardenCheckBox.IsChecked == true ? true : false;
                //currentUnit.childrenAttractions =  ChildrenAttractionCheckBox.IsChecked == true ? true :false;
                bl.addHostingUnit(currentUnit);
                currentUnit = new HostingUnit();
                this.DataContext = currentUnit;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdultsPlacesBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
