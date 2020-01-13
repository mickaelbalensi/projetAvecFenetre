using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL1
{
    public class FactoryDal
    {
        public static IDAL GetDal()
        {
            return new DAL_XML();
        }
}
}