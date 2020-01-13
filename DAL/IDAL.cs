using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using static DS.DataSource;

namespace DAL
{
    public interface Idal
    {
        //request
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
    }

}