using System;
using System.Collections.Generic;
using BE1;

namespace BL1
{
    public interface IBL1
    {
        void addHostingUnit(HostingUnit unit);
        void addOrder(GuestRequest request);
        void addRequest(GuestRequest request);
        void deleteHostingUnit(HostingUnit unit);
        IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null);
        IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null);
        IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null);
        List<HostingUnit> getSuggestionList(long guestRequestKey);
        void updateHostingUnit(HostingUnit unit);
        void updateOrder(Order order);
        void updateRequest(GuestRequest request);

        //void addHost(Host host);
        //void getHost(long key);
        //IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null);


    }
}