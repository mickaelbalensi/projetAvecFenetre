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
    /// Logique d'interaction pour SubscriptionAsHost.xaml
    /// </summary>
    public partial class SubscriptionAsHost : Window
    {
        public Host currentHost;
        IBL bl;
        public SubscriptionAsHost()
        {
            InitializeComponent();
            currentHost = new Host();
            this.details.DataContext = currentHost;

            bl = FactoryBL.getBL();


        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)

        {
            try
            {
                checkExceptions();
                
                bl.addHost(currentHost);
                currentHost = new Host();
                this.details.DataContext = currentHost;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         private void checkExceptions()
         {
             string family = familyName.Text;
             for (int i = 0; i < familyName.Text.Count(); i++)
             {
                 if (family[i] < 65 || (family[i] > 90 && family[i] < 96) || family[i] > 123)
                     throw new Exception("Your family name isn't valid");
             }
             for (int i = 0; i < phoneNumber.Text.Count(); i++)
             {
                 if (phoneNumber.Text[i]<48 || phoneNumber.Text[i] > 57)
                     throw new Exception("Your phone number isn't valid");
             }
             for (int i = 0; i < hostKey.Text.Count(); i++)
             {
                 if (hostKey.Text[i] < 48 || hostKey.Text[i] > 57)
                     throw new Exception("Your ID isn't valid");
             }
             for (int i = 0; i < numBranch.Text.Count(); i++)
             {
                 if (numBranch.Text[i] < 48 || numBranch.Text[i] > 57)
                     throw new Exception("Your num branch isn't valid");
             }
             for (int i = 0; i < numBankAccount.Text.Count(); i++)
             {
                 if (numBankAccount.Text[i] < 48 || numBankAccount.Text[i] > 57)
                     throw new Exception("Your num bank account isn't valid");
             }
             bool flag = false;
             for (int i = 0; i < mailAddress.Text.Count(); i++)
             {
                 if (mailAddress.Text[i] == '@')
                     flag = true;
             }
             if (!flag)
                 throw new Exception("Your mail address isn't valid");
            if (passwordBox.Text != checkPasswordBox.Text)
                throw new Exception("You didn't enter the well password");
         }

        private void BankComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
