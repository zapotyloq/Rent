using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Organizations;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using RentOfPremises.Infrastructure;
using RentOfPremises.Infrastructure.Filters;

namespace RentOfPremises.Controllers
{
    public class OrganizationsController : Controller
    {
        ApplicationContext db;

        public OrganizationsController(ApplicationContext db)
        {
            this.db = db;
        }
        
        [SetToSession("Organizations")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            var sessionOrganizations = HttpContext.Session.Get("Organizations");
            if(sessionOrganizations != null && id == null && name == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                if(sessionOrganizations.Keys.Contains("id"))
                    id = Convert.ToInt32(sessionOrganizations["id"]);
                if(sessionOrganizations.Keys.Contains("name"))
                    name = sessionOrganizations["name"];
                if(sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if(sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            }

            int pageSize = 10;  // количество элементов на странице

            IQueryable<Organization> source = db.Organizations;
            if(page == 0)
            {
                page = 1;
            }
            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                case SortState.MailAsc:
                    source = source.OrderBy(s => s.Mail);
                    break;
                case SortState.MailDesc:
                    source = source.OrderByDescending(s => s.Mail);
                    break;
                default:
                    source = source.OrderBy(s => s.Id);
                    break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Organizations = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Organizations.ToList(), id, name)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> InfoForThePeriod(string buildingName = "", string d0 = "01.01.1970", string d = "01.01.2030", int page = 1)
        {
            int pageSize = 10;  // количество элементов на странице
            List<object[]> source = new List<object[]>();
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                DbParameter parameter = command.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@building_name";
                parameter.Value = buildingName;
                command.Parameters.Add(parameter);
                DbParameter parameter2 = command.CreateParameter();
                parameter2.DbType = System.Data.DbType.String;
                parameter2.ParameterName = "@d0";
                parameter2.Value = d0;
                command.Parameters.Add(parameter2);
                DbParameter parameter3 = command.CreateParameter();
                parameter3.DbType = System.Data.DbType.String;
                parameter3.ParameterName = "@d";
                parameter3.Value = d;
                command.Parameters.Add(parameter3);
                command.CommandText = "my_proc1";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection.Open();
                DbDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        object[] item = new object[reader.FieldCount];
                        reader.GetValues(item);
                        source.Add(item);
                    }
                }
                command.Connection.Close();
            }

            var count = source.Count;
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ProcedureViewModel viewModel = new ProcedureViewModel
            {
                FilterViewModel = new FilterViewModel(source, buildingName, d0, d, db.Buildings.ToList()),
                PageViewModel = pageViewModel,
                Objects = items,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> EnteredForThePeriod(string buildingName = "", string d0 = "01.01.1970", string d = "01.01.2030", int page = 1)
        {
            int pageSize = 10;  // количество элементов на странице
            List<object[]> source = new List<object[]>();
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                DbParameter parameter = command.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@building_name";
                parameter.Value = buildingName;
                command.Parameters.Add(parameter);
                DbParameter parameter2 = command.CreateParameter();
                parameter2.DbType = System.Data.DbType.String;
                parameter2.ParameterName = "@d0";
                parameter2.Value = d0;
                command.Parameters.Add(parameter2);
                DbParameter parameter3 = command.CreateParameter();
                parameter3.DbType = System.Data.DbType.String;
                parameter3.ParameterName = "@d";
                parameter3.Value = d;
                command.Parameters.Add(parameter3);
                command.CommandText = "my_proc2";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection.Open();
                DbDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        object[] item = new object[reader.FieldCount];
                        reader.GetValues(item);
                        source.Add(item);
                    }
                }
                command.Connection.Close();
            }

            var count = source.Count;
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ProcedureViewModel viewModel = new ProcedureViewModel
            {
                FilterViewModel = new FilterViewModel(source, buildingName, d0, d, db.Buildings.ToList()),
                PageViewModel = pageViewModel,
                Objects = items,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Debtors(string d = "01.01.2030", int page = 1)
        {
            int pageSize = 10;  // количество элементов на странице
            List<object[]> source = new List<object[]>();
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                DbParameter parameter = command.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@d";
                parameter.Value = d;
                command.Parameters.Add(parameter);
                command.CommandText = "my_proc3";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection.Open();
                DbDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        object[] item = new object[reader.FieldCount];
                        reader.GetValues(item);
                        source.Add(item);
                    }
                }
                command.Connection.Close();
            }

            var count = source.Count;
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ProcedureViewModel viewModel = new ProcedureViewModel
            {
                FilterViewModel = new FilterViewModel(source, d),
                PageViewModel = pageViewModel,
                Objects = items,
            };
            return View(viewModel);
        }

        public IQueryable<Organization> SortSearch(IQueryable<Organization> source, SortState sortOrder, int? id, string name, int pageSize, int page = 1)
        {
            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                case SortState.MailAsc:
                    source = source.OrderBy(s => s.Mail);
                    break;
                case SortState.MailDesc:
                    source = source.OrderByDescending(s => s.Mail);
                    break;
                default:
                    source = source.OrderBy(s => s.Id);
                    break;
            }
            
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return items.AsQueryable();
        }

        [HttpGet]
        public ActionResult Insert(string name, string mail)
        {
            Organization organization = new Organization
            {
                Name = name,
                Mail = mail
            };
            db.Organizations.Add(organization);
            db.SaveChanges();
            JsonResult data = Json(organization);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Organization organization = null;
            try
            {
                organization = db.Organizations.Where(c => c.Id == id).First();
                db.Organizations.Remove(organization);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(organization);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, string name, string mail)
        {
            Organization organization = null;
            try
            {
                organization = db.Organizations.Where(c => c.Id == id).First();
                organization.Name = name;
                organization.Mail = mail;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(organization);
            return data;
        }
    }
}