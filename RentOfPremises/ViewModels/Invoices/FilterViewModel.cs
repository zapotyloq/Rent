using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Invoices
{
    public class FilterViewModel
    {
        public SelectList Invoices { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Invoice> invoices, int? id, string name)
        {
            invoices.Insert(0, new Invoice { Id = 0 });
            Invoices = new SelectList(invoices, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
