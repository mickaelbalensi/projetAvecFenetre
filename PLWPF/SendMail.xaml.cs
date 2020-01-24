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
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net;
using System.Net.Mime;
using BE1;
using BL1;

namespace PLWPF
{
    /// <summary>
    /// Logique d'interaction pour SendMail.xaml
    /// </summary>
    public partial class SendMail : Window
    {
        public HostingUnit currentUnit;
        public SendMail(HostingUnit unit)
        {
            currentUnit = new HostingUnit();
            InitializeComponent();
        }

        private void Sendmail_Click(object sender, RoutedEventArgs e)
        {
            Outlook.Application App = new Outlook.Application();
            Outlook.MailItem msg = (Outlook.MailItem)App.CreateItem(Outlook.OlItemType.olMailItem);
            msg.HTMLBody = ("<img src=\"" + currentUnit.uris[0] +"\"></img>");
            //msg.HTMLBbody = namehotel;
            msg.Subject = "sujet";
            Outlook.Recipients recips = (Outlook.Recipients)msg.Recipients;
            Outlook.Recipient recip = (Outlook.Recipient)recips.Add("bibasitshak@gmail.com");
            recip.Resolve();
            msg.Send();
            recips = null;
            recip = null;
            App = null;
            MessageBox.Show("regarde ton mail");


            //MailMessage mm = new MailMessage();
            //mm.From = new MailAddress("bibasitshak@gmail.com");
            //mm.To.Add("bibasitshak@gmail.com");
            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("https://www.sortiraparis.com/images/80/86252/453698-la-tour-eiffel-fete-ses-130-ans-13.jpg", null, "text/html");

            //LinkedResource image = new LinkedResource(@"C:\Users\bibas\Documents\csharp\imagesprojet\hotelpiscine.jpg");
            //image.ContentId = "monimage";
            //htmlView.LinkedResources.Add(image);
            //mm.AlternateViews.Add(htmlView);
            
        }
    }
}
