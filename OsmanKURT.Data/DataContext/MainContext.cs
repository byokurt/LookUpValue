using Microsoft.EntityFrameworkCore;
using OsmanKURT.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<User> SetUser { get; set; }
        public DbSet<LookUpValue> SetLookUpValue { get; set; }
    }
}
