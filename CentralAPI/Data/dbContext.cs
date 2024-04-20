using CentralAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CentralAPI.Data
{
    public class dbContext : DbContext 
    {
        public DbSet<Complaint> Complaints { get; set; }
        //public DbSet<Account> Accounts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIKEYSPC\\SQLEXPRESS;Initial Catalog=CentralAPI;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
