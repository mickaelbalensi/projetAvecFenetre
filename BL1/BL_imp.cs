using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE1;
using DAL1;
using System.Threading;
using DS1;


namespace BL1
{
    public class BL_imp : IBL

    {

        // DAL1.IDAL dal = FactoryDal.GetDal();
        DAL1.IDAL dal;
        public List<Host> HostList;
        public List<GuestRequest> guestRequestList;
        public List<HostingUnit> unitList;
        public BL_imp()
        {
            dal = FactoryDal.Instance;

            HostList = new List<Host> {
            new Host
            {
                hostKey=123,
                privateName = "itshak",
                familyName="bibas",
                phoneNumber=123,
                mailAddress="bib@gmail.com",
                bankAccountNumber=123,
                collectionClearance=true,
                password="123",
            },
            new Host
            {
                hostKey=456,
                privateName = "micka",
                familyName="balensi",
                phoneNumber=456,
                mailAddress="micka@gmail.com",
                bankAccountNumber=456,
                collectionClearance=true,
                password="456",
            },
            new Host
            {
                hostKey=789,
                privateName = "chmoul",
                familyName="illouz",
                phoneNumber=789,
                mailAddress="chmoul@gmail.com",
                bankAccountNumber=789,
                collectionClearance=false,
                password="789",
            }
            };
            //for(int i = 0; i < 3; i++)
            //{
            //    dal.addHost(HostList[i]);
            //}

        }


        #region essai 
        void initList()
        {
            guestRequestList = new List<GuestRequest> {
            new GuestRequest
            {
                guestRequestKey=123,
                privateName = "itshak",
                familyName="bibas",
                mailAddress="bib@gmail.com",
                status = GuestRequestStatus.active,
                jacuzzi = Options.yes,
                garden = Options.optional,
                childrenAttractions = Options.yes,
                adults = 10,
                children = 12,

            },



            new GuestRequest
            {
                guestRequestKey = 124,
                entryDate = DateTime.Parse("08.10.88"),
                releaseDate = DateTime.Parse("10.12.88"),
                privateName = "mickael",
                familyName = "Balensi",
                mailAddress = "blensimickael@gmail.com",
                status = GuestRequestStatus.active,
                jacuzzi = Options.no,
                garden = Options.optional,
                childrenAttractions = Options.yes,
                adults = 2,
                children = 10,
            },
            new GuestRequest
            {
                guestRequestKey = 125,
                entryDate = DateTime.Parse("11.12.88"),
                releaseDate = DateTime.Parse("25.12.88"),
                privateName = "Shmuel",
                familyName = "Illouz",
                mailAddress = "IllouzShmuel@gmail.com",
                status = GuestRequestStatus.active,
                jacuzzi = Options.no,
                garden = Options.no,
                childrenAttractions = Options.no,
                adults = 2,
                children = 15,


            },
            };

            unitList = new List<HostingUnit> {
            new HostingUnit
            {
                hostingUnitKey = 1234,
                adultPlaces = 2,
                childrenPlaces = 15,
                hostingUnitName = "le palace",
                jacuzzi = true,
                garden = true,
                childrenAttractions = true,
                pool = true,

            },
            new HostingUnit
            {
                hostingUnitKey = 12,
                adultPlaces = 34,
                childrenPlaces = 150,
                hostingUnitName = "le palace",
                jacuzzi = true,
                garden = true,
                childrenAttractions = true,
                pool = true,

            },
            };
            for (int i = 0; i < 3; i++)
            {
                dal.addRequest(guestRequestList[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                dal.addHostingUnit(unitList[i]);
            }

        }
        #endregion
        #region request 
        /// <summary>
        /// to add my request and check immediatlty if there is any room possible threw the addOrder
        /// </summary>
        /// <param name="request"></param>
        public void addRequest(GuestRequest request)
        {
            checkDate(request);
            dal.addRequest(request.Copy());
            addOrder(request.Copy());
        }
        public void updateRequest(GuestRequest request) {
            dal.updateRequest(request);
        }
        /// <summary>
        /// to get all my request 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            return dal.getAllGuestRequest(predicate);
        }
        /// <summary>
        /// to propose an order we must check is all the days of the room are free
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool isRoomFree(HostingUnit unit, GuestRequest request, ref int sum)
        {

            int firstDay = request.entryDate.Day;
            int firstMonth = request.entryDate.Month;
            int lastDay = request.releaseDate.Day;
            int lastMonth = request.releaseDate.Month;
            firstDay -= 1;
            firstMonth -= 1;
            lastDay -= 1;
            lastMonth -= 1;
            while (firstDay != lastDay || firstMonth != lastMonth)
            {
                if (unit.diary[firstMonth, firstDay++])//if one's of the day is already taken 
                    return false;
                sum++;
                if (firstDay == 31) { firstMonth++; firstDay = 0; }//if we got to the end of the month               //
            }
            return true;
        }

        /// <summary>
        /// check if the release date>entrydate
        /// </summary>
        /// <param name="request"></param>
        public void checkDate(GuestRequest request)
        {
            if (request.entryDate > request.releaseDate)
                throw new Exception("ERROR ! The Date of entry > Date of release");
        }
        public int numDaysBetweenTwoDates(DateTime date1, DateTime date2 = default(DateTime))
        {
            if (date1 > date2)
                date2 = DateTime.Now;
            var diff = date2 - date1;
            int numDays = int.Parse(diff.TotalDays.ToString());
            return numDays;
        }
        #endregion request 
        #region unit 

        //hostingUnit
        //hostingUnit
        /// <summary>
        /// to add an unit to my list
        /// </summary>
        /// <param name="unit"></param>
        public void addHostingUnit(HostingUnit unit) {
            dal.addHostingUnit(unit);
            throw new Exception("your unit has been registred sucessfully");
        }
        /// <summary>
        /// to delete an unit 
        /// </summary>
        /// <param name="unit"></param>
        public void deleteHostingUnit(HostingUnit unit) {
            foreach (Order orders in getAllOrder(x => x.hostingUnitKey == unit.hostingUnitKey))
            {
                if (orders.status == OrderStatus.reserved || orders.status == OrderStatus.notYetAddressed || orders.status == OrderStatus.mailWasSent)
                {
                    throw new Exception("You can't delete this unit because it has been already proposed or reserved to a client ");
                }
            }
            dal.deleteHostingUnit(unit);
        }
        /// <summary>
        /// to update an unit 
        /// </summary>
        /// <param name="unit"></param>
        public void updateHostingUnit(HostingUnit unit) {
            foreach (Order orders in getAllOrder(x => x.hostingUnitKey == unit.hostingUnitKey))
            {
                if (orders.status == OrderStatus.reserved || orders.status == OrderStatus.notYetAddressed || orders.status == OrderStatus.mailWasSent)
                {
                    throw new Exception("You can't update this unit because it has been already proposed or reserved to a client ");
                }
            }
            dal.updateHostingUnit(unit);
        }
        /// <summary>
        /// same function that getAllGuestRequest but for hostingUnit
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            return dal.getAllHostingUnit(predicate);
        }
        public IEnumerable<IGrouping<int, GuestRequest>> groupRequestByNumOfperson()
        {
            return from request in getAllGuestRequest()
                   group request.Copy() by (request.adults + request.children);
        }

        #endregion
        #region host
        public void addHost(Host host)
        {
            dal.addHost(host);
        }

        //    
        //public Host checkParameters(Host host)
        /// <summary>
        /// to check if the passsword and the id of the host are goods 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Host checkParameters(long key, string pwd)
        {
            Host h = getAllHost(ho => ho.hostKey == key).FirstOrDefault();
            if (h == null)
                throw new Exception("Wrong ID !");
            if (pwd != h.password)
                throw new Exception("Wrong password !");
            return h;
        }


        public IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null)
        {
            return dal.getAllHost(predicate);
        }
        public Host getHost(long key)
        {
            return dal.getHost(key);
        }

        /// <summary>
        /// to check if the host does have an account number
        /// </summary>
        /// <param name="BankCode"></param>
        /// <param name="BranchCode"></param>
        /// <returns></returns>
        public BankBranch checkBanckBranch(int BankCode, int BranchCode)
        {
            var branch = dal.getBankBranch(x => (x.bankCode == BankCode) && (x.branchCode == BranchCode)).FirstOrDefault();
            return branch;
        }

        public IEnumerable<BankBranch> getBankBranch(Func<BankBranch, bool> predicate = null)
        {
            return dal.getBankBranch(predicate);
        }



        #endregion

        //public void reservePlaces(Order order)
        //{
        //    HostingUnit unit = getHostingUnit(order.hostingUnitKey);
        //    GuestRequest request = getRequest(order.guestRequestKey);
        //    int firstDay = request.entryDate.Day;
        //    int firstMonth = request.entryDate.Month;
        //    int lastDay = request.entryDate.Day;
        //    int lastMonth = request.entryDate.Month;
        //    firstDay -= 1;
        //    firstMonth -= 1;
        //    while (firstDay != lastDay || firstMonth != lastMonth)
        //    {
        //        unit.diary[firstMonth, firstDay++]=true;
        //        if (firstDay == 31) { firstMonth++; firstDay = 0; }//if we got to the end of the month               //
        //    }
        //    dal.updateHostingUnit(unit);
        //}
        //public int cashMoneyFromHost(HostingUnit unit)
        //{
        //    int sum = 0;
        //    int month = 0;
        //    int day = 0;
        //    while (month != 11 || day!= 30)
        //    {
        //        if (unit.diary[month, day++])//if one's of the day is already taken 
        //            sum += 10;
        //        if (day == 31) { day++; day = 0; }//if we got to the end of the month               //
        //    }
        //    return sum;
        //}


        #region order
        /// <summary>
        /// if there is a unit which does correspond to the request we create a new order therefore we check several parameters as if there is a jacuzzi or if it's free
        /// </summary>
        /// <param name="request"></param>
        public void addOrder(GuestRequest request)
        {
            Func<HostingUnit, bool> predicate = unit =>
            {
                bool b1 = unit.adultPlaces >= request.adults;
                bool b2 = unit.childrenPlaces >= request.children;
                bool b3 = (request.jacuzzi == Options.yes) ? unit.jacuzzi : (request.jacuzzi == Options.no) ? !unit.jacuzzi : true;
                bool b4 = (request.pool == Options.yes) ? unit.pool : (request.pool == Options.no) ? !unit.pool : true;
                bool b5 = (request.childrenAttractions == Options.yes) ? unit.childrenAttractions : (request.childrenAttractions == Options.no) ? !unit.childrenAttractions : true;
                bool b6 = (request.garden == Options.yes) ? unit.garden : (request.garden == Options.no) ? !unit.garden : true;
                bool b7 = (request.typeArea == TypeAreaOfTheCountry.all) ? true : request.typeArea == unit.typeArea;
                return b1 && b2 && b3 && b4 && b5 && b6 && b7;
            };
            foreach (HostingUnit unit in getAllHostingUnit(predicate))
            {
                int nbOfDays = 0;
                if (isRoomFree(unit, request, ref nbOfDays))//check if the room is free 
                {

                    dal.addOrder(new Order
                    {
                        //orderKey = Configuration.orderCount++,
                        hostingUnitKey = unit.hostingUnitKey,
                        guestRequestKey = request.guestRequestKey,
                        status = OrderStatus.notYetAddressed,
                        createDate = request.entryDate,
                        numberOfDays = nbOfDays,
                        price = nbOfDays * 10
                    }.Copy());
                }
            }
        }
        /// <summary>
        /// to pass from mail was sent to reserve or expired 
        /// </summary>
        /// <param name="order"></param>

        public void updateOrder(Order order,Host host)
        {
            List<Order> listToUpdate = new List<Order>();
            List<Order> helpList = new List<Order>();
            foreach (Order orders in getAllOrder())
            {
                helpList.Add(orders);
            }

            for (int i=0;i<helpList.Count;i++)
            {
                HostingUnit unit = getHostingUnit(helpList[i].hostingUnitKey);
                if (helpList[i].hostingUnitKey == order.hostingUnitKey && helpList[i].orderKey != order.orderKey && host.hostKey==unit.owner.hostKey )
                {
                    helpList[i].status = OrderStatus.expired;
                    dal.updateOrder(helpList[i]);
                }
            }
            order.status = OrderStatus.reserved;
            dal.updateOrder(order);

        }
        //for (int i=0;i< listToUpdate.Count;i++)
        // dal.updateOrder(listToUpdate[i]);  

        
        
        public IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null)
        {
            return dal.getAllOrder(predicate);
        }

        #endregion
        #region functions
        /// <summary>
        /// to lock places for the client so another one wont be able to have acces during the same period 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="flag"></param>
        public void reservePlaces(Order order, bool flag)
        {
            HostingUnit unit = getHostingUnit(order.hostingUnitKey);
            GuestRequest request = getRequest(order.guestRequestKey);
            int firstDay = request.entryDate.Day;
            int firstMonth = request.entryDate.Month;
            int lastDay = request.entryDate.Day;
            int lastMonth = request.entryDate.Month;
            firstDay -= 1;
            firstMonth -= 1;
            lastDay -= 1;
            lastMonth -= 1;
            while (firstDay != lastDay || firstMonth != lastMonth)
            {
                if (flag)
                {
                    unit.diary[firstMonth, firstDay] = true;                                    
                }
               
                firstDay++;
                if (firstDay == 31) { firstMonth++; firstDay = 0; }//if we got to the end of the month               //
            }
            dal.updateOrder(order);
            if (flag)dal.updateHostingUnit(unit);
        }
        //public int cashMoneyFromHost(HostingUnit unit)
        //{
        //    int sum = 0;
        //    int month = 0;
        //    int day = 0;
        //    while (month != 11 || day != 30)
        //    {
        //        if (unit.diary[month, day++])//if one's of the day is already taken 
        //            sum += 10;
        //        if (day == 31) { day++; day = 0; }//if we got to the end of the month               //
        //    }
        //    return sum;
        //}

        //public IEnumerable<HostingUnit> getFreeUnitList(DateTime entrydate, DateTime releasedate)
        //{
        //    GuestRequest request = new GuestRequest
        //    {
        //        entryDate = entrydate,
        //        releaseDate = releasedate,
        //    };
        //    return from unit in getAllHostingUnit()
        //           where isRoomFree(unit, request,)
        //           select unit.Copy();
        //    return null;
        //}
        public int numOrderForGuestRequest(GuestRequest request)
        {
            Func<Order, bool> predicate = order =>
            {
                bool b1 = order.guestRequestKey == request.guestRequestKey;
                bool b2 = order.status == OrderStatus.mailWasSent;
                return b1 && b2;
            };

            var orderMailWasSentList = getAllOrder(predicate);
            return orderMailWasSentList.Count();
        }
        public int numOrderForHostingUnit(HostingUnit unit)
        {
            Func<Order, bool> predicate = order =>
            {
                bool b1 = order.hostingUnitKey == unit.hostingUnitKey;
                bool b2 = order.status == OrderStatus.mailWasSent;
                bool b3 = order.status == OrderStatus.reserved;
                return b1 && (b2 || b3);
            };

            var orderMailWasSentList = getAllOrder(predicate);
            return orderMailWasSentList.Count();
        }

        public bool checkTransactionSigned(Order order)
        {
            GuestRequest req = getAllGuestRequest().FirstOrDefault(x => x.guestRequestKey == order.guestRequestKey);
            return req.transactionSigned;
        }


        public IEnumerable<IGrouping<TypeAreaOfTheCountry, HostingUnit>> groupUnitByAreaList(bool flag)
        {
            return from unit in getAllHostingUnit()
                   group unit by unit.typeArea;
        }

        public List<HostingUnit> getSuggestionList(long guestRequestKey)
        {
            int c = 0;
            List<HostingUnit> listSuggestion = new List<HostingUnit>();
            List<Order> helpList = new List<Order>();
            foreach (Order order in getAllOrder())
            {
                if (guestRequestKey == order.guestRequestKey)
                {
                    order.status = OrderStatus.mailWasSent;
                    helpList.Add(order);
                    listSuggestion.Add(getHostingUnit(order.hostingUnitKey));
                }
            }
            for (int i = 0; i < helpList.Count; i++)
                dal.updateOrder(helpList[i]);
            return listSuggestion;
        }
        

        public GuestRequest getRequest(long key)
        {
            return dal.getRequest(key); ;
        }

        public HostingUnit getHostingUnit(long key)
        {
            return dal.getHostingUnit(key);
        }

        public Order getOrder(long key)
        {
           return dal.getOrder(key);
        }
        public IEnumerable<IGrouping<TypeAreaOfTheCountry, GuestRequest>> groupRequestByAreaList()
          {
        return from request in getAllGuestRequest()
               //let req = request.ToString()
               group request.Copy() by request.typeArea;
          }

        #endregion
        #region configuration
        public long getGuestRequestCount()
        {
            return dal.getGuestRequestCount();
        }
        public long getHostingUnitCount()
        {
            return dal.getHostingUnitCount();
        }
        public long getOrderCount()
        {
            return dal.getOrderCount();
        }

        public long getHostCount()
        {
            return dal.getHostCount();
        }

        #endregion

    }
}
