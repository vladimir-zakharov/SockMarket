using SockMarket.Models;
using System;
using System.Collections.Generic;

namespace SockMarket.DAL
{
    public class MarketInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MarketContext>
    {
        protected override void Seed(MarketContext context)
        {
            List<Company> companies = new List<Company>
            {
                new Company{Name="TestCompany1"},
                new Company{Name="TestCompany2"}
            };

            companies.ForEach(company => context.Companies.Add(company));

            List<Contact> contacts = new List<Contact>
            {
                new Contact{FirstName="John", LastName="Doe", Email="john.doe@gmail.com", PhoneNumber="+70000000001" },
                new Contact{FirstName="Max", LastName="Black", Email="max.black@gmail.com", PhoneNumber="+70000000002" },
            };

            contacts.ForEach(contact => context.Contacts.Add(contact));

            List<Deal> deals = new List<Deal>
            {
                new Deal{CreationTime=DateTime.Now, Stage=Stage.InitialContact, CompanyID=1},
                new Deal{CreationTime=DateTime.Now, Stage=Stage.Decision, CompanyID=2}
            };

            deals.ForEach(deal => context.Deals.Add(deal));

            context.SaveChanges();
        }
    }
}