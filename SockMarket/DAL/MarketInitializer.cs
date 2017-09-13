using SockMarket.Models;
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

            List<ContactPerson> contactPersons = new List<ContactPerson>
            {
                new ContactPerson{FirstName="John", Lastname="Doe", Email="john.doe@gmail.com", PhoneNumber="+70000000001" },
                new ContactPerson{FirstName="Max", Lastname="Black", Email="max.black@gmail.com", PhoneNumber="+70000000002" },
            };

            contactPersons.ForEach(person => context.ContactPersons.Add(person));
            context.SaveChanges();

            List<Contact> contacts = new List<Contact>
            {
                new Contact{CompanyID=1, ContactPersonID=1},
                new Contact{CompanyID=2, ContactPersonID=1},
                new Contact{CompanyID=2, ContactPersonID=2}
            };

            contacts.ForEach(contact => context.Contacts.Add(contact));

            List<Deal> deals = new List<Deal>
            {
                new Deal{Stage=Stage.InitialContact, CompanyID=1},
                new Deal{Stage=Stage.Decision, CompanyID=2}
            };

            deals.ForEach(deal => context.Deals.Add(deal));

            context.SaveChanges();

        }
    }
}