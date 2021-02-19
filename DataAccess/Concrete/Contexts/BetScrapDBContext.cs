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
           optionsBuilder.UseSqlServer(@"Server=193.111.73.163;Database=BetScrapDB;User Id=BetScrapDB;password=M.03456789;Trusted_Connection=False;MultipleActiveResultSets=true;", options => options.EnableRetryOnFailure());

          //optionsBuilder.UseSqlServer(@"Server=.;initial catalog=BetScrapDB;integrated security=true", options => options.EnableRetryOnFailure());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

    }
}
