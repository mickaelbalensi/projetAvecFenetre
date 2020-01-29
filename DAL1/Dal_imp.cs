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
    public class Dal_imp : IDAL
    {
        static DataSource d = new DataSource();

        #region guestRequestFunctions
        public void addRequest(GuestRequest request)
        {
            Configuration.guestRequestCount++;
            GuestRequest requestLocal = getRequest(request.guestRequestKey);
            if (requestLocal != null)
                throw new Exception("there is already a request with the same guestRequestKey");
            DataSource.guestRequestList.Add(request.Copy());
            
        }
        public GuestRequest getRequest(long key)
        {
            return DataSource.guestRequestList.FirstOrDefault(req => req.guestRequestKey == key);
        }
        public void updateRequest(GuestRequest request)
        {
            int index = DS1.DataSource.guestRequestList.FindIndex(req => req.guestRequestKey == request.guestRequestKey);
            if (index == -1)
                throw new Exception("request with this number was not found...");
            DS1.DataSource.guestRequestList[index] = request.Copy();
            //request.status = CustomerRequirementStatus.transactionClosed;
        }

        public IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.guestRequestList.AsEnumerable();
            return from req in DataSource.guestRequestList
                   where predicate(req)
                   select req.Copy();
        }

        #endregion
        #region hostingUnitFunctions


        public void addHostingUnit(HostingUnit unit)
        {
            unit.hostingUnitKey = Configuration.hostingUnitCount++;
            HostingUnit unitLocal = getHostingUnit(unit.hostingUnitKey);
            if (unitLocal != null)
                throw new Exception("there is already an unit with the same hostingUnitKey");
            DataSource.hostingUnitList.Add(unit.Copy());
            throw new Exception("Your Unit has been succesfully registred !");
        }
        public HostingUnit getHostingUnit(long key)
        {
            return DataSource.hostingUnitList.FirstOrDefault(unit => unit.hostingUnitKey == key);
        }
        public void updateHostingUnit(HostingUnit unit)
        {
            int index = DS1.DataSource.hostingUnitList.FindIndex(hostUnit => hostUnit.hostingUnitKey == unit.hostingUnitKey);
            if (index == -1)
                throw new Exception("hostingUnit with this number was not found...");
            DS1.DataSource.hostingUnitList[index] = unit.Copy();
        }
        public void deleteHostingUnit(HostingUnit unit)
        {
            HostingUnit unitLocal = getHostingUnit(unit.hostingUnitKey);
            if (unitLocal == null)
                throw new Exception("there isn't such hostingUnit to remove");
            DataSource.hostingUnitList.Remove(unit.Copy());
        }
        public IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.hostingUnitList.AsEnumerable();
            return from unit in DataSource.hostingUnitList
                   where predicate(unit)
                   select unit.Copy();
        }

        #endregion
        #region orderFunctions
        public void addOrder(Order order)
        {
            Order orderLocal = getOrder(order.orderKey);
            if (orderLocal != null)
                throw new Exception("there is already an order with the same orderKey");
            DataSource.orderList.Add(order.Copy());
            
        }
        public Order getOrder(long key)
        {
            return DataSource.orderList.FirstOrDefault(ord => ord.orderKey == key);
        }
        public void updateOrder(Order order)
        {
            int index = DS1.DataSource.orderList.FindIndex(ord => ord.orderKey == order.orderKey);
            if (index == -1)
                throw new Exception("Order with this number was not found...");
            DS1.DataSource.orderList[index] = order.Copy();
        }
        public IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.orderList.AsEnumerable();
            return from ord in DataSource.orderList
                   where predicate(ord)
                   select ord.Copy();
        }
        #endregion
        #region hostFunctions
        public void addHost(Host host)
        {

            DataSource.HostList.Add(host.Copy());
        }
        public IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.HostList.AsEnumerable();
            return from host in DataSource.HostList
                   where predicate(host)
                   select host.Copy();
        }
        public Host getHost(long key)
        {
            return DataSource.HostList.FirstOrDefault(unit => unit.hostKey == key);
        }

        #endregion

        public List<BankBranch> getAllBankBranch()
        {
            //List<BankBranch> bankBranchList = new List<BankBranch>
            //{
            //    new BankBranch{
            //        bankNumber = Bank.bankHapoalim,
            //        bankName = Bank.bankHapoalim.ToString(),
            //        branchNumber = 1,
            //        branchAddress = "21 street bayit-vegan",
            //        branchCity = "jerusalem"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankHapoalim,
            //        bankName = Bank.bankHapoalim.ToString(),
            //        branchNumber = 2,
            //        branchAddress = "52 street uziel",
            //        branchCity = "jerusalem"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankHapoalim,
            //        bankName = Bank.bankHapoalim.ToString(),
            //        branchNumber = 3,
            //        branchAddress = "25 street rotshild",
            //        branchCity = "tel-aviv"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankLeumi,
            //        bankName = Bank.bankLeumi.ToString(),
            //        branchNumber = 1,
            //        branchAddress = "15 street shtraus",
            //        branchCity = "jerusalem"
            //    },
            //    new BankBranch
            //    {
            //        bankNumber = Bank.bankLeumi,
            //        bankName = Bank.bankLeumi.ToString(),
            //        branchNumber = 2,
            //        branchAddress = "19 street ben-yehuda ",
            //        branchCity = "jerusalem"
            //    }
            //};

            return bankBranchList;
        }

        #region configuration
        public long getGuestRequestCount()
        {
            return Configuration.guestRequestCount;
        }
        public long getHostingUnitCount()
        {
            return Configuration.hostingUnitCount;
        }

        public long getOrderCount()
        {
            return Configuration.orderCount;
        }

        public IEnumerable<BankBranch> getBankBranch(Func<BankBranch, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public long getHostCount()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}