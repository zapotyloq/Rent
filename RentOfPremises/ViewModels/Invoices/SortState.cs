using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.Invoices
{
    public enum SortState
    {
        IdAsc,
        IdDesc,
        DateOfPaymentAsc,
        DateOfPaymentDesc,
        RentIdAsc,
        RentIdDesc,
        MounthAsc,
        MounthDesc,
        TotalAsc,
        TotalDesc,
        BaileeAsc,
        BaileeDesc
    };
}
