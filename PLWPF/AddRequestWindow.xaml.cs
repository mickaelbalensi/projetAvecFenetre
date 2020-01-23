
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
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net;
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
                currentRequest = new GuestRequest {
                    //guestRequestKey = Configuration.guestRequestCount + 1
                };
            InitializeComponent();
            this.DataContext = currentRequest;

          //  InitializeComponent();
           // currentRequest = new GuestRequest();
            bl = FactoryBL.getBL();

            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));
            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            EntryDateCalendar.SelectedDate = DateTime.Parse("01/01/2012");
            ReleaseDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            ReleaseDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            ReleaseDateCalendar.SelectedDate = DateTime.Parse("01/01/2012");
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
            //try
            {
                checkExceptions();
                currentRequest.status = GuestRequestStatus.active;
                currentRequest.registrationDate = DateTime.Now;
                currentRequest.jacuzzi = JacuzziCheckBox.IsChecked == true ? Options.yes :  JacuzziCheckBox.IsChecked == false ? Options.no : Options.optional;
                currentRequest.pool = PoolCheckBox.IsChecked == true ? Options.yes : PoolCheckBox.IsChecked == false ? Options.no : Options.optional;
                currentRequest.garden = GardenCheckBox.IsChecked == true ? Options.yes : GardenCheckBox.IsChecked == false ? Options.no : Options.optional;
                currentRequest.childrenAttractions = ChildrenAttractionsCheckBox.IsChecked == true ? Options.yes : ChildrenAttractionsCheckBox.IsChecked == false ? Options.no : Options.optional;
                
                
                bl.addRequest(currentRequest);
                sendMail(currentRequest.mailAddress, bl.getSuggestionList(currentRequest.guestRequestKey));
                //Window suggestion = new SuggestionWindow(key);
                // this.Close();
            }
             //catch (Exception ex)
            {              
               // MessageBox.Show(ex.Message);
                

            }
        }
        private void checkExceptions()
        {
            string family = familyTextBox.Text;
            bool flag1 = false;
            for (int i = 0; i < familyTextBox.Text.Count(); i++)
            {
                if (family[i] < 65 || (family[i] > 90 && family[i] < 96) || family[i] > 123)
                { familyerror.Visibility = Visibility.Visible; flag1 = true; }
            }
            for (int i = 0; i < privateTextBox.Text.Count(); i++)
            {
                if (privateTextBox.Text[i] < 65 || (privateTextBox.Text[i] > 90 && privateTextBox.Text[i] < 96) || privateTextBox.Text[i] > 123)
                { privateerror.Visibility = Visibility.Visible; flag1 = true; }

            }
            for (int i = 0; i < AdultsTextBox.Text.Count(); i++)
            {
                if (AdultsTextBox.Text[i] < 48 || AdultsTextBox.Text[i] > 57)
                { adultserror.Visibility = Visibility.Visible; flag1 = true; }
            }
            for (int i = 0; i < ChildrenTextBox.Text.Count(); i++)
            {
                if (ChildrenTextBox.Text[i] < 48 || ChildrenTextBox.Text[i] > 57)
                { childrenerror.Visibility = Visibility.Visible; flag1 = true; }
            }
            bool flag = false;
            for (int i = 0; i < mailTextBox.Text.Count(); i++)
            {
                if (mailTextBox.Text[i] == '@')
                    flag = true;
            }
                if (!flag)
                { mailError.Visibility = Visibility.Visible; flag1 = true; }
                
            if (flag1) throw new Exception("please check your items and try again");
                
        }
        public void sendMail(string mail,List<HostingUnit>suggestionList)
        {
            int count = 0;
            foreach (HostingUnit unit in suggestionList)
            {
                count++;
                string unitName = unit.hostingUnitName;
                Outlook.Application App = new Outlook.Application();
                Outlook.MailItem msg = (Outlook.MailItem)App.CreateItem(Outlook.OlItemType.olMailItem);

                msg.HTMLBody = unitName;

                msg.Subject = "Choose this room !!!!!";
                Outlook.Recipients recips = (Outlook.Recipients)msg.Recipients;
                Outlook.Recipient recip = (Outlook.Recipient)recips.Add("bibasitshak@gmail.com");
                recip.Resolve();
                msg.Send();
                recips = null;
                recip = null;
                App = null;
                MessageBox.Show("Your request has been registred, check your mail to look at your options");
                this.Close();
                currentRequest = new GuestRequest();
                DataContext = currentRequest;
            }
            if (count == 0) throw new Exception("sorry there is no available room try others options");

            //throw new Exception("Your request has been registred, check your mail to look at your options");
        }

    }
}
