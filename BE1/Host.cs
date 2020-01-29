using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE1
{
    [Serializable]
    public class Host 

    {
        public long hostKey { get; set; }
        public string privateName { get; set; }
        public string familyName { get; set; }
        public long phoneNumber { get; set; }
        public string mailAddress { get; set; }
        public BankBranch bankBranchDetails { get; set; }
        public long bankAccountNumber { get; set; }
        public bool collectionClearance { get; set; }
        [XmlIgnore]
        public string password { get; set; }
        public override string ToString()
        {
            return "hostKey : " + hostKey + "\n" +
                "privateName : " + privateName + "\n" +
                "familyName : " + familyName + "\n" +
                " phoneNumber : " + phoneNumber + "\n" +
                "mailAddress : " + mailAddress + "\n" +
                "bankAccountNumber : " + bankAccountNumber + "\n" +
                "collectionClearance : " + collectionClearance + "\n"; //+
               // "bankBranchDetails : " + bankBranchDetails + "\n" +
                //"countHostingUnit : " + countHostingUnit;
        }

    }
}