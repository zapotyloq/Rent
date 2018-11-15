using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.Rents
{
    public enum SortState
    {
        IdAsc,
        IdDesc,
        PremiseIdAsc,
        PremiseIdDesc,
        OrganizationNameAsc,
        OrganizationNameDesc,
        ArrivalDateAsc,
        ArrivalDateDesc,
        DateOfDepartureAsc,
        DateOfDepartureDesc
    };
}
