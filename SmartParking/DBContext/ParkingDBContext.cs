using Microsoft.EntityFrameworkCore;
using SmartParking.Models;

namespace SmartParking.DBContext
{
    public class ParkingDBContext : DbContext
    {
        public ParkingDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Sensor> Sensor { get; set; }
    }
}
