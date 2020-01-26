
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
            idBox.Text = bl.getGuestRequestCount().ToString();
            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));

            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            EntryDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            EntryDateCalendar.SelectedDate = DateTime.Parse("01/01/2012");
            ReleaseDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            ReleaseDateCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            ReleaseDateCalendar.SelectedDate = DateTime.Parse("01/01/2012");

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
                checkExceptions();




                currentRequest.guestRequestKey = long.Parse(idBox.Text);
                //currentRequest.status = GuestRequestStatus.active;
                currentRequest.registrationDate = DateTime.Now;
                currentRequest.jacuzzi = JacuzziCheckBox.IsChecked == true ? Options.yes :  JacuzziCheckBox.IsChecked == false ? Options.optional : Options.no;
                currentRequest.pool = PoolCheckBox.IsChecked == true ? Options.yes : PoolCheckBox.IsChecked == false ? Options.optional : Options.no;
                currentRequest.garden = GardenCheckBox.IsChecked == true ? Options.yes : GardenCheckBox.IsChecked == false ? Options.optional : Options.no;
                currentRequest.childrenAttractions = ChildrenAttractionsCheckBox.IsChecked == true ? Options.yes : ChildrenAttractionsCheckBox.IsChecked == false ? Options.optional : Options.no;
                currentRequest.typeArea =
                    AreaComboBox.SelectedIndex == 0 ? TypeAreaOfTheCountry.all :
                    AreaComboBox.SelectedIndex == 1 ? TypeAreaOfTheCountry.north :
                    AreaComboBox.SelectedIndex == 2 ? TypeAreaOfTheCountry.south :
                    AreaComboBox.SelectedIndex == 3 ? TypeAreaOfTheCountry.center :
                    TypeAreaOfTheCountry.jerusalem;
                currentRequest.type =
                    UnitComboBox.SelectedIndex == 0 ? TypeOfHostingUnit.all :
                    UnitComboBox.SelectedIndex == 1 ? TypeOfHostingUnit.zimmer :
                    UnitComboBox.SelectedIndex == 2 ? TypeOfHostingUnit.apartment :
                    UnitComboBox.SelectedIndex == 3 ? TypeOfHostingUnit.roomOfHotel :
                    TypeOfHostingUnit.tent;


                bl.addRequest(currentRequest);
                sendMail(currentRequest, bl.getSuggestionList(currentRequest.guestRequestKey));
                //Window suggestion = new SuggestionWindow(key);
                // this.Close();
            }
             catch (Exception ex)
            {              
               MessageBox.Show(ex.Message);
                

            }
        }
        private void checkExceptions()
        {
            string family = familyTextBox.Text;
            bool f1 = false;
            bool f2 = false;
            bool f3 = false;
            bool f4 = false;
            bool f5 = true;
            for (int i = 0; i < familyTextBox.Text.Count(); i++)
            {
                if (family[i] < 65 || (family[i] > 90 && family[i] < 96) || family[i] > 123)
                { familyerror.Visibility = Visibility.Visible; f1 = true; }
            }
            if (f1 == false)
                familyerror.Visibility = Visibility.Hidden;
            for (int i = 0; i < privateTextBox.Text.Count(); i++)
            {
                if (privateTextBox.Text[i] < 65 || (privateTextBox.Text[i] > 90 && privateTextBox.Text[i] < 96) || privateTextBox.Text[i] > 123)
                { privateerror.Visibility = Visibility.Visible; f2 = true; }
            }
            if (f2 == false)
                privateerror.Visibility = Visibility.Hidden;

            for (int i = 0; i < AdultsTextBox.Text.Count(); i++)
            {
                if (AdultsTextBox.Text[i] < 48 || AdultsTextBox.Text[i] > 57)
                { adultserror.Visibility = Visibility.Visible; f3 = true; }
            }
            if (f3 == false)
                adultserror.Visibility = Visibility.Hidden;
            for (int i = 0; i < ChildrenTextBox.Text.Count(); i++)
            {
                if (ChildrenTextBox.Text[i] < 48 || ChildrenTextBox.Text[i] > 57)
                { childrenerror.Visibility = Visibility.Visible; f3 = true; }
            }
            if (f4 == false)
                childrenerror.Visibility = Visibility.Hidden;
            for (int i = 0; i < mailTextBox.Text.Count(); i++)
            {
                if (mailTextBox.Text[i] == '@')
                    f5 = false;
            }
             if (f5)
                { mailError.Visibility = Visibility.Visible; }
            if (f5 == false)
                mailError.Visibility = Visibility.Hidden;

            if (f1||f2||f3||f4||f5) throw new Exception("please check your items and try again");
                
        }
        public void sendMail(GuestRequest request ,List<HostingUnit>suggestionList)
        {
            bool flag = false;
            foreach (HostingUnit unit in suggestionList)
            {
                flag = true;
                string unitName = unit.hostingUnitName;
                Outlook.Application App = new Outlook.Application();
                Outlook.MailItem msg = (Outlook.MailItem)App.CreateItem(Outlook.OlItemType.olMailItem);
                //msg.HTMLBody = ("<img src=\"" + unit.uris[0] + "\"></img>"/*+ unit.ToString()*/);
                //
                msg.HTMLBody = ("<p><img src=\"http://www.voyagercacher.com/images/voyages-cacher/1389-11635.jpg\" alt=\"hotel\" width=\"400\" height=\"265\" /></p>< p >< img src = \"https://r-cf.bstatic.com/images/hotel/max1024x768/122/122205831.jpg\" alt = \"\" width = \"400\" height = \"265\" /></ p >        < p > Decription de l'hotel :</p>            < p > Ths hotel is in...</ p >               < p > It contains... places of Adults and ...&nbsp;< span style = \"font-size: 0.9em;\" > places of </ span >< span style = \"font-size: 0.9em;\" > &nbsp; children </ span ></ p >                              < p > there are...</ p > ");
                // msg.HTMLBody=unit.ToString() ;
                msg.Subject = "Choose this room !!!!!";
                Outlook.Recipients recips = (Outlook.Recipients)msg.Recipients;
                Outlook.Recipient recip = (Outlook.Recipient)recips.Add(request.mailAddress);
                recip.Resolve();
                msg.Send();
                recips = null;
                recip = null;
                App = null;            
                currentRequest = new GuestRequest();
                DataContext = currentRequest;
            }
            if (!flag) throw new Exception("sorry there is no available room try others options");
            else
            {
                MessageBox.Show("Your request has been registred, check your mail to look at your options");
                this.Close();
            }
            //throw new Exception("Your request has been registred, check your mail to look at your options");
        }

    }
}
