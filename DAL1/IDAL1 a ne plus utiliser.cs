using System;
using System.Collections.Generic;
using BE1;

namespace DAL1
{
    public interface IDAL1
    {
        void addHostingUnit(HostingUnit unit);
        void addOrder(Order order);
        void addRequest(GuestRequest guest);
        void deleteHostingUnit(HostingUnit hosting);
        IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null);
        IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null);
        IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null);
        void updateHostingUnit(HostingUnit unit);
        void updateOrder(Order order);
        void updateRequest(GuestRequest guest);
        void addHost(Host host);
        void getHost(long key);
        IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null);
    }
}