
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE1;
using BL1;

namespace PLWPF
{
    /// <summary>
    /// Logique d'interaction pour AddRequestWindow.xaml
    /// </summary>
    public partial class AddRequestWindow : Window
    {
        public GuestRequest currentRequest { get; set; }
        IBL bl;
        

        public AddRequestWindow()
        {
            InitializeComponent();
            if (currentRequest == null)
                currentRequest = new GuestRequest {
                    guestRequestKey = Configuration.guestRequestCount + 1
                };
            firstGrid.DataContext = currentRequest;

          //  InitializeComponent();
           // currentRequest = new GuestRequest();
            bl = FactoryBL.getBL();

            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));

            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            //EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
           // EntryDateCalendar.SelectedDate = DateTime.Parse("01/01/2012");
            #region commentaire du calendar
            //myCalendar = CreateCalendar();
            //EntryDateCalendar.Child = null;
            //EntryDateCalendar.Child = myCalendar;
            #endregion
        }
        #region commentaire du calendar
        //private Calendar myCalendar;

        //private Calendar CreateCalendar()
        //{
        //    Calendar MonthlyCalendar = new Calendar();
        //    MonthlyCalendar.Name = "MonthlyCalendar";
        //    MonthlyCalendar.DisplayMode = CalendarMode.Month;
        //    MonthlyCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
        //    MonthlyCalendar.IsTodayHighlighted = true;
        //    return MonthlyCalendar;
        //}
        #endregion

        private void buttonRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //bool flag = false;
                //string word = (string)mailTextBox.Text;
                //string aro = "@";
                //for (int i=0; i < mailTextBox.Text.Count();i++)
                //{
                ////    if (word[i] == "@")
                //  //      flag = true;
                //}
                //if (!flag)
                //    throw new Exception("Your email isn't valid,please try again");
                currentRequest.registrationDate = DateTime.Now;
                currentRequest.jacuzzi = JacuzziCheckBox.IsChecked == true ? Options.yes :  JacuzziCheckBox.IsChecked == false ? Options.no : Options.optional;
                currentRequest.pool = PoolCheckBox.IsChecked == true ? Options.yes : PoolCheckBox.IsChecked == false ? Options.no : Options.optional;
                currentRequest.garden = GardenCheckBox.IsChecked == true ? Options.yes : GardenCheckBox.IsChecked == false ? Options.no : Options.optional;
                currentRequest.childrenAttractions = ChildrenAttractionsCheckBox.IsChecked == true ? Options.yes : ChildrenAttractionsCheckBox.IsChecked == false ? Options.no : Options.optional;
                bl.addRequest(currentRequest);
                currentRequest = new GuestRequest();
                DataContext = currentRequest;
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChildrenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonRequest_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
