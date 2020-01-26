
using BE1;
using DAL1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Net;

namespace DAL1
{
    public class DAL_XML : IDAL
    {
        XElement guestRequestRoot;
        string guestRequestPath = @"guestRequestXml.xml";

        XElement hostRoot;
        string hostPath = @"hostXml.xml";

        XElement hostingUnitRoot;
        string hostingUnitPath = @"hostingUnitXml.xml";

        XElement orderRoot;
        string orderPath = @"orderXml.xml";

        XElement bankBranchRoot;
        string bankBranchPath = @"bankBranchXml.xml";

        XElement configRoot;
        string configPath = @"config.xml";

        const string xmlLocalPath = @"atm.xml"; 

        long guestRequestCount;
        long hostingUnitCount;
        long orderCount;

        public DAL_XML()
        {
            if (!File.Exists(guestRequestPath)
                || !File.Exists(hostPath)
                || !File.Exists(hostingUnitPath)
                || !File.Exists(orderPath)
                || !File.Exists(configPath)
                || !File.Exists(bankBranchPath))
                CreateFiles();
            else
                CreateFiles();
        }

        private void CreateFiles()
        {
            guestRequestRoot = new XElement("Request");
            guestRequestRoot.Save(guestRequestPath);

            hostRoot = new XElement("Host");
            hostRoot.Save(hostPath);

            hostingUnitRoot = new XElement("HostingUnit");
            hostingUnitRoot.Save(hostingUnitPath);

            orderRoot = new XElement("Order");
            orderRoot.Save(orderPath);

            bankBranchRoot = new XElement("BankBranch");
            bankBranchRoot.Save(bankBranchPath);


            configRoot = new XElement("configuration",
                new XElement("orderCount", "1"),
                new XElement("hostingUnitCount", "1"),
                new XElement("guestRequestCount", "1"));
            String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            configRoot.Save(path + configPath);

            DownloadBankBranch();
        }

        private void LoadData()
        {
            try
            {
                guestRequestRoot = XElement.Load(guestRequestPath);
                hostRoot = XElement.Load(hostPath);
                hostingUnitRoot = XElement.Load(hostingUnitPath);
                orderRoot = XElement.Load(orderPath);
                bankBranchRoot = XElement.Load(bankBranchPath);
                configRoot = XElement.Load(configPath);

            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        #region Conversion regions

        XElement ConvertGuestRequestToXAML(GuestRequest request)
        {
            return 
                new XElement("Request",
                new XElement("GuestRequestKey", request.guestRequestKey.ToString()),
                new XElement("PrivateName", request.privateName),
                new XElement("FamilyName", request.familyName),
                new XElement("MailAddress", request.mailAddress),
                new XElement("Status", request.status.ToString()),
                new XElement("RegistrationDate", request.registrationDate.ToShortDateString()),
                new XElement("EntryDate", request.entryDate.ToShortDateString()),
                new XElement("ReleaseDate", request.releaseDate.ToShortDateString()),
                new XElement("TransactionSigned", request.transactionSigned.ToString()),
                new XElement("TypeArea", request.typeArea.ToString()),
                new XElement("Type", request.type.ToString()),
                new XElement("Adults", request.adults.ToString()),
                new XElement("Children", request.children.ToString()),
                new XElement("Pool", request.pool.ToString()),
                new XElement("Jacuzzi", request.jacuzzi.ToString()),
                new XElement("Garden", request.garden.ToString()),
                new XElement("ChildrenAttractions", request.childrenAttractions.ToString()));
        }
        GuestRequest ConvertXAMLToGuestRequest(XElement element)
        {
            GuestRequest request = new GuestRequest();

            request.guestRequestKey = (from req in element.Elements()
                                       where req.Name== "GuestRequestKey"
                                       select long.Parse(req.Value)).FirstOrDefault();

            request.privateName = (from req in element.Elements()
                                   where req.Name == "PrivateName"
                                   select req.Value).FirstOrDefault();

            request.familyName = (from req in element.Elements()
                                  where req.Name == "FamilyName"
                                  select req.Value).FirstOrDefault();

            request.mailAddress = (from req in element.Elements()
                                   where req.Name == "MailAddress"
                                   select req.Value).FirstOrDefault();
            
            string status= (from req in element.Elements()
                            where req.Name == "Status"
                            select req.Value).FirstOrDefault();

            request.status = 
                status == "active" ?
                GuestRequestStatus.active :
                (status == "hasExpired" ?
                GuestRequestStatus.hasExpired :
                GuestRequestStatus.transactionClosed);

            request.registrationDate = (from req in element.Elements()
                                        where req.Name == "RegistrationDate"
                                        select DateTime.Parse(req.Value)).FirstOrDefault();

            request.entryDate = (from req in element.Elements()
                                 where req.Name == "EntryDate"
                                 select DateTime.Parse(req.Value)).FirstOrDefault();

            request.releaseDate = (from req in element.Elements()
                                   where req.Name == "ReleaseDate"
                                   select DateTime.Parse(req.Value)).FirstOrDefault();

            request.transactionSigned = (from req in element.Elements()
                                         where req.Name == "TransactionSigned"
                                         select Boolean.Parse(req.Value)).FirstOrDefault();
            
            string typeArea = (from req in element.Elements()
                               where req.Name == "TypeArea"
                               select req.Value).FirstOrDefault();

            request.typeArea =
                typeArea == "all" ?
                TypeAreaOfTheCountry.all :
                typeArea == "north" ? TypeAreaOfTheCountry.north :
                typeArea == "south" ? TypeAreaOfTheCountry.south :
                typeArea == "center" ? TypeAreaOfTheCountry.center :
                TypeAreaOfTheCountry.jerusalem;
                
            string type = (from req in element.Elements()
                           where req.Name == "Type"
                           select req.Value.ToString()).FirstOrDefault();

            request.type =
                type == "all" ? TypeOfHostingUnit.all :
                type == "apartment" ? TypeOfHostingUnit.apartment :
                type == "roomOfHotel" ? TypeOfHostingUnit.roomOfHotel :
                type == "tent" ? TypeOfHostingUnit.tent :
                TypeOfHostingUnit.zimmer;

            request.adults = (from req in element.Elements()
                              where req.Name == "Adults"
                              select int.Parse(req.Value)).FirstOrDefault();

            request.children = (from req in element.Elements()
                                where req.Name == "Children"
                                select int.Parse(req.Value)).FirstOrDefault();

            string pool = (from req in element.Elements()
                           where req.Name == "Pool"
                           select (req.Value)).FirstOrDefault();

            request.pool =
                pool == "yes" ? Options.yes :
                pool == "no" ? Options.no :
                Options.optional;

            string jacuzzi = (from req in element.Elements()
                              where req.Name == "Jacuzzi"
                              select (req.Value)).FirstOrDefault();

            request.jacuzzi =
                jacuzzi == "yes" ? Options.yes :
                jacuzzi == "no" ? Options.no :
                Options.optional;

            string garden = (from req in element.Elements()
                             where req.Name == "Garden"
                             select (req.Value)).FirstOrDefault();

            request.garden=
                garden == "yes" ? Options.yes :
                garden == "no" ? Options.no :
                Options.optional;

            string childrenAttractions = (from req in element.Elements()
                                          where req.Name == "ChildrenAttractions"
                                          select (req.Value)).FirstOrDefault();

            request.childrenAttractions =
                childrenAttractions == "yes" ? Options.yes :
                childrenAttractions == "no" ? Options.no :
                Options.optional;

            return request;
        }
        XElement ConvertHostingUnitToXAML(HostingUnit unit) {
            //XElement uris = new XElement("uris");

            //foreach (string str in unit.uris)
            //{
            //    XElement img = new XElement("img", str);
            //    uris.Add(img);
            //}

            return
                new XElement("HostingUnit",
                new XElement("hostingUnitKey", unit.hostingUnitKey.ToString()),
                new XElement("hostingUnitName", unit.hostingUnitName),
                new XElement("owner", ConvertHostToXAML(unit.owner)),
                new XElement("adultPlaces", unit.adultPlaces.ToString()),
                new XElement("childrenPlaces", unit.childrenPlaces.ToString()),
                new XElement("jacuzzi", unit.jacuzzi.ToString()),
                new XElement("garden", unit.garden.ToString()),
                new XElement("pool", unit.pool.ToString()),
                new XElement("childrenAttractions", unit.childrenAttractions.ToString()),
                new XElement("typeArea", unit.typeArea.ToString()),
                new XElement("typeOfUnit", unit.typeOfUnit.ToString()),
                new XElement("tempDiary", unit.tempDiary),
                new XElement("tempUris", unit.tempUris));
        }
        HostingUnit ConvertXAMLToHostingUnit(XElement element)//vérifié, lire eara ds func
        {
            HostingUnit unit = new HostingUnit();

            unit.hostingUnitKey = (from hosting in element.Elements()
                                   where hosting.Name== "hostingUnitKey"
                                   select long.Parse(hosting.Value)).FirstOrDefault();

            unit.hostingUnitName = (from hosting in element.Elements()
                                    where hosting.Name == "hostingUnitName"
                                    select (hosting.Value)).FirstOrDefault();

            unit.owner = ConvertXAMLToHost(element.Element("owner").Element("Host"));//attention type du param
                
            unit.adultPlaces = (from hosting in element.Elements()
                                where hosting.Name == "adultPlaces"
                                select int.Parse(hosting.Value)).FirstOrDefault();

            unit.childrenPlaces = (from hosting in element.Elements()
                                   where hosting.Name == "childrenPlaces"
                                   select int.Parse(hosting.Value)).FirstOrDefault();

            unit.jacuzzi = (from hosting in element.Elements()
                            where hosting.Name == "jacuzzi"
                            select bool.Parse(hosting.Value)).FirstOrDefault();

            unit.garden = (from hosting in element.Elements()
                           where hosting.Name == "garden"
                           select bool.Parse(hosting.Value)).FirstOrDefault();

            unit.pool = (from hosting in element.Elements()
                         where hosting.Name == "pool"
                         select bool.Parse(hosting.Value)).FirstOrDefault();

            unit.childrenAttractions = (from hosting in element.Elements()
                                        where hosting.Name == "childrenAttractions"
                                        select bool.Parse(hosting.Value)).FirstOrDefault();
 
            string typeArea = (from hosting in element.Elements()
                               where hosting.Name == "typeArea"
                               select hosting.Value).FirstOrDefault();

            unit.typeArea =
                typeArea == "all" ? TypeAreaOfTheCountry.all :
                typeArea == "north" ? TypeAreaOfTheCountry.north :
                typeArea == "south" ? TypeAreaOfTheCountry.south :
                typeArea == "center" ? TypeAreaOfTheCountry.center :
                TypeAreaOfTheCountry.jerusalem;

            string typeOfUnit = (from hosting in element.Elements()
                                 where hosting.Name == "typeOfUnit"
                                 select hosting.Value).FirstOrDefault();

            unit.typeOfUnit =
                typeOfUnit == "all" ? TypeOfHostingUnit.all :
                typeOfUnit == "apartment" ? TypeOfHostingUnit.apartment :
                typeOfUnit == "roomOfHotel" ? TypeOfHostingUnit.roomOfHotel :
                typeOfUnit == "tent" ? TypeOfHostingUnit.tent :
                TypeOfHostingUnit.zimmer;

            unit.tempDiary = (from hosting in element.Elements()
                              where hosting.Name == "tempDiary"
                              select (hosting.Value)).FirstOrDefault();
            //recupérer les liens d'images
            unit.tempUris = (from hosting in element.Elements()
                           where hosting.Name == "tempUris"
                             select (hosting.Value)).FirstOrDefault();

            //unit.uris= (from hosting in element.Elements()
            //           where hosting.Name == "img"
            //            select (hosting.Value)).FirstOrDefault();


            return unit;
        }
        XElement ConvertHostToXAML(Host host)
        {
            return 
                new XElement("Host",
                new XElement("hostKey", host.hostKey.ToString()),
                new XElement("privateName", host.privateName),
                new XElement("familyName", host.familyName),
                new XElement("phoneNumber", host.phoneNumber.ToString()),
                new XElement("mailAddress", host.mailAddress),
                //new XElement("bankBranchDetails", ConvertBankBranchToXAML(host.bankBranchDetails)),
                new XElement("bankAccountNumber", host.bankAccountNumber.ToString()),
                new XElement("collectionClearance", host.collectionClearance.ToString()));
        }
        Host ConvertXAMLToHost(XElement element)
        {
            Host host = new Host();

            host.hostKey = (from ost in element.Elements()
                            where ost.Name=="hostKey"
                            select long.Parse(ost.Value)).FirstOrDefault();

            host.privateName = (from ost in element.Elements()
                                where ost.Name== "privateName"
                                select (ost.Value)).FirstOrDefault();

            host.familyName = (from ost in element.Elements()
                               where ost.Name=="familyName"
                               select (ost.Value)).FirstOrDefault();

            host.phoneNumber = (from ost in element.Elements()
                                where ost.Name== "phoneNumber"
                                select long.Parse(ost.Value)).FirstOrDefault();

            host.mailAddress = (from ost in element.Elements()
                                where ost.Name=="mailAddress"
                                select (ost.Value)).FirstOrDefault();

            //host.bankBranchDetails = ConvertXAMLToBankBranch(element);

            host.bankAccountNumber = (from ost in element.Elements()
                                      where ost.Name== "bankAccountNumber"
                                      select long.Parse(ost.Value)).FirstOrDefault();

            host.collectionClearance = (from ost in element.Elements()
                                        where ost.Name== "collectionClearance"
                                        select bool.Parse(ost.Value)).FirstOrDefault();

            return host;
        }
        //XElement ConvertBankBranchToXAML(BankBranch bank)
        //{
        //    return 
        //        new XElement("BankBranch",
        //        new XElement("bankNumber", bank.bankNumber.ToString()),
        //        new XElement("bankName",bank.bankName),
        //        new XElement("branchNumber",bank.branchNumber.ToString()),
        //        new XElement("branchAddress", bank.branchAddress),
        //        new XElement("branchCity",bank.branchCity));
        //}
        //BankBranch ConvertXAMLToBankBranch(XElement element)
        //{
        //    BankBranch branch = new BankBranch();

        //    string bankNumber = (from bank in element.Elements()
        //                         where bank.Name=="bankNumber"
        //                         select bank.Value).FirstOrDefault();

        //    branch.bankNumber =
        //        bankNumber == "bankHapoalim" ? Bank.bankHapoalim :
        //        bankNumber == "bankLeumi" ? Bank.bankLeumi :
        //        bankNumber == "BNP" ? Bank.BNP :
        //        Bank.HSBC;

        //    branch.bankName = (from bank in element.Elements()
        //                       where bank.Name== "bankName"
        //                       select bank.Value).FirstOrDefault();

        //    branch.branchNumber = (from bank in element.Elements()
        //                           where bank.Name== "branchNumber"
        //                           select int.Parse(bank.Value)).FirstOrDefault();

        //    branch.branchAddress = (from bank in element.Elements()
        //                            where bank.Name=="branchNumber"
        //                            select bank.Value).FirstOrDefault();

        //    branch.branchCity = (from bank in element.Elements()
        //                         where bank.Name=="branchCity"
        //                         select bank.Value).FirstOrDefault();

        //    return branch;
        //}
        BankBranch ConvertXAMLToBankBranch(XElement element)
        {
            BankBranch branch = new BankBranch();

            branch.bankCode = (from bank in element.Elements()
                               where bank.Name== "קוד_בנק"
                               select int.Parse(bank.Value)).FirstOrDefault();

            branch.bankName = (from bank in element.Elements()
                             where bank.Name == "שם_בנק"
                             select (bank.Value)).FirstOrDefault();

            branch.branchCode = (from bank in element.Elements()
                                 where bank.Name == "קוד_סניף"
                                 select int.Parse(bank.Value)).FirstOrDefault();

            branch.ATMaddress = (from bank in element.Elements()
                                 where bank.Name == "כתובת_ה-ATM"
                                 select (bank.Value)).FirstOrDefault();

            branch.village = (from bank in element.Elements()
                              where bank.Name == "ישוב"
                              select (bank.Value)).FirstOrDefault();

            string amala= (from bank in element.Elements()
                           where bank.Name == "עמלה"
                           select (bank.Value)).FirstOrDefault();

            branch.commission = amala == "לא" ? false : true;

            branch.ATMtype = (from bank in element.Elements()
                              where bank.Name == "ATMtype"
                              select (bank.Value)).FirstOrDefault();

            branch.LocationOfATMrelativeToBranch = (from bank in element.Elements()
                             where bank.Name == "מיקום_ה-ATM_ביחס_לסניף"
                             select (bank.Value)).FirstOrDefault();

            string accesshandicap = (from bank in element.Elements()
                               where bank.Name == "גישה_לנכים"
                               select (bank.Value)).FirstOrDefault();

            branch.accesshandicap = accesshandicap == "לא" ? false : true;

            branch.CoordinateX = (from bank in element.Elements()
                                   where bank.Name == "קואורדינטת_X"
                                   select float.Parse(bank.Value)).FirstOrDefault();

            branch.CoordinateY = (from bank in element.Elements()
                                   where bank.Name == "קואורדינטת_Y"
                                   select float.Parse(bank.Value)).FirstOrDefault();

            return branch;
        }

        XElement ConvertOrderToXAML(Order order)
        {
            return
                new XElement("Order",
                new XElement("hostingUnitKey", order.hostingUnitKey.ToString()),
                new XElement("guestRequestKey", order.guestRequestKey.ToString()),
                new XElement("orderKey", order.orderKey.ToString()),
                new XElement("status", order.status.ToString()),
                new XElement("createDate", order.createDate.ToShortDateString()),
                new XElement("orderDate", order.orderDate.ToShortDateString()));
        }
        Order ConvertXAMLToOrder(XElement element)
        {
            Order order = new Order();

            order.hostingUnitKey = (from ord in element.Elements()
                                    where ord.Name== "hostingUnitKey"
                                    select long.Parse(ord.Value)).FirstOrDefault();

            order.guestRequestKey = (from ord in element.Elements()
                                     where ord.Name== "guestRequestKey"
                                     select long.Parse(ord.Value)).FirstOrDefault();

            order.orderKey = (from ord in element.Elements()
                              where ord.Name== "orderKey"
                              select long.Parse(ord.Value)).FirstOrDefault();

            string status= (from ord in element.Elements()
                            where ord.Name=="status"
                            select (ord.Value)).FirstOrDefault();

            order.status =
                status == "closedDueLackOfCustomersResponse" ? OrderStatus.closedDueLackOfCustomersResponse :
                status == "mailWasSent" ? OrderStatus.mailWasSent :
                status == "notYetAddressed" ? OrderStatus.notYetAddressed :
                OrderStatus.reserved;

            order.createDate = (from ord in element.Elements()
                                where ord.Name== "createDate"
                                select DateTime.Parse(ord.Value)).FirstOrDefault();

            order.orderDate = (from ord in element.Elements()
                               where ord.Name== "orderDate"
                               select DateTime.Parse(ord.Value)).FirstOrDefault();

            return order;
        }

        void DownloadBankBranch()
        {
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }
        }

        #endregion

        #region request region
        public void addRequest(GuestRequest guest)
        {
            //if a such guestRequest already exists
            if (getAllGuestRequest(x => x.guestRequestKey == guest.guestRequestKey).FirstOrDefault() != null) 
                throw new Exception("A guestRequest with the same key already exists.");

            guestRequestCount = (from g in configRoot.Elements()
                                 where g.Name== "guestRequestCount"
                                 select long.Parse(g.Value)).FirstOrDefault();

            guest.guestRequestKey = guestRequestCount;

            guestRequestRoot.Add(ConvertGuestRequestToXAML(guest));
            guestRequestRoot.Save(guestRequestPath);

            XElement toRemove = (from x in configRoot.Elements()
                                 where x.Name == "guestRequestCount"
                                 select x).FirstOrDefault();
            toRemove.Remove();

            guestRequestCount++;
            configRoot.Add(new XElement("guestRequestCount", guestRequestCount.ToString()));
            configRoot.Save(configPath);


        }
        //public GuestRequest getRequest(long key)
        //{
        //    throw new NotImplementedException();
        //}
        public void updateRequest(GuestRequest guest)
        {
            if (getAllGuestRequest(x => x.guestRequestKey == guest.guestRequestKey).FirstOrDefault() == null)
                throw new Exception("There is no such GuestRequest.");

            //  remove the GuestRequest before updating it
            (from item in guestRequestRoot.Elements()
             where long.Parse(item.Element("Request").Element("GuestRequestKey").Value) == guest.guestRequestKey
             select item).FirstOrDefault().Remove();

            //implement the update according to the demand
            guestRequestRoot.Add(ConvertGuestRequestToXAML(guest));
            guestRequestRoot.Save(guestRequestPath);
        }
        public IEnumerable<GuestRequest> getAllGuestRequest(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in guestRequestRoot.Elements()
                       select ConvertXAMLToGuestRequest(item);
            }
            return from item in guestRequestRoot.Elements()
                   let req = ConvertXAMLToGuestRequest(item)
                   where predicate(req)
                   select req;
        }
        public GuestRequest getRequest(long key)
        {
            return getAllGuestRequest().FirstOrDefault(x=>x.guestRequestKey==key);
        }

        #endregion
        #region hostingUnit region
        public void addHostingUnit(HostingUnit unit)
        {
            //if a such HostingUnit already exists
            if (getAllHostingUnit(x => x.hostingUnitKey == unit.hostingUnitKey).FirstOrDefault() != null)
                throw new Exception("A HostingUnit with the same key already exists.");

            hostingUnitCount = (from g in configRoot.Elements()
                                where g.Name== "hostingUnitCount"
                                select long.Parse(g.Value)).FirstOrDefault();

            unit.hostingUnitKey = hostingUnitCount;

            hostingUnitRoot.Add(ConvertHostingUnitToXAML(unit));
            hostingUnitRoot.Save(hostingUnitPath);

            XElement toRemove = (from x in configRoot.Elements()
                                 where x.Name == "hostingUnitCount"
                                 select x).FirstOrDefault();
            toRemove.Remove();

            hostingUnitCount++;
            configRoot.Add(new XElement("hostingUnitCount", hostingUnitCount.ToString()));
            configRoot.Save(configPath);
        }
        public void updateHostingUnit(HostingUnit unit)
        {
            if (getAllHostingUnit(x => x.hostingUnitKey == unit.hostingUnitKey).FirstOrDefault() == null)
                throw new Exception("There is no such HostingUnit.");

            //  remove the HostingUnit before updating it
            (from item in hostingUnitRoot.Elements()
             where long.Parse(item./*Element("HostingUnit").*/Element("hostingUnitKey").Value) == unit.hostingUnitKey
             select item).FirstOrDefault().Remove();

            //implement the update according to the demand
            hostingUnitRoot.Add(ConvertHostingUnitToXAML(unit));
            hostingUnitRoot.Save(hostingUnitPath);
        }
        public void deleteHostingUnit(HostingUnit unit)
        {
            XElement toRemove = (from item in hostingUnitRoot.Elements()
                                 where long.Parse(item.Element("hostingUnitKey").Value) == unit.hostingUnitKey
                                 select item).FirstOrDefault();

            if (toRemove == null)
                throw new Exception("There is no such unit.");

            toRemove.Remove();
            hostingUnitRoot.Save(hostingUnitPath);
        }
        public IEnumerable<HostingUnit> getAllHostingUnit(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in hostingUnitRoot.Elements()
                       select ConvertXAMLToHostingUnit(item);
            }
            var variable= from item in hostingUnitRoot.Elements()
                          let hos = ConvertXAMLToHostingUnit(item)
                          where predicate(hos)
                          select hos;

            return variable;
        }


        public HostingUnit getHostingUnit(long key)
        {
            return getAllHostingUnit().FirstOrDefault(x=> x.hostingUnitKey==key);
        }

        #endregion
        #region order region
        public void addOrder(Order order)
        {
            
            orderCount = (from x in configRoot.Elements()
                          where x.Name == "orderCount"
                          select long.Parse(x.Value)).FirstOrDefault();

            order.orderKey = orderCount;

            orderRoot.Add(ConvertOrderToXAML(order));
            orderRoot.Save(orderPath);

            XElement toRemove = (from x in configRoot.Elements()
                                 where x.Name == "orderCount"
                                 select x).FirstOrDefault();
            toRemove.Remove();

            orderCount++;

            configRoot.Add(new XElement("orderCount", orderCount.ToString()));
            configRoot.Save(configPath);


        }
        public void updateOrder(Order order)
        {
            if (getAllOrder(x => x.orderKey == order.orderKey).FirstOrDefault() == null)
                throw new Exception("There is no such order.");

            //  remove the GuestRequest before updating it
            (from item in orderRoot.Elements()
             where long.Parse(item/*.Element("Order").*/.Element("orderKey").Value) == order.orderKey
             select item).FirstOrDefault().Remove();

            //implement the update according to the demand
            orderRoot.Add(ConvertOrderToXAML(order));
            orderRoot.Save(orderPath);
        }
        public IEnumerable<Order> getAllOrder(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
            {
                return from item in orderRoot.Elements()
                       select ConvertXAMLToOrder(item);
            }
            var variable= from item in orderRoot.Elements()
                   let ord = ConvertXAMLToOrder(item)
                   where predicate(ord)
                   select ord;
            return variable;
        }
        public Order getOrder(long key)
        {
            return getAllOrder().FirstOrDefault(x=> x.orderKey==key);
        }
        #endregion
        #region configuration
        public long getGuestRequestCount()
        {
            return (from g in configRoot.Elements()
                    where g.Name == "guestRequestCount"
                    select long.Parse(g.Value)).FirstOrDefault(); ;
        }

        public long getHostingUnitCount()
        {
            return (from g in configRoot.Elements()
                    where g.Name == "hostingUnitCount"
                    select long.Parse(g.Value)).FirstOrDefault(); 
        }

        public long getOrderCount()
        {
            return (from x in configRoot.Elements()
                    where x.Name == "orderCount"
                    select long.Parse(x.Value)).FirstOrDefault();
        }

        #endregion


        #region host
        public void addHost(Host host)
        {
            //if a such HostingUnit already exists
            //if (getAllHost(x => x.hostKey == host.hostKey).FirstOrDefault() != null)
            //    throw new Exception("An Host with the same key already exists.");

            //hostingUnitCount = (from g in configRoot.Elements()
            //                    where g.Name == "hostingUnitCount"
            //                    select long.Parse(g.Value)).FirstOrDefault();

            //host.hostKey = hostingUnitCount;

            hostRoot.Add(ConvertHostToXAML(host));
            hostRoot.Save(hostPath);

            //XElement toRemove = (from x in configRoot.Elements()
            //                     where x.Name == "hostingUnitCount"
            //                     select x).FirstOrDefault();
            //toRemove.Remove();

            //hostingUnitCount++;
            //configRoot.Add(new XElement("hostingUnitCount", hostingUnitCount.ToString()));
            //configRoot.Save(configPath);
        }


        public Host getHost(long key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        #endregion











    }
}