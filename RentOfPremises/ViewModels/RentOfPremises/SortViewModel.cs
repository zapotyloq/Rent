using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.RentOfPremises
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState PremisesIdSort { get; private set; }
        public SortState OrganizationNameSort { get; private set; }
        public SortState ArrivalDateSort { get; private set; }
        public SortState DateOfDepartureSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            PremisesIdSort = sortOrder == SortState.PremisesIdAsc ? SortState.PremisesIdDesc : SortState.PremisesIdAsc;
            OrganizationNameSort = sortOrder == SortState.OrganizationNameAsc ? SortState.OrganizationNameDesc : SortState.OrganizationNameAsc;
            ArrivalDateSort = sortOrder == SortState.ArrivalDateAsc ? SortState.ArrivalDateDesc : SortState.ArrivalDateAsc;
            DateOfDepartureSort = sortOrder == SortState.DateOfDepartureAsc ? SortState.DateOfDepartureDesc : SortState.DateOfDepartureAsc;
            Current = sortOrder;
        }
    }
}
