
using BE1;
using DAL1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

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

        int orderKey;
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
                LoadData();
        }

        private void CreateFiles()
        {
            guestRequestRoot = new XElement("guestRequest");
            guestRequestRoot.Save(guestRequestPath);

            hostRoot = new XElement("nannies");
            hostRoot.Save(hostPath);

            hostingUnitRoot = new XElement("nannies");
            hostingUnitRoot.Save(hostingUnitPath);

            orderRoot = new XElement("mothers");
            orderRoot.Save(orderPath);

            bankBranchRoot = new XElement("contracts");
            bankBranchRoot.Save(bankBranchPath);


            configRoot = new XElement("configuration",
                new XElement("orderKey", "1"),
                new XElement("hostingUnitCount", "1"),
                new XElement("guestRequestCount", "1")); 
            configRoot.Save(configPath);
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
                new XElement("Name",
                new XElement("PrivateName", request.privateName),
                new XElement("FamilyName", request.familyName)),
                new XElement("MailAddress", request.mailAddress),
                new XElement("Status", request.status.ToString()),
                new XElement("RegistrationDate", request.registrationDate.ToShortDateString()),
                new XElement("DateOfHollydays",
                new XElement("EntryDate", request.entryDate.ToShortDateString()),
                new XElement("ReleaseDate", request.releaseDate.ToShortDateString())),
                new XElement("TransactionSigned", request.transactionSigned.ToString()),
                new XElement("CriterionOnHostingUnit",
                new XElement("TypeArea", request.typeArea.ToString()),
                new XElement("Type", request.type.ToString()),
                new XElement("NumSleeping",
                new XElement("Adults", request.adults.ToString()),
                new XElement("Children", request.children.ToString())),
                new XElement("Options",
                new XElement("Pool", request.pool.ToString()),
                new XElement("Jacuzzi", request.jacuzzi.ToString()),
                new XElement("Garden", request.garden.ToString()),
                new XElement("ChildrenAttractions", request.childrenAttractions.ToString()))));
        }
        GuestRequest ConvertXAMLToGuestRequest(XElement element)
        {
            GuestRequest request = new GuestRequest();

            request.guestRequestKey = (from req in element.Elements()
                                       select long.Parse(req.Element("Request").Element("GuestRequestKey").Value)).FirstOrDefault();

            request.privateName = (from req in element.Elements()
                                   select req.Element("Request").Element("Name").Element("PrivateName").Value).FirstOrDefault();

            request.familyName = (from req in element.Elements()
                                  select req.Element("Request").Element("Name").Element("FamilyName").Value.ToString()).FirstOrDefault();

            request.mailAddress = (from req in element.Elements()
                                   select req.Element("Request").Element("MailAddress").Value.ToString()).FirstOrDefault();
            
            string status= (from req in element.Elements()
                            select req.Element("Request").Element("Status").Value).FirstOrDefault();

            request.status = 
                status == "active" ?
                GuestRequestStatus.active :
                (status == "hasExpired" ?
                GuestRequestStatus.hasExpired :
                GuestRequestStatus.transactionClosed);

            request.registrationDate = (from req in element.Elements()
                                   select DateTime.Parse(req.Element("Request").Element("RegistrationDate").Value.ToString())).FirstOrDefault();

            request.entryDate = (from req in element.Elements()
                                   select DateTime.Parse(req.Element("Request").Element("DateOfHollydays").Element("EntryDate").Value.ToString())).FirstOrDefault();

            request.releaseDate = (from req in element.Elements()
                                   select DateTime.Parse(req.Element("Request").Element("DateOfHollydays").Element("ReleaseDate").Value.ToString())).FirstOrDefault();

            request.transactionSigned = (from req in element.Elements()
                                 select Boolean.Parse(req.Element("Request").Element("TransactionSigned").Value)).FirstOrDefault();
            
            string typeArea = (from req in element.Elements()
                               select req.Element("Request").Element("CriterionOnHostingUnit").Element("TypeArea").Value.ToString()).FirstOrDefault();

            request.typeArea =
                typeArea == "all" ?
                TypeAreaOfTheCountry.all :
                typeArea == "north" ? TypeAreaOfTheCountry.north :
                typeArea == "south" ? TypeAreaOfTheCountry.south :
                typeArea == "center" ? TypeAreaOfTheCountry.center :
                TypeAreaOfTheCountry.jerusalem;
                
            string type = (from req in element.Elements()
                           select req.Element("Request").Element("CriterionOnHostingUnit").Element("Type").Value.ToString()).FirstOrDefault();

            request.type =
                type == "all" ? TypeOfHostingUnit.all :
                type == "apartment" ? TypeOfHostingUnit.apartment :
                type == "roomOfHotel" ? TypeOfHostingUnit.roomOfHotel :
                type == "tent" ? TypeOfHostingUnit.tent :
                TypeOfHostingUnit.zimmer;

            request.adults = (from req in element.Elements()
                                         select int.Parse(req.Element("Request").Element("NumSleeping").Element("Adults").Value)).FirstOrDefault();

            request.children = (from req in element.Elements()
                                         select int.Parse(req.Element("Request").Element("NumSleeping").Element("Children").Value)).FirstOrDefault();

            string pool = (from req in element.Elements()
                           select (req.Element("Request").Element("Options").Element("Pool").Value)).FirstOrDefault();

            request.pool =
                pool == "yes" ? Options.yes :
                pool == "no" ? Options.no :
                Options.optional;

            string jacuzzi = (from req in element.Elements()
                           select (req.Element("Request").Element("Options").Element("Jacuzzi").Value)).FirstOrDefault();

            request.jacuzzi =
                jacuzzi == "yes" ? Options.yes :
                jacuzzi == "no" ? Options.no :
                Options.optional;

            string garden = (from req in element.Elements()
                           select (req.Element("Request").Element("Options").Element("Garden").Value)).FirstOrDefault();

            request.garden=
                garden == "yes" ? Options.yes :
                garden == "no" ? Options.no :
                Options.optional;

            string childrenAttractions = (from req in element.Elements()
                                          select (req.Element("Request").Element("Options").Element("ChildrenAttractions").Value)).FirstOrDefault();

            request.childrenAttractions =
                childrenAttractions == "yes" ? Options.yes :
                childrenAttractions == "no" ? Options.no :
                Options.optional;

            return request;
        }
        XElement ConvertHostingUnitToXAML(HostingUnit unit) {

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
                new XElement("tempDiary", unit.tempDiary));
        }
        HostingUnit ConvertXAMLToHostingUnit(XElement element)
        {
            HostingUnit unit = new HostingUnit();

            unit.hostingUnitKey = (from hosting in element.Elements()
                                   select int.Parse(hosting.Element("HostingUnit").Element("hostingUnitKey").Value)).FirstOrDefault();

            unit.hostingUnitName = (from hosting in element.Elements()
                                   select (hosting.Element("HostingUnit").Element("hostingUnitName").Value)).FirstOrDefault();

            unit.owner = ConvertXAMLToHost(element);
                
            unit.adultPlaces = (from hosting in element.Elements()
                                   select int.Parse(hosting.Element("HostingUnit").Element("adultPlaces").Value)).FirstOrDefault();

            unit.childrenPlaces = (from hosting in element.Elements()
                                   select int.Parse(hosting.Element("HostingUnit").Element("childrenPlaces").Value)).FirstOrDefault();

            unit.jacuzzi = (from hosting in element.Elements()
                           select bool.Parse(hosting.Element("HostingUnit").Element("jacuzzi").Value)).FirstOrDefault();

            unit.garden = (from hosting in element.Elements()
                           select bool.Parse(hosting.Element("HostingUnit").Element("garden").Value)).FirstOrDefault();

            unit.pool = (from hosting in element.Elements()
                         select bool.Parse(hosting.Element("HostingUnit").Element("pool").Value)).FirstOrDefault();

            unit.childrenAttractions = (from hosting in element.Elements()
                                        select bool.Parse(hosting.Element("HostingUnit").Element("childrenAttractions").Value)).FirstOrDefault();
 
            string typeArea = (from hosting in element.Elements()
                               select hosting.Element("HostingUnit").Element("typeArea").Value.ToString()).FirstOrDefault();

            unit.typeArea =
                typeArea == "all" ?
                TypeAreaOfTheCountry.all :
                typeArea == "north" ? TypeAreaOfTheCountry.north :
                typeArea == "south" ? TypeAreaOfTheCountry.south :
                typeArea == "center" ? TypeAreaOfTheCountry.center :
                TypeAreaOfTheCountry.jerusalem;

            string typeOfUnit = (from hosting in element.Elements()
                           select hosting.Element("HostingUnit").Element("typeOfUnit").Value.ToString()).FirstOrDefault();

            unit.typeOfUnit =
                typeOfUnit == "all" ? TypeOfHostingUnit.all :
                typeOfUnit == "apartment" ? TypeOfHostingUnit.apartment :
                typeOfUnit == "roomOfHotel" ? TypeOfHostingUnit.roomOfHotel :
                typeOfUnit == "tent" ? TypeOfHostingUnit.tent :
                TypeOfHostingUnit.zimmer;

            unit.tempDiary = (from hosting in element.Elements()
                              select (hosting.Element("HostingUnit").Element("tempDiary").Value)).FirstOrDefault();

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
                new XElement("bankBranchDetails", ConvertBankBranchToXAML(host.bankBranchDetails)),
                new XElement("bankAccountNumber", host.bankAccountNumber.ToString()),
                new XElement("collectionClearance", host.collectionClearance.ToString()));
        }
        Host ConvertXAMLToHost(XElement element)
        {
            Host host = new Host();

            host.hostKey = (from ost in element.Elements()
                            select long.Parse(ost.Element("Host").Element("hostKey").Value)).FirstOrDefault();

            host.privateName = (from ost in element.Elements()
                            select (ost.Element("Host").Element("privateName").Value)).FirstOrDefault();

            host.familyName = (from ost in element.Elements()
                            select (ost.Element("Host").Element("familyName").Value)).FirstOrDefault();

            host.phoneNumber = (from ost in element.Elements()
                            select long.Parse(ost.Element("Host").Element("phoneNumber").Value)).FirstOrDefault();

            host.mailAddress = (from ost in element.Elements()
                            select (ost.Element("Host").Element("mailAddress").Value)).FirstOrDefault();

            host.bankBranchDetails = ConvertXAMLToBankBranch(element);

            host.bankAccountNumber = (from ost in element.Elements()
                            select long.Parse(ost.Element("Host").Element("bankAccountNumber").Value)).FirstOrDefault();

            host.collectionClearance = (from ost in element.Elements()
                            select bool.Parse(ost.Element("Host").Element("hostKey").Value)).FirstOrDefault();

            return host;
        }
        XElement ConvertBankBranchToXAML(BankBranch bank)
        {
            return 
                new XElement("BankBranch",
                new XElement("bankNumber", bank.bankNumber.ToString()),
                new XElement("bankName",bank.bankName),
                new XElement("branchNumber",bank.branchNumber.ToString()),
                new XElement("branchAddress", bank.branchAddress),
                new XElement("branchCity",bank.branchCity));
        }
        BankBranch ConvertXAMLToBankBranch(XElement element)
        {
            BankBranch branch = new BankBranch();

            string bankNumber = (from bank in element.Elements()
                                 select bank.Element("BankBranch").Element("bankNumber").Value).FirstOrDefault();

            branch.bankNumber =
                bankNumber == "bankHapoalim" ? Bank.bankHapoalim :
                bankNumber == "bankLeumi" ? Bank.bankLeumi :
                bankNumber == "BNP" ? Bank.BNP :
                Bank.HSBC;

            branch.bankName = (from bank in element.Elements()
                               select bank.Element("BankBranch").Element("bankName").Value).FirstOrDefault();

            branch.branchNumber = (from bank in element.Elements()
                               select int.Parse(bank.Element("BankBranch").Element("branchNumber").Value)).FirstOrDefault();

            branch.branchAddress = (from bank in element.Elements()
                               select bank.Element("BankBranch").Element("branchAddress").Value).FirstOrDefault();

            branch.branchCity = (from bank in element.Elements()
                               select bank.Element("BankBranch").Element("branchCity").Value).FirstOrDefault();

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
                                    select long.Parse(ord.Element("Order").Element("hostingUnitKey").Value)).FirstOrDefault();

            order.guestRequestKey = (from ord in element.Elements()
                                    select long.Parse(ord.Element("Order").Element("guestRequestKey").Value)).FirstOrDefault();

            order.orderKey = (from ord in element.Elements()
                                    select long.Parse(ord.Element("Order").Element("orderKey").Value)).FirstOrDefault();

            string status= (from ord in element.Elements()
                             select (ord.Element("Order").Element("status").Value)).FirstOrDefault();

            order.status =
                status == "closedDueLackOfCustomersResponse" ? OrderStatus.closedDueLackOfCustomersResponse :
                status == "mailWasSent" ? OrderStatus.mailWasSent :
                status == "notYetAddressed" ? OrderStatus.notYetAddressed :
                OrderStatus.reserved;

            order.createDate = (from ord in element.Elements()
                                    select DateTime.Parse(ord.Element("Order").Element("createDate").Value)).FirstOrDefault();

            order.orderDate = (from ord in element.Elements()
                                    select DateTime.Parse(ord.Element("Order").Element("orderDate").Value)).FirstOrDefault();

            return order;
        }

        #endregion

        #region request region
        public void addRequest(GuestRequest guest)
        {
            //if a such guestRequest already exists
            if (getAllGuestRequest(x => x.guestRequestKey == guest.guestRequestKey).FirstOrDefault() != null)
                throw new Exception("A guestRequest with the same key already exists.");

            if (guest.guestRequestKey == 0)
                throw new Exception("Please select a valid key for the guestRequest (not 0)");


            guestRequestRoot.Add(ConvertGuestRequestToXAML(guest));
            guestRequestRoot.Save(guestRequestPath);
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

        #endregion
        #region hostingUnit region
        public void addHostingUnit(HostingUnit unit)
        {
            //if a such HostingUnit already exists
            if (getAllHostingUnit(x => x.hostingUnitKey == unit.hostingUnitKey).FirstOrDefault() != null)
                throw new Exception("A HostingUnit with the same key already exists.");

            if (unit.hostingUnitKey == 0)
                throw new Exception("Please select a valid key for the HostingUnit (not 0)");


            hostingUnitRoot.Add(ConvertHostingUnitToXAML(unit));
            hostingUnitRoot.Save(hostingUnitPath);
        }
        public void updateHostingUnit(HostingUnit unit)
        {
            if (getAllHostingUnit(x => x.hostingUnitKey == unit.hostingUnitKey).FirstOrDefault() == null)
                throw new Exception("There is no such HostingUnit.");

            //  remove the HostingUnit before updating it
            (from item in guestRequestRoot.Elements()
             where long.Parse(item.Element("HostingUnit").Element("hostingUnitKey").Value) == unit.hostingUnitKey
             select item).FirstOrDefault().Remove();

            //implement the update according to the demand
            hostingUnitRoot.Add(ConvertHostingUnitToXAML(unit));
            hostingUnitRoot.Save(hostingUnitPath);
        }
        public void deleteHostingUnit(HostingUnit unit)
        {
            XElement toRemove = (from item in hostingUnitRoot.Elements()
                                 where long.Parse(item.Element("HostingUnit").Element("hostingUnitKey").Value) == unit.hostingUnitKey
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
            return from item in guestRequestRoot.Elements()
                   let hos = ConvertXAMLToHostingUnit(item)
                   where predicate(hos)
                   select hos;
        }
        //public HostingUnit getHostingUnit(long key)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
        #region order region
        public void addOrder(Order order)
        {
            //if a such guestRequest or/and HostingUnit or/and order already exists
            if (getAllGuestRequest(x => x.guestRequestKey == order.guestRequestKey).FirstOrDefault() != null)
                throw new Exception("A Request with the same key already exists.");
            if (getAllHostingUnit(x => x.hostingUnitKey == order.hostingUnitKey).FirstOrDefault() != null)
                throw new Exception("A hostingUnit with the same key already exists.");
            //if (getAllOrder(x => x.orderKey == order.orderKey).FirstOrDefault() != null)
            //    throw new Exception("An order with the same key already exists.");

            //orderKey == (from x in configRoot.Elements()
            //             where x.Name == "orderKey"
            //             select Convert.ToInt32(x.Value)).FirstOrDefault();
            orderKey = (from x in configRoot.Elements()
                          where x.Name == "orderKey"
                        select Convert.ToInt32(x.Value)).FirstOrDefault();

            order.orderKey = orderKey;

            if (order.orderKey == 0)
                throw new Exception("Please select a valid key for the order (not 0)");

            orderKey++;
            configRoot.Add(new XElement("orderKey", orderKey.ToString()));
            configRoot.Save(configPath);

            orderRoot.Add(ConvertOrderToXAML(order));
            orderRoot.Save(orderPath);

        }
        //public Order getOrder(long key)
        //{
        //    throw new NotImplementedException();
        //}
        public void updateOrder(Order order)
        {
            if (getAllOrder(x => x.orderKey == order.orderKey).FirstOrDefault() == null)
                throw new Exception("There is no such order.");

            //  remove the GuestRequest before updating it
            (from item in orderRoot.Elements()
             where long.Parse(item.Element("Order").Element("orderKey").Value) == order.orderKey
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
            return from item in orderRoot.Elements()
                   let ord = ConvertXAMLToOrder(item)
                   where predicate(ord)
                   select ord;
        }

        public void addHost(Host host)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Host> getAllHost(Func<Host, bool> predicate = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        //public void addHost(Host host)
        //{
        //    throw new NotImplementedException();
        //}













    }
}