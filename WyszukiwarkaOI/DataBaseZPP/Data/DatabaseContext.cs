
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseZPP.Data;

internal class DatabaseContext : DbContext
{
    //public DbSet<Product> Product { get; set; } = null!;
    //public DbSet<Shop> Shop { get; set; } = null!;
    //public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=DataBaseZPP.db");
    }
}

