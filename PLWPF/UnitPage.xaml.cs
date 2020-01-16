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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UnitPage.xaml
    /// </summary>
    public partial class UnitPage : Window
    {
        public UnitPage()
        {
            InitializeComponent();
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            Window addUnit = new AddUnitWindow();
            addUnit.Show();
        }

        private void UpdateUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteUnit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
