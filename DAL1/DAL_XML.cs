
using BE1;
using DAL1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1
{
    public class DAL_XML : IDAL
    {
        public void addHostingUnit(HostingUnit unit)
        {
            throw new NotImplementedException();
        }

        public void addOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void addRequest(GuestRequest guest)
        {
            throw new NotImplementedException();
        }

        public void deleteHostingUnit(HostingUnit hosting)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public HostingUnit getHostingUnit(long key)
        {
            throw new NotImplementedException();
        }

        public Order getOrder(long key)
        {
            throw new NotImplementedException();
        }

        public GuestRequest getRequest(long key)
        {
            throw new NotImplementedException();
        }

        public void updateHostingUnit(HostingUnit unit)
        {
            throw new NotImplementedException();
        }

        public void updateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void updateRequest(GuestRequest guest)
        {
            throw new NotImplementedException();
        }
    }
}