using InspectionService.Models;
using Microsoft.EntityFrameworkCore;

namespace InspectionService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Status>? Statuses { get; set; }
        public DbSet<InspectionType>? InspectionTypes { get; set; }
        public DbSet<Inspection>? Inspections { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}