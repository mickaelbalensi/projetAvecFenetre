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

        //request
        #region request
        void addRequest(GuestRequest request);
        //GuestRequest getRequest(long key);
        void updateRequest(GuestRequest request);
        //        void printAllCustomer(GuestRequest request);
        IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null);



        #endregion
        #region hostingUnit
        void addHostingUnit(HostingUnit unit);
        // HostingUnit getHostingUnit(long key);
        void updateHostingUnit(HostingUnit unit);
        void deleteHostingUnit(HostingUnit unit);
        void printAllHostingUnit(HostingUnit unit);
        IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null);
        #endregion
        #region order
        void addOrder(GuestRequest request);
        void updateOrder(Order order);
        void printAllOrder(Order order);

        // Order getOrder(long key);
        IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null);


        #endregion






        //prints 


        void printAllBranchesOfBank(BankBranch bank);






    }
}
