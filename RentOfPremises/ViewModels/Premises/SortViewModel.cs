using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.Premises
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState AreaSort { get; private set; }
        public SortState BuildingNameSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            AreaSort = sortOrder == SortState.AreaAsc ? SortState.AreaDesc : SortState.AreaAsc;
            BuildingNameSort = sortOrder == SortState.BuildingNameAsc ? SortState.BuildingNameDesc : SortState.BuildingNameAsc;
            Current = sortOrder;
        }
    }
}
