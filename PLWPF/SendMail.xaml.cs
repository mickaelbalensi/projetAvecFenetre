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


namespace PLWPF
{
    /// <summary>
    /// Logique d'interaction pour SendMail.xaml
    /// </summary>
    public partial class SendMail : Window
    {
        public SendMail()
        {
            InitializeComponent();
        }

        private void Sendmail_Click(object sender, RoutedEventArgs e)
        {
            Outlook.Application App = new Outlook.Application();
            Outlook.MailItem msg = (Outlook.MailItem)App.CreateItem(Outlook.OlItemType.olMailItem);
            msg.HTMLBody = ("ta race");
            msg.Subject = "sujet";
            Outlook.Recipients recips = (Outlook.Recipients)msg.Recipients;
            Outlook.Recipient recip = (Outlook.Recipient)recips.Add("mickaelbalensi2652@gmail.com");
            recip.Resolve();
            msg.Send();
            recips = null;
            recip = null;
            App = null;
            MessageBox.Show("regarde ton mail");
        }
    }
}
