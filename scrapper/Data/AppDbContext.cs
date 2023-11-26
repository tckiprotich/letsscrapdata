using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace scrapper.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<scrapper.Models.parkModels> parkModels { get; set; }
        public DbSet<scrapper.Models.parkName> parkName { get; set; }
        
    }
}