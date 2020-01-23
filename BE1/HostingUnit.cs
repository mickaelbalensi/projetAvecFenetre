using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace BE1
{
    [Serializable]
    public class HostingUnit
    {
        //doit on ajouter { get; set; } au champs hostingUnitKey ?
        public long hostingUnitKey { get; set; }
        public string hostingUnitName { get; set; }
        public Host owner { get; set; }
        public int adultPlaces { get; set; }
        public int childrenPlaces { get; set; }
        public bool jacuzzi { get; set; }
        public bool garden { get; set; }
        public bool pool { get; set; }
        public bool childrenAttractions { get; set; }
        public List<string> uris { get; set; }
        public TypeAreaOfTheCountry typeArea { get; set; }
        public TypeOfHostingUnit typeOfUnit { get; set; }

        //number of order the room received
        public int countOrder { get; set; }
        public bool[,] diary { get; set; }
        public string tempDiary
        {
            get
            {
                if (diary == null)
                    return null;

                string result = "";
                int sizeA = diary.GetLength(0);
                int sizeB = diary.GetLength(1);
                result += "" + sizeA + "," + sizeB;

                for (int i = 0; i < sizeA; i++)
                    for (int j = 0; j < sizeB; j++)
                        result += "," + diary[i, j];

                return result;
            }
            set
            {
                if (value != null && value.Length>0)
                {
                    string[] values = value.Split(',');

                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    diary = new bool[sizeA, sizeB];

                    int index = 2;

                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            diary[i, j] = bool.Parse(values[index++]);
                }

            }
        }

        public HostingUnit()
        {
            //hostingUnitKey = Configuration.hostingUnitCount;
            //hostingUnitName = "";
            //countOrder = 0;
            //diary = new bool[12, 31];
            //adultPlaces = 0;
            //childrenPlaces = 0;
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