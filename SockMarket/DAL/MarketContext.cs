﻿using SockMarket.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SockMarket.DAL
{
    public class MarketContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Deal> Deals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}