using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE1;
using DAL1;

namespace BL1
{
    public interface IBL
    {

        #region request
        void addRequest(GuestRequest request);
        void updateRequest(GuestRequest request);
        IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null);
        #endregion
        #region hostingUnit
        void addHostingUnit(HostingUnit unit);
        void updateHostingUnit(HostingUnit unit);
        void deleteHostingUnit(HostingUnit unit);
        IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null);
        List<HostingUnit> getSuggestionList(long guestRequestKey);

        #endregion
        #region order
        void addOrder(GuestRequest request);
        void updateOrder(Order order);
        IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null);
        Order getOrder(long key);

        #endregion
        #region configuration
        long getGuestRequestCount();
        long getHostingUnitCount();

        long getOrderCount();
        #endregion
        #region host
        void addHost(Host host);
        Host checkParameters(long key, string pwd);
        Host getHost(long key);
        IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null);
        #endregion

        IEnumerable<IGrouping<TypeAreaOfTheCountry, HostingUnit>> groupUnitByAreaList(bool flag);
        IEnumerable<IGrouping<int, GuestRequest>> groupRequestByNumOfperson();
        IEnumerable<IGrouping<TypeAreaOfTheCountry, GuestRequest>> groupRequestByAreaList();

    }
}
