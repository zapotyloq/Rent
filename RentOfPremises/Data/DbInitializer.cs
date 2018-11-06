using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentOfPremises.Models;
using System.Text;

namespace RentOfPremises.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext db)
        {
            db.Database.EnsureCreated();
            if (db.Organizations.Any())
            {
                return;
            }

            Random rand = new Random();

            int organizationsLength = 30;
            int buildingsLength = 30;
            int premisesLength = 200;
            int rentOfPremisesLength = 4000;
            int invoicesLength = 10000;
            string voc = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";

            for (int i = 0; i < organizationsLength; i++)
            {
                Organization organization = new Organization
                {
                    Name = GetRandomString(voc, 20),
                    Mail = GetRandomString(voc, 20)
                };
                db.Organizations.Add(organization);
            }
            db.SaveChanges();

            for( int i = 0; i < buildingsLength; i++)
            {
                Building building = new Building
                {
                    Name = GetRandomString(voc, 20),
                    Mail = GetRandomString(voc, 20),
                    Characteristic = GetRandomString(voc, 20),
                    NumberOfStoreys = rand.Next(1, 16)
                };
                db.Buildings.Add(building);
            }
            db.SaveChanges();

            for( int i = 0; i < premisesLength; i++)
            {
                Premises premises = new Premises
                {
                    BuildingNumber = rand.Next(1, buildingsLength - 1),
                    Area = rand.Next(20, 300),
                    FloorPlan = GetRandomString(voc, 20),
                    Photos = GetRandomString(voc, 20)
                };
                db.Premises.Add(premises);
            }
            db.SaveChanges();

            for( int i = 0; i < rentOfPremisesLength; i++)
            {
                Models.RentOfPremises rent = new Models.RentOfPremises
                {
                    OrganizationId = rand.Next(1, organizationsLength),
                    PremisesId = rand.Next(1, premisesLength),
                    ArrivalDate = new DateTime(2000, 1, 1).AddDays(rand.Next(0, 6000)),
                    DateOfDeparture = new DateTime(2018, 1, 1).AddDays(rand.Next(0, 1000))
                };
                db.RentOfPremises.Add(rent);
            }
            db.SaveChanges();

            for (int i = 0; i < invoicesLength; i++)
            {
                Invoice invoice = new Invoice
                {
                    Mounth = rand.Next(1, 12),
                    RentId = rand.Next(1, rentOfPremisesLength),
                    DateOfPayment = new DateTime(2000, 1, 1).AddMonths(rand.Next(1,240)),
                    Bailee = GetRandomString(voc, 20),
                    Total = rand.Next(1000, 30000)
                };
                db.Invoices.Add(invoice);
            }
            db.SaveChanges();
        }

        static string GetRandomString(string alphabet, int length)
        {
            Random rnd = new Random();
            int position = 0;
            string s = "";
            for (int i = 0; i < length; i++)
            {
                position = rnd.Next(0, alphabet.Length - 1);
                s = s + alphabet[position];
            }
            return s;
        }
    }
}
