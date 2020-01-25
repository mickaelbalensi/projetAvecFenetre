using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE1;

namespace DS1
{
    public class DataSource
    {
        public static List<HostingUnit> hostingUnitList { get; set; }
        public static List<GuestRequest> guestRequestList { get; set; }
        public static List<Order> orderList { get; set; }
        public static List<BankBranch> bankBranchList;
        public static List<Host> HostList { get; set; }

        public DataSource()
        {

            /*string[] privateName = new string[] { "mickael", "itshak", "chmoulik", "hillel", "acher" };
            string[] familyName = new string[] { "balensi", "bibas", "illouz", "farouz", "klein" };
            long [] phoneNumber = new long[] { 0767894905, 0584191198, 0512061998, 0648786859, 0589758695 };
            */
            hostingUnitList = new List<HostingUnit>();
            guestRequestList = new List<GuestRequest>();
            orderList = new List<Order>();
            HostList = new List<Host> {
            new Host
            {
                hostKey=1,
                privateName = "itshak",
                familyName="bibas",
                phoneNumber=123,
                mailAddress="bib@gmail.com",
                bankAccountNumber=123,
                collectionClearance=true,
                password="123",

            },
            new Host
            {
                hostKey=2,
                privateName = "micka",
                familyName="balensi",
                phoneNumber=456,
                mailAddress="micka@gmail.com",
                bankAccountNumber=456,
                collectionClearance=true,
                password="456",
            },
            new Host
            {
                hostKey=3,
                privateName = "chmoul",
                familyName="illouz",
                phoneNumber=789,
                mailAddress="chmoul@gmail.com",
                bankAccountNumber=789,
                collectionClearance=false,
                password="789",
            }
            };
            //bankBranchList = new List<BankBranch>

            //{
            //    new BankBranch{
            //        bankNumber = Bank.bankHapoalim,
            //        bankName = Bank.bankHapoalim.ToString(),
            //        branchNumber = 1,
            //        branchAddress = "21 street bayit-vegan",
            //        branchCity = "jerusalem"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankHapoalim,
            //        bankName = Bank.bankHapoalim.ToString(),
            //        branchNumber = 2,
            //        branchAddress = "52 street uziel",
            //        branchCity = "jerusalem"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankHapoalim,
            //        bankName = Bank.bankHapoalim.ToString(),
            //        branchNumber = 3,
            //        branchAddress = "25 street rotshild",
            //        branchCity = "tel-aviv"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankLeumi,
            //        bankName = Bank.bankLeumi.ToString(),
            //        branchNumber = 1,
            //        branchAddress = "15 street shtraus",
            //        branchCity = "jerusalem"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankLeumi,
            //        bankName = Bank.bankLeumi.ToString(),
            //        branchNumber = 2,
            //        branchAddress = "19 street ben-yehuda ",
            //        branchCity = "jerusalem"
            //    }
            //};
        }
        public static string GuestRequestList()
        {
            string str = "";
            string t = "\t";

            foreach (GuestRequest m in guestRequestList)
            {

            }
            return str;
        }



    }
}
