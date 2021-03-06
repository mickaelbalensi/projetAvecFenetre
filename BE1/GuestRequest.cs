﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL1;
namespace BE1
{
    [Serializable]
    public class GuestRequest 
    {

        public long guestRequestKey;

        public string privateName { get; set; }
        public string familyName { get; set; }
        public string mailAddress { get; set; }

        public GuestRequestStatus status { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime releaseDate { get; set; }
        public bool transactionSigned { get; set; }
        public TypeAreaOfTheCountry typeArea { get; set; }
        public TypeOfHostingUnit type { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public Options pool { get; set; }
        public Options jacuzzi { get; set; }
        public Options garden { get; set; }
        public Options childrenAttractions { get; set; }


        public GuestRequest()
        {

        }
        public override string ToString()
        {
            return "GuestRequestKey : " + guestRequestKey + "\n" +
                  "privateName : " + privateName + "\n" +
                  "familyName : " + familyName + "\n" +
                  "mailAddress : " + mailAddress + "\n" +
                  "entryDate : " + entryDate.ToShortDateString() + "\n" +
                  "releaseDate : " + releaseDate.ToShortDateString() + "\n" +
                  "typeArea : " + typeArea.ToString() + "\n" +
                  "type : " + type.ToString() + "\n" +
                  "adults : " + adults.ToString() + "\n" +
                  "children : " + children.ToString() + "\n" +
                  "pool : " + pool.ToString() + "\n" +
                  "jacuzzi : " + jacuzzi.ToString() + "\n" +
                  "garden : " + garden.ToString() + "\n" +
                  "childrenAttractions : " + childrenAttractions.ToString() + "\n";
                  
        }


    }
}