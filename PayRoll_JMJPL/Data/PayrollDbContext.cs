using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PayRoll_JMJPL.Models;

namespace PayRoll_JMJPL.Data
{
    /// <inheritdoc />
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<WorkerType> WorkerTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}