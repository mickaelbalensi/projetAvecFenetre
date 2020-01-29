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

        public int bankCode { get; set; }
        public string bankName { get; set; }
        public int branchCode { get; set; }
        public string ATMaddress { get; set; }
        public string branchCity { get; set; }
        //public bool commission { get; set; }
        //public string ATMtype { get; set; }
        //public string LocationOfATMrelativeToBranch {  get; set; }
        //public bool accesshandicap { get; set; }
        //public float CoordinateX { get; set; }
        //public float CoordinateY { get; set; }

//public Bank bankNumber { get; set; }
//    //    public string bankName { get; set; }
//        public int branchNumber { get; set; }
//        public string branchAddress { get; set; }
//        public string branchCity { get; set; }

        public BankBranch()
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}