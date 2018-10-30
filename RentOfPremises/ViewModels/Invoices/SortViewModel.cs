using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.Invoices
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState DateOfPaymentSort { get; private set; }
        public SortState RentIdSort { get; private set; }
        public SortState MounthSort { get; private set; }
        public SortState TotalSort { get; private set; }
        public SortState BaileeSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            DateOfPaymentSort = sortOrder == SortState.DateOfPaymentAsc ? SortState.DateOfPaymentDesc : SortState.DateOfPaymentAsc;
            RentIdSort = sortOrder == SortState.RentIdAsc ? SortState.RentIdDesc : SortState.RentIdAsc;
            MounthSort = sortOrder == SortState.MounthAsc ? SortState.MounthDesc : SortState.MounthAsc;
            TotalSort = sortOrder == SortState.TotalAsc ? SortState.TotalDesc : SortState.TotalAsc;
            BaileeSort = sortOrder == SortState.BaileeAsc ? SortState.BaileeDesc : SortState.BaileeAsc;
            Current = sortOrder;
        }
    }
}
