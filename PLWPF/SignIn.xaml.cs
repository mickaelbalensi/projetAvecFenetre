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
    /// Logique d'interaction pour SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public Host currentHost;
        public BankBranch currentBranch;
        IBL bl;
        public SignIn()
        {
            InitializeComponent();
            if (currentHost == null)
                currentHost = new Host();

            if (currentBranch == null)
                currentBranch = new BankBranch();
            this.DataContext = currentHost;
            bl = FactoryBL.getBL();
            idBox.Text = bl.getHostCount().ToString();
        }


        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            try {
                checkExceptions();

                currentBranch = bl.checkBanckBranch(int.Parse(bankCodeBox.Text), int.Parse(branchCodeBox.Text));
                if (currentBranch == null)
                    throw new Exception("this Bank doesn't exist");

                confirmBank confirmBank = new confirmBank(currentBranch);
                confirmBank.Show();
                MessageBoxResult result=
                MessageBox.Show("Do you confirm that it is your bank branch ?", "Confirmation Bank's Information",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes,
                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                confirmBank.Close();

                if (result== MessageBoxResult.No)
                    throw new Exception("Change again Bank's informations");

                currentHost.bankBranchDetails = currentBranch;
                //currentHost.hostKey = long.Parse(idBox.Text);
                currentHost.phoneNumber = long.Parse(pnoneNumberBox.Text);
                currentHost.password = passwordBox.Password;
                currentHost.bankAccountNumber = long.Parse(accountNumberBox.Text);
                currentHost.collectionClearance = collecionClearanceCheck.IsChecked == true ? true : false;

                bl.addHost(currentHost);
                throw new Exception("Welcome dear new Host !!");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.Message=="Welcome dear new Host !!") this.Close();
            }
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Window menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void checkExceptions()
        {
            string family = familyTextBox.Text;
            bool f1 = true;
            bool f2 = true;
            bool f3 = false;
            bool f4 = true;
            bool f5 = true;
            bool f6 = true;
            bool f7 = true;
            bool f8 = true;


            for (int i = 0; i < familyTextBox.Text.Count(); i++)
            {
                if (family[i] < 65 || (family[i] > 90 && family[i] < 96) || family[i] > 123)
                { familyerror.Visibility = Visibility.Visible; f1 = false; }
            }
            if (f1)
                familyerror.Visibility = Visibility.Hidden;
            
            for (int i = 0; i < privateTextBox.Text.Count(); i++)
            {
                if (privateTextBox.Text[i] < 65 || (privateTextBox.Text[i] > 90 && privateTextBox.Text[i] < 96) || privateTextBox.Text[i] > 123)
                { privateerror.Visibility = Visibility.Visible; f2 = false; }
            }
            if (f2)
                privateerror.Visibility = Visibility.Hidden;
            
            for (int i = 0; i < mailTextBox.Text.Count(); i++)
            {
                if (mailTextBox.Text[i] == '@')
                    f3 = true;
            }
            if (!f3)
                mailError.Visibility = Visibility.Visible; 
            else
                mailError.Visibility = Visibility.Hidden;


            if (pnoneNumberBox.Text.Count() < 9 || pnoneNumberBox.Text.Count() > 10)
                f4 = false;
            else
            for (int i = 0; i < pnoneNumberBox.Text.Count(); i++)
            {
                if (pnoneNumberBox.Text[i] < 48 || pnoneNumberBox.Text[i] > 57)
                {
                    phoneError.Visibility = Visibility.Visible;
                    f4 = false;
                }
            }
            if (f4)
                phoneError.Visibility = Visibility.Hidden;

            string password = passwordBox.Password;
            string confirmPassword= password2Box.Password;
            if (password.Length != confirmPassword.Length)
                f5 = false;
            else
                for (int i = 0; i < confirmPassword.Count(); i++)
                {
                    if (password[i] != confirmPassword[i])
                    {
                        passwordError.Visibility = Visibility.Visible;
                        f5 = false;
                    }
                }
            if (f5)
                passwordError.Visibility = Visibility.Hidden;


            for (int i = 0; i < bankCodeBox.Text.Count(); i++)
            {
                if (bankCodeBox.Text[i] < 48 || bankCodeBox.Text[i] > 57)
                {
                    bankCodeError.Visibility = Visibility.Visible;
                    f6 = false;
                }
            }
            if (f6)
                bankCodeError.Visibility = Visibility.Hidden;

            for (int i = 0; i < branchCodeBox.Text.Count(); i++)
            {
                if (branchCodeBox.Text[i] < 48 || branchCodeBox.Text[i] > 57)
                {
                    branchCodeError.Visibility = Visibility.Visible;
                    f7 = false;
                }
            }
            if (f7)
                branchCodeError.Visibility = Visibility.Hidden;


            for (int i = 0; i < accountNumberBox.Text.Count(); i++)
            {
                if (accountNumberBox.Text[i] < 48 || accountNumberBox.Text[i] > 57)
                {
                    accountNumberError.Visibility = Visibility.Visible;
                    f8 = false;
                }
            }
            if (f8)
                accountNumberError.Visibility = Visibility.Hidden;

            if (f1 && f2 && f3 && f4 && f5 && f6 && f7 && f8)
                family = "sup";
            else
                throw new Exception("please check your items and try again");

        }

    }
}
