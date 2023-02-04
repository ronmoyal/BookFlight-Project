using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Flight_Project.Models;

namespace Flight_Project.Dal
{
    public class UserDal : DbContext

    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("tblUser");
            modelBuilder.Entity<Flight>().ToTable("tblFlight");
            modelBuilder.Entity<Order>().ToTable("tblOrder");
            modelBuilder.Entity<Card>().ToTable("tblCredit");

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Card> Cards { get; set; }

    }

}