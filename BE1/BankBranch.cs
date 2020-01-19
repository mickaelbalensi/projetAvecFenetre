using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE1
{
    //changement nom en minuscule
    public class BankBranch //: Clonable
    {
        //ESSAI GIT HUB 
        public Bank bankNumber { get; set; }
        public string bankName { get; set; }
        public int branchNumber { get; set; }
        public string branchAddress { get; set; }
        public string branchCity { get; set; }

        public BankBranch()
        {
            bankNumber = Bank.bankHapoalim;
            bankName = Bank.bankHapoalim.ToString();
            branchNumber = 0;
            branchAddress = "";
            branchCity = "";
        }

        public override string ToString()
        {
            return "";

        }
    }
}