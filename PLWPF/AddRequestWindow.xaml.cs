
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
            if (currentRequest == null)
                currentRequest = new GuestRequest();
            DataContext = currentRequest;

            InitializeComponent();
            currentRequest = new GuestRequest();
            bl = FactoryBL.getBL();

            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));

            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            EntryDateCalendar.SelectedDate = DateTime.Parse("01/01/2012");
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
            //try
            {
                currentRequest.registrationDate = DateTime.Now;
                currentRequest.jacuzzi = JacuzziCheckBox.IsThreeState ? Options.optional : JacuzziCheckBox.IsChecked == true ? Options.yes : Options.no;
                currentRequest.pool = PoolCheckBox.IsThreeState ? Options.optional : PoolCheckBox.IsChecked == true ? Options.yes : Options.no;
                currentRequest.garden = GardenCheckBox.IsThreeState ? Options.optional : GardenCheckBox.IsChecked == true ? Options.yes : Options.no;
                currentRequest.childrenAttractions = ChildrenAttractionsCheckBox.IsThreeState ? Options.optional : ChildrenAttractionsCheckBox.IsChecked == true ? Options.yes : Options.no;
            }
            // catch ()
            {

            }
        }
    }
}
