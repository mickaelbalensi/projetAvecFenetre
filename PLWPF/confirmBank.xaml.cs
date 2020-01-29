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
    /// Logique d'interaction pour confirmBank.xaml
    /// </summary>
    public partial class confirmBank : Window
    {
        public BankBranch currentBranch;
        public confirmBank(BankBranch branch)
        {
            InitializeComponent();
            if (currentBranch == null)
            {
                currentBranch = new BankBranch();
                currentBranch = branch;
            }
            this.DataContext = currentBranch;

        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Change again Bank's informations");
            this.Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        


    }
}
