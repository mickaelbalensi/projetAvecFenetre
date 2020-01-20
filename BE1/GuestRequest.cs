using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL1;
namespace BE1
{
    //changement nom en minuscule
    [Serializable]
    public class GuestRequest 
    {

        public long guestRequestKey;

        public string privateName { get; set; }
        public string familyName { get; set; }
        public string mailAddress { get; set; }
        //status


        //faut il definir le satut de CustomerRequirementStatus 
        //dans le champs directement plutot que ds le ctor
        //car interdit d'utiliser ctor d'apres ennoncé


        public GuestRequestStatus status { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime releaseDate { get; set; }
        public bool transactionSigned { get; set; }
        public TypeAreaOfTheCountry typeArea { get; set; }
        //subArea
        public TypeOfHostingUnit type { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public Options pool { get; set; }
        public Options jacuzzi { get; set; }
        public Options garden { get; set; }
        public Options childrenAttractions { get; set; }


        //doit onn laisser le ctor, sinon attention au compte de Configuration.guestRequestCount++;
        public GuestRequest()
        {
/*
            guestRequestKey = Configuration.guestRequestCount;
            privateName = "";
            familyName = "";
            mailAddress = "";
            status = GuestRequestStatus.active;
            registrationDate = new DateTime(2000, 1, 1);
            entryDate = new DateTime(2000, 1, 1);
            releaseDate = new DateTime(2000, 1, 1);
            typeArea = TypeAreaOfTheCountry.all;
            type = TypeOfHostingUnit.all;
            adults = 1;
            children = 0;
            pool = Options.optional;
            garden = Options.optional;
            childrenAttractions = Options.optional;
            */

        }
        public override string ToString()
        {
            return string.Format("");
        }


    }
}