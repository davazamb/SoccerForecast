using Microsoft.EntityFrameworkCore;
using SoccerForecast.Web.Data.Entities;

namespace SoccerForecast.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TeamEntity> Teams { get; set; }
    }

}
