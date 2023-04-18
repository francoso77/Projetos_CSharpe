using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppSales.Models;

namespace AppSales.Data
{
    public class AppSalesContext : DbContext
    {
        public AppSalesContext (DbContextOptions<AppSalesContext> options)
            : base(options)
        {
        }

        public DbSet<AppSales.Models.Department> Department { get; set; } = default!;
    }
}
