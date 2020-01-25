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
    /// Interaction logic for DeleteUnit.xaml
    /// </summary>
    public partial class DeleteUnit : Window
    {
        public HostingUnit currentUnit;
        IBL bl;
        public DeleteUnit(Host host)
        {
            InitializeComponent();
            currentUnit = new HostingUnit();
            this.DataContext = currentUnit;
            bl = FactoryBL.getBL();
            UnitsDetails.ItemsSource = bl.getAllHostingUnit(x => x.owner.hostKey == host.hostKey);
        }

        private void DeleteUnits_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteHostingUnit(currentUnit);
                MessageBox.Show("Your unit has been sucessfully deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
