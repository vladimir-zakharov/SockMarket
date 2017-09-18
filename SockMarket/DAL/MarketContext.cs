using Microsoft.AspNet.Identity.EntityFramework;
using SockMarket.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SockMarket.DAL
{
    public class MarketContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public MarketContext() : base("MarketContext", throwIfV1Schema: false) { }

        public static MarketContext Create()
        {
            return new MarketContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}