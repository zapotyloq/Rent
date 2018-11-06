using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RentOfPremises.Models;
using RentOfPremises.Data;

namespace RentOfPremises.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate next;

        public DbInitializerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context, ApplicationContext db)
        {
            DbInitializer.Initialize(db);
            return next.Invoke(context);
        }
    }
}
