
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
                currentRequest = new GuestRequest();
            InitializeComponent();
            this.DataContext = currentRequest;

            bl = FactoryBL.getBL();
            idBox.Text = bl.getGuestRequestCount().ToString();
            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));

            EntryDateCalendar.DisplayDateStart = DateTime.Now;

            ReleaseDateCalendar.DisplayDateStart = DateTime.Now;

        }

        private void buttonRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                checkExceptions();
                currentRequest.guestRequestKey = long.Parse(idBox.Text);
                currentRequest.registrationDate = DateTime.Now;
                currentRequest.jacuzzi = JacuzziCheckBox.IsChecked == true ? Options.yes : JacuzziCheckBox.IsChecked == false ? Options.optional : Options.no;
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
                currentRequest.entryDate = EntryDateCalendar.SelectedDate.Value;
                currentRequest.releaseDate = ReleaseDateCalendar.SelectedDate.Value;
                bl.addRequest(currentRequest);
                sendMail(currentRequest);
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

            if (f1 || f2 || f3 || f4 || f5) throw new Exception("please check your items and try again");

        }
        public void sendMail(GuestRequest request)
        {
            bool flag = false;
            foreach (HostingUnit unit in bl.getSuggestionList(currentRequest.guestRequestKey))
            {
                flag = true;
                string unitName = unit.hostingUnitName;
                
                MailMessage mail = new MailMessage();
                mail.To.Add(request.mailAddress);
                mail.From = new MailAddress("mickaelbalensi2652@gmail.com");
                mail.Subject = "mailSubject";
                if (unit.uris.Count != 0)
                    mail.Body = request.ToString() + "<img src=\"" + unit.tempUris[0] + "\"></img>";
                else
                    mail.Body = request.ToString();

                //+ "<img src=\"" + unit.tempUris[1] + "\"></img>"
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("bibasitshak@gmail.com", "Lui261004");
                smtp.EnableSsl = true;
            
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //*/
                //Outlook.Application App = new Outlook.Application();
                //Outlook.MailItem msg = (Outlook.MailItem)App.CreateItem(Outlook.OlItemType.olMailItem);

                ////msg.HTMLBody = ("<img src=\"" + unit.uris[0] + "\"></img>"/*+ unit.ToString()*/);
                //string description= unit.ToString();
                //msg.HTMLBody = ("<p><img src=\"http://www.voyagercacher.com/images/voyages-cacher/1389-11635.jpg\" alt=\"hotel\" width=\"400\" height=\"265\" /></p>   < p >"+ description+"</p>  ");
                //// msg.HTMLBody=unit.ToString() ;
                //msg.Subject = unit.hostingUnitName;
                //Outlook.Recipients recips = (Outlook.Recipients)msg.Recipients;
                //Outlook.Recipient recip = (Outlook.Recipient)recips.Add(request.mailAddress);
                //recip.Resolve();
                //msg.Send();
                //recips = null;
                //recip = null;
                //App = null;            
                currentRequest = new GuestRequest();
                DataContext = currentRequest;
                }
                if (!flag) throw new Exception("sorry there is no available room try others options");
                else
                {
                    MessageBox.Show("Your request has been registred, check your mail to look at your options");
                    this.Close();
                }
                throw new Exception("Your request has been registred, check your mail to look at your options");
            }

        }
    }

