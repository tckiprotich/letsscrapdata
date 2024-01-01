global using ExploreParks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Data
{
    public class ExploreParksDbContext : DbContext
    {
        public ExploreParksDbContext(DbContextOptions<ExploreParksDbContext> options) : base(options)
        {

        }

        public DbSet<ParkModel> Parks { get; set; }
        public DbSet<ContinentModel> Continents { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<getDescription> Descriptions { get; set; }

    }
}