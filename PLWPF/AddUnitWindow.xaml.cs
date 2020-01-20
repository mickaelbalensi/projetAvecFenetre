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
            InitializeComponent();
            currentUnit = new HostingUnit();
            this.details.DataContext = currentUnit;

            bl = FactoryBL.getBL();

            this.BankComboBox.ItemsSource = Enum.GetValues(typeof(BE1.Bank));
        }

        private void butoun_addUnit(object sender, RoutedEventArgs e)

        {
            try
            {
                checkExceptions();
                currentUnit.jacuzzi =  JacuzziCheckBox.IsChecked == true ? true : false;
                currentUnit.pool =  PoolCheckBox.IsChecked == true ? true : false;
                currentUnit.garden =  GardenCheckBox.IsChecked == true ? true : false;
                currentUnit.childrenAttractions =  ChildrenAttractionCheckBox.IsChecked == true ? true :false;
                bl.addHostingUnit(currentUnit);
                currentUnit = new HostingUnit();
                this.details.DataContext = currentUnit;
            }
            catch(Exception ex)
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
            for (int i = 0; i < nameHotel.Text.Count(); i++)
            {
                if (nameHotel.Text[i] < 65 || (nameHotel.Text[i] > 90 && nameHotel.Text[i] < 96) || nameHotel.Text[i] > 123)
                    throw new Exception("Your name hotel  isn't valid");
            }
            for (int i = 0; i < numChildren.Text.Count(); i++)
            {
                if (numChildren.Text[i] < 48 || numChildren.Text[i] > 57)
                    throw new Exception("Your number of children must be a number !");
            }
            for (int i = 0; i < numAdults.Text.Count(); i++)
            {
                if (numAdults.Text[i] < 48 || numAdults.Text[i] > 57)
                    throw new Exception("Your number of adults must be a number !");
            }
            bool flag = false;
            for (int i = 0; i < mailAddress.Text.Count(); i++)
            {
                if (mailAddress.Text[i] == '@')
                    flag = true;
            }
            if (!flag)
                throw new Exception("Your mail address isn't valid");

        }

    }
}
