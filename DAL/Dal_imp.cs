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
    public class Dal_imp : Idal
    {
        #region guestRequestFunctions
        public void addRequest(GuestRequest request)
        {
            GuestRequest requestLocal = getRequest(request.guestRequestKey);
            if (requestLocal != null)
                throw new Exception("there is already a request with the same guestRequestKey");
            DataSource.guestRequestList.Add(request);
        }
        public GuestRequest getRequest(long key)
        {
            return DataSource.guestRequestList.FirstOrDefault(req => req.guestRequestKey == key);
        }
        public void updateRequest(GuestRequest request)
        {
            int index = DS.DataSource.guestRequestList.FindIndex(req => req.guestRequestKey == request.guestRequestKey);
            if (index == -1)
                throw new Exception("request with this number was not found...");
            DS.DataSource.guestRequestList[index] = request;

            //request.status = CustomerRequirementStatus.transactionClosed;
        }

        public IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null)

        {
            if (predicate == null)
                return DataSource.guestRequestList.AsEnumerable();
            return from req in DataSource.guestRequestList
                   where predicate(req)
                   select req;
        }

        #endregion

        #region hostingUnitFunctions


        public void addHostingUnit(HostingUnit unit)
        {
            HostingUnit unitLocal = getHostingUnit(unit.hostingUnitKey);
            if (unitLocal != null)
                throw new Exception("there is already an unit with the same hostingUnitKey");
            DataSource.hostingUnitList.Add(unit);

        }
        public HostingUnit getHostingUnit(long key)
        {
            return DataSource.hostingUnitList.FirstOrDefault(unit => unit.hostingUnitKey == key);
        }
        public void updateHostingUnit(HostingUnit unit)
        {
            int index = DS.DataSource.hostingUnitList.FindIndex(hostUnit => hostUnit.hostingUnitKey == unit.hostingUnitKey);
            if (index == -1)
                throw new Exception("hostingUnit with this number was not found...");
            DS.DataSource.hostingUnitList[index] = unit;
        }

        public void deleteHostingUnit(HostingUnit unit)
        {
            HostingUnit unitLocal = getHostingUnit(unit.hostingUnitKey);
            if (unitLocal == null)
                throw new Exception("there isn't such hostingUnit to remove");
            DataSource.hostingUnitList.Remove(unit);
        }
        public IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.hostingUnitList.AsEnumerable();
            return from unit in DataSource.hostingUnitList
                   where predicate(unit)
                   select unit;
        }

        #endregion

        #region orderFunctions
        public void addOrder(Order order)
        {
            Order orderLocal = getOrder(order.orderKey);
            if (orderLocal != null)
                throw new Exception("there is already an order with the same orderKey");
            DataSource.orderList.Add(order);
        }
        public Order getOrder(long key)
        {
            return DataSource.orderList.FirstOrDefault(ord => ord.orderKey == key);
        }
        public void updateOrder(Order order)
        {
            int index = DS.DataSource.orderList.FindIndex(ord => ord.orderKey == order.orderKey);
            if (index == -1)
                throw new Exception("Order with this number was not found...");
            DS.DataSource.orderList[index] = order;
        }
        public IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.orderList.AsEnumerable();
            return from ord in DataSource.orderList
                   where predicate(ord)
                   select ord;
        }
        #endregion
        public List<BankBranch> getAllBankBranch()
        {
            List<BankBranch> bankBranchList = new List<BankBranch>
            {
                new BankBranch{
                    bankNumber = Bank.bankHapoalim,
                    bankName = Bank.bankHapoalim.ToString(),
                    branchNumber = 1,
                    branchAddress = "21 street bayit-vegan",
                    branchCity = "jerusalem"
                },
                new BankBranch
                {
                    bankNumber = Bank.bankHapoalim,
                    bankName = Bank.bankHapoalim.ToString(),
                    branchNumber = 2,
                    branchAddress = "52 street uziel",
                    branchCity = "jerusalem"
                },
                new BankBranch
                {
                    bankNumber = Bank.bankHapoalim,
                    bankName = Bank.bankHapoalim.ToString(),
                    branchNumber = 3,
                    branchAddress = "25 street rotshild",
                    branchCity = "tel-aviv"
                },
                new BankBranch
                {
                    bankNumber = Bank.bankLeumi,
                    bankName = Bank.bankLeumi.ToString(),
                    branchNumber = 1,
                    branchAddress = "15 street shtraus",
                    branchCity = "jerusalem"
                },
                new BankBranch
                {
                    bankNumber = Bank.bankLeumi,
                    bankName = Bank.bankLeumi.ToString(),
                    branchNumber = 2,
                    branchAddress = "19 street ben-yehuda ",
                    branchCity = "jerusalem"
                }
            };

            return bankBranchList;
        }
    }
}