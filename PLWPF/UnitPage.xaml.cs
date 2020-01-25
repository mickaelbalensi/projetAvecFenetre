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
    /// Interaction logic for UnitPage.xaml
    /// </summary>
    public partial class UnitPage : Window
    {
        public Host currentHost { get; set; }
        public UnitPage(Host host)
        {
            InitializeComponent();
            currentHost = host;
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
            Window deleteUnit = new DeleteUnit(currentHost);
            deleteUnit.Show();
        }
    }
}
