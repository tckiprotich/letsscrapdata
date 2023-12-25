using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Data
{
    public class ExploreParksDBContext : DbContext
    {
        public ExploreParksDBContext(DbContextOptions<ExploreParksDBContext> options) : base(options)
        {

        }
        public DbSet<ParksModel> Parks { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<ContinentModel> Continents { get; set; }

    }
}