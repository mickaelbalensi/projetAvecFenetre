using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL1
{
    public class FactoryBL
    {
        private static IBL instance = null;

        static FactoryBL() { }

        public static IBL Instance
        {
            get
            {
                if (instance == null)
                    instance = new BL_imp();
                return instance;
            }
        }

        public static IBL getBL()
        {
            return new BL_imp();
        }
    }
}
