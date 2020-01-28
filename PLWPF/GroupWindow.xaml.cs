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
    /// Logique d'interaction pour GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        IBL bl;
        public GroupWindow()
        {
            InitializeComponent();
            bl = FactoryBL.getBL();
        }

        private void UnitByAreaList_Click(object sender, RoutedEventArgs e)
        {
            GroupRequestByAreaWindow unit = new GroupRequestByAreaWindow();
            unit.Source = bl.groupUnitByAreaList(true);
            unit.Background = new RadialGradientBrush(Colors.White, Colors.LightGray);
            this.page.Content = unit;

        }

        private void RequestByArea_Click(object sender, RoutedEventArgs e)
        {
            byArea request = new byArea();
            request.Source = bl.groupRequestByAreaList();
            request.Background = new RadialGradientBrush(Colors.White, Colors.LightGray);
            this.page.Content = request;

        }

        private void RequestByNumOfperson_Click(object sender, RoutedEventArgs e)
        {
            GroupRequestByNumPerson request = new GroupRequestByNumPerson();
            request.Source = bl.groupRequestByNumOfperson();
            request.Background = new RadialGradientBrush(Colors.White, Colors.LightGray);
            this.page.Content = request;
        }
    }
}
