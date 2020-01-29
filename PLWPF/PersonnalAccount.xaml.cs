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
    /// Interaction logic for PersonnalAccount.xaml
    /// </summary>
    public partial class PersonnalAccount : Window
    {
        public Host currentHost { get; set; }
        IBL bl;
        public PersonnalAccount()
        {
            if (currentHost == null)
                currentHost = new Host();
            InitializeComponent();
            //this.DataContext = currentHost;

            bl = FactoryBL.getBL();
        }

        private void buttonPersonnal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long id = long.Parse(hostKeyBox.Text);
                string pwd = Password.Password;
                currentHost =bl.checkParameters(id,pwd);
                Window hostPage = new HostPage(currentHost);
                hostPage.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Window menu = new MainWindow();
            menu.Show();
            this.Close();
        }
    }
}
