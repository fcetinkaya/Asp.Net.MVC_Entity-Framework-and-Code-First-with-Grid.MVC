using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Asp.NetMvc_MvcGrid.Models.Manager
{
    public class DatabaseContext:DbContext
    {       
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new Create());
        }

       public class Create:CreateDatabaseIfNotExists<DatabaseContext>
        {
            protected override void Seed(DatabaseContext context)
            {
                List<Country> countryList = new List<Country>();
                for (int i = 0; i < 100; i++)
                {
                    Country country = new Country();
                    country.Name = FakeData.PlaceData.GetCountry();
                    countryList.Add(country);
                    context.Countries.Add(country);
                }

                context.SaveChanges();

                for (int i = 0; i < 100; i++)
                {
                    Employee emp = new Employee();
                    emp.firstName = FakeData.NameData.GetFirstName();
                    emp.lastName = FakeData.NameData.GetSurname();
                    emp.Age = FakeData.NumberData.GetNumber(10, 90);

                    Random r = new Random();
                    int value = r.Next(0, 10);

                    emp.Country = countryList[value];
                    context.Employees.Add(emp);
                }

                context.SaveChanges();
            }
        }
    }
}