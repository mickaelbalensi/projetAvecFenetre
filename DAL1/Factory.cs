using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL1
{
    public class FactoryDal
    {
        private static IDAL instance = null;
        static FactoryDal() { }

        public static IDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_XML();
                return instance;
            }
        }
        /*
        public static IDAL GetDal()
        {
            return new DAL_XML();
        }*/
    }
}