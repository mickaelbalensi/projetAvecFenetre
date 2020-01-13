using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        //doit on ajouter { get; set; } au champs hostingUnitKey ?
        public long hostingUnitKey { get; set; }
        public string hostingUnitName { get; set; }
        public Host owner { get; set; }
        public bool[,] diary { get; set; }
        public int adultPlaces { get; set; }
        public int childrenPlaces { get; set; }
        public bool jacuzzi { get; set; }
        public bool garden { get; set; }
        public bool pool { get; set; }
        public bool childrenAttractions { get; set; }

        public TypeAreaOfTheCountry typeArea { get; set; }

        //number of order the room received
        public int countOrder { get; set; }
        public HostingUnit()
        {
            hostingUnitKey = Configuration.hostingUnitCount;
            hostingUnitName = "";
            countOrder = 0;
            diary = new bool[12, 31];
            adultPlaces = 0;
            childrenPlaces = 0;
        }
        public override string ToString()
        {
            return
                "HostingUnitKey : " + hostingUnitKey + "\n" +
                      "HostingUnitName : " + hostingUnitName + "\n" +
                      "Adultplaces : " + adultPlaces + "\n" +
                      "Childrenplaces : " + childrenPlaces + "\n";
        }
    }
}
