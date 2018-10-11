﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.ViewModels.Organizations
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState MailSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            MailSort = sortOrder == SortState.MailAsc ? SortState.MailDesc : SortState.MailAsc;
            Current = sortOrder;
        }
    }
}
