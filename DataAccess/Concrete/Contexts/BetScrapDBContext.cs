using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Contexts
{
   public  class BetScrapDBContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=*\MSSQLSERVER2014;Database=BetScrapDB;User Id=BetScrapDB;password=66eNuAMBw4Ep;Trusted_Connection=False;MultipleActiveResultSets=true;", options => options.EnableRetryOnFailure());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

    }
}
