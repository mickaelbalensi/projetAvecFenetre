using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum GuestRequestStatus
    {
        active,
        transactionClosed,
        hasExpired
    }
    public enum OrderStatus
    {
        notYetAddressed,
        mailWasSent,
        closedDueLackOfCustomersResponse,
        reserved
    }

    public enum TypeOfHostingUnit
    {
        all,
        zimmer,
        apartment,
        roomOfHotel,
        tent
    }

    public enum TypeAreaOfTheCountry
    {
        all,
        north,
        south,
        center,
        jerusalem
    }

    public enum Options
    {
        yes,
        no,
        optional
    }


    public enum Bank
    {
        bankHapoalim,
        bankLeumi,
        bankMizrahiTefahot,
        firstInternationalBankOfIsrael,
        israelDiscountBank,
        mercantile,
        BNP,
        citibank,
        HSBC
    }

    //rajouter subArea
}
