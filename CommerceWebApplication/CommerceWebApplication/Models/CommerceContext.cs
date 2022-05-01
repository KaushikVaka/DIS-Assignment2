using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommerceWebApplication.Models
{
    public class CommerceContext: DbContext
    {
        public CommerceContext(DbContextOptions<CommerceContext> options) :base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<Operator> Operators { get; set; }

    }
}
