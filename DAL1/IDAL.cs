using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE1;
using DS1;
using static DS1.DataSource;

namespace DAL1
{
    public interface IDAL
    {
        
        #region request
        void addRequest(GuestRequest guest);
        GuestRequest getRequest(long key);
        void updateRequest(GuestRequest guest);
        IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null);
        #endregion
        #region hostingUnit
        void addHostingUnit(HostingUnit unit);
        HostingUnit getHostingUnit(long key);
        void updateHostingUnit(HostingUnit unit);
        void deleteHostingUnit(HostingUnit hosting);
        IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null);
        #endregion
        #region order
        void addOrder(Order order);
        void updateOrder(Order order);
        Order getOrder(long key);
        IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null);

        #endregion
        #region host
       void addHost(Host host);
       void getHost(long key);
       IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null);
            
        
        




        #endregion
        //void addHost(Host host);
    }

}