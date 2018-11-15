using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.Buildings
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState MailSort { get; private set; }
        public SortState NumberOfStoreysSort { get; private set; }
        public SortState CharasteristicSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            MailSort = sortOrder == SortState.MailAsc ? SortState.MailDesc : SortState.MailAsc;
            NumberOfStoreysSort = sortOrder == SortState.NumberOfStoreysAsc ? SortState.NumberOfStoreysDesc : SortState.NumberOfStoreysAsc;
            CharasteristicSort = sortOrder == SortState.CharasteristicAsc ? SortState.CharasteristicDesc : SortState.CharasteristicAsc;

            Current = sortOrder;
        }
    }
}
